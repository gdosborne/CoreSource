using GregOsborne.Application;
using GregOsborne.Application.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using SysIO = System.IO;

namespace GregOsborne.BarDock
{
    public partial class App : System.Windows.Application
    {
        public static string DataDirectory { get; private set; }
        public static string ToolbarDirectory { get; private set; }
        public static string SettingsDirectory { get; private set; }
        public static string SettingsFileName { get; private set; }
        private TaskbarIcon taskbarIcon;
        public static T GetSetting<T>(string sectionName, string settingName, T defaultValue)
        {
            var doc = XDocument.Load(SettingsFileName);
            var root = doc.Root;
            var sectionElement = root.Element(sectionName);
            if (sectionElement == null)
                return defaultValue;
            var itemElement = sectionElement.Elements().FirstOrDefault(x => x.Attribute("key") != null && x.Attribute("key").Value.Equals(settingName) && x.Attribute("value") != null);
            if (itemElement == null)
                return defaultValue;
            else
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)itemElement.Attribute("value").Value;
                else
                    return (T)(object)Convert.ChangeType(itemElement.Attribute("value").Value, typeof(T));
            }            
        }

        public static void SetSetting<T>(string sectionName, string settingName, T value)
        {
            var doc = XDocument.Load(SettingsFileName);
            var root = doc.Root;
            var sectionElement = root.Element(sectionName);
            if(sectionElement == null)
            {
                sectionElement = new XElement(sectionName);
                root.Add(sectionElement);
            }
            var itemElement = sectionElement.Elements().FirstOrDefault(x => x.Attribute("key") != null && x.Attribute("key").Value.Equals(settingName) && x.Attribute("value") != null);
            if (itemElement != null)
                itemElement.Attribute("value").Value = value.ToString();
            else
                sectionElement.Add(new XElement("add", new XAttribute("key", settingName), new XAttribute("value", value.ToString())));
            doc.Save(SettingsFileName);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            DataDirectory = SysIO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GregOsborne.BarDock");
            if (!SysIO.Directory.Exists(DataDirectory))
                SysIO.Directory.CreateDirectory(DataDirectory);
            ToolbarDirectory = SysIO.Path.Combine(DataDirectory, "Toolbars");
            if (!SysIO.Directory.Exists(ToolbarDirectory))
                SysIO.Directory.CreateDirectory(ToolbarDirectory);
            SettingsDirectory = SysIO.Path.Combine(DataDirectory, "Settings");
            if (!SysIO.Directory.Exists(SettingsDirectory))
                SysIO.Directory.CreateDirectory(SettingsDirectory);
            SettingsFileName = SysIO.Path.Combine(SettingsDirectory, "ApplicationSettings.xml");
            if (!SysIO.File.Exists(SettingsFileName))
            {
                var doc = new XDocument(new XElement("settings"));
                doc.Save(SettingsFileName);
            }
            InitializeNotifyIcon();
            Task.Factory.StartNew(() => LoadToolbars());

        }
        private void LoadToolbars()
        {

        }
        protected override void OnExit(ExitEventArgs e)
        {
            taskbarIcon.Visibility = Visibility.Collapsed;
        }
        private void InitializeNotifyIcon()
        {
            taskbarIcon = (TaskbarIcon)FindResource("MainTaskbarIcon");
            taskbarIcon.ContextMenu.Opened += ContextMenu_Opened;
            foreach (var item in taskbarIcon.ContextMenu.Items)
            {
                if(item is MenuItem)
                {
                    if (item.As<MenuItem>().Name.Equals("ExitMenuItem"))
                        item.As<MenuItem>().Click += Exit_Click;
                    else if(item.As<MenuItem>().Name.Equals("AddToolbarMenuItem"))
                        item.As<MenuItem>().Click += AddToolbar_Click;
                }
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            foreach (var item in sender.As<ContextMenu>().Items)
            {
                if (item is MenuItem)
                {
                    if (item.As<MenuItem>().Name.Equals("AddToolbarMenuItem"))
                        item.As<MenuItem>().Header = MainWindow.Visibility == Visibility.Collapsed ? "Show Settings" : "Hide Settings";
                }
            }            
        }

        private void AddToolbar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = MainWindow.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Shutdown(0);
        }
    }
}
