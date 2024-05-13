/* File="USState"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using OzFramework.Primitives;
using OzFramework.Text;
using System;
using System.Collections.Generic;

namespace OzFramework {
    [Serializable]
    public sealed class USState {
        private static readonly List<USState> usStates = new() {
            new() { Name ="Alabama", Abbreviation ="AL" },
            new() { Name ="Alaska", Abbreviation ="AK" },
            new() { Name ="Arizona", Abbreviation ="AZ" },
            new() { Name ="Arkansas", Abbreviation ="AR" },
            new() { Name ="California", Abbreviation ="CA" },
            new() { Name ="Colorado", Abbreviation ="CO" },
            new() { Name ="Connecticut", Abbreviation ="CT" },
            new() { Name ="District of Columbia", Abbreviation ="DC" },
            new() { Name ="Delaware", Abbreviation ="DE" },
            new() { Name ="Florida", Abbreviation ="FL" },
            new() { Name ="Georgia", Abbreviation ="GA" },
            new() { Name ="Hawaii", Abbreviation ="HI" },
            new() { Name ="Idaho", Abbreviation ="ID" },
            new() { Name ="Illinois", Abbreviation ="IL" },
            new() { Name ="Indiana", Abbreviation ="IN" },
            new() { Name ="Iowa", Abbreviation ="IA" },
            new() { Name ="Kansas", Abbreviation ="KS" },
            new() { Name ="Kentucky", Abbreviation ="KY" },
            new() { Name ="Louisiana", Abbreviation ="LA" },
            new() { Name ="Maine", Abbreviation ="ME" },
            new() { Name ="Maryland", Abbreviation ="MD" },
            new() { Name ="Massachusettes", Abbreviation ="MA" },
            new() { Name ="Michigan", Abbreviation ="MI" },
            new() { Name ="Minnesota", Abbreviation ="MN" },
            new() { Name ="Mississippi", Abbreviation ="MS" },
            new() { Name ="Missouri", Abbreviation ="MO" },
            new() { Name ="Montana", Abbreviation ="MT" },
            new() { Name ="Nebraska", Abbreviation ="NE" },
            new() { Name ="Nevada", Abbreviation ="NV" },
            new() { Name ="New Hampshire", Abbreviation ="NH" },
            new() { Name ="New Jersey", Abbreviation ="NJ" },
            new() { Name ="New Mexico", Abbreviation ="NM" },
            new() { Name ="New York", Abbreviation ="NY" },
            new() { Name ="North Carolina", Abbreviation ="NC" },
            new() { Name ="North Dakota", Abbreviation ="ND" },
            new() { Name ="Ohio", Abbreviation ="OH" },
            new() { Name ="Oklahoma", Abbreviation ="OK" },
            new() { Name ="Oregon", Abbreviation ="OR" },
            new() { Name ="Pennsylvania", Abbreviation ="PA" },
            new() { Name ="Rhode Island", Abbreviation ="RI" },
            new() { Name ="South Carolina", Abbreviation ="SC" },
            new() { Name ="South Dakota", Abbreviation ="SD" },
            new() { Name ="Tennessee", Abbreviation ="TN" },
            new() { Name ="Texas", Abbreviation ="TX" },
            new() { Name ="Utah", Abbreviation ="UT" },
            new() { Name ="Vermont", Abbreviation ="VT" },
            new() { Name ="Virginia", Abbreviation ="VA" },
            new() { Name ="Washington", Abbreviation ="WA" },
            new() { Name ="West Virginia", Abbreviation ="WV" },
            new() { Name ="Wisconsin", Abbreviation ="WI" },
            new() { Name ="Wyoming", Abbreviation ="WY" },
        };

        public static List<USState> States => usStates;

        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public override string ToString() => $"{Name} ({Abbreviation})";
        public string ToString(bool useAbbreviation) => useAbbreviation ? Abbreviation : Name;
        public override bool Equals(object obj) {
            var stateObj = obj.As<USState>();
            if (stateObj.IsNull())
                return false;
            return Name.EqualsIgnoreCase(stateObj.Name)
                && Abbreviation.EqualsIgnoreCase(stateObj.Abbreviation);
        }
        public override int GetHashCode() => Name.GetHashCode() + Abbreviation.GetHashCode();

    }
}
