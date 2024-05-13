/* File="IViewModelBase"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.ComponentModel;

namespace Common.MVVMFramework {
    public interface IViewModelBase {
        event PropertyChangedEventHandler PropertyChanged;
        event ExecuteUiActionHandler ExecuteUiAction;
        void Initialize();
        bool IgnoreChanges { get; set; }
    }
}
