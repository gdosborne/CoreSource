namespace Greg.Osborne.Installer.Support {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Xml.Linq;
	using GregOsborne.MVVMFramework;

	public class SideItem : NotifyClassBase {
		public static SideItem FromXElement(XElement element) {
			var result = new SideItem {
				Header = element.GetAttributeValue("header", "Header"),
				IconHexValue = element.GetAttributeValue("icon_hex_value", "&#xE909;"),
				SideText = element.GetElementValue("Text not found")
			};
			return result;
		}

		public XElement ToXElement() {
			var result = new XElement("item",
				new XAttribute("icon_hex_value", this.IconHexValue),
				new XAttribute("header", this.Header),
				new XCData(this.SideText));
			return result;
		}

		private SideItem() { }

		private DelegateCommand selectIconCommand = default;
		private int segoeMDL2AssetsCharacterIndex = default;
		private char segoeMDL2AssetsCharacter = default;
		private string iconHexValue = default;
		private string sideText = default;
		private string header = default;
		private string image = default;
		private string sample = default;

		public int SegoeMDL2AssetsCharacterIndex {
			get => this.segoeMDL2AssetsCharacterIndex;
			set {
				this.segoeMDL2AssetsCharacterIndex = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public char SegoeMDL2AssetsCharacter {
			get => this.segoeMDL2AssetsCharacter;
			set {
				this.segoeMDL2AssetsCharacter = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string IconHexValue {
			get => this.iconHexValue;
			set {
				this.iconHexValue = value;
				var disallowdCharacters = new char[] {
					';', '&', '#', 'x'
				};
				var remaining = new string(value.ToCharArray().Except(disallowdCharacters).ToArray());
				this.SegoeMDL2AssetsCharacterIndex = Convert.ToInt32(remaining, 16);
				this.Image = char.ConvertFromUtf32(this.SegoeMDL2AssetsCharacterIndex);
				this.SegoeMDL2AssetsCharacter = (char)this.SegoeMDL2AssetsCharacterIndex;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string SideText {
			get => this.sideText;
			set {
				this.sideText = value;
				using (var sr = new StringReader(value)) {
					this.Sample = sr.ReadLine();
				}
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string Sample {
			get => this.sample;
			private set {
				this.sample = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string Header {
			get => this.header;
			set {
				this.header = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public string Image {
			get => this.image;
			set {
				this.image = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public override string ToString() => this.Header;

		public DelegateCommand SelectIconCommand => this.selectIconCommand ?? (this.selectIconCommand = new DelegateCommand(this.SelectIcon, this.ValidateSelectIconState));
		private bool ValidateSelectIconState(object state) => true;
		private void SelectIcon(object state) {
			var p = new Dictionary<string, object> {
				{ "index", this.SegoeMDL2AssetsCharacterIndex },
				{ "cancel", false }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("SelectIcon", p));
			if ((bool)p["cancel"]) {
				return;
			}
			this.IconHexValue = $"&#{((int)p["index"]).ToString("X")};";
		}

		public event ExecuteUiActionHandler ExecuteUiAction;
	}
}
