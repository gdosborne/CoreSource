using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeCompositeIcon {
    internal partial class CharacterWindowView {

        #region SelectCharacterCommand
        private DelegateCommand _SelectCharacterCommand = default;
        /// <summary>Gets the SelectCharacter command.</summary>
        /// <value>The SelectCharacter command.</value>
        public DelegateCommand SelectCharacterCommand => _SelectCharacterCommand ??= new DelegateCommand(SelectCharacter, ValidateSelectCharacterState);
        private bool ValidateSelectCharacterState(object state) => true;
        private void SelectCharacter(object state) {
            DialogResult = true;
        }
        #endregion

        #region CloseCommand
        private DelegateCommand _CloseCommand = default;
        /// <summary>Gets the Close command.</summary>
        /// <value>The Close command.</value>
        public DelegateCommand CloseCommand => _CloseCommand ??= new DelegateCommand(Close, ValidateCloseState);
        private bool ValidateCloseState(object state) => true;
        private void Close(object state) {
            DialogResult = false;
        }
        #endregion

    }
}
