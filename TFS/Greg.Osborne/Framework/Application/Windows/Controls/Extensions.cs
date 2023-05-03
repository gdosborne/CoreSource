using System;
using System.Windows;
using System.Windows.Controls;
using GregOsborne.Application.Media;

namespace GregOsborne.Application.Windows.Controls {
    public static class Extensions {
        private static Delegate _conversionHandler;

#if !DOTNET3_5
        public static async void GetElementAsImageSourceAsync(this FrameworkElement rootElement, Delegate handler) {
            _conversionHandler = handler;
            var converter = new XamlToPngConverter();
            converter.ConversionComplete += Converter_ConversionComplete;
            await converter.Convert(rootElement);
        }
#endif

        public static void RemoveOverflow(this ToolBar value) {
            if (!(value.Template.FindName("OverflowGrid", value) is FrameworkElement overflowGrid))
                return;
            overflowGrid.Visibility = Visibility.Collapsed;
        }

        private static void Converter_ConversionComplete(object sender, ConversionCompleteEventArgs e) {
            _conversionHandler?.DynamicInvoke(sender, e);
        }
    }
}