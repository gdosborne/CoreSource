// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// DesignerObject.cs
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {
	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Xml.Serialization;

	[Serializable]
	public class DesignerObject : ObjectBase {

		#region Public Constructors
		public DesignerObject()
			: base() {
		}
		#endregion Public Constructors

		#region Public Events
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Point _Location;
		private Size _Size;
		#endregion Private Fields

		#region Public Properties
		[XmlIgnore]
		public UserControl Control {
			get;
			protected set;
		}
		[XmlElementAttribute("Location")]
		public Point Location {
			get {
				return _Location;
			}
			set {
				_Location = value;
				if (this.GetType() == typeof(DesignerRectangle)) {
					Control.As<uRectangle>().SetValue(Canvas.LeftProperty, value.X);
					Control.As<uRectangle>().SetValue(Canvas.TopProperty, value.Y);
				}
				else if (this.GetType() == typeof(DesignerRectangle)) {
					Control.As<uEllipse>().SetValue(Canvas.LeftProperty, value.X);
					Control.As<uEllipse>().SetValue(Canvas.TopProperty, value.Y);
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Location"));
			}
		}
		[XmlElementAttribute("Size")]
		public Size Size {
			get {
				return _Size;
			}
			set {
				_Size = value;
				if (this.GetType() == typeof(DesignerRectangle)) {
					Control.As<uRectangle>().Width = value.Width;
					Control.As<uRectangle>().Height = value.Height;
				}
				else if (this.GetType() == typeof(DesignerRectangle)) {
					Control.As<uEllipse>().Width = value.Width;
					Control.As<uEllipse>().Height = value.Height;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Size"));
			}
		}
		#endregion Public Properties
	}
}
