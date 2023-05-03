//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc. All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Runtime (RT) Status WCF service for remote clients
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Data;

using SNC.OptiRamp.Services.fRTStatus;
using SNC.OptiRamp.Services.fRT;

namespace SNC.OptiRamp.Services.fRTRemoteData
{

    [ServiceContract]
    public interface IRTRemoteData
    {
        /// <summary>
        /// Test that IRTRemoteData endpoint communication with the server is alive.
        /// </summary>
        /// <param name="msg">A message.</param>
        /// <returns>Echoes the message received.</returns>
        [OperationContract]
        string IsAlive(string msg);
        /// <summary>
        /// Gets historical data between t1 and t2 for array of tags
        /// </summary>
        /// <param name="project"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        DataSet GetItemsData(ProjectInfoExt project, string[] items, DateTime t1UTC, DateTime t2UTC, out string error);
        /// <summary>
        /// Gets historical data between t1 and t2 for one tag
        /// </summary>
        /// <param name="project"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OperationContract]
        IEnumerable<RTValueDouble> GetItemData(ProjectInfoExt project, string item, DateTime t1UTC, DateTime t2UTC, out string error);
        /// <summary>
        /// Attempt to write data to VTS
        /// Function checks tag against user name/role, and non-continuous update type.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="itemValue"> Pair "UID:Value"
        /// For example "2045:1234.56" or "2046:1=AUTO" or "2047:0", 
        /// Invariant culture shall be used for numeric</param>
        /// <param name="userName">user name for logging purpose</param>
        /// <param name="role">'long' type is 'Authorizations' enum type from SNC.OptiRamp.Security namespace</param>
        /// <param name="error"></param>
        /// <returns>returns true if ok, false if there is error</returns>
        [OperationContract]
        bool WriteItemData(ProjectInfoExt project, string itemValue, string userName, string computerName, long role, out string error);

        /// <summary>
        /// Gets the "Value" section of the xml pipeline profile file
        /// </summary>
        /// <param name="project"></param>
        /// <param name="profileName">name of profile</param>
        /// <param name="timeUTC">time of UTC.
        /// It will try ti find a pipeline object from database that is closet to the time using descending direction.</param>
        /// <param name="error">error</param>
        /// <returns>Table includes the columns:
        /// "Name", "Tag", "EU"</returns>
        [OperationContract]
        DataTable GetPipelineProfileDescription(ProjectInfoExt project, string profileName, DateTime timeUTC, out string error);
        
        /// <summary>
        /// Gets the Pipeline data for selected parameter name from the fixed list.
        // "Offset", "Pressure", "Temperature", "Mass Flow", "Elevation", "Density", "Speed"
        /// </summary>
        /// <param name="project"></param>
        /// <param name="profileName">name of profile</param>
        /// <param name="paramName">Name of parAmeter. see above.</param>
        /// <param name="timeUTC">time of UTC.
        /// It will try ti find a pipeline object from database that is closet to the time using descending direction.</param>
        /// <param name="error">error</param>
        /// <returns> returns a collection of data</returns>
        [OperationContract]
        IEnumerable<RTValueDouble> GetPipelineProfileData(ProjectInfoExt project, string profileName, string paramName, DateTime timeUTC, out string error);

        /// <summary>
        /// Gets the events and offsets
        /// </summary>
        /// <param name="project"></param>
        /// <param name="profileName">name of profile</param>
        /// <param name="paramName">Name of parAmeter. see above.</param>
        /// <param name="timeUTC">time of UTC.
        /// It will try ti find a pipeline object from database that is closet to the time using descending direction.</param>
        /// <param name="error">error</param>
        /// <returns>Returns a table with first column "Offset" and others matching all events names: "Congealing", etc.. 
        /// If event happens at the specified offset it is true, otherwise it is false</returns>
        [OperationContract]
        DataTable GetPipelineProfileEvents(ProjectInfoExt project, string profileName, DateTime timeUTC, out string error);

    }
}