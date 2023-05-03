using GregOsborne.Application.Windows;
using System.Windows;

namespace Life.Savings.Extensions
{
    public static class WindowExtensions
    {
        public static void Position(this Window win, double left, double top)
        {
            while (left > Screen.FullWidth)
                left = left - Screen.FullWidth;
            if (left < 0)
                left = 0;
            win.Left = left;
            win.Top = top;
        }
    }
}
