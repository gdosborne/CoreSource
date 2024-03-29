// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// Created: 
// Updated: 06/08/2016 16:19:55
// Updated by: Greg Osborne
// -----------------------------------------------------------------------
// 
// ExecuteUIAction.cs
//
using System;
using System.Collections.Generic;

namespace MVVMFramework
{
	public delegate void ExecuteUIActionHandler(object sender, ExecuteUIActionEventArgs e);

	public class ExecuteUIActionEventArgs : EventArgs
	{
		#region Public Constructors
		public ExecuteUIActionEventArgs(string commandToExecute, Dictionary<string, object> parameters)
		{
			CommandToExecute = commandToExecute;
			Parameters = parameters;
		}
		#endregion Public Constructors

		#region Public Properties
		public string CommandToExecute { get; private set; }
		public Dictionary<string, object> Parameters { get; private set; }
		#endregion Public Properties
	}
}
