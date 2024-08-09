/* File="ApplicationProcess"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Common.Primitives;
using System.Linq;

namespace Common.Windows {
    public class ApplicationProcess {
        public ApplicationProcess() { }

        public ApplicationProcess(string processName) => ProcessName = processName;

        public string ProcessName { get; set; }

        public System.Diagnostics.Process[] GetAll() {
            if (ProcessName.IsNull())
                System.Diagnostics.Process.GetProcesses();
            return System.Diagnostics.Process.GetProcessesByName(ProcessName);
        }

        public System.Diagnostics.Process[] GetAll(string processName) {
            ProcessName = processName;
            return GetAll();
        }

        public string[] GetWindowTitles(int[] ids) => System.Diagnostics.Process.GetProcesses()
            .Where(x => ids.Contains(x.Id))
            .Select(x => x.MainWindowTitle).ToArray();

    }
}
