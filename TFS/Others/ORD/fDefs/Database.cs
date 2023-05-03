//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc. All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Definition of interfaces and classes for database interfaces.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using SNC.OptiRamp.Services.fDefs;
using SNC.OptiRamp.Services.fWeb;
using SNC.OptiRamp.Services.fSncZip;
using SNC.OptiRamp.Services.fRT;
 
namespace SNC.OptiRamp.Services.fDatabase
{
    /// <summary>
    /// Definitions of bits (flags). Used by Trend Data Service to know how to get historical data
    /// Applied in DatabaseDescriptor.Options field
    /// For example if Request for data includes runtime and BLOBs the Options = BLOBS | RUNTIME = 0x3;
    /// </summary>
    public class HDAOptions 
    {
        public const int RUNTIME = 0x1;         // read historical data from runtime service
        public const int BLOBS = 0x2;           // read BLOBS for seconds, otherwise the "second" table from database
        
        /// <summary>
        /// Auto selection of SCALE (sec, min, hours) based on request time span. 
        /// If this option is set, the client request scale is ignored.
        /// t1~t2 less 4 hours:                scale = SEC
        /// t1~t2 betwee 4 hours and 2 days:   scale = MIN
        /// t1~t2 greater 2 days:              scale = HOUR
        /// </summary>
        public const int AUTOSCALE = 0x4;       // Trend Data Service makes Auto selection of SCALE

        public static bool IsRUNTIME(int options)
        {
            return (options & RUNTIME) == RUNTIME;
        }

        public static bool IsBLOBS(int options)
        {
            return (options & BLOBS) == BLOBS;
        }

