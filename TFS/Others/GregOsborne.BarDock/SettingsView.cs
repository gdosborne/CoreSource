using GregOsborne.MVVMFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GregOsborne.BarDock
{
    public class SettingsView : ViewModelBase
    {
        
        private DelegateCommand _ExitCommand;
        public DelegateCommand ExitCommand => _ExitCommand ?? (_ExitCommand = new DelegateCommand(Exit, ValidateExitState));
        private void Exit(object state)
        {
            WindowVisibility = Visibility.Collapsed;
        }
        private static bool ValidateExitState(object state)
        {
            return true;
        }
        
        private Visibility _WindowVisibility;
        public Visibility WindowVisibility {
            get => _WindowVisibility;
            set {
                _WindowVisibility = value;                
                UpdateInterface();
                InvokePropertyChanged(nameof(WindowVisibility));
            }
        }
    }
}
