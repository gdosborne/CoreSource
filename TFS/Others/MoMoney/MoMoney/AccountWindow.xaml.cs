// -----------------------------------------------------------------------
// Copyright GDOsborne.com 2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// AccountWindow
//
namespace MoMoney
{
	using MoMoney.Views;
	using MVVMFramework;
	using MyApplication.Windows;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using MyApplication.Primitives;
	using System.Windows.Controls;

	public partial class AccountWindow : Window
	{
		#region Public Constructors
		public AccountWindow() {
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e) {
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
			base.OnSourceInitialized(e);
			NameTextBox.Focus();
		}
		#endregion Protected Methods

		#region Private Methods
		private void AccountWindowView_ExecuteUIAction(object sender, MVVMFramework.ExecuteUIActionEventArgs e) {
			var okButton = new TaskDialogButton(ButtonType.Ok);
			var yesButton = new TaskDialogButton(ButtonType.Yes);
			var noButton = new TaskDialogButton(ButtonType.No);
			var cancelButton = new TaskDialogButton(ButtonType.Cancel); 
			switch (e.CommandToExecute) {
				case "ShowZeroBalanceQuestion":
					var td1 = new TaskDialog
					{
						AllowDialogCancellation = false,
						ButtonStyle = TaskDialogButtonStyle.Standard,
						CenterParent = true,
						MainIcon = TaskDialogIcon.Information,
						MainInstruction = "The account you are creating has a 0$ starting balance. Are you sure you want to create the account?",
						MinimizeBox = false,
						WindowTitle = "Zero balance"
					};
					td1.Buttons.Add(yesButton);
					td1.Buttons.Add(noButton);
					var result1 = td1.ShowDialog(this);
					if (result1.ButtonType == ButtonType.No)
						return;
					DialogResult = true;
					break;
			}
		}

		private void AccountWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
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
		public AccountWindowView View {
			get {
				return LayoutRoot.GetView<AccountWindowView>();
			}
		}
		#endregion Public Properties

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
			sender.As<TextBox>().SelectAll();
		}
	}
}
