using System;
using System.Collections.Generic;
using System.ComponentModel;
using GregOsborne.Application.Primitives;

namespace GregOsborne.Application {
    [Serializable]
    public class USState : INotifyPropertyChanged {
        public static List<USState> States = new List<USState> {
        new USState { Name ="Alabama", Abbreviation ="AL" },
        new USState { Name ="Alaska", Abbreviation ="AK" },
        new USState { Name ="Arizona", Abbreviation ="AZ" },
        new USState { Name ="Arkansas", Abbreviation ="AR" },
        new USState { Name ="California", Abbreviation ="CA" },
        new USState { Name ="Colorado", Abbreviation ="CO" },
        new USState { Name ="Connecticut", Abbreviation ="CT" },
        new USState { Name ="District of Columbia", Abbreviation ="DC" },
        new USState { Name ="Delaware", Abbreviation ="DE" },
        new USState { Name ="Florida", Abbreviation ="FL" },
        new USState { Name ="Georgia", Abbreviation ="GA" },
        new USState { Name ="Hawaii", Abbreviation ="HI" },
        new USState { Name ="Idaho", Abbreviation ="ID" },
        new USState { Name ="Illinois", Abbreviation ="IL" },
        new USState { Name ="Indiana", Abbreviation ="IN" },
        new USState { Name ="Iowa", Abbreviation ="IA" },
        new USState { Name ="Kansas", Abbreviation ="KS" },
        new USState { Name ="Kentucky", Abbreviation ="KY" },
        new USState { Name ="Louisiana", Abbreviation ="LA" },
        new USState { Name ="Maine", Abbreviation ="ME" },
        new USState { Name ="Maryland", Abbreviation ="MD" },
        new USState { Name ="Massachusettes", Abbreviation ="MA" },
        new USState { Name ="Michigan", Abbreviation ="MI" },
        new USState { Name ="Minnesota", Abbreviation ="MN" },
        new USState { Name ="Mississippi", Abbreviation ="MS" },
        new USState { Name ="Missouri", Abbreviation ="MO" },
        new USState { Name ="Montana", Abbreviation ="MT" },
        new USState { Name ="Nebraska", Abbreviation ="NE" },
        new USState { Name ="Nevada", Abbreviation ="NV" },
        new USState { Name ="New Hampshire", Abbreviation ="NH" },
        new USState { Name ="New Jersey", Abbreviation ="NJ" },
        new USState { Name ="New Mexico", Abbreviation ="NM" },
        new USState { Name ="New York", Abbreviation ="NY" },
        new USState { Name ="North Carolina", Abbreviation ="NC" },
        new USState { Name ="North Dakota", Abbreviation ="ND" },
        new USState { Name ="Ohio", Abbreviation ="OH" },
        new USState { Name ="Oklahoma", Abbreviation ="OK" },
        new USState { Name ="Oregon", Abbreviation ="OR" },
        new USState { Name ="Pennsylvania", Abbreviation ="PA" },
        new USState { Name ="Rhode Island", Abbreviation ="RI" },
        new USState { Name ="South Carolina", Abbreviation ="SC" },
        new USState { Name ="South Dakota", Abbreviation ="SD" },
        new USState { Name ="Tennessee", Abbreviation ="TN" },
        new USState { Name ="Texas", Abbreviation ="TX" },
        new USState { Name ="Utah", Abbreviation ="UT" },
        new USState { Name ="Vermont", Abbreviation ="VT" },
        new USState { Name ="Virginia", Abbreviation ="VA" },
        new USState { Name ="Washington", Abbreviation ="WA" },
        new USState { Name ="West Virginia", Abbreviation ="WV" },
        new USState { Name ="Wisconsin", Abbreviation ="WI" },
        new USState { Name ="Wyoming", Abbreviation ="WY" },
    };

        private string name = default;
        public string Name {
            get => name;
            set {
                name = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        private string abbreviation = default;
        public string Abbreviation {
            get => abbreviation;
            set {
                abbreviation = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public override string ToString() => $"{Name} ({Abbreviation})";
        public string ToString(bool useAbbreviation) => useAbbreviation ? Abbreviation : Name;
        public override bool Equals(object obj) {
            var stateObj = obj.As<USState>();
            if (stateObj == null)
                return false;
            return Name.Equals(stateObj.Name, StringComparison.OrdinalIgnoreCase)
                && Abbreviation.Equals(stateObj.Abbreviation, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode() => Name.GetHashCode() + Abbreviation.GetHashCode();

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
