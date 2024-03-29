using System.Linq;

namespace GregOsborne.Application.Windows {
    public static class Screen {
        public static double FullWidth {
            get { return System.Windows.Forms.Screen.AllScreens.Aggregate(0.0, (current, scrn) => current + scrn.Bounds.Width); }
        }

        public static bool HasMultipleScreens => System.Windows.Forms.Screen.AllScreens.Count() > 1;

        public static double MaxHeight {
            get { return System.Windows.Forms.Screen.AllScreens.Aggregate(0.0, (current, scrn) => current < scrn.Bounds.Height ? scrn.Bounds.Height : current); }
        }

        public static double PrimaryScreenHeight => System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

        public static double PrimaryScreenWidth => System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
    }
}