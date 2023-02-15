using Common.Applicationn.Primitives;
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
using System.Windows.Shapes;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public partial class TerritoryWindow : Window {
        public TerritoryWindow() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
        }

        public TerritoryWindowViewModel View => DataContext.As<TerritoryWindowViewModel>();

        private void TitlebarBorder_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }
    }
}
