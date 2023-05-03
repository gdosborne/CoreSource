namespace ORDModel
{
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Linq;

	public sealed class Computer : INotifyPropertyChanged, IPropertyItem
	{
		public static Computer FromXElement(XElement element) {
			var result = new Computer();
			result.Name = element.Attribute("Name").Value;
			if (element.Attribute("NetworkAddress") != null)
				result.Address = element.Attribute("NetworkAddress").Value;
			return result;
		}
		#region Public Constructors
		public Computer() {

		}
		#endregion Public Constructors

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _Address;
		private string _Name;
		#endregion Private Fields

		#region Public Properties
		public string Address {
			get {
				return _Address;
			}
			set {
				_Address = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Address"));
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