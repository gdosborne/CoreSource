using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.Controls.Configuration {
    public class DocumentTypeTypeConverter : TypeConverter {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            if (!(value is string)) return base.ConvertFrom(context, culture, value);
            var valString = (string) value;
            switch (valString) {
                case "Text":
                    return Enumerations.DocumentTypes.Text;
                case "Xml":
                    return Enumerations.DocumentTypes.Xml;
                case "CSharp":
                    return Enumerations.DocumentTypes.CSharp;
                default:
                    throw new ApplicationException($"DocumentType {valString} does not exist");
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (destinationType != typeof(string)) return base.ConvertTo(context, culture, value, destinationType);
            var docValue = (Enumerations.DocumentType) value;
            if (docValue == Enumerations.DocumentTypes.Text) return "Text";
            if (docValue == Enumerations.DocumentTypes.Xml) return "Xml";
            if (docValue == Enumerations.DocumentTypes.CSharp) return "CSharp";
            throw new ApplicationException("Unknown document type");
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
            return new StandardValuesCollection(new List<Enumerations.DocumentType> {Enumerations.DocumentTypes.Text, Enumerations.DocumentTypes.Xml});
        }
    }
}
