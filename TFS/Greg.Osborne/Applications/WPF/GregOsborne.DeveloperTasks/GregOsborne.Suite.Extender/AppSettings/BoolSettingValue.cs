namespace GregOsborne.Suite.Extender.AppSettings {
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;
	using GregOsborne.WPFControls;

	/// <summary>A bool setting value</summary>
	public class BoolSettingValue : SettingValue<bool>, INotifyPropertyChanged {

		/// <summary>Initializes a new instance of the <see cref="BoolSettingValue" /> class.</summary>
		/// <param name="name">The name.</param>
		/// <param name="propertyName">the name of the property</param>
		public BoolSettingValue(string name, string propertyName)
			: base(name, propertyName) { }

		private bool currentValue = false;
		/// <summary>Gets or sets the current value.</summary>
		/// <value>The value.</value>
		public override bool CurrentValue {
			get {
				return this.currentValue;
			}
			set {
				this.currentValue = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}				
	}
}
