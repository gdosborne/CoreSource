using Common.Application.Primitives;
using Common.Application.Windows;
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

namespace MakeCompositeIcon {
    public partial class OptionsWindow : Window {
        public OptionsWindow() {
            InitializeComponent();

            View.Initialize();
            View.PropertyChanged += View_PropertyChanged;
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                this.DialogResult = View.DialogResult;
        }

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }

        internal OptionsWindowView View => DataContext.As<OptionsWindowView>();
    }
}
