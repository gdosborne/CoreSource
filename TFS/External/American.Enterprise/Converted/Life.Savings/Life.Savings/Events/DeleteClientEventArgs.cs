using System;

namespace Life.Savings.Events
{
    public delegate void DeleteClientHandler(object sender, DeleteClientEventArgs e);
    public class DeleteClientEventArgs : EventArgs
    {
        public bool IsContinueDelete { get; set; }
    }
}
