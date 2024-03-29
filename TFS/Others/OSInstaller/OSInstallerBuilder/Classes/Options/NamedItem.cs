namespace OSInstallerBuilder.Classes.Options
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	public abstract class NamedItem : INotifyPropertyChanged
	{
		#region Public Events
		public virtual event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool _IsExpanded;
		private bool _IsSelected;
		private string _Name;
		#endregion Private Fields

		#region Public Properties
		public bool IsExpanded
		{
			get
			{
				return _IsExpanded;
			}
			set
			{
				_IsExpanded = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsExpanded"));
			}
		}
		public bool IsSelected
		{
			get
			{
				return _IsSelected;
			}
			set
			{
				_IsSelected = value;
				IsExpanded = true;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
			}
		}
		public string Name
		{
			get
			{
				return _Name;
			}
			protected set
			{
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		#endregion Public Properties
	}
}
