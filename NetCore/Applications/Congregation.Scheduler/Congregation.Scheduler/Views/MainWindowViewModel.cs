using Common.MVVMFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congregation.Scheduler.Views {
    internal class MainWindowViewModel : ViewModelBase {
        internal MainWindowViewModel () {
            Title = $"{App.ApplicationName} [Designer]";
        }

        public override void Initialize () {
            base.Initialize();
            Title = $"{App.ApplicationName}";
        }

        public enum Actions {
            ShowPersonWindow
        }

        #region Persons Command
        private DelegateCommand _PersonsCommand = default;
        public DelegateCommand PersonsCommand => _PersonsCommand ??= new DelegateCommand(Persons, ValidatePersonsState);
        private bool ValidatePersonsState (object state) => true;
        private void Persons (object state) {
            ExecuteAction(nameof(Actions.ShowPersonWindow));
        }
        #endregion


    }
}
