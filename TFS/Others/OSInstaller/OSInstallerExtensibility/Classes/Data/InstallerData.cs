namespace OSInstallerExtensibility.Classes.Data
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public class InstallerData : IInstallerData
	{
		#region Public Constructors
		public InstallerData(string name)
		{
			Name = name;
		}
		#endregion Public Constructors

		#region Public Methods
		public void BeginEdit()
		{
			_OldData = new InstallerData(Name)
			{
				IsEditable = IsEditable,
				IsStepData = IsStepData,
				MustValidate = MustValidate,
				Value = Value,
				IsRequired = IsRequired
			};
		}
		public void CancelEdit()
		{
			IsEditable = _OldData.IsEditable;
			IsStepData = _OldData.IsStepData;
			MustValidate = _OldData.MustValidate;
			Value = _OldData.Value;
			IsRequired = _OldData.IsRequired;
		}
		public void EndEdit()
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(null));
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private InstallerData _OldData = null;
		#endregion Private Fields

		#region Public Properties
		public bool IsEditable { get; set; }
		public bool IsRequired { get; set; }
		public bool IsStepData { get; set; }
		public bool MustValidate { get; set; }
		public string Name { get; private set; }
		public string Value { get; set; }
		#endregion Public Properties
	}
}
