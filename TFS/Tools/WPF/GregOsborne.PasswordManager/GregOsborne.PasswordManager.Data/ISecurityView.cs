using System.Windows.Media;

namespace GregOsborne.PasswordManager.Data {
    public interface ISecurityView {
        SolidColorBrush ControlBorderBrush {
            get; set;
        }

        double FontSize {
            get; set;
        }

        double ItemTitleFontSize {
            get; set;
        }

        SolidColorBrush WindowBrush {
            get; set;
        }

        SolidColorBrush WindowTextBrush {
            get; set;
        }
    }
}
