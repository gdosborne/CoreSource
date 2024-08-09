/* File="DomainItemBase"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Common.MVVMFramewok {
    public abstract class DomainItemBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = default) {
            if (!PauseChange)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _IgnoreChanges;
        public bool PauseChange {
            get { return _IgnoreChanges; }
            set { _IgnoreChanges = value; }
        }

        public virtual XElement ToXElement() {
            return null;
        }
    }
}
