using Common.Application.Primitives;
using Common.Application.Windows;
using System;
using System.Windows;

namespace MakeCompositeIcon {
    public partial class ViewCodeWindow : Window {
        public ViewCodeWindow() {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
        }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult")
                DialogResult = View.DialogResult;
        }

        internal ViewCodeWindowView View => DataContext.As<ViewCodeWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }
    }
}
