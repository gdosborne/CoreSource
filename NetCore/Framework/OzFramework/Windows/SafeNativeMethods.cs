/* File="SafeNativeMethods"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Common.Windows {
    public static class SafeNativeMethods {
        private const int SwHide = 0;

        private const int SwShow = 5;

        private const string VistaStartMenuCaption = "Start";

        private static IntPtr vistaStartMenuWnd = IntPtr.Zero;

        public static bool Visible {
            set => SetVisibility(value);
        }

        public static void Hide() => SetVisibility(false);

        public static void Show() => SetVisibility(true);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool EnumThreadWindows(int threadId, EnumThreadProc pfnEnum, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        private static IntPtr GetVistaStartMenuWnd(IntPtr taskBarWnd) {
            GetWindowThreadProcessId(taskBarWnd, out var procId);
            var p = System.Diagnostics.Process.GetProcessById(procId);
            foreach (ProcessThread t in p.Threads) {
                EnumThreadWindows(t.Id, MyEnumThreadWindowsProc, IntPtr.Zero);
            }

            return vistaStartMenuWnd;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

        private static bool MyEnumThreadWindowsProc(IntPtr hWnd, IntPtr lParam) {
            var buffer = new StringBuilder(256);
            if (GetWindowText(hWnd, buffer, buffer.Capacity) <= 0) {
                return true;
            }
#if DEBUG
            Debug.WriteLine(buffer);
#endif
            if (buffer.ToString() != VistaStartMenuCaption) {
                return true;
            }

            vistaStartMenuWnd = hWnd;
            return false;
        }

        private static void SetVisibility(bool show) {
            var taskBarWnd = FindWindow("Shell_TrayWnd", null);
            var startWnd = FindWindowEx(taskBarWnd, IntPtr.Zero, "Button", "Start");
            if (startWnd == IntPtr.Zero) {
                startWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, "Start");
            }

            if (startWnd == IntPtr.Zero) {
                startWnd = FindWindow("Button", null);
                if (startWnd == IntPtr.Zero) {
                    startWnd = GetVistaStartMenuWnd(taskBarWnd);
                }
            }
            ShowWindow(taskBarWnd, show ? SwShow : SwHide);
            ShowWindow(startWnd, show ? SwShow : SwHide);
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);
    }
}
