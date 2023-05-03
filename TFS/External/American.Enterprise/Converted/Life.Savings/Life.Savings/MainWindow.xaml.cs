using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialog;
using Life.Savings.Data.Model;
using Life.Savings.Events;
using Life.Savings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using GregOsborne.Application.Logging;

namespace Life.Savings
{
    public partial class MainWindow
    {
        private readonly bool _isInitializing;

        public MainWindow()
        {
            InitializeComponent();

            _isInitializing = true;

            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
            LocationChanged += MainWindow_LocationChanged;

            var rect = App.GetWindowBounds("MainWindow", 940, this.Height);

            this.Position(rect.Left, rect.Top);
            Width = rect.Width;

            _isInitializing = false;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
        public MainWindowView View => DataContext.As<MainWindowView>();

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            App.SavePosition(_isInitializing, "MainWindow", this);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.Repository = App.Repository;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.SavePosition(_isInitializing, "MainWindow", this);
        }

        private void View_ShowSettings(object sender, ShowSettingsEventArgs e)
        {
            var settingsDialog = new SettingsWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            settingsDialog.View.Repository = View.Repository;
            settingsDialog.ShowDialog();
            if (DataContext.As<MainWindowView>().PreparedBy != Settings.GetSetting(App.ApplicationName, "Preparer", "Name", Environment.UserName))
                DataContext.As<MainWindowView>().PreparedBy = Settings.GetSetting(App.ApplicationName, "Preparer", "Name", Environment.UserName);
            if (DataContext.As<MainWindowView>().ContactTelephoneNumber != Settings.GetSetting(App.ApplicationName, "Preparer", "PhoneNumber", string.Empty).FormatAsPhoneNumber())
                DataContext.As<MainWindowView>().ContactTelephoneNumber = Settings.GetSetting(App.ApplicationName, "Preparer", "PhoneNumber", string.Empty).FormatAsPhoneNumber();
        }

        private void MainWindowView_WarnToRefresh(object sender, WarningToRefreshEventArgs e)
        {
            var shouldAskAboutRefresh = !Settings.GetSetting(App.ApplicationName, "Application", "DoNotAskAboutRefresh", false);
            if (shouldAskAboutRefresh)
            {
                var cb = App.GetAdditionalCheckBox("Do not ask me this question again.");
                var td = new TaskDialog
                {
                    Image = ImagesTypes.Warning,
                    MessageText = "This function requires that the page be cleared after the refresh of the data.\n\nDo you want to refresh the static data?",
                    Title = "Static data refresh",
                    Width = 400,
                    Height = 200,
                    AdditionalControl = cb,
                    AdditionalControlPosition = AdditionalControlPositions.ButtonArea
                };
                td.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
                var result = td.ShowDialog(this);
                e.Answer = result == (int)ButtonTypes.Yes;
                if (e.Answer.Value) Settings.SetSetting(App.ApplicationName, "Application", "DoNotAskAboutRefresh", cb.IsChecked.HasValue && cb.IsChecked.Value);
            }
            else
            {
                e.Answer = true;
            }
        }

        private void MainWindowView_AskToClear(object sender, AskToClearEventArgs e)
        {
            var shouldAskAboutClear = !Settings.GetSetting(App.ApplicationName, "Application", "DoNotAskAboutClear", false);
            if (shouldAskAboutClear)
            {
                var cb = App.GetAdditionalCheckBox("Do not ask me this question again.");
                var td = new TaskDialog
                {
                    Image = ImagesTypes.Warning,
                    MessageText = "You are about to clear all data from this form.\n\nDo you want to clear the data?",
                    Title = "Clear data",
                    Width = 400,
                    Height = 200,
                    AdditionalControl = cb,
                    AdditionalControlPosition = AdditionalControlPositions.ButtonArea
                };
                td.AddButtons(ButtonTypes.Yes, ButtonTypes.No);
                var result = td.ShowDialog(this);
                e.Answer = result == (int)ButtonTypes.Yes;
                if (e.Answer.Value) Settings.SetSetting(App.ApplicationName, "Application", "DoNotAskAboutClear", cb.IsChecked.HasValue && cb.IsChecked.Value);
            }
            else
            {
                e.Answer = true;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }
        //private DispatcherTimer askForDataSetTimer = null;
        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var isAskForDataSetChecked = Settings.GetSetting(App.ApplicationName, "Application", "AskForDataSetOnStart", true);
            //if (isAskForDataSetChecked)
            //{
            //    askForDataSetTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
            //    askForDataSetTimer.Tick += AskForDataSetTimer_Tick;
            //    askForDataSetTimer.Start();
            //}
            ClientFirstNameTextBox.Focus();
        }

        //private void AskForDataSetTimer_Tick(object sender, EventArgs e)
        //{
        //    askForDataSetTimer.Stop();
        //    var win = new AskForDataSetWindow
        //    {
        //        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        //        Owner = this
        //    };
        //    var result = win.ShowDialog();
        //    //App.Illustration = GetNewIllustrationInfo();

