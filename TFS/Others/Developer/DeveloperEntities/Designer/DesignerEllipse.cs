// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// DesignerEllipse.cs
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {

	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Media;

	public class DesignerEllipse : DesignerShape {

		#region Public Constructors
		public DesignerEllipse()
			: base() {
			Fill = new SolidColorBrush(Colors.White);
			Control = new uEllipse();
			Control.As<uEllipse>().MyEllipse.Fill = Fill;
		}
		public override event PropertyChangedEventHandler PropertyChanged;

		#endregion Public Constructors
	}
}
