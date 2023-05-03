// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// SettingsWindow
//
namespace MoMoney
{
	using MVVMFramework;
	using MyApplication.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using Views;

	public partial class SettingsWindow : Window
	{
		#region Public Constructors
		public SettingsWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e) {
			this.HideMinimizeAndMaximizeButtons();
			this.HideControlBox();
		}
		#endregion Protected Methods

		#region Private Methods
		private void SettingsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					if (View.DialogResult.GetValueOrDefault()) {
						MoMoney.Properties.Settings.Default.OpenLastMMFFile = View.OpenLastMMFFile;
					}
					DialogResult = View.DialogResult;
					break;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			View.InitView();
		}
		#endregion Private Methods

		#region Public Properties
		public SettingsWindowView View {
			get {
				return LayoutRoot.GetView<SettingsWindowView>();
			}
		}
		#endregion Public Properties
	}
}
