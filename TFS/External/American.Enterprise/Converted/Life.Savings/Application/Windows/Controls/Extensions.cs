using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Media;

namespace GregOsborne.Application.Windows.Controls {
    public static class Extensions {
        private static Delegate _conversionHandler;

        public static async void GetElementAsImageSourceAsync(this FrameworkElement rootElement, Delegate handler) {
            _conversionHandler = handler;
            var converter = new XamlToPngConverter();
            converter.ConversionComplete += Converter_ConversionComplete;
            await converter.Convert(rootElement);
        }

        public static void RemoveOverflow(this ToolBar value) {
            var overflowGrid = value.Template.FindName("OverflowGrid", value) as FrameworkElement;
            if (overflowGrid == null) return;
            overflowGrid.Visibility = Visibility.Collapsed;
            value.Margin = new Thickness(value.Margin.Left, value.Margin.Top, value.Margin.Right - 10, value.Margin.Bottom);
        }

        private static void Converter_ConversionComplete(object sender, ConversionCompleteEventArgs e) {
            _conversionHandler?.DynamicInvoke(sender, e);
        }
    }
}