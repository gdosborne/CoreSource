namespace Greg.Osborne.Installer.Support {
	using System;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;

	public class SettingItem : NotifyClassBase {
		public static SettingItem FromXElement(XElement element) {
			var name = element.Name.LocalName;
			var type = Type.GetType(element.Attribute("type").Value);
			var value = (type == typeof(string)) ? element.Value : Convert.ChangeType(element.Value, type);
			return new SettingItem {
				ValueType = type,
				Name = name,
				XmlName = element.Attribute("name").Value,
				Value = value
			};
		}

		private Type valueType = default;
		public Type ValueType {
			get => this.valueType;
			set {
				this.valueType = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private object value = default;
		public object Value {
			get => this.value;
			set {
				this.value = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string name = default;
		public string Name {
			get => this.name;
			set {
				this.name = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string xmlName = default;
		public string XmlName {
			get => this.xmlName;
			set {
				this.xmlName = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public override string ToString() => $"{this.Name}({this.XmlName})={this.Value}";

		public XElement ToXElement() {
			var result = new XElement(this.XmlName);
			result.Add(new XAttribute("name", this.Name));
			result.Add(new XAttribute("type", this.GetType().Name));
			result.Value = this.Value.ToString();
			return result;
		}
	}
}
