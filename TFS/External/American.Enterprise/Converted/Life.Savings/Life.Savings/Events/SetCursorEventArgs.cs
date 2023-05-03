using System;
using System.Windows.Input;

namespace Life.Savings.Events {
    public delegate void SetCursorHandler(object sender, SetCursorEventArgs e);
    public class SetCursorEventArgs : EventArgs {
        public SetCursorEventArgs(Cursor c) {
            Cursor = c;
        }
        public Cursor Cursor { get; private set; }
    }
}
