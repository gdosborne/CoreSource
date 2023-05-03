using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Logging;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialog;
using Life.Savings.Extensions;

namespace Life.Savings
{
    public partial class AdditionalInsuredWindow : Window
    {
        private readonly bool _isInitializing;
        public AdditionalInsuredWindow()
        {
            InitializeComponent();
            Logger.LogMessage($"Loading additional insureds window.");
            _isInitializing = true;

            Loaded += AdditionalInsuredWindow_Loaded;
            SizeChanged += AdditionalInsuredWindow_SizeChanged;
            LocationChanged += AdditionalInsuredWindow_LocationChanged;

            var rect = App.GetWindowBounds("AdditionalInsuredWindow", 825, 500);

            this.Position(rect.Left, rect.Top);
            Width = rect.Width;

            _isInitializing = false;
        }
        private void AdditionalInsuredWindow_LocationChanged(object sender, EventArgs e)
        {
            App.SavePosition(_isInitializing, "AdditionalInsuredWindow", this);
        }

        private void AdditionalInsuredWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            App.SavePosition(_isInitializing, "AdditionalInsuredWindow", this);
        }

        private void AdditionalInsuredWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        private void AdditionalInsuredWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DialogResult", StringComparison.OrdinalIgnoreCase))
                DialogResult = DataContext.As<AdditionalInsuredWindowView>().DialogResult;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }

        private void AdditionalInsuredWindowView_ShowMessage(object sender, Events.ShowMessageEventArgs e)
        {
            var td = new TaskDialog
            {
                Image = e.ImageType,
                MessageText = e.Text,
                Title = e.Title,
                Width = e.Width,
                Height = e.Height,
            };
            td.AddButtons(ButtonTypes.OK);
            td.ShowDialog(this);
        }
    }
}
