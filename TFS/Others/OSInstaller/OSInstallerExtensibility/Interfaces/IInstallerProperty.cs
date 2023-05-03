namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public interface IInstallerProperty : IInstallerPropertyBase, INotifyPropertyChanged, IEditableObject
	{
		#region Public Properties
		object Value { get; set; }
		#endregion Public Properties
	}
}
