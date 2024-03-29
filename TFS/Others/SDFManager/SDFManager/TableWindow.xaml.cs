namespace SDFManager
{
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using GregOsborne.Application.Windows;
	using GregOsborne.Application.Primitives;
	using System.Windows.Controls;

	public partial class TableWindow : Window
	{
		#region Public Constructors
		public TableWindow()
		{
			InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
		}
		#endregion Public Constructors

		#region Public Properties
		public TableWindowView View { get { return LayoutRoot.GetView<TableWindowView>(); } }
		#endregion Public Properties

		private void TableWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "DialogResult":
					DialogResult = View.DialogResult;
					break;
			}
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			sender.As<TextBox>().SelectAll();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
		}
	}
}
