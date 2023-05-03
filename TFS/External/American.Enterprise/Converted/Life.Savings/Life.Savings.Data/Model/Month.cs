using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model {
    public class Month : ModelBase {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Days(int year) {
            return new DateTime(year, Id + 1, 1).AddDays(-1).Day;
        }
        public override string ToString() {
            return $"{Name},{Abbreviation}";
        }
    }
}
