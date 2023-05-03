using System;
using MVVMFramework;
using tFrameTester.Views.Interface;
using System.ComponentModel;

namespace tFrameTester.Views
{
    public class MainWindowView : ViewModelBase
    {
        public MainWindowView()
        {
            SomeValue = "This is a test";
        }
        public new event PropertyChangedEventHandler PropertyChanged;
        private string _SomeValue;
        public string SomeValue {
            get { return _SomeValue; }
            set {
                _SomeValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SomeValue"));
            }
        }
    }
}
