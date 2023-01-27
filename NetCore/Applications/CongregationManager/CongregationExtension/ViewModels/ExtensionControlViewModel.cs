using Common.MVVMFramework;
using CongregationManager.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationExtension.ViewModels {
    public class ExtensionControlViewModel : ViewModelBase {
        public ExtensionControlViewModel() {

        }

        public override void Initialize() {
            base.Initialize();

            Congregations = new ObservableCollection<Congregation>();
        }

        #region Congregations Property
        private ObservableCollection<Congregation> _Congregations = default;
        /// <summary>Gets/sets the Congregations.</summary>
        /// <value>The Congregations.</value>
        public ObservableCollection<Congregation> Congregations {
            get => _Congregations;
            set {
                _Congregations = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
