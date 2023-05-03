using MVVMFramework;
using System;
using System.Diagnostics;

namespace Consoler.Views {
    public class MainWindowView : ViewModelBase {
        private string _ConsoleOutput;
        public string ConsoleOutput {
            get { return _ConsoleOutput; }
            set {
                _ConsoleOutput = value;
                base.InvokePropertyChanged(this, "ConsoleOutput");
            }
        }
        private DelegateCommand _GoCommand = null;
        public DelegateCommand GoCommand {
            get {
                if (_GoCommand == null)
                    _GoCommand = new DelegateCommand(Go, ValidateGoState);
                return _GoCommand as DelegateCommand;
            }
        }
        private void Go(object state) {
            var path = @"C:\Repos\Web\PCS.Deere\PCS.Deere.AccountTester\bin\Release\PCS.Deere.AccountTester.exe";
            AppDomain.CreateDomain("Executer").ExecuteAssembly(path);
            //Process.Start(new ProcessStartInfo(path));
        }
        private bool ValidateGoState(object state) {
            return true;
        }
    }
}
