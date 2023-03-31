﻿using Common.Application.Primitives;
using Common.Application.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Exception {
    public static class Extensions {
        public static async Task<StringBuilder> ProcessDataAsync(this System.Diagnostics.Process proc, int tabStop = 0) {
            await Task.Yield();
            var t1 = new string('\t', tabStop + 1);
            var t2 = new string('\t', tabStop + 2);
            var t3 = new string('\t', tabStop + 3);
            var t4 = new string('\t', tabStop + 4);

            var mod = proc.MainModule;

            var result = new StringBuilder();
            result.AppendLine(t1 + "Process Infomation")
                .AppendLineFormat(t2 + "Id: {0}", proc.Id)
                .AppendLineFormat(t2 + "Name: {0}", proc.ProcessName)
                .AppendLineFormat(t2 + "Handle: {0}", proc.Handle)
                .AppendLineFormat(t2 + "HandleCount: {0}", proc.HandleCount.ToString("#,0"))
                .AppendLineFormat(t2 + "MachineName: {0}", proc.MachineName)
                .AppendLineFormat(t2 + "MainWindowHandle: {0}", proc.MainWindowHandle)
                .AppendLineFormat(t2 + "IsResponding: {0}", proc.Responding)
                .AppendLineFormat(t2 + "SessionId: {0}", proc.SessionId)
                .AppendLineFormat(t2 + "StartTime: {0}", proc.StartTime)
                .AppendLineFormat(t2 + "TotalProcessorTime: {0}", proc.TotalProcessorTime)
                .AppendLineFormat(t2 + "UserProcessorTime: {0}", proc.UserProcessorTime)
                .AppendLine(t2 + "Memory")
                .AppendLineFormat(t3 + "NonpagedMemory: {0}", proc.NonpagedSystemMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "PagedMemory: {0}", proc.PagedMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "PagedSystemMemory: {0}", proc.PagedSystemMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "PeakPagedMemory: {0}", proc.PeakPagedMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "PeakVirtualMemory: {0}", proc.PeakVirtualMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "PeakWorkingSet: {0}", proc.PeakWorkingSet64.ToMbString())
                .AppendLineFormat(t3 + "PrivateMemory: {0}", proc.PrivateMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "VirtualMemory: {0}", proc.VirtualMemorySize64.ToMbString())
                .AppendLineFormat(t3 + "WorkingSet: {0}", proc.WorkingSet64.ToMbString())
                .AppendLine(t2 + "Modules")
                .AppendLineFormat(t3 + "{0}", mod.ModuleName)
                .AppendLineFormat(t4 + "Address: x{0}", mod.BaseAddress.ToString("X"))
                .AppendLineFormat(t4 + "FileName: {0}", mod.FileName)
                .AppendLineFormat(t4 + "Version: {0}", new Version(mod.FileVersionInfo.FileMajorPart, mod.FileVersionInfo.FileMinorPart, mod.FileVersionInfo.FileBuildPart, mod.FileVersionInfo.FilePrivatePart))
                .AppendLineFormat(t4 + "MemorySize: {0}", mod.ModuleMemorySize.ToMbString());
            proc.Modules.Cast<ProcessModule>()
                .Where(x => !x.ModuleName.Equals(mod.ModuleName))
                .ToList()
                .ForEach(x => {
                    result.AppendLineFormat(t3 + "{0}", x.ModuleName)
                        .AppendLineFormat(t4 + "Address: x{0}", x.BaseAddress.ToString("X"))
                        .AppendLineFormat(t4 + "FileName: {0}", x.FileName)
                        .AppendLineFormat(t4 + "Version: {0}", new Version(x.FileVersionInfo.FileMajorPart, x.FileVersionInfo.FileMinorPart, x.FileVersionInfo.FileBuildPart, x.FileVersionInfo.FilePrivatePart))
                        .AppendLineFormat(t4 + "MemorySize: {0}", x.ModuleMemorySize.ToMbString());
                });
            result.AppendLine(t2 + "Threads");
            proc.Threads.Cast<ProcessThread>()
                .ToList()
                .ForEach(x => {
                    result.AppendLineFormat(t3 + "Id: {0}", x.Id)
                        .AppendLineFormat(t4 + "Address: x{0}", x.StartAddress.ToString("X"))
                        .AppendLineFormat(t4 + "StartTime: {0}", x.StartTime)
                        .AppendLineFormat(t4 + "State: {0}", x.ThreadState)
                        .AppendLineFormat(t4 + "TotalProcessorTime: {0}", x.TotalProcessorTime)
                        .AppendLineFormat(t4 + "UserProcessorTime: {0}", x.UserProcessorTime)
                        .AppendLineFormat(t4 + "Priority: {0}", x.PriorityLevel);
                });
            return result;
        }

        public static async Task<StringBuilder> ToStringRecurseAsync(this System.Exception ex, bool isProcessDataIncluded = false) {
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
            if(isProcessDataIncluded) {
                result.Append(await ProcessDataAsync(System.Diagnostics.Process.GetCurrentProcess()));
            }
            return result;
        }
    }
}