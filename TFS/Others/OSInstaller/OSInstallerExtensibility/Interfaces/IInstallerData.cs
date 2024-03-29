namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public interface IInstallerData : IInstallerPropertyBase, INotifyPropertyChanged, IEditableObject
	{
		#region Public Properties
		bool IsEditable { get; set; }
		bool IsRequired { get; set; }
		bool IsStepData { get; set; }
		bool MustValidate { get; set; }
		string Value { get; set; }
		#endregion Public Properties
	}
}
