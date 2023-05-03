using GregOsborne.RegistryHack.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace RegistryHack
{
    public partial class App : Application
    {
        public static string ApplicationName = "Registry Hacks";
        public static string HackItemsFileName = null;
        public static IList<HackItemBase> HackItemsTree = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            HackItemsTree = new List<HackItemBase>();
            HackItemsFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Registry Hack", "HackItems.xml");
            var localMachine = new HackFolder(Registry.LocalMachine);
            var software = new HackFolder(Registry.LocalMachine.OpenSubKey("SOFTWARE"));
            var microsoft = new HackFolder(Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft"));
            var windows = new HackFolder(Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows"));
            var currentVersion = new HackFolder(Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion"));
            windows.Items.Add(currentVersion);
            microsoft.Items.Add(windows);
            software.Items.Add(microsoft);
            localMachine.Items.Add(software);
            HackItemsTree.Add(localMachine);
            HackItemsTree.Add(new HackFolder(Registry.ClassesRoot) { Name = Registry.ClassesRoot.Name });
            HackItemsTree.Add(new HackFolder(Registry.CurrentConfig) { Name = Registry.CurrentConfig.Name });
            HackItemsTree.Add(new HackFolder(Registry.CurrentUser) { Name = Registry.CurrentUser.Name });
            HackItemsTree.Add(new HackFolder(Registry.Users) { Name = Registry.Users.Name });
        }
    }
}