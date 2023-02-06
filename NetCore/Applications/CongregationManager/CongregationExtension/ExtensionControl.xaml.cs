using Common.Applicationn.Primitives;
using CongregationExtension.ViewModels;
using System.Windows.Controls;

namespace CongregationExtension {
    public partial class ExtensionControlView : UserControl {
        public ExtensionControlView() {
            InitializeComponent();

            View.Initialize();
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();

    }
}