namespace ProcessSourceFiles
{
	using MVVMFramework;
	using MyApplication.Windows;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	public partial class FindSourceFilesWindow : Window
	{
		#region Public Constructors
		public FindSourceFilesWindow()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Protected Methods
		protected override void OnSourceInitialized(EventArgs e)
		{
			this.HideMinimizeAndMaximizeButtons();
			this.HideControlBox();
			base.OnSourceInitialized(e);
		}
		#endregion Protected Methods

		#region Private Methods
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
		#endregion Private Methods

		#region Public Properties
		public FindSourceFilesWindowView View
		{
			get { return LayoutRoot.GetView<FindSourceFilesWindowView>(); }
		}
		#endregion Public Properties
	}
}
