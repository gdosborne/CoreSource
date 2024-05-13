/* File="WindowWrapper"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Windows.Interop;

namespace OzFramework.Windows {
    public class WindowWrapper : System.Windows.Forms.IWin32Window {
        public WindowWrapper(IntPtr handle) {
            Handle = handle;
        }

        public WindowWrapper(System.Windows.Window window) {
            Handle = new WindowInteropHelper(window).Handle;
        }

        private IntPtr _Handle = default;
        public IntPtr Handle {
            get => _Handle;
            private set => _Handle = value;
        }
    }
}
