// -----------------------------------------------------------------------
// Copyright © Statistics & Controls, Inc 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Backup Window
//
namespace ProjectFileManager {

	using MVVMFramework;
	using SNC.OptiRamp.Application.Dialog;
	using SNC.OptiRamp.Application.Extensions.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	internal partial class BackupWindow : Window {

		#region Public Constructors
		public BackupWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			this.HideMinimizeAndMaximizeButtons();
		}
		#endregion Protected Methods

		#region Private Methods
		private void BackupWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					if (!View.DialogResult.GetValueOrDefault()) {
						DialogResult = View.DialogResult;
						return;
					}
					var td1 = new TaskDialog {
						AllowClose = false,
						Width = 400,
						Image = ImagesTypes.Question,
						MessageText = string.Format("You are about to replace {0} with the backup from {1}.\n\nAre you sure you want to do this?", View.SelectedProject.Name, View.SelectedBackup.LastModifyTimeUtc),
						AdditionalInformation = "This action is irreversible.",
						IsAdditionalInformationExpanded = true,
						Title = "Restore project"
					};
					td1.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
					if(td1.ShowDialog(Window.GetWindow(this)) == (int)ButtonTypes.No)
						return;
					DialogResult = View.DialogResult;
					break;
			}
		}
		#endregion Private Methods

		#region Public Properties
		public BackupWindowView View {
			get {
				return LayoutRoot.GetView<BackupWindowView>();
			}
		}
		#endregion Public Properties
	}
}
