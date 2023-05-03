namespace ORDModel
{
	using SNC.OptiRamp.Application.Extensions.Media;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Media;
	using System.Xml.Linq;

	public sealed class Brush : INotifyPropertyChanged, IPropertyItem
	{
		#region Public Constructors
		public Brush() {
			BrushType = BrushTypes.Solid;
		}
		#endregion Public Constructors

		#region Public Methods
		public static Brush FromXElement(XElement element) {
			var result = new Brush();
			result.Name = element.Attribute("name").Value;
			result.BrushType = (BrushTypes)Enum.Parse(typeof(BrushTypes), element.Attribute("type").Value, true);
			result.Color1 = element.Attribute("color1").Value.ToColor();
			return result;
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private BrushTypes _BrushType;
		private Color _Color1;
		private string _Name;
		#endregion Private Fields

		#region Public Enums
		public enum BrushTypes
		{
			Solid,
			Gradient
		}
		#endregion Public Enums

		#region Public Properties
		public BrushTypes BrushType {
			get {
				return _BrushType;
			}
			set {
				_BrushType = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("BrushType"));
			}
		}
		public Color Color1 {
			get {
				return _Color1;
			}
			set {
				_Color1 = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Color1"));
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