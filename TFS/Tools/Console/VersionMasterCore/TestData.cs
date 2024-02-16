using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionMaster;

namespace VersionMasterCore {
    public class TestData {
        public TestData(ProjectData item) {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Runs = [];
        }
        public ProjectData Item { get; set; }
        public List<TestRun> Runs { get; set; }
        public TestRun LastRun {
            get {
                if (Runs.Any()) {
                    return Runs.OrderBy(x => x.RunDate).Last();
                }
                return null;
            }
        }
    }

    public class TestRun {
        public DateTime RunDate { get; set; }
        public Version StartVersion { get; set; }
        public Version? EndVersion { get; set; }
    }
}
