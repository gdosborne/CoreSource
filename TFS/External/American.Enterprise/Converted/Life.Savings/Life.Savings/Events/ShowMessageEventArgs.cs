using GregOsborne.Dialog;
using System;

namespace Life.Savings.Events
{
    public delegate void ShowMessageHandler(object sender, ShowMessageEventArgs e);
    public class ShowMessageEventArgs : EventArgs
    {
        public ShowMessageEventArgs(string text, string title, double width, double height, ImagesTypes imageType)
        {
            Text = text;
            Title = title;
            Width = width;
            Height = height;
            ImageType = imageType;
        }
        public string Text { get; private set; }
        public string Title { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public ImagesTypes ImageType { get; private set; }
        public ButtonTypes Result { get; set; }
    }
}
