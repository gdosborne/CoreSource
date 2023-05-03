// <copyright file="MainWindow.View.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace GregOsborne.MainToolbar {
    using GregOsborne.Application;
    using GregOsborne.MVVMFramework;

    public partial class MainWindowView : ViewModelBase {
        private bool? isAuthenticated = default;
        private string windowTitle = default;
        public MainWindowView() {
            WindowTitle = "Main Toolbar (Design Mode)";
        }

        public event ExecuteUiActionHandler ExecuteUiAction;
        public bool? IsAuthenticated {
            get => this.isAuthenticated;
            set {
                this.isAuthenticated = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public string WindowTitle {
            get => this.windowTitle;
            set {
                this.windowTitle = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public override void Initialize() => WindowTitle = "This application toolbar";
    }
}
