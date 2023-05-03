//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc.  All rights reserved.
// Created by:	Jeff Shafferman
// Modified by: Alex Novitskiy
//-------------------------------------------------------------------
//
// Definitions of classes, structures, enums and interfaces for real time items
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SNC.OptiRamp.Services.fDefs;
using SNC.OptiRamp.Services.fDatabase;

namespace SNC.OptiRamp.Services.fRT
{
    /// <summary>
    /// Delegate for event that must retrieve data from specific item (double value)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="value"></param>
    /// <param name="time"> watch for Kind property </param>
    /// <param name="quality"></param>
    public delegate void RTItemEventHandler(IRTItem item, Nullable<double> value, DateTime time, long quality);

    /// <summary>
    /// Delegate for event that must retrieve data form specific item (string value)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="value"></param>
    /// <param name="time"> watch for Kind property </param>
    /// <param name="quality"></param>
    public delegate void RTItemEventHandlerStr(IRTItem item, String value, DateTime time, long quality);

    /// <summary>
    /// What service type the real time data is openning
    /// </summary>
    public enum RTServiceType
    {
        ODBC,
        OPC
    }

    /// <summary>
    /// Represents Real time item data
    /// Returns as reference to object from AddItem() method of OPC or ODBC Group object.
    /// </summary>
    public interface IRTItem
    {
        bool Active { get; set; }
        bool LastValue(out string value, out DateTime time, out long quality); // return true if there is a value
    }

    /// <summary>
    /// Real time Data group
    /// </summary>
    public interface IRTGroup
    {
        IRTItem AddItem(string itemTag, out string error);
    }

    /// <summary>
    /// Real time Connection
    /// </summary>
    public interface IRTConnection
    {
        /// <summary>
        /// Open real time group
        /// </summary>
        /// <param name="groupName">
        /// The name of the real time group being opened
        /// </param>
        /// <param name="msecRefreshRate">
        /// Refresh rate in mseconds 
        /// </param>
        /// <param name="error">
        /// Output of any error that can occur while openning a real time group
        /// </param>
        IRTGroup OpenGroup(string groupName, int msecRefreshRate, out string error);
    }

    /// <summary>
    /// Real time engine data is using
    /// </summary>
    public interface IRTEngine
    {
        /// <summary>
        /// Open Client connection
        /// </summary>
        /// <param name="MachineName">
        /// Can be empty for local connection
        /// </param>
        /// <param name="serverName">
        /// Cannot be empty   
        /// </param>
        /// <param name="error">
        /// Represents an explanation of a possibe error while trying to open an real time connection to a server
        /// </param>
        IRTConnection OpenClient(string machineName, string serverName, out string error);

        /// <summary>
        /// Start a real time engine
        /// </summary>
        /// <param name="bSyncRead"></param>
        /// <param name="error">
        /// Output of any errors that can occur while attempting to start an engine
        /// </param>
        void Start(bool bSyncRead, out string error);

        /// <summary>
        /// Stop a real time engine
        /// </summary>
        /// <param name="error">
        /// Output of any errors that can occur while attempting to stop an engine
        /// </param>
        void Stop(out string error);

        /// <summary>
        /// Event for real time item representing a double value
        /// </summary>
        event RTItemEventHandler RTItemValue;

        /// <summary>
        /// Event for real time item representing a string value
        /// </summary>
        event RTItemEventHandlerStr RTItemValueStr;
    }

    /// <summary>
    /// standard OPC DA quality numbers
    /// </summary>
    public enum RTQuality : int { Bad = 0, Good = 192, NotConnected = 8, OutOfService = 28 };

