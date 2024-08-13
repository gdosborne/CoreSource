using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

using static Common.Primitives.Extension;

namespace CongregationData.Helpers {
    internal static class Extensions {
        private static object FromXElement (this XElement element, Type propType) {
            var result = default(DomainItem);
            if (propType == typeof(Address)) {
                result = Address.GetNew();
            }else if (propType == typeof(Circuit)) {
                result = Circuit.GetNew();
            } else if (propType == typeof(Congregation)) {
                result = Congregation.GetNew();
            } else if (propType == typeof(Member)) {
                result = Member.GetNew();
            } else if (propType == typeof(MemberFlags)) {
                result = MemberFlags.GetNew();
            } else if (propType == typeof(PhoneNumber)) {
                result = PhoneNumber.GetNew();
            } else if (propType == typeof(PublicTalk)) {
                result = PublicTalk.GetNew();
            } else if (propType == typeof(PublicTalkScheduleItem)) {
                result = PublicTalkScheduleItem.GetNew();
            }
            if (result != null) {
                var props = propType.GetProperties();
                foreach (var prop in props) {
                    if (element.Element(prop.Name) != null) {
                        if (prop.PropertyType.DeclaringType == typeof(DomainItem)) {
                            prop.SetValue(result, element.FromXElement(prop.PropertyType));
                        } else {
                            prop.SetValue(result, element.Value.CastTo(prop.PropertyType));
                        }
                    }
                }
            }
            return result;
        }

        public static T ItemFromXElement<T> (this XElement element) where T : DomainItem {
            return (T)element.FromXElement(typeof(T));
        }

        public static XElement ItemToXElement<T> (this T item) where T : DomainItem {
            var result = new XElement(nameof(item));
            var props = item.GetType().GetProperties();
            foreach (var prop in props) {
                var val = prop.GetValue(item, null);
                if (val.IsNull()) {
                    result.Add(new XElement(nameof(val)));
                } else if (val.GetType().DeclaringType == typeof(DomainItem)) {
                    result.Add(val.As<DomainItem>().ToXElement());
                } else {
                    result.Add(new XElement(nameof(val), val.ToString()));
                }
            }
            return result;
        }
    }
}
