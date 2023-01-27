using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Applicationn {
    public class NativeMethods {
        internal const int GwlStyle = -16;
        internal const int WsMaximizebox = 0x10000;
        internal const int WsMinimizebox = 0x20000;
        internal const int WsSysmenu = 0x80000;

        public static string PathShortener(string path, int length) {
            var sb = new StringBuilder(length + 1);
            PathCompactPathEx(sb, path, length, 0);
            return sb.ToString();
        }

        [DllImport("user32.dll")]
        internal static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        internal static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hwnd, int index, int value);
    }
}