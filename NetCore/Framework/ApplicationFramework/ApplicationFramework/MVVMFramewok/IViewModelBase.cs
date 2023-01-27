using System.ComponentModel;

namespace Common.MVVMFramework {
    public interface IViewModelBase {
        event PropertyChangedEventHandler PropertyChanged;
        event ExecuteUiActionHandler ExecuteUiAction;
        void Initialize();
    }
}