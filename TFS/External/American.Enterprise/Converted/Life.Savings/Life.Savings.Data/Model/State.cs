using System;
using System.Collections.Generic;

namespace Life.Savings.Data.Model {
    public class State : ModelBase {
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public override string ToString() {
            return Abbreviation;
        }
    }
}