using System;
using System.Collections.Generic;
using System.Windows;
using MyApplication.Windows;
using MVVMFramework;
using OptiRampDesktop.Views;

namespace OptiRampDesktop.Windows
{
	public partial class PropertiesWindow : Window
	{
		#region Public Constructors

		public PropertiesWindow()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties
		public PropertiesWindowView View
		{
			get
			{
				return LayoutRoot.GetView<PropertiesWindowView>();
			}
		}
		#endregion

		#region Protected Methods

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			this.HideControlBox();
			this.HideMinimizeAndMaximizeButtons();
		}

		#endregion

		#region Private Methods

		private void PropertiesWindowView_CancelRequest(object sender, EventArgs e)
		{
			DialogResult = false;
		}

		private void PropertiesWindowView_OKRequest(object sender, EventArgs e)
		{
			DialogResult = true;
		}

		#endregion
	}
}