using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GregOsborne.Application;
using GregOsborne.Application.Logging;
using GregOsborne.MVVMFramework;
using Life.Savings.Data;

namespace Life.Savings
{
    public class AskForDataSetWindowView : ViewModelBase
    {
        public AskForDataSetWindowView()
        {
            Ls2DataSelected = true;
        }
        private bool _Ls2DataSelected;
        public bool Ls2DataSelected {
            get => _Ls2DataSelected;
            set {
                _Ls2DataSelected = value;
                InvokePropertyChanged(nameof(Ls2DataSelected));
            }
        }
        private bool _Ls3DataSelected;
        public bool Ls3DataSelected {
            get => _Ls3DataSelected;
            set {
                _Ls3DataSelected = value;
                InvokePropertyChanged(nameof(Ls3DataSelected));
            }
        }

        private bool _DialogResult;
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                InvokePropertyChanged(nameof(DialogResult));
            }
        }
        private DelegateCommand _OKCommand;
        public DelegateCommand OKCommand => _OKCommand ?? (_OKCommand = new DelegateCommand(OK, ValidateOKState));
        private void OK(object state)
        {
            var lsFolder = Path.Combine(App.DataBaseFolder, Ls3DataSelected ? "Ls3Data" : "Ls2Data");
            Logger.LogMessage($"{(Ls3DataSelected ? "LS3" : "LS2")} selected.");
            App.Repository = new LsRepository(lsFolder, Settings.GetSetting(App.ApplicationName, "Application", "IsClientDataEncrypted", false));
            new MainWindow().Show();
            DialogResult = true;
        }
        private static bool ValidateOKState(object state)
        {
            return true;
        }
        private DelegateCommand _CancelCommand;
        public DelegateCommand CancelCommand => _CancelCommand ?? (_CancelCommand = new DelegateCommand(Cancel, ValidateCancelState));
        private void Cancel(object state)
        {
            DialogResult = false;
        }
        private static bool ValidateCancelState(object state)
        {
            return true;
        }

    }
}
