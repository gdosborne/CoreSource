// <copyright file="Manager.cs" company="">
// Copyright (c) 2020 All rights reserved
// </copyright>
// <author>IDOTCENTRAL\gosborn</author>
// <date>3/13/2020</date>

namespace Toolbar.Controller {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;
    using GregOsborne.Application;
    using GregOsborne.Application.Logging;

    public sealed class Manager {
        public Manager(string addonDirectory, bool isUserAdmin)
            : this(addonDirectory, isUserAdmin, null) {
        }

        public Manager(string addonDirectory, bool isUserAdmin, string addonExtension) {
            ManagerConstructor(addonDirectory, isUserAdmin, addonExtension);
        }
        private Session session = null;
        public event LogMessageHandler LogMessage;
        public string AddonDirectory { get; private set; } = null;

        public string AddonExtension { get; private set; } = ".dll";

        public List<ApplicationAddon> Addons { get; private set; } = default;

        public bool IsUserAdministrator { get; private set; } = false;

        public void Refresh(Session session) {
            this.session = session;
            Refresh();
        }

        public void Refresh() {
            if (!Directory.Exists(AddonDirectory)) {
                throw new DirectoryNotFoundException($"Couldn't find {AddonDirectory}");
            }
            if (Addons == null) {
                Addons = new List<ApplicationAddon>();
            } else {
                Addons.Clear();
            }

            var dir = new DirectoryInfo(AddonDirectory);
            LoadAddonsFromDirectory(dir);
        }

        private void AppAddon_RequestButtonTemplate(object sender, RequestButtonTemplateEventArgs e) {
            var btn = new Button();
            if (e.Source != null) {
                btn.Content = new Image {
                    Source = e.Source,
                };
            }
            btn.Command = e.Command;
            e.Button = btn;
        }

        private void AppAddon_RequestComboBoxTemplate(object sender, RequestComboBoxTemplateEventArgs e) {
            var cbo = new ComboBox {
                Width = e.Width
            };
            e.ComboBox = cbo;
        }

        private void LoadAddonsFromDirectory(DirectoryInfo dir) {
            var addonType = typeof(ApplicationAddon);
            var files = dir.GetFiles($"*{AddonExtension}");
            if (files.Any()) {
                foreach (var f in files) {
                    try {
                        var obj = Assembly.LoadFrom(f.FullName);
                        var types = obj.GetTypes();
                        foreach (var t in types) {
                            if (t.BaseType == addonType) {
                                var instance = (ApplicationAddon)Activator.CreateInstance(t);
                                if (addonType == typeof(ApplicationAddon)) {
                                    var appAddon = (ApplicationAddon)(object)instance;
                                    LogMessageLocal($"Found addon \"{appAddon.AddonName}\" in {f.Name}", ApplicationLogger.EntryTypes.Information);
                                    appAddon.AddonManager = this;
                                    appAddon.Session = this.session;
                                    appAddon.RequestButtonTemplate += AppAddon_RequestButtonTemplate;
                                    appAddon.RequestComboBoxTemplate += AppAddon_RequestComboBoxTemplate;
                                    appAddon.TriggerControlAddition();
                                }
                                Addons.Add(instance);
                            }
                        }
                    } catch (Exception ex) {
                        throw ex;
                    }
                }
            }
            var dirs = dir.GetDirectories();
            if (dirs.Any()) {
                foreach (var d in dirs) {
                    LoadAddonsFromDirectory(d);
                }
            }
        }

        private void LogMessageLocal(string message, ApplicationLogger.EntryTypes entryType)
            => LogMessageLocal(message, entryType, false);

        private void LogMessageLocal(string message, ApplicationLogger.EntryTypes entryType, bool isCritcal)
            => LogMessage?.Invoke(this, new LogMessageEventArgs(message, entryType, isCritcal));

        private void ManagerConstructor(string addonDirectory, bool isUserAdmin, string addonExtension) {
            if (!Directory.Exists(addonDirectory)) {
                throw new DirectoryNotFoundException($"Couldn't find {addonDirectory}");
            }
            AddonDirectory = addonDirectory;
            IsUserAdministrator = isUserAdmin;
            AddonExtension = addonExtension;
        }
    }
}
