// <copyright file="RequestButtonTemplateEventArgs.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace Toolbar.Controller {
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public delegate void RequestButtonTemplateHandler(object sender, RequestButtonTemplateEventArgs e);
    public class RequestButtonTemplateEventArgs : EventArgs {
        public RequestButtonTemplateEventArgs(ImageSource source, ICommand command, string addonName) {
            Source = source;
            Command = command;
            AddonName = addonName;
        }

        public Button Button {
            get; set;
        }

        public ICommand Command { get; private set; } = null;

        public ImageSource Source { get; private set; } = null;

        public string AddonName { get; private set; } = null;
    }
}
