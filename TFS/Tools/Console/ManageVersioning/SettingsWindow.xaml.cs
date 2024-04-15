using ColorFontPickerWPF;
using GregOsborne.Application.Windows;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows;
using System.Windows.Media;

namespace ManageVersioning {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this)) {
                View.Initialize();
                View.PropertyChanged += (s, e) => {
                    if (e.PropertyName.EqualsIgnoreCase("DialogResult")) {
                        DialogResult = View.DialogResult;
                    } 
                };
                Closing += (s, e) => {
                    this.SavePosition(App.Session.ApplicationSettings);
                };
                View.ExecuteUiAction += (s, e) => {
                    if (Enum.TryParse(typeof(SettingsWindowView.Actions), e.CommandToExecute, out var action)) {
                        switch (action) {
                            case SettingsWindowView.Actions.SelectSharedFile: {
                                    var fname = SysIO.Path.GetFileName("UpdateVersion.Projects.xml");
                                    var result = Dialogs.ShowOpenFileDialog("Select shared project version file", App.ApplicationDirectory, 
                                        fname, ("*.xml", "XML File"));
                                    if (string.IsNullOrEmpty(result)) {
                                        return;
                                    }
                                    View.SharedVersionFilePath = result;
                                }
                                break;
                            case SettingsWindowView.Actions.PickColor: {
                                    var colorDialog = new ColorDialog();
                                    colorDialog.SelectedColor = View.ConsoleImageForegroundColor.As<SolidColorBrush>().Color; //In need
                                    if (colorDialog.ShowDialog() == true)
                                        View.ConsoleImageForegroundColor = new SolidColorBrush(colorDialog.SelectedColor);
                                }
                                break;
                            case SettingsWindowView.Actions.PickImage: {
                                    var dir = SysIO.Path.GetDirectoryName(View.ConsoleBrushFilePath);
                                    var fname = SysIO.Path.GetFileName(dir);
                                    var result = Dialogs.ShowOpenFileDialog("Select image file for background", dir, fname,
                                        ("*.png", "PNG File" ), ( "*.jpg", "JPG File" ), ( "*.bmp", "BMP File" ));
                                    if (string.IsNullOrEmpty(result)) {
                                        return;
                                    }
                                    View.ConsoleBrushFilePath = result;
                                }
                                break;
                        }
                    }
                };
            }
        }

        public SettingsWindowView View => DataContext.As<SettingsWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);
            this.HideMinimizeAndMaximizeButtons();
            if (App.Settings.AreWindowPositionsSaved) {
                this.SetPosition(App.Session.ApplicationSettings);
            }
        }
    }
}
