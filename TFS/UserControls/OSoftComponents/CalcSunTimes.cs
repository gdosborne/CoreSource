namespace OSoftComponents
{
    using System;
    using System.Diagnostics;

    internal sealed class SunTimes
    {
        private SunTimes()
        {
        }

        public bool CalculateSunRiseSetTimes(LatitudeCoords lat, LongitudeCoords lon, DateTime date, ref DateTime riseTime, ref DateTime setTime, ref bool isSunrise, ref bool isSunset) => CalculateSunRiseSetTimes(lat.ToDouble(), lon.ToDouble(), date, ref riseTime, ref setTime, ref isSunrise, ref isSunset);

        public bool CalculateSunRiseSetTimes(double lat, double lon, DateTime date, ref DateTime riseTime, ref DateTime setTime, ref bool isSunrise, ref bool isSunset)
        {
            lock (_lock)    // lock for thread safety
            {
                var zone = -(int)Math.Round(TimeZone.CurrentTimeZone.GetUtcOffset(date).TotalSeconds / 3600);
                var jd = GetJulianDay(date) - 2451545;  // Julian day relative to Jan 1.5, 2000

                if ((Sign(zone) == Sign(lon)) && (zone != 0)) {
                    Debug.Print("WARNING: time zone and longitude are incompatible!");
                    return false;
                }

                lon = lon / 360;
                var tz = zone / 24;
                var ct = jd / 36525 + 1;                                 // centuries since 1900.0
                var t0 = LocalSiderealTimeForTimeZone(lon, jd, tz);      // local sidereal time

                // get sun position at start of day
                jd += tz;
                CalculateSunPosition(jd, ct);
                var ra0 = _sunPositionInSkyArr[0];
                var dec0 = _sunPositionInSkyArr[1];

                // get sun position at end of day
                jd += 1;
                CalculateSunPosition(jd, ct);
                var ra1 = _sunPositionInSkyArr[0];
                var dec1 = _sunPositionInSkyArr[1];

                // make continuous
                if (ra1 < ra0)
                    ra1 += 2 * Math.PI;

                // initialize
                _isSunrise = false;
                _isSunset = false;

                _rightAscentionArr[0] = ra0;
                _decensionArr[0] = dec0;

                // check each hour of this day
                for (int k = 0; k < 24; k++) {
                    _rightAscentionArr[2] = ra0 + (k + 1) * (ra1 - ra0) / 24;
                    _decensionArr[2] = dec0 + (k + 1) * (dec1 - dec0) / 24;
                    _vHzArr[2] = TestHour(k, zone, t0, lat);

                    // advance to next hour
                    _rightAscentionArr[0] = _rightAscentionArr[2];
                    _decensionArr[0] = _decensionArr[2];
                    _vHzArr[0] = _vHzArr[2];
                }

                riseTime = new DateTime(date.Year, date.Month, date.Day, _riseTimeArr[0], _riseTimeArr[1], 0);
                setTime = new DateTime(date.Year, date.Month, date.Day, _setTimeArr[0], _setTimeArr[1], 0);

                isSunset = true;
                isSunrise = true;

                // neither sunrise nor sunset
                if ((!_isSunrise) && (!_isSunset)) {
                    if (_vHzArr[2] < 0)
                        isSunrise = false; // Sun down all day
                    else
                        isSunset = false; // Sun up all day
                }
                // sunrise or sunset
                else {
                    if (!_isSunrise)
                        // No sunrise this date
                        isSunrise = false;
                    else if (!_isSunset)
                        // No sunset this date
                        isSunset = false;
                }

                return true;
            }
        }

        private void CalculateSunPosition(double jd, double ct)
        {
            double g, lo, s, u, v, w;

            lo = 0.779072 + 0.00273790931 * jd;
            lo = lo - Math.Floor(lo);
            lo = lo * 2 * Math.PI;

            g = 0.993126 + 0.0027377785 * jd;
            g = g - Math.Floor(g);
            g = g * 2 * Math.PI;

            v = 0.39785 * Math.Sin(lo);
            v = v - 0.01 * Math.Sin(lo - g);
            v = v + 0.00333 * Math.Sin(lo + g);
            v = v - 0.00021 * ct * Math.Sin(lo);

            u = 1 - 0.03349 * Math.Cos(g);
            u = u - 0.00014 * Math.Cos(2 * lo);
            u = u + 0.00008 * Math.Cos(lo);

            w = -0.0001 - 0.04129 * Math.Sin(2 * lo);
            w = w + 0.03211 * Math.Sin(g);
            w = w + 0.00104 * Math.Sin(2 * lo - g);
            w = w - 0.00035 * Math.Sin(2 * lo + g);
            w = w - 0.00008 * ct * Math.Sin(g);

            // compute sun's right ascension
            s = w / Math.Sqrt(u - v * v);
            _sunPositionInSkyArr[0] = lo + Math.Atan(s / Math.Sqrt(1 - s * s));

            // ...and declination
            s = v / Math.Sqrt(u);
            _sunPositionInSkyArr[1] = Math.Atan(s / Math.Sqrt(1 - s * s));
        }

        private double GetJulianDay(DateTime date)
        {
            var month = date.Month;
            var day = date.Day;
            var year = date.Year;

            var gregorian = (year < 1583) ? false : true;

            if ((month == 1) || (month == 2)) {
                year = year - 1;
                month = month + 12;
            }

            var a = Math.Floor((double)year / 100);
            var b = 0.0;

            if (gregorian)
                b = 2 - a + Math.Floor(a / 4);
            else
                b = 0.0;

            var jd = Math.Floor(365.25 * (year + 4716))
                       + Math.Floor(30.6001 * (month + 1))
                       + day + b - 1524.5;

            return jd;
        }

        private double LocalSiderealTimeForTimeZone(double lon, double jd, double z)
        {
            var s = 24110.5 + 8640184.812999999 * jd / 36525 + 86636.6 * z + 86400 * lon;
            s = s / 86400;
            s = s - Math.Floor(s);
            return s * 360 * _dR;
        }

        private int Sign(double value)
        {
            return value > 0.0 ? 1 : value < 0.0 ? -1 : 0;
        }

        private double TestHour(int k, double zone, double t0, double lat)
        {
            var ha = new double[3];
            double a, b, c, d, e, s, z;
            var time = default(double);
            int hr, min;
            double az, dz, hz, nz;

            ha[0] = t0 - _rightAscentionArr[0] + k * _k1;
            ha[2] = t0 - _rightAscentionArr[2] + k * _k1 + _k1;

            ha[1] = (ha[2] + ha[0]) / 2;    // hour angle at half hour
            _decensionArr[1] = (_decensionArr[2] + _decensionArr[0]) / 2;  // declination at half hour

            s = Math.Sin(lat * _dR);
            c = Math.Cos(lat * _dR);
            z = Math.Cos(90.833 * _dR);    // refraction + sun semidiameter at horizon

            if (k <= 0)
                _vHzArr[0] = s * Math.Sin(_decensionArr[0]) + c * Math.Cos(_decensionArr[0]) * Math.Cos(ha[0]) - z;

            _vHzArr[2] = s * Math.Sin(_decensionArr[2]) + c * Math.Cos(_decensionArr[2]) * Math.Cos(ha[2]) - z;

            if (Sign(_vHzArr[0]) == Sign(_vHzArr[2]))
                return _vHzArr[2];  // no event this hour

            _vHzArr[1] = s * Math.Sin(_decensionArr[1]) + c * Math.Cos(_decensionArr[1]) * Math.Cos(ha[1]) - z;

            a = 2 * _vHzArr[0] - 4 * _vHzArr[1] + 2 * _vHzArr[2];
            b = -3 * _vHzArr[0] + 4 * _vHzArr[1] - _vHzArr[2];
            d = b * b - 4 * a * _vHzArr[0];

            if (d < 0)
                return _vHzArr[2];  // no event this hour

            d = Math.Sqrt(d);
            e = (-b + d) / (2 * a);

            if ((e > 1) || (e < 0))
                e = (-b - d) / (2 * a);

            time = (double)k + e + (double)1 / (double)120; // time of an event

            hr = (int)Math.Floor(time);
            min = (int)Math.Floor((time - hr) * 60);

            hz = ha[0] + e * (ha[2] - ha[0]);                 // azimuth of the sun at the event
            nz = -Math.Cos(_decensionArr[1]) * Math.Sin(hz);
            dz = c * Math.Sin(_decensionArr[1]) - s * Math.Cos(_decensionArr[1]) * Math.Cos(hz);
            az = Math.Atan2(nz, dz) / _dR;
            if (az < 0)
                az = az + 360;

            if ((_vHzArr[0] < 0) && (_vHzArr[2] > 0)) {
                _riseTimeArr[0] = hr;
                _riseTimeArr[1] = min;
                _rizeAzimuth = az;
                _isSunrise = true;
            }

            if ((_vHzArr[0] > 0) && (_vHzArr[2] < 0)) {
                _setTimeArr[0] = hr;
                _setTimeArr[1] = min;
                _setAzimuth = az;
                _isSunset = true;
            }

            return _vHzArr[2];
        }

        private const double _dR = Math.PI / 180;
        private const double _k1 = 15 * _dR * 1.0027379;
        private double[] _decensionArr = new double[3] { 0.0, 0.0, 0.0 };
        private bool _isSunrise = false;
        private bool _isSunset = false;
        private object _lock = new object();
        private double[] _rightAscentionArr = new double[3] { 0.0, 0.0, 0.0 };
        private int[] _riseTimeArr = new int[2] { 0, 0 };
        private double _rizeAzimuth = 0.0;
        private double _setAzimuth = 0.0;
        private int[] _setTimeArr = new int[2] { 0, 0 };
        private double[] _sunPositionInSkyArr = new double[2] { 0.0, 0.0 };
        private double[] _vHzArr = new double[3] { 0.0, 0.0, 0.0 };

        public static SunTimes Instance { get; } = new SunTimes();

        public class LatitudeCoords : Coords
        {
            public LatitudeCoords(int degrees, int minutes, int seconds, Direction direction)
            {
                mDegrees = degrees;
                mMinutes = minutes;
                mSeconds = seconds;
                mDirection = direction;
            }

            protected internal override int Sign() => (mDirection == Direction.North ? 1 : -1);

            protected internal Direction mDirection = Direction.North;

            public enum Direction
            {
                North,
                South
            }
        }

        public class LongitudeCoords : Coords
        {
            public LongitudeCoords(int degrees, int minutes, int seconds, Direction direction)
            {
                mDegrees = degrees;
                mMinutes = minutes;
                mSeconds = seconds;
                mDirection = direction;
            }

            protected internal override int Sign() => (mDirection == Direction.East ? 1 : -1);

            protected internal Direction mDirection = Direction.East;

            public enum Direction
            {
                East,
                West
            }
        }

        internal abstract class Coords
        {
            public double ToDouble() => Sign() * (mDegrees + ((double)mMinutes / 60) + ((double)mSeconds / 3600));

            protected internal abstract int Sign();

            protected internal int mDegrees = 0;
            protected internal int mMinutes = 0;
            protected internal int mSeconds = 0;
        }
    }
}
