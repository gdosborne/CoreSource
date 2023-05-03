// <copyright file="RequestComboBoxTemplateEventArgs.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace Toolbar.Controller {
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    public delegate void RequestComboBoxTemplateHandler(object sender, RequestComboBoxTemplateEventArgs e);
    public class RequestComboBoxTemplateEventArgs : EventArgs {
        public RequestComboBoxTemplateEventArgs(string addonName, double width) {
            AddonName = AddonName;
            Width = width;
        }

        public string AddonName { get; private set; } = null;

        public ComboBox ComboBox {
            get; set;
        }

        public double Width { get; private set; } = default;
    }
}
