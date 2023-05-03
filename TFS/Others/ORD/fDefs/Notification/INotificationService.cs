//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc. All rights reserved.
// Created by:	 Rex Gray, 
// Additions by: Alex Novitskiy
//-------------------------------------------------------------------

// Summary:
//      This file contains all required definitions and descriptions of INotificationService interface to get data from database.
//      Namespaces: SNC.OptiRamp.Services.fNotification
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

using SNC.OptiRamp.Services.fDefs;
using SNC.OptiRamp.Services.fDatabase;

namespace SNC.OptiRamp.Services.fNotification
{
    /// <summary>
    /// Database request to get all Notification records from DateTime 1 tom DateTime 2.
    /// Includes database information.
    /// </summary>
    public class NotificationRequest : DatabaseDescriptor // DatabaseDescriptor defined in ITrendingDataService.cs file as part of ITrendingDataService interface
    {
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public interface INotificationService
    {
        /// <summary>
        /// Retrieve archive event notification records within a particular timespan.
        /// </summary>
        /// <param name="request">The notification request object.</param>
        /// <param name="status">The results of the process.</param>
        /// <returns>A list of archive event notification records or an empty list.</returns>
        IEnumerable<NotificationDescriptor> GetNotifications(NotificationRequest request, out ResponseStatus status);
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    public interface INotificationReader
    {
        /// <summary>
        /// Gets count of Notification Records for specific segment database, for example for stctrl_0115.fdb.
        /// </summary>
        /// <param name="database">Database information</param>
        /// <returns>Number of records</returns>
        int GetTotalCount(DatabaseDescriptor database, string folderPath, string tag,
            DateTime? startTime, DateTime? endTime, string message, ConditionStatus? state,
            string severityMode, int? severity, bool eventsOnly);

        /// <summary>
        /// Gets filtered AEH data between two Ids throws exceptions
        /// </summary>
        /// <param name="database">The DB descriptor</param>
        /// <param name="recordId">The Id you know you want</param>
        ///<param name="recordIndex">The index in the array that you want to return the recordId</param>
        /// <param name="numberOfRecords">Number of records to return</param>
        /// <param name="folderPathFilter">Folder path to filter data</param>
        /// <param name="tag">Tag to filter data</param>
        /// <param name="startTime">Start time to filter data</param>
        /// <param name="endTime">End time to filter data</param>
        /// <param name="message">Message to filter data</param>
        /// <param name="state">State to filter data</param>
        /// <param name="severityMode">String value of severity comparison method</param>
        /// <param name="severity">Severity to filter data</param>
        /// <param name="eventsOnly">Only look for events</param>
        /// <returns>List of AlarmEventObjects that fit the given filters or throws exceptions</returns>
        IEnumerable<AlarmEventObject> GetAEHFilterData(DatabaseDescriptor database, int recordId, int recordIndex, int numberOfRecords, string folderPathFilter, string tag,
            DateTime? startTime, DateTime? endTime, string message, ConditionStatus? state, string severityMode, int? severity, bool eventsOnly);

        /// <summary>
        /// Gets number of most recent AEH Data matching filters, throws exceptions
        /// </summary>
        /// <param name="database">Database descriptor</param>
        /// <param name="numberOfRecords">How many records to return</param>
        /// <param name="folderPathFilter">Folder path to filter data</param>
        /// <param name="tag">Tag to filter data</param>
        /// <param name="startTime">Start time to filter data</param>
        /// <param name="endTime">End time to filter data</param>
        /// <param name="message">Message to filter data</param>
        /// <param name="state">State to filter data</param>
        /// <param name="severityMode">String value of severity comparison method</param>
        /// <param name="severity">Severity to filter data</param>
        /// <param name="eventsOnly">Only look for events</param>
        /// <returns>List of AlarmEventObjects that fit the given filters or throws exception</returns>

        IEnumerable<AlarmEventObject> GetLastAEHFilterData(DatabaseDescriptor database, int numberOfRecords, string folderPathFilter, string tag,
            DateTime? startTime, DateTime? endTime, string message, ConditionStatus? state, string severityMode, int? severity, bool eventsOnly);
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    

    /// <summary>
    /// Class contains properties of AE Object
    /// </summary>
    public class AlarmEventObject
    {
        public int ConditionUId { get; set; }       // unique ID per project or database
        public DateTime TimeStamp { get; set; }     //
        public string Source { get; set; }          // "OilPad1.Well1.Pressure1"
        public string Message { get; set; }         // "Too high > 100 kP"
        public ConditionStatus State { get; set; }  // { none=0, active=1, cleared=2, ackn=3, unackn=4, disabled=5 };
        public int Severity { get; set; }           // 0-1000
        public bool IsEvent { get; set; }           // true for Event, false for Alarm
    }

    /// <summary>
    /// Response has a collection of AE Objects and current position of last notification in VTS circullar buffer.
    /// </summary>
    public class AEResponse
    {
        public string error { get; set; }           // if null or empty - no error of reading data
        public string project { get; set; }         // name of active project: for example 'Hantos15000-1'
        public DateTime lastUpdated { get; set; }   // date, time when project was updated last time
        public int currentPosition { get; set; }    // current position in VTS circullar buffer
        public IEnumerable<AlarmEventObject> AEObjects { get; set; }    // collection of AE Objects notifications
    }

    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    //-----------------------------------------------------------------------------    
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IRemoteNotification
    {
        /// <summary>
        /// Returns total number of current active conditions for VTS server of active project.
        /// Function can be used to check if VTS server has any alarm.
        /// </summary>
        /// <returns>Count of conditions </returns>
        [OperationContract]
        int GetTotalCount(out string error);
        /// <summary>
        /// Returns a list of AE Object Notifications from a circullar VTS buffer 
        /// from startPosition + 1 to the end.
        /// </summary>
        /// <param name="startPosition">
        /// Index of start position to read VTS conditions from circullar buffer.
        /// if start position is -1 it returns all current AE conditions and current position.
        /// </param>
        /// <returns>List of </returns>
        [OperationContract]
        AEResponse GetAEData(int startPosition, out string error);
    }

    /// <summary>
    /// Interface defines operations with notification buffer 
    /// </summary>
    public interface INotificationBuffer
    {
        /// <summary>
        /// Counter of total active conditions
        /// Increased/Decreased when new notification comes.
        /// </summary>
        int TotalActiveConditionCounter { get; }

        /// <summary>
        /// Size of AENotifications buffer
        /// </summary>
        int BufferSize { get; }

        /// <summary>
        ///  Boolean command to stop adding notifications.
        /// </summary>
        bool Freeze { get; set; }

        /// <summary>
        /// Current position in buffer of last occured notification.
        /// </summary>
        int CurrentPosition { get; }

        /// <summary>
        /// Adding new data to AEO circullar buffer
        /// </summary>
        /// <param name="data">AEObject to add</param>
        /// <param name="error">error if can't add</param>
        /// <returns>number of position in buffer where object was put</returns>
        int Add(AlarmEventObject data, out string error);

        /// <summary>
        /// Returns a list of AE Notification Objects from startPosition+1 to the current position (included).
        /// Buffer can be overwritten, a current position can be less than required. 
        /// In this case it returns all changed objects collecting from end of buffer and then from the beginning.
        /// </summary>
        /// <param name="startPosition">Position that was last known to have last changes. Will get to next position and try to read.</param>
        /// <param name="retPosition">Returned position of last changed notification object read from the buffer</param>
        /// <param name="error">Returned error</param>
        /// <returns>Collection of notification objects that arrived in buffer after last request.</returns>
        IEnumerable<AlarmEventObject> GetNextAEOs(int startPosition, out int lastPosition, out string error);
    }
    
    /// <summary>
    /// Defines operation with RT alarms buffer
    /// </summary>
    public interface IRTAlarmsBuffer
    {
        /// <summary>
        /// Adding the RT alarm to buffer
        /// </summary>
        /// <param name="objectAE">AE Notification object to add</param>
        /// <param name="rtAlarmObjectKey">key in dictionary unique for every RT alarm</param>
        void AddRTAlarm(AlarmEventObject objectAE, string rtAlarmObjectKey);
        /// <summary>
        /// Gets all active alarms from dictionary
        /// </summary>
        /// <returns></returns>
        IEnumerable<AlarmEventObject> GetRTAlarms();
    }

}
