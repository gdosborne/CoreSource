using System.Linq;

namespace Common.OzApplication.Windows {
    public class ApplicationProcess {
        public ApplicationProcess() { }

        public ApplicationProcess(string processName) => ProcessName = processName;

        public string ProcessName { get; set; }

        public System.Diagnostics.Process[] GetAll() {
            if (string.IsNullOrEmpty(ProcessName))
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

        //public int[] GetBackgroupProcesses(int[] ids) => System.Diagnostics.Process.GetProcesses()
        //   .Where(x => ids.Contains(x.Id) && !x.HasWindowStyle())
        //   .Select(x => x.Id).ToArray();

    }
}
