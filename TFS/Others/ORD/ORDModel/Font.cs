namespace ORDModel
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Linq;

	public sealed class Font : INotifyPropertyChanged, IPropertyItem
	{
		#region Public Constructors
		public Font() {
			FontStyle = FontStyles.Regular;
			FontSize = 10;
		}
		#endregion Public Constructors

		#region Public Methods
		public static Font FromXElement(XElement element) {
			var result = new Font();
			result.Name = element.Attribute("name").Value;
			result.FontFamily = element.Attribute("font").Value;
			result.FontSize = double.Parse(element.Attribute("fontsize").Value);
			result.FontStyle = (FontStyles)Enum.Parse(typeof(FontStyles), element.Attribute("fontstyle").Value, true);
			return result;
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _FontFamily;
		private double _FontSize;
		private FontStyles _FontStyle;
		private string _Name;
		#endregion Private Fields

		#region Public Enums
		public enum FontStyles
		{
			Regular,
			Bold
		}
		#endregion Public Enums

		#region Public Properties
		public string FontFamily {
			get {
				return _FontFamily;
			}
			set {
				_FontFamily = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FontFamily"));
			}
		}
		public double FontSize {
			get {
				return _FontSize;
			}
			set {
				_FontSize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FontSize"));
			}
		}
		public FontStyles FontStyle {
			get {
				return _FontStyle;
			}
			set {
				_FontStyle = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FontStyle"));
			}
		}
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		#endregion Public Properties

		public System.Windows.Controls.UserControl GetPropertiesControl() {
			throw new NotImplementedException();
		}
	}
}