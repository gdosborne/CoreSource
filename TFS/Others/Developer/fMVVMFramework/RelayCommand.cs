// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// Created: 
// Updated: 06/08/2016 16:20:01
// Updated by: Greg Osborne
// -----------------------------------------------------------------------
// 
// RelayCommand.cs
//
namespace MVVMFramework
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Input;

	public class RelayCommand : ICommand
	{
		#region Public Constructors
		public RelayCommand(Action handler)
		{
			_handler = handler;
		}
		#endregion Public Constructors

		#region Public Methods
		public bool CanExecute(object parameter)
		{
			return IsEnabled;
		}
		public void Execute(object parameter)
		{
			_handler();
		}
		#endregion Public Methods

		#region Public Events
		public event EventHandler CanExecuteChanged;
		#endregion Public Events

		#region Private Fields
		private readonly Action _handler;
		private bool _isEnabled;
		#endregion Private Fields

		#region Public Properties
		public bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}
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
		#endregion Public Properties
	}
}
