/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

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
