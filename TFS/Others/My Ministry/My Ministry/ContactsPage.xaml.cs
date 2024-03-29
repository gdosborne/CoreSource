namespace MyMinistry
{
	using MyMinistry.Common;
	using MyMinistry.Views;
	using System;
	using Windows.UI.Xaml;
	using Windows.UI.Xaml.Controls;
	using Windows.UI.Xaml.Navigation;

	public sealed partial class ContactsPage : Page
	{
		#region Private Fields

		private ObservableDictionary defaultViewModel = new ObservableDictionary();
		private NavigationHelper navigationHelper;

		#endregion Private Fields

		#region Public Constructors

		public ContactsPage()
		{
			this.InitializeComponent();
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += navigationHelper_LoadState;
			this.navigationHelper.SaveState += navigationHelper_SaveState;
		}

		#endregion Public Constructors

		#region Public Properties

		public NavigationHelper NavigationHelper {
			get { return this.navigationHelper; }
		}

		public ContactsPageView View { get { return this.DataContext as ContactsPageView; } }

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

		private void ContactsPageView_ExecuteUIAction(object sender, Utilities.ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "AddContact":
					this.Frame.Navigate(typeof(ContactPage));
					break;
			}
		}

		private void ContactsPageView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "IsCompactButtons":
					break;

				case "SelectedContact":
					MyMinistry.Utilities.CommonData.ContactToEdit = View.SelectedContact;
					this.Frame.Navigate(typeof(ContactPage));
					break;
			}
		}

		private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

		private void pageRoot_Loaded(object sender, RoutedEventArgs e)
		{
			View.Init();
			View.UpdateInterface();
		}

		#endregion Private Methods
	}
}
