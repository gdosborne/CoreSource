using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
    public class LsClientParameters : ParameterBase
    {
        public int Option { get; set; }
        public int Wpd { get; set; }
        public long Gpo { get; set; }
        public long AddFace { get; set; }
        public int AddFaceStart { get; set; }
        public int AddFaceEnd { get; set; }
        public double CurrRate { get; set; }
        public string Mode { get; set; }
        public double PlannedPrem { get; set; }
        public int YearsToPay { get; set; }
        public double LumpSum { get; set; }
        public int SpouseRider { get; set; }
        public int ChildRider { get; set; }
        public IList<int> InsuredRider { get; set; }
        public int FutureChanges { get; set; }
        public int FutureWd { get; set; }
        public IList<int> HighAges { get; set; }

        public void Clear()
        {
            Option = 0;
            Wpd = 0;
            Gpo = 0;
            AddFace = 0;
            AddFaceStart = 0;
            AddFaceEnd = 0;
            CurrRate = 0.0;
            Mode = string.Empty;
            PlannedPrem = 0.0;
            YearsToPay = 0;
            LumpSum = 0.0;
            SpouseRider = 0;
            ChildRider = 0;
            InsuredRider.Clear();
            FutureChanges = 0;
            FutureWd = 0;
            HighAges.Clear();
        }
    }
}
