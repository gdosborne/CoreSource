using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Theme {
    public delegate void ApplyPropertyHandler(object sender, ApplyPropertyEventArgs e);

    public class ApplyPropertyEventArgs : EventArgs {
        public enum ElementTypes {
            SolidColorBrush,
            Size
        }
        public ApplyPropertyEventArgs(string name, ElementTypes elementType, object value) {
            Name = name;
            Value = value;
            ElementType = elementType;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
        public ElementTypes ElementType { get; private set; }


    }
}
