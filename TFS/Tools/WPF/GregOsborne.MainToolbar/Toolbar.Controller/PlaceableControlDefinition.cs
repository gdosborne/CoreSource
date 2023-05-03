// <copyright file="PlaceableControlDefinition.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/16/2020</date>

namespace Toolbar.Controller {
    using System.Windows;

    public class PlaceableControlDefinition {
        public FrameworkElement Control { get; set; } = default;

        public bool HasSeparatorAfter { get; set; } = default;

        public bool HasSeparatorBefore { get; set; } = default;
    }
}
