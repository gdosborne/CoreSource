namespace My_Ministry
{
	using MyMinistry;
	using MyMinistry.Common;
	using MyMinistry.Views;
	using Windows.UI.Xaml.Controls;

	public sealed partial class MainPage : Page
	{
		#region Private Fields

		private NavigationHelper navigationHelper;

		#endregion Private Fields

		#region Public Constructors

		public MainPage()
		{
			this.InitializeComponent();
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += NavigationHelper_LoadState;
			this.navigationHelper.SaveState += NavigationHelper_SaveState;
		}

		#endregion Public Constructors

		#region Public Properties

		public MainPageView View { get { return this.DataContext as MainPageView; } }

		#endregion Public Properties

		#region Private Methods

		private void MainPageView_ExecuteUIAction(object sender, MyMinistry.Utilities.ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "ShowContacts":
					this.Frame.Navigate(typeof(ContactsPage));
					break;
			}
		}

		private void MainPageView_Navigate(object sender, MyMinistry.Utilities.NavigationEventArgs e)
		{
			this.Frame.Navigate(e.Type);
		}

		private void MainPageView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "SelectedContact":
					if (View.SelectedContact == null)
						return;
					MyMinistry.Utilities.CommonData.ContactToEdit = View.SelectedContact;
					this.Frame.Navigate(typeof(ContactPage));
					break;
			}
		}

		private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
		}

		private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
		{
		}

		#endregion Private Methods
	}
}
