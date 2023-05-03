// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Relay command class
//
namespace SNC.OptiRamp.Application.Extensions.Commands
{
	using System;
	using System.Windows.Input;
	using System.ComponentModel;

	/// <summary>
	/// Class RelayCommand.
	/// </summary>
	[Description("Class RelayCommand")]
	public class RelayCommand : ICommand
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="RelayCommand" /> class.
		/// </summary>
		/// <param name="handler">The handler.</param>
		[Description("Initializes a new instance of the class")]
		public RelayCommand(Action handler)
		{
			theHandler = handler;
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		/// <returns>true if this command can be executed; otherwise, false.</returns>
		[Description("Defines the method that determines whether the command can execute in its current state")]
		public bool CanExecute(object parameter)
		{
			return IsEnabled;
		}
		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		[Description("Defines the method to be called when the command is invoked")]
		public void Execute(object parameter)
		{
			Parameter = parameter;
			theHandler();
		}
		#endregion Public Methods

		#region Public Events
		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// </summary>
		[Description("Occurs when changes occur that affect whether or not the command should execute")]
		public event EventHandler CanExecuteChanged;
		#endregion Public Events

		#region Private Fields
		private bool isEnabled;
		private Action theHandler;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether this instance is enabled.
		/// </summary>
		/// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
		[Description(" Gets or sets a value indicating whether this instance is enabled")]
		public bool IsEnabled
		{
			get
			{
				return isEnabled;
			}
			set
			{
				if (value != isEnabled)
				{
					isEnabled = value;
					if (CanExecuteChanged != null)
					{
						CanExecuteChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <value>The parameter.</value>
		[Description("Gets the parameter")]
		public object Parameter { get; private set; }
		#endregion Public Properties
	}
}