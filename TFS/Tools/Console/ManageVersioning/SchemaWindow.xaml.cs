using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ManageVersioning {
    public partial class SchemaWindow : Window {
        public SchemaWindow() {
            InitializeComponent();
            View.Initialize();
            View.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(DialogResult)) {
                    if (View.DialogResult.HasValue && View.DialogResult.Value) {
                        if (View.Schema != null) {
                            View.Schema.HasChanges = View.itemHasChanges;
                        }
                    }
                    else {
                        //cancelled - revert to old values
                        if (View.Schema != null) {
                            View.Schema.Name = View.previousName;
                            View.Schema.MajorPart = View.previousMajorMethod;
                            View.Schema.MinorPart = View.previousMinorMethod;
                            View.Schema.BuildPart = View.previousBuildMethod;
                            View.Schema.RevisionPart = View.previousRevisionMethod;
                            View.Schema.MajorParameter = View.previousMajorParameter;
                            View.Schema.MinorParameter = View.previousMinorParameter;
                            View.Schema.BuildParameter = View.previousBuildParameter;
                            View.Schema.RevisionParameter = View.previousRevisionParameter;
                            View.Schema.HasChanges = false;
                        }
                    }
                    this.DialogResult = View.DialogResult;
                }
            };
            EditNameTextBox.IsVisibleChanged += (s, e) => {
                if ((bool)e.NewValue) {
                    EditNameTextBox.Focus();
                }
            };
            EditNameTextBox.GotFocus += (s, e) => {
                EditNameTextBox.SelectAll();
            };
            EditNameTextBox.PreviewKeyDown += (s, e) => {
                if (e.Key == Key.Enter || e.Key == Key.Tab) {
                    HideNameTextBox();
                    e.Handled = true;
                }
                else if (e.Key == Key.Space) {
                    s.As<TextBox>().AppendText("_");
                    s.As<TextBox>().CaretIndex = s.As<TextBox>().CaretIndex + 1;
                    e.Handled = true;
                }
            };
            EditNameTextBox.LostFocus += (s, e) => {
                HideNameTextBox();
                e.Handled = true;
            };
        }

        private void HideNameTextBox() {
            View.EditControlVisibility = System.Windows.Visibility.Collapsed;
            View.TitleControlVisibility = System.Windows.Visibility.Visible;
            View.IsOKDefault = true;
        }

        public SchemaWindowView View => DataContext.As<SchemaWindowView>();
    }
}