    /// <summary>
    /// Runtime data 
    /// </summary>
    public class RTData
    {
        /// <summary>
        /// Properties
        /// </summary>
        public Object Value { get; set; }
        public DateTime Timestamp { get; set; }
        public uint Quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void SetGoodQuality()
        {
            Quality = (uint)RTQuality.Good;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValueGood()
        {
            if (Value != null && IsQualityGood(Quality))
                return true;
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public static bool IsQualityGood(uint qual)
        {
            return qual == (uint)RTQuality.Good;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public static bool IsQualityBad(uint qual)
        {
            return !IsQualityGood(qual);
        }
    }

    /// <summary>
    /// Runtime data (double value)
    /// </summary>
    public class RTDataDouble : RTData
    {
        new public double? Value { get; set; }
        new public bool IsValueGood()
        {
            return Value.HasValue && IsQualityGood(Quality);
        }
    }
    /// <summary>
    /// Runtime data (string value)
    /// </summary>
    public class RTDataString : RTData
    {
        new public string Value { get; set; }
        new public bool IsValueGood()
        {
            return !string.IsNullOrEmpty(Value) && IsQualityGood(Quality);
        }
    }

    /// <summary>
    /// Structure similar to RTDataDouble
    /// </summary>
    public struct RTValueDouble
    {
        public double? Value { get; set; }
        public DateTime Timestamp { get; set; }
        public uint Quality { get; set; }
        public void SetGoodQuality()
        {
            Quality = (uint)RTQuality.Good;
        }
        public void SetBadQuality()
        {
            Quality = (uint)RTQuality.Bad;
        }
        public bool IsValueGood()
        {
            return RTData.IsQualityGood(Quality);
        }
    }

    /// <summary>
    /// Structure similar to RTDataDouble + IDataChannel field
    /// </summary>
    public struct RTValue
    {
        public IDataChannel DataChannel { get; set; }   // project data (min, max, EU, etc...). It is base interface for IWebChannel, IOChannel
        public DateTime Timestamp { get; set; }         // default value = DateTime.MinValue
        public uint Quality { get; set; }               // quality code
        public double? Value;
        public bool IsValueGood()
        {
            return Value.HasValue && RTData.IsQualityGood(Quality);
        }
        public void SetGoodQuality()
        {
            Quality = (uint)RTQuality.Good;
        }
    }

    /// <summary>
    /// User information associated with run-time activity.
    /// </summary>
    public class ApplicationInfo
    {
        public string FullPath { get; set; }
        public string ComputerName { get; set; }
        public int InstanceNumber { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string UserInfo { get; set; }
        /// <summary>
        /// A hash string used as a cache index for ApplicationInfo records.
        /// </summary>
        /// <returns>A concatenation of ComputerName, FullName, ShortName and UserInfo.</returns>
        public string HashString()
        {
            string sKey1 = string.IsNullOrEmpty(ComputerName) ? string.Empty : ComputerName;
            string sKey2 = string.IsNullOrEmpty(FullName) ? string.Empty : FullName;
            string sKey3 = string.IsNullOrEmpty(ShortName) ? string.Empty : ShortName;
            string sKey4 = string.IsNullOrEmpty(UserInfo) ? string.Empty : UserInfo;
            string hash = sKey1 + sKey2 + sKey3 + sKey4;
            return string.IsNullOrEmpty(hash) ? "BAD_HASH_STRING" : hash;
        }
    }

    // ----------------------- VTS Bridge Section ------------------------------

    // VTS Bridge DA ---------
    /// <summary>
    ///  
    /// </summary>
    public interface IRT_Bridge_Client_Info
    {
        string ComputerName { get; }
        string FullPath { get; }
    };

    //
    public interface IRT_Bridge_Client_Tag_Info
    {
        string TagName { get; }
        Type TagType { get; }
    };

    //
    public delegate void IRT_Bridge_Server_CallBack(
        IRT_Bridge_Client_Info          clientInfo,
        IRT_Bridge_Client_Tag_Info      tagInfo,
        string                          value, 
        DateTime                        time, 
        long                            quality
        );

    // VTS Bridge HD -----------
    // 
    public interface IRT_Bridge_HD_Data
    {
        void Set_Data(IEnumerable<RTDataDouble> pData);
        void Set_Error(string sError);
    };

    //
    public delegate void IRT_Bridge_Server_HD_CallBack(
        IRT_Bridge_Client_Tag_Info      tagInfo,
        DateTime                        timeFromUTC,
        DateTime                        timeToUTC,
        IRT_Bridge_HD_Data              bridgeHDData
        );

    // VTS Bridge AE ---------------
    public delegate void IRT_Bridge_Server_AE_CallBack(
        IRT_Bridge_Client_Info          clientInfo,
        fNotification.AlarmEventObject  objectAE
        );

    // VTS Bridge Img ---------------
    public struct Img_Def { public string Name; public int Width, Height; }
    /// <summary>
    /// depending of valueType the object is casted differently
    /// Error ----> string
    /// PMG   ----> byte[]
    /// </summary>
    public enum Img_Result_Type { Error, PNG }
    //
    public struct Img_Result {  public Img_Result_Type valueType;  public object value;  }

    // VTS Bridge PP ---------------
    public delegate void IRT_Bridge_Server_PP_CallBack(
        string                      namePipelineProfile,
        DateTime                    recordTimeUtc,
        System.Xml.XmlDocument      documentPipelineProfile
        );

    /// <summary>
    /// Run-time Bridge Server
    /// </summary>
    public interface IRT_Bridge_Server
    {
        /// <summary>
        /// 
        /// </summary>
        void Add_Tag(string TagName, Type TagType);
        void Write_Data(string TagName, RTDataDouble data);
        void Write_Data(string TagName, RTDataString data);
        //
        event IRT_Bridge_Server_CallBack evtValue;
        //
        void Request_All();
        //
        event IRT_Bridge_Server_HD_CallBack evtHD;
        //
        event IRT_Bridge_Server_AE_CallBack evtAE;
        //
        event IRT_Bridge_Server_PP_CallBack evtPP;


        /// <summary>
        /// the DataTime.MinValue is used when no image available
        /// </summary>
        DateTime Img_Current_Time(Img_Def img_def);
        //
        Img_Result Img_Current_Value(Img_Def img_def);
    }



    /// <summary>
    /// 
    /// </summary>
    public interface IRTItemData
    {
        /// <summary>
        /// Properties
        /// </summary>
        string ProcessArea { get; set; }    // full or some part of path in project to element. 
                                            // Depeneds on project type. 
                                            // For example for RodPump it can be a pump name: 4E0406A
                                            // For VTS project it is full path: folder.folder.folder etc...
        string Name { get; set; }           // variable name in developer
        string FullName { get; set; }       // process area + name = "folder.folder.folder.Name"
        string Tag { get; set; }            // item name
        string Description { get; set; }    // Description of tag. For example: "Well head pressure".
        string EU { get; set; }
        double? Min { get; set; }
        double? Max { get; set; }
        double? SPAN { get; set; }
        double? OFFSET { get; set; }
        string RTServerName { get; set; }
        string RTGroupName { get; set; }
        double? WriteValue { get; set; }
        bool WriteOnly { get; set; }
        bool Active { get; set; }
        IRTItem RTItem { get; set; }
        IProjectEU iEU { get; set; }
        ICondition[] conditions { get; set; }
        IDeviation devFolderData { get; set; }
        ICADataBuffer CircularBuffer { get; }
        double? Period { get; set; }
        DateTime LastSystemValueTimeUTC { get; set; }
        int uid { get; set; }           // if not defined: uid = 0;
        bool NCUpdateType { get; set; } // true for NC Update Type
        bool DiscreteType { get; set; } // true for Discrete Type, false for Analog
        string DiscreteValues { get; set; } // for exanmple: 0=False, 1=True, 2= Manual, 3=Remote
        string WriteAccess { get; set; }
        string UpdateValue { get; set; } // typically this value comes from user by "write" operation

        /// <summary>
        /// Methods
        /// </summary>
        void SetData(double? value, DateTime ts, RTQuality quality);
        void GetData(RTDataDouble outData, bool spanOffset);
        void UpdateProperties(IRTItemData iRTItem);
        bool SetDeviationValue(out string error);
    }

    /// <summary>
    ///  Used in WPF controls
    /// </summary>
    public class ConfigValue3
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string EU { get; set; }
        public bool Valid { get; set; }
        public bool Changed { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
    }

}
