// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.Developer.Views {
	using SNC.OptiRamp.Application.Developer.Properties;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using System;
	using System.ComponentModel;
	using System.IO;
	using System.Windows;

	internal partial class OpenRemoteWindowView : INotifyPropertyChanged {

		#region Public Methods
		public void Initialize(Window window) {

		}
		public void InitView() {

		}
		public void Persist(Window window) {
			Settings.Default.LastWebServiceAddress = Address;
			Settings.Default.Save();
		}
		public void UpdateInterface() {

		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events
		
		#region Private Fields
		private string _Address;
		private bool? _DialogResult;
		private string _FileName;
		private Stream _Stream;
		private Uri _WebServiceUri;
		#endregion Private Fields

		#region Public Properties
		public string Address {
			get {
				return _Address;
			}
			set {
				_Address = value;
				if (!string.IsNullOrEmpty(value))
					WebServiceUri = new Uri(value);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Address"));
			}
		}
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		public string FileName {
			get {
				return _FileName;
			}
			set {
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public Stream Stream {
			get {
				return _Stream;
			}
			set {
				_Stream = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Stream"));
			}
		}
		public Uri WebServiceUri {
			get {
				return _WebServiceUri;
			}
			set {
				_WebServiceUri = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("WebServiceUri"));
			}
		}
		#endregion Public Properties
	}
}
