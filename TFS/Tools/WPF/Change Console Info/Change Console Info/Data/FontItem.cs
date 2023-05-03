using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Osborne.ChangeConsoleInfo.Data
{
    public class FontItem : INotifyPropertyChanged
    {
        private System.Drawing.FontFamily _fontFamily;
        private bool _isSelected;

        private string _keyName;

        public event PropertyChangedEventHandler PropertyChanged;

        public System.Drawing.FontFamily FontFamily {
            get => _fontFamily;
            set {
                _fontFamily = value;
                InvokePropertyChanged(nameof(FontFamily));
            }
        }

        public bool IsSelected {
            get => _isSelected;
            set {
                _isSelected = value;
                InvokePropertyChanged(nameof(IsSelected));
            }
        }

        public string KeyName {
            get => _keyName;
            set {
                _keyName = value;
                InvokePropertyChanged(nameof(KeyName));
            }
        }

        private void InvokePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}