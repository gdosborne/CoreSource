using MyMinistry.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace MyMinistry
{
    public sealed partial class OptionsFlyout : SettingsFlyout
    {
        #region Public Constructors

        public OptionsFlyout()
        {
            this.InitializeComponent();
            View.Close += View_Close;
        }

        #endregion Public Constructors

        #region Public Properties

        public OptionsFlyoutView View {
            get {
                return this.DataContext as OptionsFlyoutView;
            }
        }

        #endregion Public Properties

        #region Private Methods

        private void View_Close(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion Private Methods
    }
}
