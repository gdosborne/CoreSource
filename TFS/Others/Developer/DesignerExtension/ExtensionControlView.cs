// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// ExtensionControlView.cs
//
namespace SNC.OptiRamp.Application.Developer.Extensions.DesignerExtension {

	using System;
	using System.ComponentModel;
	using System.Windows;

	public class ExtensionControlView : INotifyPropertyChanged {

		#region Public Constructors
		public ExtensionControlView() {
			CanvasWidth = 800;
			CanvasHeight = 600;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(Window window) {
		}
		public void InitView() {
		}
		public void Persist(Window window) {
		}
		public void UpdateInterface() {
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private double _CanvasHeight;
		private double _CanvasWidth;
		private Extender _Extender;
		private Point _LastPoint;
		#endregion Private Fields

		#region Public Properties
		public double CanvasHeight {
			get {
				return _CanvasHeight;
			}
			set {
				_CanvasHeight = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CanvasHeight"));
			}
		}
		public double CanvasWidth {
			get {
				return _CanvasWidth;
			}
			set {
				_CanvasWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CanvasWidth"));
			}
		}
		public Extender Extender {
			get {
				return _Extender;
			}
			set {
				_Extender = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Extender"));
			}
		}
		public Point LastPoint {
			get {
				return _LastPoint;
			}
			set {
				_LastPoint = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastPoint"));
			}
		}
		#endregion Public Properties
	}
}
