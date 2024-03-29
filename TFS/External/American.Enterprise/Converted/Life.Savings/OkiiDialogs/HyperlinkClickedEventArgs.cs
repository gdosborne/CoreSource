using System;
using System.Collections.Generic;
using System.Text;
namespace Ookii.Dialogs.Wpf
{
    public class HyperlinkClickedEventArgs : EventArgs
    {
        private string _href;
        public HyperlinkClickedEventArgs(string href)
        {
            _href = href;
        }
        public string Href
        {
            get { return _href; }
        }
    }
}
