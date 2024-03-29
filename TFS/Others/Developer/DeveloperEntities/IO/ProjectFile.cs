// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.IO {

	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using SNC.OptiRamp.Application.DeveloperEntities.Designer;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Windows;
	using System.Xml.Serialization;

	public class ProjectFile : INotifyPropertyChanged {

		#region Public Methods
		public static ProjectFile Create(string fileName, Stream stream) {
			return null;
		}
		public static ProjectFile Create(string fileName) {
			XmlSerializer mySerializer = new XmlSerializer(typeof(Project));
			Project project = null;
			ProjectFile result = null;
			if (!File.Exists(fileName)) {
				project = new Project();
				result = new ProjectFile {
					FilePath = fileName,
					Project = project,
					IsChanged = true
				};
				result.Project.Name = "No Name";
				result.Project.Pages.Add(new Page {
					Name = "Page 1",
					Size = new Size(800, 600)
				});
			}
			else {
				try {
					using (StreamReader myReader = new StreamReader(fileName)) {
						project = (Project)mySerializer.Deserialize(myReader);
						myReader.Close();
					}
					result = new ProjectFile {
						FilePath = fileName,
						Project = project,
						IsChanged = false
					};
				}
				catch (Exception) {
					throw;
				}
			}
			return result;
		}
		public void Save() {
			try {
				XmlSerializer mySerializer = new XmlSerializer(typeof(Project));
				using (StreamWriter myWriter = new StreamWriter(FilePath)) {
					mySerializer.Serialize(myWriter, this.Project);
					myWriter.Close();
				}
				IsChanged = false;
			}
			catch (Exception) {
				throw;
			}
		}
		public void Save(string fileName) {
			FilePath = fileName;
			Save();
		}
		#endregion Public Methods

		#region Private Methods
		private static bool IsProjectFile(string fileName) {
			//validate here
			return true;
		}
		private static bool IsProjectFile(Stream stream) {
			//validate here
			return true;
		}

		private void Project_AddControl(object sender, Controls.AddControlEventArgs e) {
			if (AddControl != null)
				AddControl(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event AddControlHandler AddControl;
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private string _FileName;
		private string _FilePath;
		private bool _IsChanged;

		private Project _Project;
		#endregion Private Fields

		#region Public Properties
		public string FileName {
			get {
				return _FileName;
			}
			private set {
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public string FilePath {
			get {
				return _FilePath;
			}
			set {
				_FilePath = value;
				if (!string.IsNullOrEmpty(value))
					FileName = System.IO.Path.GetFileName(value);
				else
					FileName = string.Empty;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FilePath"));
			}
		}
		public bool IsChanged {
			get {
				return _IsChanged;
			}
			set {
				_IsChanged = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsChanged"));
			}
		}
		public Project Project {
			get {
				return _Project;
			}
			set {
				_Project = value;
				if (_Project != null) {
					Project.AddControl += Project_AddControl;
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Project"));
			}
		}
		#endregion Public Properties
	}
}
