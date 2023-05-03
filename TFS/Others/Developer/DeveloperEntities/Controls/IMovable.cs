// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// IMovable.cs
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Controls {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Input;

	public interface IMovable {

		#region Public Methods
		void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e);
		void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e);
		void UserControl_MouseMove(object sender, MouseEventArgs e);
		#endregion Public Methods
	}
}
