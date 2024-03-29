using System;
using System.Windows.Threading;

namespace GregOsborne.MVVMFramework {
    public delegate void CheckAccessEventHandler(object sender, CheckAccessEventArgs e);

    public class CheckAccessEventArgs : EventArgs
    {
        #region Public Properties

        public Dispatcher Dispatcher { get; set; }

        public bool HasAccess { get; set; }

        #endregion Public Properties
    }
}
