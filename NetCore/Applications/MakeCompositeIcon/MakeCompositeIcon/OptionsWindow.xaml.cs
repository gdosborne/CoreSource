using Common.Application.Primitives;
using Common.Application.Windows;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace MakeCompositeIcon {
    public partial class OptionsWindow : Window {
        public OptionsWindow() {
            InitializeComponent();

            View.Initialize();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(OptionsWindowView.Actions), e.CommandToExecute, out var action)) {
                var act = (OptionsWindowView.Actions)action;
                switch (act) {
                    case OptionsWindowView.Actions.ClearRecycleBin: {
                            App.ClearRecycleBin();
                            View.UpdateInterface();
                            break;
                        }
                }
            }
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
