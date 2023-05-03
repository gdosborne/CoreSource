//-------------------------------------------------------------------
// (©) 2014 Statistics & Control Inc.  All rights reserved.
// Written by:	Alex Novitskiy
//-------------------------------------------------------------------
//
// Implements SNC Log servises
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SNC.OptiRamp.Services.fDiagnostics {

	/// <summary>
	/// Counters indicating how many calls occured for a period of time.
	/// </summary>
	public class ApplicationCalls {

		#region Public Constructors
		/// <summary>
		/// constructor, initializing the counters;
		/// </summary>
		public ApplicationCalls() {
			Reset();
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Reset counters to zero. No LOCK. no protection from race condition.
		/// </summary>
		public void Reset() {
			for (int i = 0; i < Enum.GetNames(typeof(Checks)).Length; i++)
				data[i] = 0;
		}
		#endregion Public Methods

		#region Public Fields
		/// <summary>
		/// array of counters' names
		/// </summary>
		public static string[] dataTitles = { "OPC calls", "ODBC calls", "Event Archive checks", "Remote Client requests", "Runtime Application calls", "OPC UA Client Cache updates", "VTS Simulation updates" };
		/// <summary>
		/// array of counters
		/// </summary>
		public int[] data = new int[Enum.GetNames(typeof(Checks)).Length];
		#endregion Public Fields

		#region Public Enums
		/// <summary>
		/// enum of counters
		/// </summary>
		public enum Checks {
			OpcCall = 0,
			OdbcCall = 1,
			ArchiveCheck = 2,
			RemoteCall = 3,
			RTAppCall = 4,
			OpcUACacheUpdate = 5,
			VTSSimulation = 6
		};
		#endregion Public Enums
	}

	/// <summary>
	/// Checking and logging memory and application flag status periodically
	/// </summary>
	public class MemoryCheck : IDisposable {

		#region Public Constructors
		/// <summary>
		/// Log and application name used as parameters
		/// </summary>
		/// <param name="log_">    </param>
		/// <param name="appName_"></param>
		public MemoryCheck(OptiRampLog log_, string appName_, ApplicationCalls appCalls_ = null) {
			log = log_;
			appName = appName_;
			appCalls = appCalls_;
			tcb = Check;
			try {
				timer = new Timer(tcb, null, firstCallDelay, timerInterval); // first call in 20 sec
			}
			catch (Exception e) {
				log.WriteRecord("DIAGNOSTICS - Can't create timer: " + e.Message, appName);
				return;
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Disposing the timer. Waiting until callback complete or queue is cleared
		/// </summary>
		public void Dispose() {
			if (timer != null) {
				WaitHandle wait = new AutoResetEvent(false);
				timer.Dispose(wait);
				wait.WaitOne(200);
				timer = null;
				wait.Dispose();
			}
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Logging the amount of memory and some diag params every 20 minutes
		/// </summary>
		/// <param name="status"></param>
		private void Check(object status) {
			try {
				System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
				StringBuilder diagMessage = new StringBuilder(256);
				string separator = "******************************************************************";
				string format = "{0,-10}{1,-35}{2,-20:N0}{3}";
				string startOfLine = "*";
				string endOfLine = startOfLine + Environment.NewLine;

				diagMessage.AppendLine("DIAGNOSTICS:");
				diagMessage.AppendLine(separator);
				diagMessage.AppendFormat(format, startOfLine, "Memory used", System.GC.GetTotalMemory(false), endOfLine);
				if (appCalls != null) {
					for (int i = 0; i < appCalls.data.Length; i++) {
						diagMessage.AppendFormat(format, startOfLine, ApplicationCalls.dataTitles[i], appCalls.data[i], endOfLine);
					}
					appCalls.Reset(); // no lock protection though race coindition may happen
				}
				diagMessage.AppendLine(separator);
				log.WriteRecord(diagMessage.ToString(), appName);
			}
			catch (System.Exception) {
				// nothing we can do here
			}
		}
		#endregion Private Methods

		#region Private Fields
		private const int firstCallDelay = 20000;
		private const int timerInterval = 1200000;
		private ApplicationCalls appCalls = null;
		private string appName;
		/// <summary>
		/// private fields
		/// </summary>
		private OptiRampLog log;
		private TimerCallback tcb = null;
		private Timer timer = null;
		#endregion Private Fields

		// reference to flags 20 mins 20 sec
	}

	// OptiRampLog
	public class OptiRampLog : IOptiRampLog, IDisposable {
		// private members

		#region Public Methods
		/// <summary>
		/// Returns default data about executable module, .net and OS
		/// </summary>
		/// <param name="folderPath"></param>
		/// <returns></returns>
		public string DefaultHeaderData(string folderPath) {
			const string separator = "-----------------------------------------------------";
			System.Text.StringBuilder tRet = new System.Text.StringBuilder(255);

			try {
				var os = Environment.OSVersion;
				var ver = os.Version;

				string[] vNames =
                {
                    "Operating system",
                    "Computer name",
                    "User",
                    "ProcessorCount",
                    "Time zone",
                    "UTC offset",
                    "Culture",
                    "App path",
                    "Log folder",
                    "Time",
                    "TickCount"
                };

				var pTimeZone = TimeZoneInfo.Local;

				string[] vValues =
                {
                    string.Format("{0} ({1})", os.VersionString, ver.ToString()),
                    Environment.MachineName,
                    Environment.UserName,
                    Environment.ProcessorCount.ToString(),
                    pTimeZone.StandardName,
                    pTimeZone.GetUtcOffset(DateTime.Now).ToString(),
                    Thread.CurrentThread.CurrentCulture.EnglishName,
                    Get_App_Path(),
                    folderPath,
                    DateTime.Now.ToString(OptiRampLogFile.dtHeaderFormatter),
                    Environment.TickCount.ToString("N0")
                };

				const int iPad_Size = 20;
				tRet.AppendLine();
				tRet.AppendLine(separator);
				for (int I = 0; I < vNames.Length; ++I) {
					tRet.Append(vNames[I].PadLeft(iPad_Size));
					tRet.Append(": ");
					tRet.AppendLine(vValues[I]);
				}

				tRet.AppendLine(separator);

				string sPrefix = "Assemblies";
				var tCovered_Assemblies = new System.Collections.Generic.List<string>();
				var tAssemblies_to_DiveInto = new System.Collections.Generic.List<System.Reflection.Assembly>();

				foreach (System.Reflection.Assembly pIter in System.AppDomain.CurrentDomain.GetAssemblies()) {
					string sLine = pIter.FullName;
					try {
						var vBuf = pIter.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false);
						if (vBuf != null && vBuf.Length > 0) {
							var pAttr = vBuf[0] as System.Reflection.AssemblyProductAttribute;
							string sCompany = (pAttr != null) ? pAttr.Product : null;
							if (sCompany != null && sCompany.StartsWith("Microsoft")) {
								continue;
							}
						} // if ( vBuf != nullptr .....
						tCovered_Assemblies.Add(sLine);
						tAssemblies_to_DiveInto.Add(pIter);
					}
					catch (System.Exception pErr) {
						sLine += "; " + pErr.Message;
					}

					tRet.Append(sPrefix.PadLeft(iPad_Size));
					tRet.Append(": ");
					tRet.AppendLine(sLine);
					sPrefix = string.Empty;
				} // for each (System::Reflection::Assembly ^ pIter  ......

				if (tAssemblies_to_DiveInto.Count > 0) {
					string[] vExcludePrefix = { "System", "mscorlib", "WindowsBase", "Microsoft" };
					int iCount_Lines = tCovered_Assemblies.Count;
					foreach (var tAssembly in tAssemblies_to_DiveInto) {
						foreach (var tAssemblyRef in tAssembly.GetReferencedAssemblies()) {
							string sLine = tAssemblyRef.ToString();
							if (tCovered_Assemblies.Contains(sLine)) {
								continue;
							}
							bool bHas_Exclude_Prefix = false;
							foreach (string tIterS in vExcludePrefix) {
								if (sLine.StartsWith(tIterS)) {
									bHas_Exclude_Prefix = true;
									break;
								}
							}
							if (bHas_Exclude_Prefix) {
								continue;
							}

							if (iCount_Lines == tCovered_Assemblies.Count) {
								tRet.AppendLine(separator);
							}
							tRet.Append(string.Empty.PadLeft(iPad_Size));
							tRet.Append(": ");
							tRet.AppendLine(sLine);
							tRet.Append(string.Empty.PadLeft(iPad_Size + 4));
							tRet.AppendLine(tAssembly.GetName().Name);

							tCovered_Assemblies.Add(sLine);
						}
					}
				} // if (tAssemblies_to_DiveInto.Count > 0) ....
			}
			catch (Exception err) {
				tRet.AppendLine(err.Message);
			}
			tRet.AppendLine(separator);

			return tRet.ToString();
		}
		// destructor flush the buffers delete (dispose) timer dispose muitex - no need
		public void Dispose() {
			if (timerWriteToFiles != null) {
				WaitHandle wait = new AutoResetEvent(false);
				timerWriteToFiles.Dispose(wait);
				wait.WaitOne(200);
				timerWriteToFiles = null;
				wait.Dispose();
			}
			// flush records to files
			WritingToFiles(new Object());
		}
		/// <summary>
		/// Force to write records to disk
		/// </summary>
		/// <param name="logName"></param>
		public void Flush(string logName) {
			lock (criticalSection) {
				try {
					OptiRampLogFile logFile = logFiles.First(x => x.logName == logName);
					IEnumerable<string> records_copy = logFile.ResetRecords();  //gets records from buffer
					if (records_copy == null)
						return; //returns if no records found in buffer
					logFile.WriteRecordsToFile(records_copy.ToArray()); //writes records to logfile
				}
				catch (Exception ex) {
					error = ex.Message;
				}
			}
		}
		public LogVerbosity GetCurrentVerbosity(string logName) {
			return LogVerbosity.low;
		}
		/// <summary>
		/// Returns all names of files currently used in service
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetLogFiles() {
			int i = 0;
			string[] retArray = new string[logFiles.Count];
			lock (criticalSection) {
				foreach (OptiRampLogFile logFile in logFiles) {
					retArray[i++] = logFile.logName;
				}
			}
			return retArray;
		}
		// interface
		public bool InitLog(string folderPath, string logName, string logHeader, int daysInLogFolder) {
			error = string.Empty;
			if (folderPath == null || folderPath == string.Empty) {
				error = "Folder path is not specified";
				return false;
			}

			if (string.IsNullOrEmpty(logName))
				logName = OptiRampLogFile.noName;
			LogName = LogName;
			lock (criticalSection) // protect THIS instance accessing from different threads: 1) Timer will be created just once 2) Adding file to list is safe.
            {
				if (timerWriteToFiles == null) {
					tcb = WritingToFiles;
					try {
						timerWriteToFiles = new Timer(tcb, 1, 2000, 2000);
					}
					catch (Exception e) {
						error = "Can't init log timer: " + e.Message;
						return false;  // exit from lock
					}
				}

				foreach (OptiRampLogFile logFile in logFiles) {
					if ((logFile.logName == logName))
						return true;
				}

				try {
					OptiRampLogFile newLogFile = new OptiRampLogFile(logName, out error); // if logName is empty - it will create 'NoName' file
					if (!string.IsNullOrEmpty(error))
						return false;

					newLogFile.folderPath = folderPath;
					newLogFile.logHeader = logHeader;
					newLogFile.limitDays = validateDays(daysInLogFolder);

					logFiles.Add(newLogFile);
				}
				catch (Exception ex) {
					error = "Can't make new log object: " + ex.Message;
					return false;
				}
			} // exit from lock
			return true;
		}
		/// <summary>
		/// MAIN function of service. Adding a text to buffer in non-blocking way. All text in buffer will be written to file in separate thread.
		/// </summary>
		/// <param name="msg">         </param>
		/// <param name="logName">     </param>
		/// <param name="useTimeStamp"></param>
		/// <returns></returns>
		public bool WriteRecord(string msg, string logName, bool useTimeStamp = true) {
			error = string.Empty;
			try {
				lock (criticalSection) // if another thread try to add a log file, enumerator is safe by this lock
                {
					foreach (OptiRampLogFile logFile in logFiles) {
						if ((logFile.logName == logName) || (logFile.logName == string.Empty && logName == string.Empty)) {
							logFile.AddRecordToQueue(msg, useTimeStamp);  // lock is inside logFile to protect from Timer thread
							return true;
						}
					}
				}
			}
			catch (Exception ex) // if logFiles list empty or changed in another thread by adding a new log file the enumerator will throw excpetion.
			{
				error = "Log is not intilized. " + ex.Message;
			}
			return false;
		}
		/// <summary>
		/// Writes the record.
		/// </summary>
		/// <param name="msg">         The MSG.</param>
		/// <param name="useTimeStamp">if set to <c>true</c> [use time stamp].</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public bool WriteRecord(string msg, bool useTimeStamp = true) {
			return WriteRecord(msg, LogName, useTimeStamp);
		}
		public bool WriteRecordV(string msg, string logName, bool useTimeStamp = true, LogVerbosity verbosity = LogVerbosity.low) {
			return true;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// aux function to get app path
		/// </summary>
		/// <returns></returns>
		private static string Get_App_Path() {
			try {
				var pAssembly = System.Reflection.Assembly.GetEntryAssembly();
				if (pAssembly == null) {
					pAssembly = System.Reflection.Assembly.GetExecutingAssembly();
				}
				return (pAssembly == null) ? string.Empty : pAssembly.Location;
			}
			catch (System.Exception pErr) {
				return pErr.Message;
			}
		}
		/// <summary>
		/// Days to keep log file can't be great than 90 or less than 1.
		/// </summary>
		/// <param name="days"></param>
		/// <returns></returns>
		private int validateDays(int days) {
			if (days >= 1 && days <= 60)
				return days; // valid number
			else
				return 1; // default 1 last day
		}
		// implementation
		private void WritingToFiles(Object status) {
			// TimerCallback to write To files Called every 2 secs on a different thread every time obtained form .net thread pool

			// wait on mutex until previous thread completed mutex- because access to the same log files can be from different processes

			// if Mutex is non sgnalled fo 1000 msec - skip for another timer tic

			try  // use TRY/CATCH if ReleaseMutex or other operation throws an exception
			{
				lock (criticalSection) // the logFiles list enmumerator protected by this lock from adding another log file in InitLog() function in another thread.
                {
					foreach (OptiRampLogFile logFile in logFiles) {
						try {
							if (logFile.logMtx != null && logFile.logMtx.WaitOne(1000)) {
								IEnumerable<string> records_copy = logFile.ResetRecords();  // copy a reference to records and reset, lock is inside logFile for main thread protection
								if (records_copy == null)
									continue;
								logFile.WriteRecordsToFile(records_copy.ToArray());
								logFile.CleanUp();              // if there is something to delete - delete
							}
						}
						catch (Exception ex) {
							error = ex.Message;
						}
						finally {
							logFile.logMtx.ReleaseMutex();
						}
					}
				}
			}
			catch (Exception ex) {
				error = ex.Message;
			}
		}
		#endregion Private Methods

		#region Private Fields
		private object criticalSection = new object();
		private string error = string.Empty;
		private List<OptiRampLogFile> logFiles = new List<OptiRampLogFile>();
		private TimerCallback tcb = null;
		private Timer timerWriteToFiles = null;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets the name of the log.
		/// </summary>
		/// <value>The name of the log.</value>
		public string LogName {
			get;
			private set;
		}
		#endregion Public Properties
	}

	/// <summary>
	/// Class used to get system info
	/// </summary>
	public class SystemDiagnostics : ISystemDiagnostics, IDisposable {

		#region Public Constructors
		/// <summary>
		/// constructor
		/// </summary>
		public SystemDiagnostics() {
			tcb = Check;
			try {
				cpuCounter.NextValue(); // Try to get first CPU counter value that is = 0. Next timer cycle some real sample value will be obtained.
				string physicalDiskCategory = "PhysicalDisk";
				PerformanceCounterCategory perfCategory = new PerformanceCounterCategory(physicalDiskCategory);
				string[] instanceNames = perfCategory.GetInstanceNames(); // get all instances of disk category
				DriveInfo[] drives = DriveInfo.GetDrives();
				foreach (DriveInfo di in drives) {
					if (di.DriveType == DriveType.Fixed && di.IsReady) {
						// remember the name of fixed disk
						fixedDisksNames.Add(di.Name);

						// create performance counter for this disk
						string driveName = di.Name.Remove(di.Name.LastIndexOf('\\'), 1); // c: or d: etc...
						string instanceName = instanceNames.FirstOrDefault(s => s.IndexOf(driveName) > 0); // if it is in the list - create counter
						if (!string.IsNullOrEmpty(instanceName)) {
							PerformanceCounter dCounter = new PerformanceCounter(physicalDiskCategory, "% Idle Time", instanceName);
							diskIdleCounters.Add(dCounter);
						}
					}
				}
				timerToGetInfo = new Timer(tcb, 1, 2000, 2000); // start timer
			}
			catch (Exception ex) {
				error = ex.Message;
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Disposing the timer. Waiting until callback complete or queue is cleared
		/// </summary>
		public void Dispose() {
			if (timerToGetInfo != null) {
				WaitHandle wait = new AutoResetEvent(false);
				timerToGetInfo.Dispose(wait);
				wait.WaitOne(200);
				timerToGetInfo = null;
				wait.Dispose();
			}
		}
		/// <summary>
		/// returns systeminfo if available
		/// </summary>
		/// <returns></returns>
		public ISystemInfo GetInfo() {
			if (notReady)
				GetSystemInfo();  // first time get system info.
			return info;                    // updated by timer.
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Gets the name of operating system being used on computer.
		/// </summary>
		/// <returns></returns>
		private static string GetOSFriendlyName() {
			string result = string.Empty;
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
			foreach (ManagementObject os in searcher.Get()) {
				result = os["Caption"].ToString();
				break;
			}
			return result;
		}
		/// <summary>
		/// Used to retreive info about current usage of physical and virtual memory.
		/// </summary>
		/// <param name="lpBuffer"></param>
		/// <returns></returns>
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);
		/// <summary>
		/// retreives system info, called by timercallback.
		/// </summary>
		/// <param name="status"></param>
		private void Check(Object status) {
			if (busy)
				return; // prevent timer to enter again until previous call is complete
			busy = true;

			try {
				GetSystemInfo();
			}
			catch (Exception) {
				// will try next time to get the info. Exception should not happen under normal conditions.
			}
			finally {
				busy = false;
			}
		}
		/// <summary>
		/// stores system performance values in systeminfo class.
		/// </summary>
		private void GetSystemInfo() {
			string error;
			long constMB = 1024 * 1024; // calculated by compiler, not by run time
			long constGB = 1024 * constMB;
			try {
				// CPU Performance Counter
				if (notReady) {
					cpuCounter.NextValue(); // get CPU first time
					Thread.Sleep(100);
				}
				info.CpuUsedPercent = Convert.ToInt32(cpuCounter.NextValue());

				// Memory Performance Counter
				ramCounter.NextValue();
				float freeMemory = ramCounter.NextValue();
				ulong installedMemory = 0;
				MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
				if (GlobalMemoryStatusEx(memStatus))
					installedMemory = memStatus.ullTotalPhys;
				float totalMemory = ((float)installedMemory / constMB);
				info.MemoryFreeMB = (int)freeMemory;
				info.MemoryUsedMB = (int)(totalMemory - freeMemory); // can be interrupted at this point by a client thread. Free/Used memory ratio may be not consistent.
				info.MemoryUsedPercent = (int)(100 - (100 * freeMemory / totalMemory));

				// OS Name
				info.OsName = GetOSFriendlyName();

				// Disk usage info
				List<DiskSpace> dsl = new List<DiskSpace>(); // list of disk info to return
				DriveInfo[] drives = DriveInfo.GetDrives();
				foreach (DriveInfo di in drives) {
					if (fixedDisksNames.Contains(di.Name)) // if name of drive is for hard disk and ready
                    {
						DiskSpace ds = new DiskSpace(); // create disk info for this drive
						ds.DiskName = di.Name;
						ds.DriveType = DriveType.Fixed;
						ds.FreeSpaceGB = (int)(di.TotalFreeSpace / constGB);
						ds.UsedSpaceGB = (int)((di.TotalSize - di.TotalFreeSpace) / constGB);
						ds.UsedPercentage = (int)(100 * (di.TotalSize - di.TotalFreeSpace) / di.TotalSize);
						string driveName = di.Name.Remove(di.Name.LastIndexOf('\\'), 1);
						PerformanceCounter pc = diskIdleCounters.Find(x => x.InstanceName.Contains(driveName)); // find Performance counter in our list
						if (pc != null) {
							if (notReady)
								ds.IdleTime = 100;
							else
								ds.IdleTime = (int)pc.NextValue(); // get idle of I/O operation
						}
						dsl.Add(ds);
					}
				}
				info.DisksInfo = dsl.ToArray(); // every timer we replaced the disk info by new one. Thread safe, replaceing pointer in atomic operation.
			}
			catch (Exception ex) {
				error = ex.Message;
			}
			finally {
				notReady = false; // whatever happens the info data is used next time
			}
		}
		#endregion Private Methods

		#region Private Fields
		private bool busy = false;
		private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		private List<PerformanceCounter> diskIdleCounters = new List<PerformanceCounter>();
		// timer entry protection
		private string error;
		private List<string> fixedDisksNames = new List<string>();
		/// <summary>
		/// private fields
		/// </summary>
		private SystemInfo info = new SystemInfo(); // system info that filled out by timer
		private bool notReady = true;
		// list of disk performance couinters counter for CPU
		private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
		private TimerCallback tcb = null;
		private Timer timerToGetInfo = null;
		#endregion Private Fields

		// names only for devices that are hard disks

		// counter for memory

		#region Public Properties
		// first time entry protection if construction is failed that the reason
		/// <summary>
		/// A reason if construction of class was failed
		/// </summary>
		public string errorConstruction {
			get {
				return error;
			}
		}
		#endregion Public Properties

		#region Private Classes

		/// <summary>
		/// class represents information about the current state of physical and virtual memory.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private class MEMORYSTATUSEX {
			public uint dwLength;
			public uint dwMemoryLoad;
			public ulong ullTotalPhys;
			public ulong ullAvailPhys;
			public ulong ullTotalPageFile;
			public ulong ullAvailPageFile;
			public ulong ullTotalVirtual;
			public ulong ullAvailVirtual;
			public ulong ullAvailExtendedVirtual;
			public MEMORYSTATUSEX() {
				this.dwLength = (uint)Marshal.SizeOf(this);
			}
		}

		#endregion Private Classes
	}

	/// <summary>
	/// Class represents various information about computer
	/// </summary>
	public class SystemInfo : ISystemInfo {

		#region Private Fields
		private int cpuUsedPercent;
		private int diskIOPercent;
		private IDiskSpace[] disksInfo;
		private int memoryFreeMB;
		private int memoryUsedMB;
		private int memoryUsedPercent;
		private string osName;
		#endregion Private Fields

		#region Public Properties
		public int CpuUsedPercent {
			get {
				return cpuUsedPercent;
			}
			set {
				cpuUsedPercent = value;
			}
		}
		public int DiskIOPercent {
			get {
				return diskIOPercent;
			}
			set {
				diskIOPercent = value;
			}
		}
		public IDiskSpace[] DisksInfo {
			get {
				return disksInfo;
			}
			set {
				disksInfo = value;
			}
		}
		public int MemoryFreeMB {
			get {
				return memoryFreeMB;
			}
			set {
				memoryFreeMB = value;
			}
		}
		public int MemoryUsedMB {
			get {
				return memoryUsedMB;
			}
			set {
				memoryUsedMB = value;
			}
		}
		public int MemoryUsedPercent {
			get {
				return memoryUsedPercent;
			}
			set {
				memoryUsedPercent = value;
			}
		}
		public string OsName {
			get {
				return osName;
			}
			set {
				osName = value;
			}
		}
		#endregion Public Properties
	}

	/// <summary>
	/// Class represents disk space, both free and used space.
	/// </summary>
	internal class DiskSpace : IDiskSpace {

		#region Private Fields
		private string diskName;
		private DriveType driveType;
		private int freeSpaceGB;
		private float idleTime;
		private int usedPercentage;
		private int usedSpaceGB;
		#endregion Private Fields

		#region Public Properties
		public string DiskName {
			get {
				return diskName;
			}
			set {
				diskName = value;
			}
		}
		public DriveType DriveType {
			get {
				return driveType;
			}
			set {
				driveType = value;
			}
		}
		public int FreeSpaceGB {
			get {
				return freeSpaceGB;
			}
			set {
				freeSpaceGB = value;
			}
		}
		public float IdleTime {
			get {
				return idleTime;
			}
			set {
				idleTime = value;
			}
		}
		public int UsedPercentage {
			get {
				return usedPercentage;
			}
			set {
				usedPercentage = value;
			}
		}
		public int UsedSpaceGB {
			get {
				return usedSpaceGB;
			}
			set {
				usedSpaceGB = value;
			}
		}
		#endregion Public Properties
	}

	// OptiRampLogFile
	internal class OptiRampLogFile {

		#region Public Constructors
		// public functions
		/// <summary>
		/// constructor creates the mutex with name of Log
		/// </summary>
		/// <param name="name"></param>
		public OptiRampLogFile(string name, out string error) {
			error = string.Empty;
			try {
				logName = string.IsNullOrEmpty(name) ? noName : name;
				logMtx = new Mutex(false, logName); // Mutex object is created once per log file for the process. This mutex object will be deleted when process is ended. Other processes still can get access to the mutex.
			}
			catch (Exception ex) {
				logMtx = null;
				error = ex.Message;
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// </summary>
		/// <param name="record">      </param>
		/// <param name="useTimeStamp"></param>
		public void AddRecordToQueue(string record, bool useTimeStamp = true) {
			ChangeRecords(LogRecords.add, record, useTimeStamp);
		}
		/// <summary>
		/// Deletes files after two days. <remarks>Shall use the number of days as input parameter</remarks>
		/// </summary>
		public void CleanUp() {
			try {
				List<string> filesToKeep = new List<string>(); // list of files to keep
				for (int daySubtract = 0; daySubtract < limitDays; daySubtract++) {
					filesToKeep.Add(DateTime.Now.Subtract(new TimeSpan(daySubtract, 0, 0, 0)).ToString(dtFileFormatter));
				}

				string[] filesToDelete = Directory.GetFiles(folderPath, "*" + logName + ".log"); // all files with logName
				foreach (string fileDel in filesToDelete) // delete files except files to keep
                {
					bool keep = false;
					foreach (string fileKeep in filesToKeep) {
						if (fileDel.Contains(fileKeep)) // keep, no deleting
                        {
							keep = true;
							break;
						}
					}
					if (keep)
						continue; // check next one
					else
						File.Delete(fileDel);
				}
			}
			catch { // do nothing
			}
		}
		/// <summary>
		/// not used, reserved
		/// </summary>
		public void Init() {
		}
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> ResetRecords() {
			return ChangeRecords(LogRecords.reset);
		}
		/// <summary>
		/// </summary>
		/// <param name="records"></param>
		public void WriteRecordsToFile(string[] records) {
			string filePath = Path.Combine(folderPath, GetLogFileName());
			bool noHeader = (File.Exists(filePath) == true) ? true : false;
			using (StreamWriter logfile = new StreamWriter(filePath, true)) {
				if (noHeader == false)
					logfile.WriteLine(ReplaceTime(logHeader));
				foreach (string rec in records) {
					logfile.WriteLine(rec);
				}
			}
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// </summary>
		/// <param name="operation">   </param>
		/// <param name="record">      </param>
		/// <param name="useTimeStamp"></param>
		/// <returns></returns>
		private IEnumerable<string> ChangeRecords(LogRecords operation, string record = null, bool useTimeStamp = true) {
			List<string> records_copy = null;
			List<string> records_empty = null;

			if (operation == LogRecords.reset) {
				if (recordsQueue.Count == 0)
					return null;
				records_empty = new List<string>();
			}
			lock (criticalSection) {
				switch (operation) {
					case LogRecords.add:
						string msg = string.Empty;
						if (useTimeStamp == true) // add timestamp here
                        {
							msg = DateTime.Now.ToString("HH:mm:ss") + '.' + DateTime.Now.Millisecond.ToString("d3") + ' ';
						}
						msg += record;
						recordsQueue.Add(msg);
						break;
					case LogRecords.reset:
						records_copy = recordsQueue;         // rerturn a copy of 'records' reference
						recordsQueue = records_empty;
						break; // assign 'records' to new empty list
				}
			}
			return records_copy;
		}
		/// <summary>
		/// </summary>
		/// <returns></returns>
		private string GetLogFileName() {
			// 2014-07-04-TrendService1-1.log
			string current = DateTime.Now.ToString(dtFileFormatter);
			return string.Format("{0:yyyy-MM-dd}-{1}.log", DateTime.Now, logName);
		}
		/// <summary>
		/// </summary>
		/// <param name="header"></param>
		/// <returns></returns>
		private string ReplaceTime(string header) {
			// replace time by current time between < > it will happen even first time a header is created

			int startTimeIndex = header.IndexOf("Time: <");
			if (startTimeIndex >= 0) {
				startTimeIndex += 7; // skip "Time: " and point to symbol after "<";
				int endTimeIndex = header.IndexOf('>', startTimeIndex - 1);
				if (endTimeIndex - startTimeIndex == dtHeaderFormatter.Length)   // looking for last ">" in "<2014-07-08 17:16:07>" "yyyy-MM-dd HH:mm:ss" = 19 symbols
                {
					string oldTime = header.Substring(startTimeIndex, dtHeaderFormatter.Length);
					string nowTime = DateTime.Now.ToString(dtHeaderFormatter);
					return header.Replace(oldTime, nowTime);
				}
			}
			return header;
		}
		#endregion Private Methods

		#region Public Fields
		public const string dtHeaderFormatter = "yyyy-MM-dd HH:mm:ss";
		/// <summary>
		/// Thi string is used if name for log is empty
		/// </summary>
		public const string noName = "NoName";

		/// <summary>
		/// public fields
		/// </summary>
		public string logHeader;
		public Mutex logMtx;
		#endregion Public Fields

		#region Private Fields
		/// <summary>
		/// private fileds
		/// </summary>
		private const string dtFileFormatter = "yyyy-MM-dd";
		// private members
		private object criticalSection = new object();
		// mutex handle is created for specific name log file name and per process. It is destroyed when process is exited. If process during exit had the mutex ownershin, the other processes will see the abandoned state for this mutex, but can get the ownership ok.
		private List<string> recordsQueue = new List<string>();
		#endregion Private Fields

		#region Private Enums
		// records to write to file implementation
		private enum LogRecords {
			add,
			reset
		}
		#endregion Private Enums

		#region Public Properties
		public List<string> filePaths {
			get;
			set;
		}
		// for example "c:\...\2014-07-04-TrendService1.log", "c:\...\2014-07-05-TrendService1.log" etc...
		public string folderPath {
			get;
			set;
		}
		// for example "c:\adcs\log"
		public int limitDays {
			get;
			set;
		}
		/// <summary>
		/// Properties
		/// </summary>
		public string logName {
			get;
			private set;
		}
		#endregion Public Properties

		// for example "TrendService" how many days keep log files in the log folder
	}
}