using System;

namespace Life.Savings.Events
{
    public delegate void ShowClientListHandler(object sender, ShowClientListEventArgs e);
    public class ShowClientListEventArgs : EventArgs
    {
        public ShowClientListEventArgs(bool isAddAllowed)
        {
            IsAddAllowed = isAddAllowed;
        }
        public bool IsAddAllowed { get; }
    }
}
