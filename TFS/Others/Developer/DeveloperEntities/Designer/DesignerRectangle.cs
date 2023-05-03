// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// DesignerRectangle.cs
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {
	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Media;
	using System.Xml.Serialization;

	[Serializable]
	public class DesignerRectangle : DesignerShape {

		#region Public Constructors
		public DesignerRectangle()
			: base() {
			Fill = new SolidColorBrush(Colors.White);
			Control = new uRectangle();
			Control.As<uRectangle>().MyRectangle.Fill = Fill;
		}
		#endregion Public Constructors

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private double _CornerRadius;
		#endregion Private Fields

		#region Public Properties
		[XmlElementAttribute("CornerRadius")]
		public double CornerRadius {
			get {
				return _CornerRadius;
			}
			set {
				_CornerRadius = value;
				if (Control != null) {
					Control.As<uRectangle>().MyRectangle.RadiusX = value;
					Control.As<uRectangle>().MyRectangle.RadiusY = value;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CornerRadius"));
			}
		}
		#endregion Public Properties
	}
}
