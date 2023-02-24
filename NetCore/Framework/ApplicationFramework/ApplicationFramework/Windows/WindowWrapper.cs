using System;
using System.Windows.Interop;

namespace Common.Application.Windows {
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
