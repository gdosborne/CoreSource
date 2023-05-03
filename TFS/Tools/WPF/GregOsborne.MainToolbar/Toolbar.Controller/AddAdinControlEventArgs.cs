// <copyright file="AddAdinControlEventArgs.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace Toolbar.Controller {
    using System;

    public delegate void AddAddonControlHandler<T>(object sender, AddAddonControlEventArgs<T> e);

    public class AddAddonControlEventArgs<T> : EventArgs {
        public AddAddonControlEventArgs(string addonName, T control) {
            AddonName = addonName;
            Control = control;
        }

        public string AddonName { get; private set; } = null;

        public T Control { get; private set; } = default(T);
    }
}
