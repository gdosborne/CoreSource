using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GregOsborne.Controls.Configuration {
    public class BrushData : INotifyPropertyChanged {
        public BrushData(int key, string name, Brush value) {
            Key = key;
            Name = name;
            Value = value;
        }

        public BrushData(int key, string name) {
            Key = key;
            Name = name;
        }
        private int _Key;
        public int Key {
            get => _Key;
            set {
                _Key = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().Name.Substring(4)));
            }
        }
        private string _Name;
        public string Name {
            get => _Name;
            set {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().Name.Substring(4)));
            }
        }
        private Brush _Value;
        public Brush Value {
            get => _Value;
            set {
                _Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(MethodBase.GetCurrentMethod().Name.Substring(4)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
