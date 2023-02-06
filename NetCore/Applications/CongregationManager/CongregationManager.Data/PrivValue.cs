using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CongregationManager.Data.Member;

namespace CongregationManager.Data {
    public class PrivValue : INotifyPropertyChanged {
        #region Privilege Property
        private PrivilegeFlags _Privilege = default;
        /// <summary>Gets/sets the Privilegs.</summary>
        /// <value>The Privilege.</value>
        public PrivilegeFlags Privilege {
            get => _Privilege;
            set {
                _Privilege = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ActualValue Property
        private long _ActualValue = default;
        /// <summary>Gets/sets the ActualValue.</summary>
        /// <value>The ActualValue.</value>
        public long ActualValue {
            get => _ActualValue;
            set {
                _ActualValue = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Text Property
        private string _Text = default;
        /// <summary>Gets/sets the Text.</summary>
        /// <value>The Text.</value>
        public string Text {
            get => _Text;
            set {
                _Text = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsChecked Property
        private bool _IsChecked = default;
        /// <summary>Gets/sets the IsChecked.</summary>
        /// <value>The IsChecked.</value>
        public bool IsChecked {
            get => _IsChecked;
            set {
                _IsChecked = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = default) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
