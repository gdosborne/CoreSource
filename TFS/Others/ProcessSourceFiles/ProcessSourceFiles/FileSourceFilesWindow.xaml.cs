using MVVMFramework;
using GregOsborne.Application.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace ProcessSourceFiles
{
	public partial class FindSourceFilesWindow : Window
	{
		public FindSourceFilesWindow()
		{
			InitializeComponent();
		}
		protected override void OnSourceInitialized(EventArgs e)
		{
			this.HideMinimizeAndMaximizeButtons();
			this.HideControlBox();
			base.OnSourceInitialized(e);
		}
		private void FindSourceFilesWindowView_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "SelectTopFolder":
					var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
					{
						Description = "Select folder...",
						UseDescriptionForTitle = true,
						ShowNewFolderButton = false,
						RootFolder = Environment.SpecialFolder.MyDocuments,
						SelectedPath = string.IsNullOrEmpty(View.TopFolder) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : View.TopFolder
					};
					var dlgResult = dlg.ShowDialog(this);
					if (!dlgResult.GetValueOrDefault())
						return;
					View.TopFolder = dlg.SelectedPath;
					break;
				case "CloseWindow":
					this.DialogResult = (bool)e.Parameters["result"];
					break;
			}
		}
		public FindSourceFilesWindowView View
		{
			get { return LayoutRoot.GetView<FindSourceFilesWindowView>(); }
		}
	}
}
