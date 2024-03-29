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
	using System.Windows;

	internal partial class DeveloperMainWindowView : INotifyPropertyChanged {

		#region Public Constructors
		public DeveloperMainWindowView() {
			TreeViewWidth = new GridLength(200, GridUnitType.Pixel);
		}
		#endregion Public Constructors

		#region Public Methods
		public void Initialize(Window window) {
			window.Left = Settings.Default.LastMainWindowLeft;
			window.Top = Settings.Default.LastMainWindowTop;
			window.Width = Settings.Default.LastMainWindowWidth;
			window.Height = Settings.Default.LastMainWindowHeight;
			window.WindowState = Settings.Default.LastMainWindowWindowState;
			TreeViewWidth = new GridLength(Settings.Default.LastMainWindowTreeViewWidth, GridUnitType.Pixel);
		}
		public void InitView() {
			UpdateInterface();
		}
		public void Persist(Window window) {
			Settings.Default.LastMainWindowLeft = window.RestoreBounds.Left;
			Settings.Default.LastMainWindowTop = window.RestoreBounds.Top;
			Settings.Default.LastMainWindowWidth = window.RestoreBounds.Width;
			Settings.Default.LastMainWindowHeight = window.RestoreBounds.Height;
			Settings.Default.LastMainWindowWindowState = window.WindowState;
			Settings.Default.LastMainWindowTreeViewWidth = TreeViewWidth.Value;
			Settings.Default.Save();
		}
		public void Save() {
			ProjectFile.Save();
			ProjectFile.IsChanged = false;
		}
		public void Save(string fileName) {
			//save as new name
			ProjectFile.IsChanged = false;
		}
		public void UpdateInterface() {
			OpenLocalCommand.RaiseCanExecuteChanged();
			OpenRemoteCommand.RaiseCanExecuteChanged();
			NewProjectCommand.RaiseCanExecuteChanged();
			CloseProjectCommand.RaiseCanExecuteChanged();
			SaveProjectCommand.RaiseCanExecuteChanged();
			SaveProjectAsCommand.RaiseCanExecuteChanged();
			if (App.ApplicationExtensions != null && App.ApplicationExtensions.Extensions != null) {
				foreach (var extension in App.ApplicationExtensions.Extensions) {
					extension.UpdateInterface();
				}
			}
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private ProjectFile _ProjectFile;
		private GridLength _TreeViewWidth;
		#endregion Private Fields

		#region Public Properties
		public ProjectFile ProjectFile {
			get {
				return _ProjectFile;
			}
			set {
				_ProjectFile = value;
				foreach (var extension in App.ApplicationExtensions.Extensions) {
					extension.ProjectFile = value;
				}
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ProjectFile"));
			}
		}
		public GridLength TreeViewWidth {
			get {
				return _TreeViewWidth;
			}
			set {
				_TreeViewWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("TreeViewWidth"));
			}
		}
		#endregion Public Properties
	}
}
