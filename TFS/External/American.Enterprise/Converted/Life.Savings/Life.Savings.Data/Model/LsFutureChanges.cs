using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model
{
    public class LsFutureChanges
    {
        public int DB_Age { get; set; }
        public double DB_Amount { get; set; }
        public int Prem_Age { get; set; }
        public double Prem_Amount { get; set; }
        public int Int_Age { get; set; }
        public double Int_Amount { get; set; }
        public int Opt_Age { get; set; }
        public int Opt_Type { get; set; }
    }
}
