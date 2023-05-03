namespace GregOsborne.Developers.Suite.Configuration {
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;

	public class Program : Changeable {

		private string text = default;
		public string Text {
			get => this.text;
			set {
				this.text = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private bool isIntegral = default;
		public bool IsIntegral {
			get => this.isIntegral;
			set {
				this.isIntegral = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string id = default;
		public string Id {
			get => this.id;
			set {
				this.id = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private string path = default;
		public string Path {
			get => this.path;
			set {
				this.path = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private Dictionary<int, string> arguments = default;
		public Dictionary<int, string> Arguments {
			get => this.arguments;
			set {
				this.arguments = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public static Program FromXElement(XElement element) {
			var result = new Program {
				Id = element.Attribute("id").Value,
				IsIntegral = bool.Parse(element.Attribute("integral").Value),
				Path = element.Value
			};
			var argsElement = element.Element("arguments");
			if (argsElement != null) {
				foreach (var e in argsElement.Elements()) {
					result.Arguments.Add(int.Parse(e.Attribute("sequence").Value), e.Value);
				}
			}
			return result;
		}

		public override XElement ToXElement() {
			var result = new XElement("program",
				new XAttribute("id", this.Id),
				new XAttribute("integral", this.isIntegral),
				new XElement("path", this.Path));
			var argElement = new XElement("arguments");

			foreach (var arg in this.Arguments.OrderBy(x => x.Key)) {
				argElement.Add(new XElement("argument",
					new XAttribute("sequence", arg.Key),
					new XAttribute("value", arg.Value)));
			}

			return result;
		}
	}
}
