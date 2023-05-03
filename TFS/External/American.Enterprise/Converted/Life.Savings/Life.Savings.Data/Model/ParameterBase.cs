using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model {
    public class ParameterBase : ModelBase {
        public string ClientName { get; set; }
        public int IssueAge { get; set; }
        public string SexCode { get; set; }
        public long FaceAmt { get; set; }
        public string State { get; set; }
        public int IllusType { get; set; }
        public int PrintPages { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int COLA { get; set; }
        public int PlanCode { get; set; }
        public int SubStd { get; set; }
    }
}
