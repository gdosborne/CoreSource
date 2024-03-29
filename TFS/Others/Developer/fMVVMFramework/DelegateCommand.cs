// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// Created: 
// Updated: 06/08/2016 16:19:51
// Updated by: Greg Osborne
// -----------------------------------------------------------------------
// 
// DelegateCommand.cs
//
using System;
using System.Windows.Input;

namespace MVVMFramework
{

	public class DelegateCommand : ICommand
	{

		#region Public Constructors
		public DelegateCommand(Action<object> execute)
			: this(execute, null)
		{
		}
		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_Execute = execute;
			_CanExecute = canExecute;
		}
		#endregion Public Constructors

		public string Name { get; set; }
		#region Public Methods
		public bool CanExecute(object parameter)
		{
			if (_CanExecute == null)
				return true;
			return _CanExecute(parameter);
		}
		public void Execute(object parameter)
		{
			_Execute(parameter);
		}
		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
				CanExecuteChanged(this, EventArgs.Empty);
		}
		#endregion Public Methods

		#region Public Events
		public event EventHandler CanExecuteChanged;
		#endregion Public Events

		#region Private Fields
		private readonly Predicate<object> _CanExecute;
		private readonly Action<object> _Execute;
		#endregion Private Fields

		#region Public Properties
		public string Description { get; set; }
		#endregion Public Properties
	}
}
