// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// Created: 
// Updated: 06/08/2016 16:20:04
// Updated by: Greg Osborne
// -----------------------------------------------------------------------
// 
// ViewModelBase.cs
//
namespace MVVMFramework
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class ViewModelBase : INotifyPropertyChanged
	{
		#region Protected Methods
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion Protected Methods

		#region Public Events
		public virtual event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events
	}
}
