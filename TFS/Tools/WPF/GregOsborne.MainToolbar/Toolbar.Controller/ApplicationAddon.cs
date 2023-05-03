// <copyright file="ApplicationAddon.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace Toolbar.Controller {
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using GregOsborne.Application;
    using GregOsborne.MVVMFramework;

    public abstract class ApplicationAddon {
        public event RequestButtonTemplateHandler RequestButtonTemplate;
        public event RequestComboBoxTemplateHandler RequestComboBoxTemplate;
        public string AddonName {
            get; protected set;
        }

        public abstract void CloseAddon();

        public List<PlaceableControlDefinition> PlaceableControls { get; private set; } = new List<PlaceableControlDefinition>();

        public abstract void TriggerControlAddition();

        protected Button RequestButton(ImageSource source, ICommand command, string addonName) {
            var e = new RequestButtonTemplateEventArgs(source, command, addonName);
            RequestButtonTemplate?.Invoke(this, e);
            return e.Button;
        }

        protected ComboBox RequestComboBox<T>(string addonName, double width, IEnumerable<T> itemSource) {
            var e = new RequestComboBoxTemplateEventArgs(addonName, width);
            RequestComboBoxTemplate?.Invoke(this, e);
            e.ComboBox.ItemsSource = itemSource;
            return e.ComboBox;
        }

        public abstract Window MainWindow {
            get; protected set;
        }

        public abstract ViewModelBase View {
            get; protected set;
        }

        public Manager AddonManager {
            get; set;
        }

        public Session Session {
            get; set;
        }
    }
}
