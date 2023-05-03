//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc. All rights reserved.
// Written by:	Greg Osborne and Alex Novitskiy
//-------------------------------------------------------------------

// Summary:
//      This file contains all required definitions and descriptions of ITrendingDataService interface
//      Namespace: SNC.OptiRamp.Services.TrendDataProcessing
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using SNC.OptiRamp.Services.fDatabase;
using SNC.OptiRamp.Services.fDefs;

namespace SNC.OptiRamp.Services.TrendDataProcessing
{
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public enum PredefinedRanges
    {
        None,
        Last1Hour,
        Last2Hours,
        Last3Hours,
        Last4Hours,
        Last5Hours,
        Last6Hours,
        Last7Hours,
        Last8Hours,
        Last9Hours,
        Last10Hours,
        Last11Hours,
        Last12Hours,
        Last13Hours,
        Last14Hours,
        Last15Hours,
        Last16Hours,
        Last17Hours,
        Last18Hours,
        Last19Hours,
        Last20Hours,
        Last21Hours,
        Last22Hours,
        Last23Hours,
        Last24Hours,
        Today,
        ThisWeek,
        ThisMonth,
        ThisYear,
        Last12Months,
        Current
    }
    public enum ScaleTypes
    {
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Year
    }
    public enum TrendChartModes
    {
        SpecifyDates,
        CurrentDates
    }
    public enum ValueTypes
    {
        Actual,
        Average,
        Maximum,
        Minimum
    }
    public enum TrendDBType
    {
        SNCGenerator,
        Firebird,
        MSSQL,
        PI,
        CSV
    }
    public enum TrendDataSource
    {
        VTS,
        ADCS,
        ADCS_WEB,
        DIAG_WEB,
        ADCS_WEB_TARGET_VALUE,
        DIAG_WEB_TARGET_VALUE
    }
    public class TrendDataItemEquality : IEqualityComparer<TrendDataItem>
    {
        public bool Equals(TrendDataItem x, TrendDataItem y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y))
                return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the objects' properties are equal.
            return x.DateTimeUTC == y.DateTimeUTC && x.Value == y.Value;
        }
        public int GetHashCode(TrendDataItem item)
        {
            if (Object.ReferenceEquals(item, null))
                return 0;

            int hashDate = item.DateTimeUTC == null ? 0 : item.DateTimeUTC.GetHashCode();

            int hashValue = item.Value.GetHashCode();

            return hashDate ^ hashValue;
        }
    }
    public class TrendDataItem : IComparable
    {
        public string ChannelName { get; set; }
        public DateTime DateTimeUTC { get; set; }
        public bool IsError { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// Compare the timestamps of two data items for sort order.
        /// </summary>
        /// <param name="obj">The data item to be compared with this data item.</param>
        /// <returns>-1, 0 or 1 indicating sort order between two data items.</returns>
        public int CompareTo(object obj)
        {
            TrendDataItem other = obj as TrendDataItem;
            if (other != null)
            {
                return (this.DateTimeUTC).CompareTo(other.DateTimeUTC);
            }
            return 0;
        }
    }
    public class ChannelItem :IComparable
    {
        public bool IsGroupName { get; set; }
        public string Alias { get; set; }
        public string DataSourceName { get; set; }
        public string EngineeringUnitName { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// compares two channelitem objects using channelname of each to compare.
        /// </summary>
        /// <param name="obj">
        /// channel item to compare to
        /// </param>
        /// <returns>
        /// returns int value indicating whether channelitem object comes before/after other channelitem object
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj is ChannelItem)
            {
                ChannelItem other = obj as ChannelItem;
                return (this.Name).CompareTo(other.Name);
            }
            return 0;
        }
    }
    public class BoxDataItem
    {
        public BoxDataItem() { }
        public BoxDataItem(DateTime dt, int countItems) // instance is created for specific DateTime
        {
            DateTimeUTC = dt;
            DataSetCount = countItems;
            InterQuartile = LowerExtreme = LowerQuartile = Mean = Median = UpperExtreme = UpperQuartile = Variance = double.NaN;
        }
        public int DataSetCount { get; set; }
        public double InterQuartile { get; set; }
        public double LowerExtreme { get; set; }
        public double LowerQuartile { get; set; }
        public double Mean { get; set; }
        public double Median { get; set; }
        public double UpperExtreme { get; set; }
        public double UpperQuartile { get; set; }
        public double Variance { get; set; }
        public DateTime DateTimeUTC { get; set; }
        public void DeepCopy(BoxDataItem from)
        {
            DataSetCount = from.DataSetCount;
            InterQuartile = from.InterQuartile;
            LowerExtreme = from.LowerExtreme;
            LowerQuartile = from.LowerQuartile;
            Mean = from.Mean;
            Median = from.Median;
            UpperExtreme = from.UpperExtreme;
            UpperQuartile = from.UpperQuartile;
            Variance = from.Variance;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class TrendRequestResponse : DatabaseDescriptor
    {
        public string ChannelName { get; set; }     // name of channel, for example: Steam Generator2.Efficiency
        public string ChannelsTable { get; set; }   // channels table, for example "ADCS_WEB_CHANNELS"
        public string ValuesTable { get; set; }     // values table for example: "ADCS_DIAG_CHANNELS"
    }

    /// <summary>
    /// 
    /// </summary>
    public class TopValuesRequest : TrendRequestResponse
    {
        public TopValuesRequest() { }
        public TopValuesRequest(TrendRequestResponse request)
        {
            this.ServerAddress = request.ServerAddress;
            this.ChannelsTable = request.ChannelsTable;
            this.ValuesTable = request.ValuesTable;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.DataSource = request.DataSource;
            this.Password = request.Password;
            this.UserName = request.UserName;
        }

        public List<string> ChannelNames { get; set; }
    }
    public class TrendingChartRequest : TrendRequestResponse
    {
        public DateTime StartDateUTC { get; set; }
        public DateTime? EndDateUTC { get; set; }
        public ScaleTypes ScaleType { get; set; }
        public TrendChartModes TrendChartMode { get; set; }
        public ValueTypes ValueType { get; set; }

        public TrendingChartRequest Copy()
        {
            return (TrendingChartRequest)this.MemberwiseClone();
        }
    }
    public class TrendingChartResponse : TrendingChartRequest
    {
        public TrendingChartResponse(TrendingChartRequest request)
        {
            this.ServerAddress = request.ServerAddress;
            this.ChannelName = request.ChannelName;
            this.ChannelsTable = request.ChannelsTable;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.DataSource = request.DataSource;
            this.Password = request.Password;
            this.UserName = request.UserName;
            this.ValuesTable = request.ValuesTable;
            this.EndDateUTC = request.EndDateUTC;
            this.ScaleType = request.ScaleType;
            this.StartDateUTC = request.StartDateUTC;
            this.TrendChartMode = request.TrendChartMode;
            this.ValueType = request.ValueType;
        }
        public ObservableCollection<TrendDataItem> DataItems { get; set; }
    }
    public class TrendingDateTimeRequest : TrendRequestResponse
    {
        public DateTime EstimatedDateUTC { get; set; }
        public ScaleTypes ScaleType { get; set; }
    }
    public class TrendingDateTimeResponse : TrendRequestResponse
    {
        public TrendingDateTimeResponse(TrendRequestResponse request)
        {
            this.ServerAddress = request.ServerAddress;
            this.ChannelName = request.ChannelName;
            this.ChannelsTable = request.ChannelsTable;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.DataSource = request.DataSource;
            this.Password = request.Password;
            this.UserName = request.UserName;
            this.ValuesTable = request.ValuesTable;
        }
        public TrendDataItem DataItem { get; set; }
    }
    public class ParameterlessRequest : TrendRequestResponse
    {
    }
    public class TrendingChannelsRequest : TrendRequestResponse
    {
        public string GroupName { get; set; }
    }
    public class TrendingBoxRequest : DatabaseDescriptor
    {
        public DateTime EndDateUTC { get; set; }
        public DateTime StartDateUTC { get; set; }
        public IEnumerable<string> TargetValueList { get; set; }
        public ScaleTypes ScaleType { get; set; }
    }
    public class TrendingBoxResponse : TrendingBoxRequest
    {
        public TrendingBoxResponse(TrendingBoxRequest request)
        {
            this.ServerAddress = request.ServerAddress;
            this.Database = request.Database;
            this.DatabaseType = request.DatabaseType;
            this.DataSource = request.DataSource;
            this.Name = request.Name;
            this.Password = request.Password;
            this.UserName = request.UserName;
            this.EndDateUTC = request.EndDateUTC;
            this.StartDateUTC = request.StartDateUTC;
            this.TargetValueList = request.TargetValueList;
        }
        public ObservableCollection<BoxDataItem> DataItems { get; set; }
    }
    public class TrendingColumnRequest
    {
        public int Sequence { get; set; }
        public string ChannelName { get; set; }
        public bool HeaderColumn { get; set; }
    }
    public class TrendingRowRequest
    {
        public int Sequence { get; set; }
        public string RowName { get; set; }
        public List<TrendingColumnRequest> Channels { get; set; }
    }
    public class TrendingRowResponse
    {
        public int Sequence { get; set; }
        public string RowName { get; set; }
        public List<TrendDataItem> Values { get; set; }
    }
    public class TrendingMatrixRequest : TrendRequestResponse
    {
        public TrendingMatrixRequest() { Delimiter = '.'; }
        public List<TrendingRowRequest> ChannelMatrix { get; set; }
        public char Delimiter { get; set; }
    }
    public class TrendingMatrixResponse
    {
        public TrendingMatrixRequest Request { get; set; }
        public List<TrendingRowResponse> ResponseMatrix { get; set; }
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public interface ITrendingDataService
    {
      
        // Summary:
        //     Obsolete - don't use
        // 
        Dictionary<DateTime, double> GetStaticDataForChannel(TrendingChartRequest request, out ResponseStatus Status);


        // Summary:
        //      Returns TrendDataItem collection from database >= StartDateUTC and <=EndDateUTC
        //      For StartDateUTC it takes a first nearest value <=StartDateUTCfrom database. 
        //      If there is no such value, the TrendDataItem of StartDateUTC time will be not in the collection.
        //      For EndDateUTC it takes value <=EndDateUTC. 
        //      It is possible that the TrendDataItem for EndDateUTC time will be not in the collection.
        //      Collection can be empty if there is no one value in database.
        //
        // Parameters:
        //      request: 
        //      status: 
        //
        // Returns:
        //     TrendDataItem collection, status
        //
        // Exceptions:
        //      No exceptions
        //
        TrendingChartResponse GetTrendChartDataForChannel(TrendingChartRequest request, out ResponseStatus Status);


        // Summary:
        //      Returns a single TrendDataItem for a value with nearest time <=EstimatedDateUTC
        //      to show the point value on the chart. 
        //      If there is no such value in database the returned DataItem = null.
        //
        // Parameters:
        //      request: 
        //      status: 
        //
        // Returns:
        //     TrendingDateTimeResponse, status
        //
        // Exceptions:
        //      No exceptions
        //
        TrendingDateTimeResponse GetDateTimeValuesForChannel(TrendingDateTimeRequest request, out ResponseStatus Status);


        // Summary:
        //      Returns a ChannelItem collection.
        //      The optional GroupName  is used in request as a parent to get children channels.
        //      If  GroupName is null or empty the root channels will be returned.
        //
        // Parameters:
        //      request: 
        //      status: 
        //
        // Returns:
        //     ChannelItem collection, status
        //
        // Exceptions:
        //      No exceptions
        //
        ObservableCollection<ChannelItem> GetChannels(TrendingChannelsRequest request, out ResponseStatus Status);

        //
        //
        // Not implemented
        ObservableCollection<string> GetEngineeringUnits(ParameterlessRequest request, out ResponseStatus Status);

        /// <summary>
        ///     Returns a BoxDataItem collection greater/equal the StartDateUTC and less/equal the EndDateUTC for TargetsValuesList.
        ///     TargetValuesList is a list of channels in (target, value) sequence.
        ///     If timescale is ‘minute’, ‘second’ it returns the box data collection calculated of actual records from database, i.e. timestamps will be taken from database without modification.
        ///     
        ///     If timescale is hours, weeks, months, years this function produces the averaging for specific interval.
        ///     It starts collecting the average exactly from StartDateUTC and ends on EndDateUTC.
        ///     It returns a collection of data with ALL timestamps in row. 
        ///     If for some timestamp there is no data it returns BoxDataItem with DataSetCount=0 and all ‘double’ values = n/a.
        ///
        ///     For example: request of 'hour' of timescale from t1=8:39.20 of Jul 29 to t2=8:39.20 of Jul 30 will return the following row of data:
        ///     Jul 29 8:00-n/a, 9:00-data if any, 10:00-data if any, 11:00-data if any, ... Jul 30 ... 5:00-data if any, 6:00-data if any, 7:00-data if any, 8:00-data if any – 25 items.
        ///     The first value is PROBABLY = n/a since from 8:39 to 9:00 there was PROBABBLY no data.
        ///     To get data for the 'hour' scale  with the first value != n/a the following request example will work: t1=8:00.00 of Jul 29, t2=8:39.20 of Jul 30.
        ///     It returns 25 box items starting with Jul 29 8:00-data if any, 9:00-data if any, etc...
        ///     
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        /// <returns>
        ///     TrendingBoxResponse, ResponseStatus status
        /// </returns>
        /// <remarks>No exception</remarks>
        /// 
        TrendingBoxResponse GetBoxDataForChannels(TrendingBoxRequest request, out ResponseStatus Status); // for mean, max, min,  and etc see box & whiskers data definition
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        List<TrendDataItem> GetTopItemForListOfChannels(TopValuesRequest request, out ResponseStatus Status);

        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        TrendingMatrixResponse GetDataForMatrix(TrendingMatrixRequest request, out ResponseStatus Status);

    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public interface ITrendingDataServiceDebug
    {
        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugConnectionString(ParameterlessRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        bool DebugConnect(ParameterlessRequest request, string connectionString, out ResponseStatus status);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugChannelsRequestString(TrendingChannelsRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        ObservableCollection<ChannelItem> DebugChannelsRequest(TrendingChannelsRequest request, out ResponseStatus status, string command);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugSingleValueRequestString(TrendingDateTimeRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        TrendingDateTimeResponse DebugSingleValueRequest(TrendingDateTimeRequest request, out ResponseStatus status, string command);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugT1T2ValuesRequestString(TrendingChartRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        TrendingChartResponse DebugT1T2ValuesRequest(TrendingChartRequest request, out ResponseStatus status, string command);

        // Avg: min, hours, days, months, years
        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugAvrgSingleValueRequestString(TrendingDateTimeRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        TrendingDateTimeResponse DebugAvrgSingleValueRequest(TrendingDateTimeRequest request, out ResponseStatus status, string command);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        string DebugAvgT1T2ValuesRequestString(TrendingChartRequest request);

        // Summary:
        //      
        // Parameters:
        //      
        // Returns:
        //    
        // Exceptions:
        //     
        TrendingChartResponse DebugAvgT1T2ValuesRequest(TrendingChartRequest request, out ResponseStatus status, string command);
    }
}