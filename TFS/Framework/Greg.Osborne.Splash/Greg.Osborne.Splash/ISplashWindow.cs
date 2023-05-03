using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Greg.Osborne.Splash.Windows;

namespace Greg.Osborne.Splash {
    public interface ISplashWindow {
        DisplayResult Show();
        void ShowAsync();
        CornerRadius CornerRadius { get; set; }
        void WriteStatus(string value);
        event EventHandler WindowClosed;
    }
}
