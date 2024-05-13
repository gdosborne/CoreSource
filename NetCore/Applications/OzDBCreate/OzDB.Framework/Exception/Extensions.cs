namespace OzDB.Application.Exception {
	using System;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Text;
	using OzDB.Application.Primitives;
	using OzDB.Application.Text;

	public static class Extensions {
		public static StringBuilder ProcessData(this System.Diagnostics.Process proc) {
			const string t1 = "\t";
			const string t2 = "\t\t";
			const string t3 = "\t\t\t";
			const string t4 = "\t\t\t\t";

			var result = new StringBuilder();
			result.AppendLine(t1 + "Process Infomation");
			result.AppendLineFormat(t2 + "Id: {0}", proc.Id);
			result.AppendLineFormat(t2 + "Name: {0}", proc.ProcessName);
			result.AppendLineFormat(t2 + "Handle: {0}", proc.Handle);
			result.AppendLineFormat(t2 + "HandleCount: {0}", proc.HandleCount.ToString("#,0"));
			result.AppendLineFormat(t2 + "MachineName: {0}", proc.MachineName);
			result.AppendLineFormat(t2 + "MainWindowHandle: {0}", proc.MainWindowHandle);
			result.AppendLineFormat(t2 + "IsResponding: {0}", proc.Responding);
			result.AppendLineFormat(t2 + "SessionId: {0}", proc.SessionId);
			result.AppendLineFormat(t2 + "StartTime: {0}", proc.StartTime);
			result.AppendLineFormat(t2 + "TotalProcessorTime: {0}", proc.TotalProcessorTime);
			result.AppendLineFormat(t2 + "UserProcessorTime: {0}", proc.UserProcessorTime);
			result.AppendLine(t2 + "StartInfo");
			result.AppendLineFormat(t3 + "Arguments: {0}", proc.StartInfo.Arguments);
			result.AppendLineFormat(t3 + "FileName: {0}", proc.StartInfo.FileName);
			result.AppendLineFormat(t3 + "WorkingDirectory: {0}", proc.StartInfo.WorkingDirectory);
			result.AppendLine(t2 + "Memory");
			result.AppendLineFormat(t3 + "NonpagedMemory: {0}", proc.NonpagedSystemMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "PagedMemory: {0}", proc.PagedMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "PagedSystemMemory: {0}", proc.PagedSystemMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "PeakPagedMemory: {0}", proc.PeakPagedMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "PeakVirtualMemory: {0}", proc.PeakVirtualMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "PeakWorkingSet: {0}", proc.PeakWorkingSet64.ToMbString());
			result.AppendLineFormat(t3 + "PrivateMemory: {0}", proc.PrivateMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "VirtualMemory: {0}", proc.VirtualMemorySize64.ToMbString());
			result.AppendLineFormat(t3 + "WorkingSet: {0}", proc.WorkingSet64.ToMbString());
			result.AppendLine(t2 + "Modules");
			var mod = proc.MainModule;
			result.AppendLineFormat(t3 + "{0}", mod.ModuleName);
			result.AppendLineFormat(t4 + "Address: x{0}", mod.BaseAddress.ToString("X"));
			result.AppendLineFormat(t4 + "FileName: {0}", mod.FileName);
			result.AppendLineFormat(t4 + "Version: {0}", new Version(mod.FileVersionInfo.FileMajorPart, mod.FileVersionInfo.FileMinorPart, mod.FileVersionInfo.FileBuildPart, mod.FileVersionInfo.FilePrivatePart));
			result.AppendLineFormat(t4 + "MemorySize: {0}", mod.ModuleMemorySize.ToMbString());
			proc.Modules.Cast<ProcessModule>()
				.Where(x => !x.ModuleName.Equals(mod.ModuleName))
				.ToList()
				.ForEach(x => {
					result.AppendLineFormat(t3 + "{0}", x.ModuleName);
					result.AppendLineFormat(t4 + "Address: x{0}", x.BaseAddress.ToString("X"));
					result.AppendLineFormat(t4 + "FileName: {0}", x.FileName);
					result.AppendLineFormat(t4 + "Version: {0}", new Version(x.FileVersionInfo.FileMajorPart, x.FileVersionInfo.FileMinorPart, x.FileVersionInfo.FileBuildPart, x.FileVersionInfo.FilePrivatePart));
					result.AppendLineFormat(t4 + "MemorySize: {0}", x.ModuleMemorySize.ToMbString());
				});
			result.AppendLine(t2 + "Threads");
			proc.Threads.Cast<ProcessThread>()
				.ToList()
				.ForEach(x => {
					result.AppendLineFormat(t3 + "Id: {0}", x.Id);
					result.AppendLineFormat(t4 + "Address: x{0}", x.StartAddress.ToString("X"));
					result.AppendLineFormat(t4 + "StartTime: {0}", x.StartTime);
					result.AppendLineFormat(t4 + "State: {0}", x.ThreadState);
					result.AppendLineFormat(t4 + "TotalProcessorTime: {0}", x.TotalProcessorTime);
					result.AppendLineFormat(t4 + "UserProcessorTime: {0}", x.UserProcessorTime);
					result.AppendLineFormat(t4 + "Priority: {0}", x.PriorityLevel);
				});
			return result;
		}

		public static StringBuilder ToStringRecurse(this System.Exception ex) {
			var result = new StringBuilder();
			result.AppendLine("Exception " + new string('=', 40));
			var tabLevel = 1;
			result.AppendLineFormat(new string('\t', tabLevel) + "Type: {0}", ex.GetType().Name);
			using (var sr = new StringReader(ex.ToString())) {
				result.AppendLine(new string('\t', tabLevel) + sr.ReadLine());
			}
			while (ex.InnerException != null) {
				tabLevel++;
				ex = ex.InnerException;
				result.AppendLineFormat(new string('\t', tabLevel) + "Type: {0}", ex.GetType().Name);
				using (var sr = new StringReader(ex.Message.ToString())) {
					result.AppendLine(new string('\t', tabLevel) + sr.ReadLine());
				}
			}
			return result;
		}
	}
}