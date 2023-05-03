// <copyright file="MainWindowView.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace ApplicationSettings.Addon {
    using GregOsborne.Application;
    using GregOsborne.MVVMFramework;
    using Toolbar.Controller;

    public partial class MainWindowView : ViewModelBase {
        private string windowTitle = default;
        public string WindowTitle {
            get => this.windowTitle;
            set {
                this.windowTitle = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }


        private ApplicationAddon addon = default;
        public ApplicationAddon Addon {
            get => addon;
            set {
                addon = value;
                InvokePropertyChanged(Reflection.GetPropertyName());
            }
        }

        public override void Initialize() {
        }
    }
}
