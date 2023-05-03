using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Logging;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialog;
using Life.Savings.Extensions;

namespace Life.Savings {
    public partial class PremiumCalculationWindow : Window
    {
        private readonly bool _isInitializing;

        public PremiumCalculationWindow()
        {
            Logger.LogMessage($"Loading premium calculation window.");
            InitializeComponent();
            _isInitializing = true;

            Loaded += PremiumCalculationWindow_Loaded;
            SizeChanged += PremiumCalculationWindow_SizeChanged;
            LocationChanged += PremiumCalculationWindow_LocationChanged;

            var rect = App.GetWindowBounds("PremiumWindow", 825, this.Height);

            this.Position(rect.Left, rect.Top);
            Width = rect.Width;

            _isInitializing = false;
        }

        private void PremiumCalculationWindow_LocationChanged(object sender, EventArgs e)
        {
            App.SavePosition(_isInitializing, "PremiumWindow", this);
        }

        private void PremiumCalculationWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.SavePosition(_isInitializing, "PremiumWindow", this);
        }

        private void PremiumCalculationWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        private void PremiumCalculationWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult", StringComparison.OrdinalIgnoreCase))
                DialogResult = DataContext.As<PremiumCalculationWindowView>().DialogResult;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void PremiumCalculationWindowView_ShowMessage(object sender, Events.ShowMessageEventArgs e)
        {
            var td = new TaskDialog
            {
                Image = e.ImageType,
                MessageText = e.Text,
                Title = e.Title,
                Width = e.Width,
                Height = e.Height
            };
            td.AddButtons(ButtonTypes.OK);
            e.Result = (ButtonTypes)td.ShowDialog(this);
        }

        private void PremiumCalculationWindowView_SetCursor(object sender, Events.SetCursorEventArgs e) {
            Cursor = e.Cursor;
        }

        private void PremiumCalculationWindowView_ShowIllustration(object sender, EventArgs e) {
            var win = new IllustrateWindow {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Owner = this
            };
            win.ShowDialog();
        }
    }
}
