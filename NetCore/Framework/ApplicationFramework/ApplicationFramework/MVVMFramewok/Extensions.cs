using System.ComponentModel;
using System.Windows;

namespace Common.MVVMFramework {
    public static class Extensions {
        #region Public Methods

        public static T GetView<T>(this FrameworkElement root) {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(root)) {
                return default;
            }

            return (T)root.DataContext;
        }

        #endregion Public Methods
    }
}
