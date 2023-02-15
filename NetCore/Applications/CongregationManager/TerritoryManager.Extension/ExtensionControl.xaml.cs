using Common.Applicationn.Primitives;
using System.Windows.Controls;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public partial class ExtensionControl : UserControl {
        public ExtensionControl() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();
    }
}
