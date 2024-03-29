// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// DesignerShape.cs
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

	public class DesignerShape : DesignerObject {

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Brush _Fill;
		#endregion Private Fields

		#region Public Properties
		[XmlElementAttribute("Fill")]
		public Brush Fill {
			get {
				return _Fill;
			}
			set {
				_Fill = value;
				if (Control != null) {
					if (this.GetType() == typeof(DesignerRectangle))
						Control.As<uRectangle>().MyRectangle.Fill = Fill;
					else if (this.GetType() == typeof(DesignerEllipse))
						Control.As<uEllipse>().MyEllipse.Fill = Fill;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Fill"));
			}
		}
		#endregion Public Properties
	}
}
