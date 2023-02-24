using Common.Application.Primitives;
using Common.Application.Windows;
using CongregationExtension.ViewModels;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TerritoryManager.Extension.ViewModels;
using static ApplicationFramework.Dialogs.Helpers;

namespace TerritoryManager.Extension {
    public partial class BuildTerritoriesWindow : Window {
        public BuildTerritoriesWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
            Closing += BuildTerritoriesWindow_Closing;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            var action = (LocalBase.Actions)Enum.Parse(typeof(LocalBase.Actions), e.CommandToExecute);
            switch (action) {
                case LocalBase.Actions.CloseWindow: {
                        DialogResult = false;
                        break;
                    }
                case LocalBase.Actions.AcceptData: {
                        View.FinalResult = GenerateTerritories();
                        DialogResult = true;
                        break;
                    }
                case LocalBase.Actions.Generate: {
                        var tList = GenerateTerritories();
                        if (tList != null) {
                            var msg = "These are the territory numbers generated based upon the groupings " +
                                "you have provided:\n\n";
                            msg += string.Join(", ", tList);
                            ShowOKDialog("Territory numbers", msg, Ookii.Dialogs.Wpf.TaskDialogIcon.Information);
                        }
                        break;
                    }
            }
        }

        private List<string> GenerateTerritories() {
            var result = new List<string>();
            var exp = "[+-]?([0-9]*[.])?[0-9,A-Z]+";
            try {
                var groups = View.Groupings.Split(',');
                foreach (var group in groups) {
                    if (group.Contains("-")) {
                        var parts = group.Split('-');
                        var start = int.Parse(parts[0]);
                        var end = int.Parse(parts[1]);
                        if (start < end) {
                            for (int i = start; i <= end; i++) {
                                result.Add(i.ToString());
                            }
                        }
                    }
                    else {
                        if(Regex.IsMatch(group, exp)) {
                            result.Add(group);
                        }
                    }
                }
            }
            catch (Exception ex) {
                ShowOKDialog("Territory numbers", ex.Message, Ookii.Dialogs.Wpf.TaskDialogIcon.Warning);
            }
            return result;
        }

        private void BuildTerritoriesWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e) =>
            this.SaveBounds(View.AppSettings);

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.SetBounds(View.AppSettings);
        }

        public BuildTerritoriesWindowViewModel View => DataContext.As<BuildTerritoriesWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) =>
            DragMove();

        private void GroupTextBox_GotFocus(object sender, RoutedEventArgs e) =>
            sender.As<TextBox>().SelectAll();
    }
}
