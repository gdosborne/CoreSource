/* File="CheckAccess"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace Common.MVVMFramework {
    public delegate void CheckAccessEventHandler(object sender, CheckAccessEventArgs e);

    public class CheckAccessEventArgs : EventArgs {

        #region Public Properties

        //public Dispatcher Dispatcher { get; set; }

        public bool HasAccess { get; set; }

        #endregion Public Properties
    
    }
}
