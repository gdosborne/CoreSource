// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg
// Created: Unknown
// Updated: 06/21/2016 13:36:57
// Updated by: Greg
// -----------------------------------------------------------------------
// 
// ProjectData.cs
//
namespace SNC.OptiRamp.ProjectService
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;

	/// <summary>
	/// Class ProjectData.
	/// </summary>
	public class ProjectData : INotifyPropertyChanged
	{
		public ProjectData() {
			Backups = new List<ProjectData>();
		}
		#region Public Events
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private IList<ProjectData> _Backups;
		private DateTime _LastModifyTimeUtc;
		private string _Name;
		private int _Sequence;
		private long _Size;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the backups.
		/// </summary>
		/// <value>The backups.</value>
		public IList<ProjectData> Backups {
			get {
				return _Backups;
			}
			set {
				_Backups = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Backups"));
			}
		}

		/// <summary>
		/// Gets or sets the last modify time UTC.
		/// </summary>
		/// <value>The last modify time UTC.</value>
		public DateTime LastModifyTimeUtc {
			get {
				return _LastModifyTimeUtc;
			}
			set {
				_LastModifyTimeUtc = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("LastModifyTimeUtc"));
			}
		}
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		/// <summary>
		/// Gets or sets the sequence.
		/// </summary>
		/// <value>The sequence.</value>
		public int Sequence {
			get {
				return _Sequence;
			}
			set {
				_Sequence = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Sequence"));
			}
		}
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public long Size {
			get {
				return _Size;
			}
			set {
				_Size = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Size"));
			}
		}
		#endregion Public Properties
	}
}