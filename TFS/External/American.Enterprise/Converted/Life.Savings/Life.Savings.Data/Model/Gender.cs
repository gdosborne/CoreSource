using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Savings.Data.Model {
    public class Gender : ModelBase {
        public string Name { get; set; }
        public char Abbreviation { get; set; }
        public override string ToString() {
            return Name;
        }
        public bool IsMale => Abbreviation.Equals('M');
        public bool IsSpecified => !Abbreviation.Equals('U');
    }
}
