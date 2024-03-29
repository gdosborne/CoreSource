// -----------------------------------------------------------------------
// Copyright © Statistics & Controls, Inc 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace ProjectFileManager
{
	using fDefs.ProjectService;
	using MVVMFramework;
	using SNC.OptiRamp.Application.Extensions.Windows;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;

	internal partial class OrderProjectsWindow : Window
	{
		#region Public Constructors
		public OrderProjectsWindow() {
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
		private void OrderProjectsWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
			if (e.CommandToExecute.Equals("MoveProjectUp") || e.CommandToExecute.Equals("MoveProjectDown")) {
				var currentSequence = View.SelectedProject.Sequence;
				var index = View.Projects.IndexOf(View.SelectedProject);
				var newSequence = -1;
				if (e.CommandToExecute.Equals("MoveProjectUp")) {
					var previousItem = View.Projects[index - 1];
					newSequence = previousItem.Sequence;
					previousItem.Sequence = currentSequence;
				}
				if (e.CommandToExecute.Equals("MoveProjectDown")) {
					var nextItem = View.Projects[index + 1];
					newSequence = nextItem.Sequence;
					nextItem.Sequence = currentSequence;
				}
				View.SelectedProject.Sequence = newSequence;
				View.Projects = new ObservableCollection<ProjectData>(View.Projects.OrderBy(x => x.Sequence));
				View.UpdateInterface();
				FileListView.Focus();
			}
		}
		private void OrderProjectsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					this.DialogResult = View.DialogResult;
					break;
			}
		}
		#endregion Private Methods

		#region Public Properties
		public OrderProjectsWindowView View {
			get {
				return LayoutRoot.GetView<OrderProjectsWindowView>();
			}
		}
		#endregion Public Properties
	}
}
