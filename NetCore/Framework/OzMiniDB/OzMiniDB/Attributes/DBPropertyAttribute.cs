using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static OzMiniDB.Items.Field;

namespace OzMiniDB.Attributes {
    public class DBPropertyAttribute : Attribute {
        public DBPropertyAttribute(string name, DBDataType dataType = DBDataType.Note, int length = 0, bool isRequired = false) {
            Name = name;
            DataType = dataType;
            Length = length;
            IsRequired = isRequired;
        }

        public string Name { get; private set; }
        public DBDataType DataType { get; private set; }
        public int Length { get; private set; }
        public bool IsRequired { get; private set; }


    }
}
