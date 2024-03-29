using System;
using System.Collections.Generic;
using System.Text;
namespace Ookii.Dialogs.Wpf
{
    public class ExpandButtonClickedEventArgs : EventArgs
    {
        private bool _expanded;
        public ExpandButtonClickedEventArgs(bool expanded)
        {
            _expanded = expanded;
        }
        public bool Expanded
        {
            get { return _expanded; }
        }
    }
}
