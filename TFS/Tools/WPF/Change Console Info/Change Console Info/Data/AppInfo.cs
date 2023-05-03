using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Osborne.ChangeConsoleInfo.Data
{
    public class AppInfo : INotifyPropertyChanged
    {
        private bool _changeHolder = false;
        private string _FaceName;
        private int _fontHeight;
        private int _fontSize;
        private bool _hasChanges;
        private string _hexFontSize;
        private int _id;
        private string _name;

        public AppInfo()
        {
            _changeHolder = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FaceName {
            get => _FaceName;
            set {
                _FaceName = value;
                HasChanges = true;
                InvokePropertyChanged(nameof(FaceName));
            }
        }

        public int FontHeight {
            get => _fontHeight;
            set {
                _fontHeight = value;
                HasChanges = true;
                InvokePropertyChanged(nameof(FontHeight));
            }
        }

        public int FontSize {
            get => _fontSize;
            set {
                _fontSize = value;
                _hexFontSize = FontSize.ToString("X").PadLeft(8, '0');
                FontHeight = int.Parse(_hexFontSize.Substring(0, 4), System.Globalization.NumberStyles.HexNumber);
                InvokePropertyChanged(nameof(FontSize));
            }
        }

        public bool HasChanges {
            get => _hasChanges;
            private set {
                _hasChanges = value;
                InvokePropertyChanged(nameof(HasChanges));
            }
        }

        public int Id {
            get => _id;
            set {
                _id = value;
                InvokePropertyChanged(nameof(Id));
            }
        }

        public RegistryKey Key { get; set; }

        public string Name {
            get => _name;
            set {
                _name = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public void CompleteInitilization() => HasChanges = _changeHolder;

        public void PrepareForInitialization() => _changeHolder = HasChanges;

        public void UpdateRegistry()
        {
            var h = FontHeight.ToString("X").PadLeft(4, '0');
            var w = (FontHeight / 2).ToString("X").PadLeft(4, '0');
            FontSize = int.Parse($"{h}{w}", System.Globalization.NumberStyles.HexNumber);
            Key.SetValue("FaceName", FaceName);
            Key.SetValue("FontSize", FontSize);
            HasChanges = false;
        }

        private void InvokePropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}