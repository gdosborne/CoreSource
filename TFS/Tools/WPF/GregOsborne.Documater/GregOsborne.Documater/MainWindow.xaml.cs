namespace GregOsborne.Documater
{
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
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows.Controls;
    using GregOsborne.Dialogs;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            View.ExecuteUiAction += View_ExecuteUiAction;
        }

        private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e)
        {
            switch (e.CommandToExecute) {
                case "OpenFile":
                    var ofd1 = new VistaOpenFileDialog {
                        AddExtension = false,
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Filter = "All files|*.*",
                        InitialDirectory = (string)e.Parameters["initialdirectory"],
                        Multiselect = true,
                        RestoreDirectory = true,
                        Title = "Open file..."
                    };
                    var result1 = ofd1.ShowDialog(this);
                    e.Parameters["cancel"] = !result1.HasValue || !result1.Value;
                    e.Parameters["filename"] = ofd1.FileName;
                    break;
            }
        }

        public MainWindowView View {
            get => DataContext.As<MainWindowView>();
        }
        

        protected override void OnSourceInitialized(EventArgs e)
        {
            myToolbar.RemoveOverflow();
        }
    }
}
