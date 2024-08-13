using Common.MVVMFramework;

using CongregationData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congregation.Scheduler.Views {
    internal class PersonsWindowViewModel : ViewModelBase {
        public PersonsWindowViewModel () {
            Title = "Members [designer]";
                        
        }

        public override void Initialize () {
            base.Initialize();
            Title = "Members";
        }

        public enum Actions {
            AddMember
        }

        #region SelectedMember Property
        private Member _SelectedMember = default;
        public Member SelectedMember {
            get => _SelectedMember;
            set {
                _SelectedMember = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AddNewMember Command
        private DelegateCommand _AddNewMemberCommand = default;
        public DelegateCommand AddNewMemberCommand => _AddNewMemberCommand ??= new DelegateCommand(AddNewMember, ValidateAddNewMemberState);
        private bool ValidateAddNewMemberState (object state) => true;
        private void AddNewMember (object state) {

        }
        #endregion

    }
}
