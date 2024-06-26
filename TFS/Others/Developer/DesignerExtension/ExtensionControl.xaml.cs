// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// ExtensionControl.xaml.cs
//
namespace SNC.OptiRamp.Application.Developer.Extensions.DesignerExtension {

    using GregOsborne.MVVMFramework;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class ExtensionControl : UserControl {

        #region Public Constructors
        public ExtensionControl() {
            InitializeComponent();
        }
        #endregion Public Constructors

        #region Private Methods
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e) {
            View.LastPoint = e.GetPosition(this);
        }
        #endregion Private Methods

        #region Public Properties
        public ExtensionControlView View {
            get {
                return LayoutRoot.GetView<ExtensionControlView>();
            }
        }
        #endregion Public Properties
    }
}
