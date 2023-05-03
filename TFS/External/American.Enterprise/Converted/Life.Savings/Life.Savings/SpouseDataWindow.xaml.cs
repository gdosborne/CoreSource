using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Logging;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using Life.Savings.Data.Model;
using Life.Savings.Extensions;

namespace Life.Savings
{
    public partial class SpouseDataWindow : Window
    {
        private readonly bool _isInitializing;
        public SpouseDataWindow()
        {
            InitializeComponent();
            Logger.LogMessage($"Loading spouse data window.");
            _isInitializing = true;

            Loaded += SpouseWindow_Loaded;
            SizeChanged += SpouseWindow_SizeChanged;
            LocationChanged += SpouseWindow_LocationChanged;

            var rect = App.GetWindowBounds("SpouseWindow", 825, this.Height);

            this.Position(rect.Left, rect.Top);
            Width = rect.Width;
            if (App.Illustration.SpouseAsClientData == null)
                App.Illustration.SpouseAsClientData = new IndividualData();
            _isInitializing = false;
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
        private void SpouseWindow_LocationChanged(object sender, EventArgs e)
        {
            App.SavePosition(_isInitializing, "SpouseWindow", this);
        }
        public SpouseDataWindowView View => DataContext.As<SpouseDataWindowView>();
        private void SpouseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.Repository = App.Repository;
        }

        private void SpouseWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.SavePosition(_isInitializing, "SpouseWindow", this);
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }

        private void SpouseDataWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult"))
                DialogResult = View.DialogResult;
        }

        private void SpouseDataWindowView_ShowPremiumCalc(object sender, EventArgs e)
        {
            var win = new PremiumCalculationWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.DataContext.As<PremiumCalculationWindowView>().IsForSpouse = true;
            win.ShowDialog();
        }
    }
}
