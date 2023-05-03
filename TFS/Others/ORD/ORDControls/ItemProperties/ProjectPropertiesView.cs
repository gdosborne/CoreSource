// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace ORDControls.ItemProperties
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;

	/// <summary>
	/// Class ProjectPropertiesView.
	/// </summary>
	public class ProjectPropertiesView : INotifyPropertyChanged
	{
		#region Public Methods
		/// <summary>
		/// Initializes the view.
		/// </summary>
		public void InitView() {
		}
		/// <summary>
		/// Updates the interface.
		/// </summary>
		public void UpdateInterface() {
		}
		#endregion Public Methods

		#region Public Events
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool? _DialogResult;
		private string _ProjectDescription;
		private string _ProjectLocation;
		private string _ProjectName;
		private long _ProjectSize;
		private ObservableCollection<object> _Revisions;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether [dialog result].
		/// </summary>
		/// <value><c>null</c> if [dialog result] contains no value, <c>true</c> if [dialog result]; otherwise, <c>false</c>.</value>
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
		/// <summary>
		/// Gets or sets the project description.
		/// </summary>
		/// <value>The project description.</value>
		public string ProjectDescription {
			get {
				return _ProjectDescription;
			}
			set {
				_ProjectDescription = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectDescription"));
			}
		}
		/// <summary>
		/// Gets or sets the project location.
		/// </summary>
		/// <value>The project location.</value>
		public string ProjectLocation {
			get {
				return _ProjectLocation;
			}
			set {
				_ProjectLocation = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectLocation"));
			}
		}
		/// <summary>
		/// Gets or sets the name of the project.
		/// </summary>
		/// <value>The name of the project.</value>
		public string ProjectName {
			get {
				return _ProjectName;
			}
			set {
				_ProjectName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectName"));
			}
		}
		/// <summary>
		/// Gets or sets the size of the project.
		/// </summary>
		/// <value>The size of the project.</value>
		public long ProjectSize {
			get {
				return _ProjectSize;
			}
			set {
				_ProjectSize = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectSize"));
			}
		}
		/// <summary>
		/// Gets or sets the revisions.
		/// </summary>
		/// <value>The revisions.</value>
		public ObservableCollection<object> Revisions {
			get {
				return _Revisions;
			}
			set {
				_Revisions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Revisions"));
			}
		}
		#endregion Public Properties
	}
}