// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// PasswordWindow
//
namespace MoMoney
{
	using MoMoney.Views;
	using MVVMFramework;
	using MyApplication.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;

	public partial class PasswordWindow : Window
	{
		#region Public Constructors
		public PasswordWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e) {
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
			base.OnSourceInitialized(e);
			MyPassword.Focus();
		}
		#endregion Protected Methods

		#region Private Methods
		private void MyPassword_PasswordChanged(object sender, RoutedEventArgs e) {
			View.Password = (sender as PasswordBox).Password;
		}
		private void PasswordWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e) {
		}
		private void PasswordWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e) {
			View.InitView();
		}
		#endregion Private Methods

		#region Public Properties
		public PasswordWindowView View {
			get {
				return LayoutRoot.GetView<PasswordWindowView>();
			}
		}
		#endregion Public Properties
	}
}
