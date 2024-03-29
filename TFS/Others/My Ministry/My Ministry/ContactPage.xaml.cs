using MyMinistry.Common;
using MyMinistry.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MyMinistry
{
	public sealed partial class ContactPage : Page
	{
		#region Private Fields

		private ObservableDictionary defaultViewModel = new ObservableDictionary();
		private NavigationHelper navigationHelper;

		#endregion Private Fields

		#region Public Constructors

		public ContactPage()
		{
			this.InitializeComponent();
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += navigationHelper_LoadState;
			this.navigationHelper.SaveState += navigationHelper_SaveState;
		}

		#endregion Public Constructors

		#region Public Properties

		public ObservableDictionary DefaultViewModel {
			get { return this.defaultViewModel; }
		}

		public NavigationHelper NavigationHelper {
			get { return this.navigationHelper; }
		}

		public ContactPageView View { get { return this.DataContext as ContactPageView; } }

		#endregion Public Properties

		#region Protected Methods

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			navigationHelper.OnNavigatedFrom(e);
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			navigationHelper.OnNavigatedTo(e);
		}

		#endregion Protected Methods

		#region Private Methods

		private void ContactPageView_Close(object sender, EventArgs e)
		{
			this.navigationHelper.GoBack();
		}

		private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

		private void pageRoot_Loaded(object sender, RoutedEventArgs e)
		{
			View.Contact = MyMinistry.Utilities.CommonData.ContactToEdit;
			View.UpdateInterface();
		}

		#endregion Private Methods
	}
}
