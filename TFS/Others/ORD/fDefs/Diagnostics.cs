//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Created by:	Alex Novitskiy
//-------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SNC.OptiRamp.Services.fDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptiRampDiagnostics
    {
        bool GetLastErrors(out string erorMsg, out int errorID);
        IEnumerable<string> GetVersions();
        void GetDiskSpace();
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LogLimit { noLimit, bySize, byDays }

    /// <summary>
    /// 
    /// </summary>
    public enum LogVerbosity {  low, medium, high };

    /// <summary>
    /// 
    /// </summary>
    public interface IOptiRampLog
    {
        bool InitLog(string folderPath, string logName, string header, int daysInLogFolder = 1);
        bool WriteRecord(string msg, string logName, bool useTimeStamp = true);
        bool WriteRecord(string msg, bool useTimeStamp = true);
        bool WriteRecordV(string msg, string logName, bool useTimeStamp = true, LogVerbosity verbosity = LogVerbosity.low);
        LogVerbosity GetCurrentVerbosity(string logName);
        IEnumerable<string> GetLogFiles();
        string DefaultHeaderData(string folderPath);
        void Flush(string LogName);
		string LogName { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IThreadsWatchdog
    {
        void ThreadRegister(int WatchdogSeconds = int.MaxValue);
        void ThreadUnregister();
        void ThreadNudge();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDiskSpace
    {
        string DiskName { get; set; }
        int FreeSpaceGB { get; set; }
        int UsedSpaceGB { get; set; }
        int UsedPercentage { get; set; }
        float IdleTime { get; set; }
        DriveType DriveType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISystemInfo
    {
        string OsName { get; set; }
        int MemoryFreeMB { get; set; }
        int MemoryUsedMB { get; set; }
        int MemoryUsedPercent { get; set; }
        int CpuUsedPercent { get; set; }
        int DiskIOPercent { get; set; }
        IDiskSpace[] DisksInfo { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemDiagnostics
    {
        ISystemInfo GetInfo();
    }
}
