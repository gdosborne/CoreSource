using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Logging;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using Life.Savings.Extensions;

namespace Life.Savings
{
    public partial class FutureYearsWindow : Window
    {
        private readonly bool _isInitializing;
        public FutureYearsWindow()
        {
            InitializeComponent();
            Logger.LogMessage($"Loading future years window.");
            _isInitializing = true;

            Loaded += FutureYearsWindow_Loaded;
            SizeChanged += FutureYearsWindow_SizeChanged;
            LocationChanged += FutureYearsWindow_LocationChanged;

            var rect = App.GetWindowBounds("FutureYearsWindow", 825, 500);

            this.Position(rect.Left, rect.Top);
            Width = rect.Width;

            _isInitializing = false;
        }
        private void FutureYearsWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult", StringComparison.OrdinalIgnoreCase))
                DialogResult = DataContext.As<FutureYearsWindowView>().DialogResult;
            else if (e.PropertyName.Equals("Age") || e.PropertyName.Equals("Value") || e.PropertyName.Equals("EndAge"))
                App.Illustration.IsChanged = true;
        }
        private void FutureYearsWindow_LocationChanged(object sender, EventArgs e)
        {
            App.SavePosition(_isInitializing, "FutureYearsWindow", this);
        }

        private void FutureYearsWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.SavePosition(_isInitializing, "FutureYearsWindow", this);
        }

        private void FutureYearsWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
            this.HideControlBox();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }
    }
}
