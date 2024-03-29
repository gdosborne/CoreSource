// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Controls {

	using System;
	using System.Linq;
	using System.Windows.Controls;

	public delegate void AddControlHandler(object sender, AddControlEventArgs e);

	public class AddControlEventArgs : EventArgs {

		#region Public Constructors
		public AddControlEventArgs(UserControl control) {
			Control = control;
		}
		#endregion Public Constructors

		#region Public Properties
		public UserControl Control {
			get;
			private set;
		}
		#endregion Public Properties
	}
}
