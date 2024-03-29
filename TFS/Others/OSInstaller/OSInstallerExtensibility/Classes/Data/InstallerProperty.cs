namespace OSInstallerExtensibility.Classes.Data
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class InstallerProperty : IInstallerProperty
	{
		#region Public Constructors
		public InstallerProperty(string name)
		{
			Name = name;
		}
		public InstallerProperty(string name, object value)
			: this(name)
		{
			Value = value;
		}
		#endregion Public Constructors

		#region Public Methods
		public void BeginEdit()
		{
			_Previous = new InstallerProperty(this.Name, this.Value);
		}
		public void CancelEdit()
		{
			this.Name = _Previous.Name;
			this.Value = _Previous.Value;
		}
		public void EndEdit()
		{
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IInstallerProperty _Previous = null;
		#endregion Private Fields

		#region Public Properties
		public string Name { get; private set; }
		public object Value { get; set; }
		#endregion Public Properties
	}
}