        public static bool IsAUTOSCALE(int options)
        {
            return (options & AUTOSCALE) == AUTOSCALE;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DatabaseDescriptor
    {
		public bool IsVts { get; set; }             // flag set if instance is a vts data source
        public string ServerAddress { get; set; }   // domain name or IP, for example: localhost, xps-conference, 192.169.0.23
        public string Database { get; set; }        // name or path to database depending on database type. Firebird example: c:\adcs\bin\db\stctrl.fdb
        public string DatabaseType { get; set; }    // Enum.GetNames(typeof(TrendDBType))
        public string DataSource { get; set; }      // computername.datatype, for example: xps-conference.ADCS_WEB or xps-conference.DIAG_WEB. Datatype is ADCS_WEB | DIAG_WEB. ## was Enum.GetNames(typeof(TrendDataSource))
        public string Name { get; set; }            // ??
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Options { get; set; }            // see HDAOptions flags above. Flags can be combined using bitwise operations. For example:  BLOBS | RUNTIME | AUTOSCALE; 
    }

    /// <summary>
    /// Conditions for alarm object.
    /// </summary>
    public enum ConditionStatus { none=0, active=1, cleared=2, ackn=3, unackn=4, disabled=5 };    // letters: space,A,C,K,U,D
    
    /// <summary>
    /// Types of archive triggers.
    /// </summary>
    public enum ArchiveTrigger { none, onCondition, onDeviation };      // letters: space,C,D
    
    /// <summary>
    /// Descriptor of Notification holding information about changing of system condition.
    /// </summary>
    public class NotificationDescriptor
    {
        /// <summary>
        /// private fields
        /// </summary>
        private DateTime? t1;
        private DateTime? t2;

        /// <summary>
        /// public properties
        /// </summary>
        public DateTime NTime                 { get; set; } // notification time when condition changed.
        public string NSource                 { get; set; } // source of condition. Full path to tag element: WellPad1.Well1.Inlet_Pressure
        public string NMessage                { get; set; } // text of condition. For example: Value is higher than 120 kPa.
        public ConditionStatus NState         { get; set; } // state: active, cleared etc...
        public ArchiveTrigger NArchiveTrigger { get; set; } // type of archive trigger: deviation or event.
        public int NSeverity                  { get; set; } // severity. From opc standard 0 -1000.
        public DateTime? NTime1                             // Time (if not null) of beginning of event.
        {
            get { return t1.HasValue ? t1.Value : new Nullable<DateTime>(); }
            set { t1 = value; }
        }
        public DateTime? NTime2                             // Time (if not null) of ending of event.
        {
            get { return t2.HasValue ? t2.Value : new Nullable<DateTime>(); }
            set { t2 = value; }
        }
        public string NGroupName              { get; set; }  // Group name. Obsolete. Niot used. must be list of folders that related to this event. Needs only for trender to show event sign for group of channels.
        public int NEvent                     { get; set; }  // Type of notification: Event or Alarm.
    }

    //-------------------------------------------------------------------
    // (©) 2015 Statistics & Control Inc. All rights reserved.
    // Created by:	Andrew Novitskiy
    //-------------------------------------------------------------------
    //
    // Definitions of classes, structures and interfaces for the CACircleBuffer
    //

    /// <summary>
    /// Channel Data: Name, Engineering Units
    /// </summary>
    public class ChannelDescriptor
    {
        public string Name;
        public string EU;
        public string Description;
    }

    /// <summary>
    /// Interface that defines adding to the buffer and returning the buffer in form of array
    /// </summary>
    public interface ICADataBuffer
    {
        /// <summary>
        /// 
        /// </summary>
        bool Freeze { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ChannelDescriptor Channel { get; }

        /// <summary>
        /// 
        /// </summary>
        TimeSpan BufferTimeSpan { get; } // the amount of time allowed to pass before dequeueing from DataHolder
        
        /// <summary>
        /// 
        /// </summary>
        DateTime FirstBufferTime { get; }

        /// <summary>
        /// 
        /// </summary>
        DateTime LastBufferTime { get; }

        /// <summary>
        /// how many points in buffer
        /// </summary>
        int PointsBufferCount      { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="error"></param>
        void Add(RTValueDouble data, out string error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns>GetBuffer returns empty array if error</returns>
        IEnumerable<RTValueDouble> GetBuffer(out string error);
    }
    
    /// <summary>
    /// Interface for transforming collection of CABuffers to Dataset or Blob
    /// </summary>
    public interface ICADataSet
    {
        /// Creates a data set that will have a table which holds values as Timestamp, and Value. Timestamps will be sorted.
        ///
        DataSet MakeCATable(CADescriptor eventDescriptor, IEnumerable<ICADataBuffer> dataBuffers, DateTime reqdBegTimeUTC, DateTime reqdEndTimeUTC, out string error, string formatter);
    }

    //-------------------------------------------------------------------
    // (©) 2015 Statistics & Control Inc. All rights reserved.
    // Created by:	Alex Novitskiy, Ilya Markevich
    //-------------------------------------------------------------------

    //
    // Definition of interfaces and classes to read/write CA data from/to OptiRamp database.
    //

    /// <summary>
    /// This class provides the description information of Archive Event
    /// </summary>
    public class CADescriptor
    {
        public int ID { get; set; }             // Unique ID produced by database when CADescriptor record is written.
        public string EventName { get; set; }   // name of Evet
        public string EventReason { get; set; } // name of conditions that waas a reason for writing the event
        public DateTime TStart { get; set; }    // start time 
        public DateTime TEnd { get; set; }      // end time 
        public DateTime TEvent { get; set; }    // time of Event. The time is always in the middle between tstart and tend
        public string[] Channels { get; set; }  // array of channel NAMES
    }
    
    /// <summary>
    /// Dataset tables names
    /// </summary>
    public enum CASections { General, Channels, Data, Annotations };
    
    /// <summary>
    /// General CA attributes
    /// </summary>
    public enum CAAttributes { Interval, NumberOfSamples, CEDateTime, AfterCETime, Time, NaN, OPCItem, EU, Name, Description, TimeStamp, CABRelease, ControlModule, File, TStart, TEnd };

    /// <summary>
    /// Interface to write/read CA data.
    /// All functions are blocking. 
    /// Client is assumed to use a separate working thread.
    /// </summary>
    public interface ICADatabase
    {
        /// <summary>
        /// Find information on all events that occurred within a particular time span.
        /// </summary>
        /// <param name="t1UTC">Start of the time span represented in universal time.</param>
        /// <param name="t2UTC">End of the time span represented in universal time.</param>
        /// <param name="error"></param>
        /// <returns>A list of events.</returns>
        IEnumerable<IDboEventInfo> GetEvents(DateTime t1UTC, DateTime t2UTC, out string error);

        /// <summary>
        /// Extracting a collection of the descriptor data for period t1-t2.
        /// The time interval suppose to include either or both TStart, TEnd of any critical event.
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="t1UTC"></param>
        /// <param name="t2UTC"></param>
        /// <param name="error"></param>
        /// <returns> 
        /// Returns the enumerator to collection of CA descriptors. 
        /// Can return null.
        /// No exceptions.
        /// </returns>
        IEnumerable<CADescriptor> ReadCADescriptors(int channelID, DateTime t1UTC, DateTime t2UTC, out string error);

        /// <summary>
        /// Reads CA data blob from database to DataSet.
        /// DataSet is assumed to include General, Channels and Data tables written to blob.
        /// </summary>
        /// <param name="Descriptor"></param>
        /// <param name="error"></param>
        /// <returns>
        /// Returns the tables with data.
        /// Can return null.
        /// No exceptions.
        /// </returns>
        DataSet ReadCAData(CADescriptor Descriptor, out string error);

        /// <summary>
        /// Writes data to database from Dataset as a BLOB
        /// </summary>
        /// <param name="Descriptor">
        /// The descriptor information
        /// </param>
        /// <param name="data">
        /// Input dataset converted into a BLOB. Includes three tables: General, Channels, Data
        /// </param>
        /// <param name="error"></param>
        /// <returns>
        /// If error it returns false. If there is no error it returns true.
        /// </returns>
        bool WriteCAData(CADescriptor Descriptor, MemoryStream data, out string error);
    }
    
    //-------------------------------------------------------------------
    // (©) 2015 Statistics & Control Inc.  All rights reserved.
    // Written by:	Omar Chughtai & Alex Novitskiy
    //------------------------------------------------------------------- 
    /// <summary>
    /// Interface provides functions to read and write data to CSV file, along with function to get Critical Archive Time data from csv file.
    /// </summary>
    public interface ICAData
    {
        /// <summary>
        /// Reads data from requested file, splits up data into different sections        
        /// </summary>
        /// <param name="filePath">the location of the CSV file</param>
        /// <param name="error">Any errors will be assigned to the variable</param>
        /// <returns>Returns a dataset that contains data from each section, each section is placed in its own data table</returns>
        DataSet GetLocalCSVData(string filePath, out string error);

        /// <summary>
        /// Writes data from given dataset into file specified by filepath
        /// </summary>
        /// <param name="filePath">Provides file location used to write to</param>
        /// <param name="data">The dataset that contains data tables</param>
        /// <param name="error">Any errors will be assigned to this variable</param>
        void SetLocalCSVData(string filePath, DataSet data, out string error);

        /// <summary>
        /// Provide critical archive time information
        /// </summary>
        /// <param name="ds">The dataset that contains all the data from each section</param>
        /// <param name="t1">The beginning time</param>
        /// <param name="t2">The end time</param> 
        /// <param name="CETime">Critical Event Time</param>
        /// <param name="error">Any errors will be assigned to this variable</param>
        /// <returns>returns true or false depending on whether an error occured or not</returns>
        bool GetCATime(DataSet ds, out DateTime t1, out DateTime t2, out DateTime CETime, out string error);

        /// <summary>
        /// Convert a DataSet into a MemoryStream for storage as a BLOB in a database.
        /// </summary>
        /// <param name="blobFileName">An internal file name without directory path corresponding to the DataSet.</param>
        /// <param name="data">The DataSet to be zipped.</param>
        /// <param name="error">An error message.</param>
        /// <returns>A MemoryStream that can be stored as a BLOB in a database.</returns>
        MemoryStream ConvertDataSetIntoBlob(string blobFileName, DataSet data, out string error);

        /// <summary>
        /// Convert a database BLOB MemoryStream into a DataSet.
        /// </summary>
        /// <param name="blob">A DataSet BLOB read from a database.</param>
        /// <param name="error">An error message.</param>
        /// <returns>The DataSet created from the BLOB.</returns>
        DataSet ConvertBlobIntoDataSet(MemoryStream blob, out string error);
    }


    //---------------------------------------- Definitions for fDBOConnection and fDBOWriter in ADCSServices  ----------------
    /// <summary>
    /// 
    /// </summary>
    public interface IDboEventInfo
    {
        string message  { get; set; }
        bool error      { get; set; }
        object data     { get; set; }
    }
    
    /// <summary>
    ///
    /// callback function signature to notify a client about problems during any DBO processing
    ///
    /// <param name="?"></param>
    public delegate void DboNotifyHandler(IDboEventInfo ei);

    /// <summary>
    /// Interface defines database connection operations
    /// </summary>
    public interface IDBConnection
    {
        IProject project    { get; }
        IElement database   { get; }
        bool Open(out string sError);
        void Close();
        event DboNotifyHandler notificationEvent; // notify a client about problems during any DBO processing
    }

    /// <summary>
    /// Interface defines db 'write' operations
    /// </summary>
    public interface IDBOWriter
    {
        bool WriteWebChannel(IElement pElement, IWebChannel pWebChannel, RTData pData, out string sError);
        bool WriteWebChannelBlocking(IElement pElement, IWebChannel pWebChannel, RTData pData, ref bool bStop, out string sError);
        bool WriteWebChannels(IElement pElement, IEnumerable<RTValue> pData, out string sError);
        bool WriteWebChannelsBlocking(IElement pElement, IEnumerable<RTValue> pData, ref bool bStop, out string sError);
        event DboNotifyHandler notificationEvent; // notify a client about problems during any DBO processing
        bool WriteNotification(NotificationDescriptor nd, out string error);
        bool WriteNcWebChannel(IElement pElement, IWebChannel pWebChannel, RTDataString pData, ApplicationInfo pAppInfo, out string sError);
        bool WriteStreamToBlob(string tableName, string dataName, DateTime timeStampUTC,  MemoryStream data, out string error);
    }
}
