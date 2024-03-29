namespace InnoUI
{
	using GregOsborne.Application.Primitives;
	using GregOsborne.Dialog;
	using Microsoft.Win32;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	public partial class MainWindow : Window
	{
		#region Public Constructors
		public MainWindow()
		{
			InitializeComponent();

			View.InitView();

			LayoutRoot.Focus();
		}
		#endregion Public Constructors

		#region Private Methods
		private void MainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "AskSave":
					var askSaveDialog = new TaskDialog
					{
						AdditionalInformation = View.File.FullPath,
						AllowClose = false,
						Image = ImagesTypes.Question,
						MessageText = "The file has changes.\n\nWould you like to save the file?",
						Title = "File changed...",
						Width = 350
					};
					askSaveDialog.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
					e.Parameters["result"] = (ButtonTypes)askSaveDialog.ShowDialog(this) == ButtonTypes.Yes;
					break;
				case "OpenFile":
					var openDialog = new OpenFileDialog
					{
						AddExtension = true,
						CheckFileExists = true,
						DefaultExt = "iss",
						Filter = "Inno Setup Files|*.iss",
						InitialDirectory = View.LastOpenPath,
						Multiselect = false,
						Title = "Open file..."
					};
					var openResult = openDialog.ShowDialog(this);
					if (openResult.GetValueOrDefault())
					{
						View.DataVisibility = Visibility.Visible;
						e.Parameters["cancel"] = false;
						e.Parameters["filename"] = openDialog.FileName;
						//e.Parameters["width"] = ScriptBox.ActualWidth;
					}
					break;
			}
		}
		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "File":
					//if (View.File == null)
					//	ScriptBox.Text = string.Empty;
					//else
					//	ScriptBox.Text = View.File.Text;
					break;
				case "IsShowScriptChecked":
					if (!View.IsShowScriptChecked)
					{
						previousWidth = MiddleGrid.ColumnDefinitions[0].ActualWidth;
						MiddleGrid.ColumnDefinitions[0].MinWidth = 0;
						MiddleGrid.ColumnDefinitions[0].Width = new GridLength(0);
					}
					else
					{
						MiddleGrid.ColumnDefinitions[0].MinWidth = 300;
						MiddleGrid.ColumnDefinitions[0].Width = new GridLength(previousWidth);
					}
					break;
			}
		}
		private void ScriptBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
			{
				View.FontSize += (View.FontSize > 8.01 && e.Delta < 0) ? -1 : (View.FontSize < 63.99 && e.Delta > 0) ? 1 : 0;
				e.Handled = true;
			}
		}
		private void ScriptBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (View.File != null)
				View.File.ControlWidth = sender.As<ListBox>().ActualWidth;
		}
		private void ScriptBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (View.File != null)
				View.File.IsDirty = true;
		}
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			View.Persist(this);
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.Initialize(this);
		}
		#endregion Private Methods

		#region Private Fields
		private double previousWidth = 0.0;
		#endregion Private Fields

		#region Public Properties
		public MainWindowView View { get { return LayoutRoot.GetView<MainWindowView>(); } }
		#endregion Public Properties
	}
}