        //    //if (!result.HasValue || !result.Value)
        //    //    App.CurrentDataSet = App.Repository.Ls2Data;
        //    //else
        //    //    App.CurrentDataSet = win.DataContext.As<AskForDataSetWindowView>().Ls2DataSelected ? App.Repository.Ls2Data : App.Repository.Ls3Data;
        //    this.DataContext.As<MainWindowView>().Reload();
        //}

        private void Border_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var ctx = sender.As<Border>().ContextMenu;
            if (ctx == null) return;
            ctx.IsOpen = true;
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.SettingsCommand.Execute(null);
        }

        private void LogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.LogFolderCommand.Execute(null);
        }

        private void RefreshMenuItem_Click(object sender, RoutedEventArgs e)
        {
            View.RefreshStaticDataCommand.Execute(null);
        }

        private void MainWindowView_ShowClientList(object sender, ShowClientListEventArgs e)
        {
            var win = new ClientWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            win.DataContext.As<ClientWindowView>().Repository = DataContext.As<MainWindowView>().Repository;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;
            Logger.LogMessage($"Selecting client ({win.DataContext.As<ClientWindowView>().SelectedClient.FullName}).");
            DataContext.As<MainWindowView>().PopulateWithClient(win.DataContext.As<ClientWindowView>().SelectedClient);
            DataContext.As<MainWindowView>().UpdateInterface();
        }
        private void SaveClientList(out string errorMessage)
        {
            errorMessage = null;
            if (string.IsNullOrEmpty(DataContext.As<MainWindowView>().ClientFirstName) || string.IsNullOrEmpty(DataContext.As<MainWindowView>().ClientLastName))
            {
                errorMessage = "First and last name is required to save client.";
                return;
            }
            Client tempClient;
            if (App.Repository.Clients.Any(x => x.Id == DataContext.As<MainWindowView>().EditingId))
                tempClient = App.Repository.Clients.First(x => x.Id == DataContext.As<MainWindowView>().EditingId);
            else
            {
                App.Repository.LastClientId++;
                tempClient = new Client(App.Repository.Genders, App.CurrentDataSet.LsClientPlans, App.CurrentDataSet.LsClientRiderOptions, App.CurrentDataSet.LsSubStandardRatings, App.CurrentDataSet.LsClientCOLAs)
                {
                    Id = App.Repository.LastClientId
                };
            }
            var illustration = App.Illustration;

            //update with everything from illustration
            tempClient.PremiumPlannedModalPremium = illustration.PlannedModalPremium;
            tempClient.IsSpousePrincipalInsured = illustration.IsSpousePrincipalInsured;
            tempClient.PremiumInitialLumpSumAmount = illustration.InitialLumpSumAmount;
            tempClient.IsChildRiderSelected = illustration.IsChildRiderSelected;
            tempClient.ChildRiderDeathBenefitAmount = illustration.ChildRiderDeathBenefitAmount;
            tempClient.ChildRiderYoungestAge = illustration.ChildRiderYoungestAge;
            //TODO: add the rest of the top level property values 

            tempClient.ClientData = illustration.ClientData;
            tempClient.SpouseData = illustration.SpouseAsClientData;
            tempClient.Riders.Clear();
            illustration.Riders.OrderBy(x => x.Index).Where(x => x.IsSelected && x.Age > 0).ToList().ForEach(rider => tempClient.Riders.Add(rider));
            tempClient.FutureSpecificDeathBenefits.Clear();
            tempClient.FutureModalPremiums.Clear();
            tempClient.FutureCurrentInterestRates.Clear();
            tempClient.FutureDeathBenefitOptions.Clear();
            tempClient.FutureWithdrawls.Clear();
            tempClient.FutureAnnualLoanRepayments.Clear();
            tempClient.FutureAnnualPolicyLoans.Clear();
            illustration.FutureSpecificDeathBenefits.ToList().ForEach(x => tempClient.FutureSpecificDeathBenefits.Add(x));
            illustration.FutureModalPremiums.ToList().ForEach(x => tempClient.FutureModalPremiums.Add(x));
            illustration.FutureCurrentInterestRates.ToList().ForEach(x => tempClient.FutureCurrentInterestRates.Add(x));
            illustration.FutureDeathBenefitOptions.ToList().ForEach(x => tempClient.FutureDeathBenefitOptions.Add(x));
            illustration.FutureWithdrawls.ToList().ForEach(x => tempClient.FutureWithdrawls.Add(x));
            illustration.FutureAnnualLoanRepayments.ToList().ForEach(x => tempClient.FutureAnnualLoanRepayments.Add(x));
            illustration.FutureAnnualPolicyLoans.ToList().ForEach(x => tempClient.FutureAnnualPolicyLoans.Add(x));

            Logger.LogMessage($"Saving modified client list.");
            DataContext.As<MainWindowView>().SaveToClientList(tempClient);
            DataContext.As<MainWindowView>().ChangedVisibility = Visibility.Collapsed;
            App.Illustration.Reset();
            DataContext.As<MainWindowView>().UpdateInterface();
        }
        private void SaveClientInfoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveClientList(out string message);
        }

        private void MainWindowView_ShowPremiumCalc(object sender, EventArgs e)
        {
            var win = new PremiumCalculationWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.ShowDialog();
            if (win.DataContext.As<PremiumCalculationWindowView>().IsInsuredRidersNext)
                MainWindowView_ShowAdditionalInsured(win.DataContext.As<PremiumCalculationWindowView>(), e);
            else if (win.DataContext.As<PremiumCalculationWindowView>().IsShowFutureChangesNext)
                MainWindowView_ShowFutureYears(win.DataContext.As<PremiumCalculationWindowView>(), e);
            DataContext.As<MainWindowView>().UpdateInterface();
        }

        private void MainWindowView_ShowAdditionalInsured(object sender, EventArgs e)
        {
            var win = new AdditionalInsuredWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.ShowDialog();
            if (win.DataContext.As<AdditionalInsuredWindowView>().IsShowPrmiumCalcNext)
                MainWindowView_ShowPremiumCalc(win.DataContext.As<AdditionalInsuredWindowView>(), e);
            else if (win.DataContext.As<AdditionalInsuredWindowView>().IsShowFutureChangesNext)
                MainWindowView_ShowFutureYears(win.DataContext.As<AdditionalInsuredWindowView>(), e);
            DataContext.As<MainWindowView>().UpdateInterface();
        }

        private void MainWindowView_ShowFutureYears(object sender, EventArgs e)
        {
            var win = new FutureYearsWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.ShowDialog();
            if (win.DataContext.As<FutureYearsWindowView>().IsInsuredRidersNext)
                MainWindowView_ShowAdditionalInsured(win.DataContext.As<FutureYearsWindowView>(), e);
            else if (win.DataContext.As<FutureYearsWindowView>().IsShowPrmiumCalcNext)
                MainWindowView_ShowPremiumCalc(win.DataContext.As<FutureYearsWindowView>(), e);
            DataContext.As<MainWindowView>().UpdateInterface();
        }

        private void MainWindowView_ShowProducts(object sender, EventArgs e)
        {
            var td = new TaskDialog
            {
                Image = ImagesTypes.Error,
                MessageText = "This action is not assigned any function.",
                Title = "Application error",
                Width = 400,
                Height = 200,
            };
            td.AddButtons(ButtonTypes.OK);
            var result = td.ShowDialog(this);
            DataContext.As<MainWindowView>().UpdateInterface();
        }

        private void MainWindowView_ShowCannotIllustrate(object sender, EventArgs e)
        {
            var td = new TaskDialog
            {
                Image = ImagesTypes.Error,
                MessageText = "You cannot illustrate until Age, Gender, Death Benefit, and Interest Rate contain values.",
                Title = "Application error",
                Width = 400,
                Height = 200,
            };
            td.AddButtons(ButtonTypes.OK);
            var result = td.ShowDialog(this);
        }

        private void MainWindowView_ShowSpouseData(object sender, EventArgs e)
        {
            IndividualData savedSpouseData = null;
            if (App.Illustration.SpouseAsClientData != null)
                savedSpouseData = App.Illustration.SpouseAsClientData.Clone().As<IndividualData>();
            var win = new SpouseDataWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.DataContext.As<SpouseDataWindowView>().Repository = DataContext.As<MainWindowView>().Repository;
            var result = win.ShowDialog();
            if (!result.HasValue || !result.Value && savedSpouseData != null)
                App.Illustration.SpouseAsClientData = savedSpouseData;
            DataContext.As<MainWindowView>().UpdateInterface();
        }

        private void MainWindowView_ShowIllustration(object sender, EventArgs e)
        {
            var win = new IllustrateWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.ShowDialog();
        }

        private void ThisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.Illustration.IsChanged)
            {
                var td = new TaskDialog
                {
                    Image = ImagesTypes.Warning,
                    MessageText = "The selected client data has changed. Do you want to save the client data before closing the application?",
                    Title = "Client data changed",
                    Width = 400,
                    Height = 200,
                };
                td.AddButtons(ButtonTypes.Yes, ButtonTypes.No, ButtonTypes.Cancel);
                var result = td.ShowDialog(this);
                if (result == (int)ButtonTypes.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (result == (int)ButtonTypes.No)
                    return;
                SaveClientList(out string message);
                if (!string.IsNullOrEmpty(message))
                {
                    e.Cancel = true;
                    td = new TaskDialog
                    {
                        Image = ImagesTypes.Error,
                        MessageText = message,
                        Title = "Error",
                        Width = 400,
                        Height = 200,
                    };
                    td.AddButtons(ButtonTypes.OK);
                    td.ShowDialog(this);
                }
            }
        }
    }
}