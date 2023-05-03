namespace Territory.Checkout {
	using Common.OzApplication.Primitives;
	using Common.OzApplication.Windows;
	using Common.OzApplication.Windows.Controls;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;
	using System.Windows.Media;
	using System.Windows.Threading;
	using Territory.Checkout.Data;
	using Territory.Checkout.ViewModels;
	using Universal.Common;

	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			Closing += MainWindow_Closing;
			theStoryboard.Begin();
			View.Initialize();
			var dt = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			dt.Tick += Dt_Tick;
			dt.Start();
		}

		private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) {
			this.SaveBounds(App.AppSession.ApplicationSettings, true);
			App.AppSession.ApplicationSettings.AddOrUpdateSetting(
				this.GetType().Name, "LeftColWidth", leftColumn.ActualWidth);
		}

		private void Dt_Tick(object? sender, EventArgs e) {
			if (View.Checkouts == null || !View.Checkouts.Any()) return;
			sender.As<DispatcherTimer>().Stop();
			View.SpinnerVisibility = Visibility.Collapsed;
		}

		protected override void OnSourceInitialized(EventArgs e) {
			base.OnSourceInitialized(e);
			areaToolbar.RemoveOverflow();
			territoryToolbar.RemoveOverflow();
			personToolbar.RemoveOverflow();
			this.SetBounds(App.AppSession.ApplicationSettings, true);
			var leftColWidth = App.AppSession.ApplicationSettings.GetValue(
				this.GetType().Name, "LeftColWidth", 200.0);
			leftColumn.Width = new GridLength(leftColWidth, GridUnitType.Pixel);
		}

		internal MainWindowView View => DataContext.As<MainWindowView>();

		private async void MainWindowView_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
			var action = (MainWindowView.Actions)Enum.Parse(typeof(MainWindowView.Actions), e.CommandToExecute);
			switch (action) {
				case MainWindowView.Actions.AskDeleteArea: {
						var isUnassignedArea = View.SelectedAreaItem.Name == "Unassigned";
						var title = "Delete Area";
						var content = isUnassignedArea ?
							$"You cannot delete the {View.SelectedAreaItem.Name} Area."
							: $"Any territories assigned to that area will be moved to the " +
							$"Unassigned area.\n\nAre you sure you want to delete this area?";
						var mainText = $"Delete the {View.SelectedAreaItem.Name} Area?";
						if (isUnassignedArea) {
							App.DisplayOKDialog(this, title, content, mainText);
							return;
						}
						var result = App.DisplayYesNoDialog(this, title, content, mainText);
						e.Parameters["Cancel"] = result;
					}
					break;
				case MainWindowView.Actions.AskDeleteTerritory: {
						var title = "Delete Territory";
						var content = $"Any check out records for this territory will be deleted " +
							$"also.\n\nAre you sure you want to delete this Territory?";
						var mainText = $"You are about to delete territory " +
							$"#{View.SelectedTerritoryItem.Number}";
						var result = App.DisplayYesNoDialog(this, title, content, mainText);
						e.Parameters["Cancel"] = result;
					}
					break;
				case MainWindowView.Actions.AskDeletePerson: {
						var title = "Delete Person";
						var content = $"Any check out records for this person will be deleted " +
							$"also.\n\nAre you sure you want to delete {View.SelectedPersonItem.FullName}?";
						var mainText = $"You are about to delete " +
							$"{View.SelectedPersonItem.FullName}";
						var result = App.DisplayYesNoDialog(this, title, content, mainText);
						e.Parameters["Cancel"] = result;
					}
					break;
				case MainWindowView.Actions.ShowCheckout: {
						var atScreen = (string)e.Parameters["screen"];
						var win = new CheckoutWindow {
							Owner = this,
							WindowStartupLocation = WindowStartupLocation.CenterOwner
						};
						win.View.CheckoutDate = DateTime.Now;
						win.View.Persons = new ObservableCollection<Data.PersonItem>(
							View.Persons.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));

						var territories = new List<TerritoryItem>();
						View.Checkouts.ToList().ForEach(co => {
							var lastCo = View.Checkouts
								.Where(x => x.Territory.ID == co.Territory.ID)
								.OrderByDescending(x => x.CheckedOut)
								.FirstOrDefault();
							if (lastCo != null && lastCo.CheckedIn.HasValue)
								territories.Add(lastCo.Territory);
						});

						win.View.Territories = new ObservableCollection<Data.TerritoryItem>(
							territories.OrderBy(x => x.Number.PadLeft(3, '0')));

						if (atScreen.Equals("person", StringComparison.OrdinalIgnoreCase))
							win.View.CheckoutBy = View.SelectedPersonItem;
						if (atScreen.Equals("territory", StringComparison.OrdinalIgnoreCase))
							win.View.Territory = View.SelectedTerritoryItem;

						var result = win.ShowDialog();
						if (!result.HasValue || !result.Value) return;
						var item = await CheckoutItem.Add(win.View.Territory.ID.CastTo<int>(),
							win.View.CheckoutBy.ID.CastTo<int>(), win.View.CheckoutDate.Value);
						if (item != null) {
							View.Checkouts.Add(item);
							View.ItemsNeedingWorked.Remove(View.ItemsNeedingWorked.FirstOrDefault(x => x.Territory.ID == item.Territory.ID));
						}
					}
					break;
				case MainWindowView.Actions.AskCheckIn: {
						var result = App.DisplayYesNoDialog(this, "Check in",
							"Would you like to check in this territory?", "Check in Territory");
						e.Parameters["cancel"] = !result;
					}
					break;
				case MainWindowView.Actions.MoveToSelectedTerritory: {
						territoryLB.ScrollIntoView(View.SelectedTerritoryItem);
					}
					break;
				case MainWindowView.Actions.ShowReports: {

					}
					break;
				case MainWindowView.Actions.ShowSettings: {
						var win = new ApplicationSettingsWindow {
							Owner = this,
							WindowStartupLocation = WindowStartupLocation.CenterOwner,
						};
						win.ShowDialog();
						if(win.View.NeedsWorkRequiresRefresh) {
							View.NeedsWorkedRefresh();
						}
					}
					break;
			}
		}

		private void MainWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(View.SelectedAreaItem)) {
				View.IsAreaItemEnabled = View.SelectedAreaItem != null;
				areaNameTB.Focus();
				AreaLB.ScrollToCenterOfView(View.SelectedAreaItem);
			}
			else if (e.PropertyName == nameof(View.SelectedTerritoryItem)) {
				View.IsTerritoryItemEnabled = View.SelectedTerritoryItem != null;
				territoryNumberTB.Focus();
				territoryLB.ScrollToCenterOfView(View.SelectedTerritoryItem);
			}
			else if (e.PropertyName == nameof(View.SelectedPersonItem)) {
				View.IsPersonItemEnabled = View.SelectedPersonItem != null;
				personTB.Focus();
				personLB.ScrollToCenterOfView(View.SelectedPersonItem);
			}
		}

		private void TextBox_GotFocus(object sender, RoutedEventArgs e) =>
			sender.As<TextBox>().SelectAll();

		private void Border_SizeChanged(object sender, SizeChangedEventArgs e) {
			if (e.HeightChanged) {
				var h = e.NewSize.Height;
				var expheights = GetExpanderHeaderHeight(expArea)
					+ GetExpanderHeaderHeight(expTerritory)
					+ GetExpanderHeaderHeight(expPerson);
				AreaLB.Height = personLB.Height = territoryLB.Height = h - expheights - 20;
			}
		}

		private double GetExpanderHeaderHeight(Expander expander) {
			var border = VisualTreeHelper.GetChild(expander, 0);
			var dockpanel = VisualTreeHelper.GetChild(border, 0);
			var count = VisualTreeHelper.GetChildrenCount(dockpanel);
			for (int i = 0; i < count; i++) {
				var c = VisualTreeHelper.GetChild(dockpanel, i);
				if (c is ToggleButton) {
					var toggle = (ToggleButton)c;
					return toggle.ActualHeight;
				}
			}
			return 0;
		}

		private void Border_SizeChanged_1(object sender, SizeChangedEventArgs e) {
			if (e.HeightChanged) {
				var h = e.NewSize.Height;
				var personHeight =
					personToolbar.ActualHeight +
					lblLast.ActualHeight +
					personTB.ActualHeight + 40;
				if (h - personHeight - 10 > 0) checkoutDataGrid.Height = h - personHeight;

				var otherItemsHeight =
					territoryToolbar.ActualHeight +
					lblNumber.ActualHeight +
					territoryNumberTB.ActualHeight +
					checkoutTBlock.ActualHeight +
					needsWorkHeader.ActualHeight + 70;
				if (h - otherItemsHeight > 0) needsWorkDataGrid.Height = h - otherItemsHeight;
			}
		}
	}
}
