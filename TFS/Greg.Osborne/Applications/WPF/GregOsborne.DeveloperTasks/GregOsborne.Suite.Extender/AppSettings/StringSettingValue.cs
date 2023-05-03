namespace GregOsborne.Suite.Extender.AppSettings {
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using GregOsborne.Application;
	using GregOsborne.Application.Primitives;

	public class StringSettingValue : SettingValue<string>, INotifyPropertyChanged {
		public StringSettingValue(string name, string propertyName)
			: base(name, propertyName) { }

		private string currentValue = default;
		protected TextBox textBox = default;

		public override string CurrentValue {
			get => this.currentValue;
			set {
				this.currentValue = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}		
	}
}
