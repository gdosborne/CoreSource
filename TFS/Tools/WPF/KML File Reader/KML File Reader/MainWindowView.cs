using System;
using System.Collections.Generic;
using GregOsborne.MVVMFramework;
using System.Linq;
using System.Text;
using System.Windows;

namespace KML_File_Reader
{
    public class MainWindowView : ViewModelBase
    {
        private string _KmlFileName;
        private Visibility _KMLFileNameWatermarkVisibility;
        private DelegateCommand _OpenKmlFileCommand;

        public MainWindowView()
        {
            KMLFileNameWatermarkVisibility = Visibility.Visible;
        }

        public string KMLFileName {
            get => _KmlFileName;
            set {
                _KmlFileName = value;
                KMLFileNameWatermarkVisibility = string.IsNullOrEmpty(KMLFileName) ? Visibility.Visible : Visibility.Hidden;
                InvokePropertyChanged(nameof(KMLFileName));
            }
        }

        public Visibility KMLFileNameWatermarkVisibility {
            get => _KMLFileNameWatermarkVisibility;
            set {
                _KMLFileNameWatermarkVisibility = value;
                InvokePropertyChanged(nameof(KMLFileNameWatermarkVisibility));
            }
        }

        public DelegateCommand OpenKmlFileCommand => _OpenKmlFileCommand ?? (_OpenKmlFileCommand = new DelegateCommand(OpenKmlFile, ValidateOpenKmlFileState));

        private void OpenKmlFile(object state)
        {
            //execute the command
        }

        private bool ValidateOpenKmlFileState(object state) => true;
    }
}