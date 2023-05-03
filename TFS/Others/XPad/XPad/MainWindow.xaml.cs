namespace XPad
{
	using GregOsborne.Application.Windows.Controls;
	using MVVMFramework;
	using Ookii.Dialogs.Wpf;
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using XPad.Views;
	using XPadLib;
	using GregOsborne.Application.Primitives;
	using XPad.Controls;

	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			WindowToolbar.RemoveOverflow();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
			View.Initialize(this);
		}

		public MainWindowView View { get { return this.GetView<MainWindowView>(); } }

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			View.Persist(this);
		}

		private void MainWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				switch (e.CommandToExecute)
				{
					case "RenameElement":
						//ElementsTreeView.SelectedItem.As<TreeViewItem>().Header.As<TreeViewItemHeader>().Title = ElementsTreeView.SelectedItem.As<TreeViewItem>().Header.As<TreeViewItemHeader>().Element.Name;
						ElementsTreeView.SelectedItem.As<TreeViewItem>().Header.As<TreeViewItemHeader>().RenameVisibility = Visibility.Visible;
						break;
					case "ClearTree":
						ElementsTreeView.Items.Clear();
						break;
					case "ShowOptions":
						var optWin = new OptionsWindow
						{
							Owner = this,
							WindowStartupLocation = WindowStartupLocation.CenterOwner
						};
						e.Parameters["result"] = optWin.ShowDialog();
						break;
					case "AddTreeItem":
						ElementsTreeView.Items.Add((TreeViewItem)e.Parameters["treeviewitem"]);
						break;
					case "OpenXmlFile":
						var fod = new VistaOpenFileDialog
						{
							AddExtension = false,
							CheckFileExists = true,
							Filter = "Xml files|*.xml",
							InitialDirectory = (string)e.Parameters["lastdirectory"],
							Multiselect = false,
							Title = "Open xml file..."
						};
						var result = fod.ShowDialog(this);
						e.Parameters["result"] = result.GetValueOrDefault();
						e.Parameters["filename"] = fod.FileName;
						break;
				}
			}
			else
				Dispatcher.BeginInvoke(new ExecuteUIActionHandler(MainWindowView_ExecuteUIAction), new object[] { sender, e });
		}

		private void TreeView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!View.Initializing)
				View.TreeWidth = TreeColumn.Width.Value;
		}

		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "TreeWidth":
					TreeColumn.Width = new GridLength(View.TreeWidth);
					break;
			}
		}

		private void ElementsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			View.SelectedElement = (XPadElement)((TreeViewItem)e.NewValue).Tag;
		}

		private void MainWindowView_StartRead(object sender, EventArgs e)
		{
			if (Dispatcher.CheckAccess())
				View.SpinnerVisibility = Visibility.Visible;
			else
				Dispatcher.BeginInvoke(new EventHandler(MainWindowView_StartRead), new object[] { sender, e });
		}

		private void MainWindowView_EndRead(object sender, EventArgs e)
		{
			if (Dispatcher.CheckAccess())
				View.SpinnerVisibility = Visibility.Collapsed;
			else
				Dispatcher.BeginInvoke(new EventHandler(MainWindowView_EndRead), new object[] { sender, e });
		}
	}
}
