using GregOsborne.Application.Primitives;
using System.Collections.Generic;
using Life.Savings.Data.Model.Application;

namespace Life.Savings.Data.Model
{
    public class MortalityItem
    {
        public MortalityItem()
        {
            PrincipalBase = new List<double> { 0, 0, 0, 0 };
            PrincipalSub = new List<double> { 0, 0, 0, 0 };
            InsuredBase = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            InsuredSub = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            InsuredCs80 = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //SpouseCs80 = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        private double _BandFaceAmount;
        public double BandFaceAmount {
            get => _BandFaceAmount;
            set {
                _BandFaceAmount = value;
                Band = new Calculations().GetBandIndex(BandFaceAmount);
            }
        }
        public IList<double> PrincipalBase { get; private set; }
        public double PrincipalCs80 { get; set; }
        public double PrincipalGpo { get; set; }
        public double PrincipalWpd { get; set; }
        public IList<double> PrincipalSub { get; private set; }
        public double SpouseBase { get; set; }
        public double SpouseSub { get; set; }
        public double SpouseCs80 { get; set; }
        public IList<double> InsuredBase { get; set; }
        public IList<double> InsuredSub { get; set; }
        public IList<double> InsuredCs80 { get; set; }
        public bool IsGuaranteed { get; set; }
        public int Year { get; set; }
        public int Band { get; set; }
        public override string ToString() {
            return $"Year: {Year}/IsGuaranteed: {IsGuaranteed}/Band: {Band}/BandFaceAmount: {BandFaceAmount}";
        }
    }
}
