using Common.Application.Primitives;
using CongregationExtension.ViewModels;
using CongregationManager.Data;
using Ookii.Dialogs.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TerritoryManager.Extension.ViewModels;
using static ApplicationFramework.Dialogs.Helpers;

namespace TerritoryManager.Extension {
    public partial class ExtensionControl : UserControl {
        public ExtensionControl() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.DeleteItem: {
                        var msg = $"Deleting a territory also deletes the history for the territory." +
                            $"\n\nDelete territory {View.SelectedTerritory.Number}?";
                        var result = ShowYesNoDialog("Delete Territory", msg, TaskDialogIcon.Warning, 200);
                        if (result) {
                            View.SelectedCongregation.Territories.Remove(View.SelectedTerritory);
                            View.DataManager.SaveCongregation(View.SelectedCongregation);
                            View.Refresh();
                        }
                        break;
                    }
                case LocalBase.Actions.CheckOutTerritory: {
                        var win = new CheckinoutTerritoryWindow();
                        win.View.Title = "Check out Territory";
                        win.View.IsMemberEnabled = true;
                        var result = win.ShowDialog();
                        if (!result.HasValue || !result.Value)
                            return;

                        var latestDate = View.SelectedTerritory.History.Max(x => x.CheckInDate);
                        if (win.View.SelectedDate < latestDate) {
                            ShowOKDialog("Invalid Date", "The new check out date cannot be before the " +
                                "last checked in date.",
                                TaskDialogIcon.Information, 150);
                            return;
                        }

                        var history = new TerritoryHistory {
                            CheckOutDate = win.View.SelectedDate,
                            CheckedOutBy = win.View.SelectedMember,
                            CheckedOutByID = win.View.SelectedMember.ID,
                            Notes = win.View.Notes
                        };
                        View.SelectedTerritory.History.Add(history);
                        View.DataManager.SaveCongregation(View.SelectedCongregation);
                        View.Refresh();
                        break;
                    }
                case LocalBase.Actions.CheckInTerritory: {
                        var history = View.SelectedTerritory.LastHistory;
                        var win = new CheckinoutTerritoryWindow();
                        win.View.Title = "Check in Territory";
                        win.View.IsMemberEnabled = false;
                        win.View.SelectedMember = history.CheckedOutBy;
                        win.View.Notes = history.Notes;
                        var result = win.ShowDialog();
                        if (!result.HasValue || !result.Value)
                            return;
                        history.CheckInDate = win.View.SelectedDate;
                        history.Notes = win.View.Notes;
                        View.DataManager.SaveCongregation(View.SelectedCongregation);
                        View.Refresh();
                        break;
                    }
                case LocalBase.Actions.ShowNotes: {

                        var win = new TerritoryWindow();

                        break;
                    }
                case LocalBase.Actions.ReverseCheckoutTerritory: {
                        var msg = $"Revesing a check out will remove the latest history record and make " +
                            $"the territory available for check out once again.\n\nReverse check out " +
                            $"for territory {View.SelectedTerritory.Number}?";
                        var result = ShowYesNoDialog("Reverse Territory Check out", msg, TaskDialogIcon.Warning, 200);
                        if (result) {
                            View.SelectedTerritory.History.Remove(View.SelectedTerritory.LastHistory);
                            View.DataManager.SaveCongregation(View.SelectedCongregation);
                            View.Refresh();
                        }
                        break;
                    }
                case LocalBase.Actions.ShowTerritoryHistory: {

                        break;
                    }
                default:
                    break;
            }
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) =>
            UpdateColumnsWidth(sender.As<ListView>());

        private void ListView_Loaded(object sender, RoutedEventArgs e) =>
            UpdateColumnsWidth(sender.As<ListView>());

        private void UpdateColumnsWidth(ListView lv) {
            var gv = lv.View.As<GridView>();
            if (lv.ActualWidth == double.NaN || lv.ActualWidth == 0)
                return;
            var otherWidth = 0.0;
            var offset = gv.Columns.Count * 6.75;
            gv.Columns.ToList().ForEach(x => {
                if (x == gv.Columns.Last()) {
                    var val = (lv.ActualWidth >= 0 ? lv.ActualWidth - otherWidth : 0) - offset;
                    x.Width = val >= 0 ? val : 20;
                }
                else
                    otherWidth += x.ActualWidth;
            });
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (View.SelectedTerritory.LastHistory == null ||
                    View.SelectedTerritory.LastHistory.CheckInDate.HasValue)
                View.CheckOutTerritoryCommand.Execute(null);
            else
                View.CheckInTerritoryCommand.Execute(null);
        }
    }
}
