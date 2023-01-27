using Common.Applicationn.Primitives;
using CongregationExtension.ViewModels;
using CongregationManager.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CongregationExtension {
    public partial class ExtensionControl : UserControl {
        public ExtensionControl() {
            InitializeComponent();
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();
    }
}