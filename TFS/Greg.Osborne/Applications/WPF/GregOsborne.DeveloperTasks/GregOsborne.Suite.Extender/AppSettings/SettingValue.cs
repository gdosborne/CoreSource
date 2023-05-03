namespace GregOsborne.Suite.Extender.AppSettings {
	using System.ComponentModel;
	using System.Windows;

	/// <summary>Base setting</summary>
	/// <typeparam name="T">The type for the setting</typeparam>
	public abstract class SettingValue<T> {

		/// <summary>Initializes a new instance of the <see cref="SettingValue{T}" /> class.</summary>
		/// <param name="name">The name.</param>
		/// <param name="propertyName">The name of the property</param>
		public SettingValue(string name, string propertyName) {
			this.Name = name;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>Gets or sets the current value.</summary>
		/// <value>The value.</value>
		public abstract T CurrentValue { get; set; }

		/// <summary>Gets or sets the default value.</summary>
		/// <value>The default value.</value>
		public T DefaultValue { get; set; }

		/// <summary>
		/// The name od the controls to connect to
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>Invokes the property changed.</summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		/// <summary>Occurs when the property is changed.</summary>
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
