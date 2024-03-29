// ***********************************************************************
// Assembly         : MVVMFramework
// Author           : Greg
// Created          : 07-16-2015
//
// Last Modified By : Greg
// Last Modified On : 07-16-2015
// ***********************************************************************
// <copyright file="RelayCommand.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MVVMFramework
{
	public class RelayCommand : ICommand
	{
		#region Private Fields
		private readonly Action _handler;
		private bool _isEnabled;
		#endregion

		#region Public Constructors

		public RelayCommand(Action handler)
		{
			_handler = handler;
		}

		#endregion

		#region Public Events
		public event EventHandler CanExecuteChanged;
		#endregion

		#region Public Properties
		public bool IsEnabled
		{
			get { return _isEnabled; }
			set
			{
				if (value != _isEnabled)
				{
					_isEnabled = value;
					if (CanExecuteChanged != null)
					{
						CanExecuteChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		#endregion

		#region Public Methods

		public bool CanExecute(object parameter)
		{
			return IsEnabled;
		}

		public void Execute(object parameter)
		{
			_handler();
		}

		#endregion
	}
}
