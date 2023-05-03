// ***********************************************************************
// Assembly         : MVVMFramework
// Author           : Greg
// Created          : 07-16-2015
//
// Last Modified By : Greg
// Last Modified On : 07-16-2015
// ***********************************************************************
// <copyright file="ViewModelBase.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVVMFramework
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		#region Public Events
		public virtual event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Protected Methods

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
