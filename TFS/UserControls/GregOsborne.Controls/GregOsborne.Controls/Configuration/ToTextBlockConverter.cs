using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using GregOsborne.Application;

namespace GregOsborne.Controls.Configuration {
    public delegate void FontSizeChangedHandler(object sender, FontSizeChangedEventArgs e);

    public class FontSizeChangedEventArgs : EventArgs {
        public FontSizeChangedEventArgs(double size) {
            Size = size;
        }

        public double Size { get; }
    }
    public abstract class ToTextBlockConverter {
        protected List<BrushData> Brushes;
        protected XDocument XDoc;
        public TextBlock TextBlock { get; protected set; }
        public FontSizeChangedHandler FontSizeChanged;
        public int XmlTabSize { get; set; }
        public int CodeTabSize { get; set; }
        protected void TextBlock_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl)) return;
            //var current = Settings.GetSetting("SnippetManager", "Editor", "LastFontSize", 14.0);
            var value = TextBlock.FontSize + (e.Delta > 0 ? 1.0 : -1.0);
            if (value < 6.0) value = 6.0;
            if (value > 50.0) value = 50.0;
            if (Math.Abs(current - value) < 0.0) return;
            TextBlock.FontSize = value;
            //Settings.SetSetting("SnippetManager", "Editor", "LastFontSize", value);
            e.Handled = true;
            FontSizeChanged?.Invoke(this, new FontSizeChangedEventArgs(value));
        }
    }
}