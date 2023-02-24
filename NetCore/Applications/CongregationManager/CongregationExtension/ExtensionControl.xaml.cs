using Common.Application.Primitives;
using CongregationExtension.ViewModels;
using System.Windows.Controls;

namespace CongregationExtension {
    public partial class ExtensionControl : UserControl {
        public ExtensionControl() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();

    }
}