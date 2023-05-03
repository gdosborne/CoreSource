using GregOsborne.Application.Primitives;
using GregOsborne.RegistryHack.Data;
using Microsoft.Win32;
using RegistryHack.Events;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace RegistryHack
{
    public partial class StartupWindow : Window
    {
        private DispatcherTimer _closeTimer = null;
        private bool _isReadingComplete = false;
        private MainWindow _mainWindow = null;

        public StartupWindow()
        {
            InitializeComponent();
        }

        private event GetRegistryKeyHandler GetRegistryKey;

        private void _CloseTimer_Tick(object sender, EventArgs e)
        {
            sender.As<DispatcherTimer>().Stop();
            if (!_isReadingComplete)
            {
                if (sender.As<DispatcherTimer>().Interval.TotalMilliseconds != 100)
                    sender.As<DispatcherTimer>().Interval = TimeSpan.FromMilliseconds(100);
                sender.As<DispatcherTimer>().Start();
                return;
            }
            _mainWindow.Show();
            Close();
        }

        private void StartRegistryRead()
        {
            GetRegistryKey += StartupWindow_GetRegistryKey;
            App.HackItemsTree = HackItemBase.GetInitialItems();

            var data = "Windows Registry Editor Version 5.00\n\n[HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion]\n\"ProgramFilesDir\"=\"C:\\\\Program Files\"\n\"ProgramFilesDir (x86)\" = \"C:\\\\Program Files (x86)\"\n\"ProgramW6432Dir\" = \"C:\\\\Program Files\"";
            var x1 = HackItemBase.GetHackFoldersFromData(data);

            if (string.IsNullOrEmpty(App.HackItemsFileName))
                return;

            var totalItems = App.HackItemsTree.Cast<HackFolder>().Sum(x => x.NumberOfSubItems);

            var pathToFind = @"SOFTWARE\Microsoft\Windows\CurrentVersion";

            var topLevel = App.HackItemsTree.FirstOrDefault(x => x.Name.EndsWith("HKEY_LOCAL_MACHINE"));
            if (topLevel != null)
            {
                var e = new GetRegistryKeyEventArgs(topLevel.As<HackFolder>(), topLevel.As<HackFolder>().RegKey, pathToFind);
                Dispatcher.Invoke(new GetRegistryKeyHandler(StartupWindow_GetRegistryKey), new object[] { this, e });
                if (e.Result != null)
                {
                }
            }
            _isReadingComplete = true;
        }

        private void StartupWindow_GetRegistryKey(object sender, GetRegistryKeyEventArgs e)
        {
            var result = default(RegistryKey);
            if (e.Parent == null || string.IsNullOrEmpty(e.Path))
                result = e.Parent;
            else
            {
                var parts = e.Path.Split('\\');
                var names = e.Parent.GetSubKeyNames();
                if (names.Contains(parts[0]))
                {
                    var partName = parts[0];
                    var tmp = parts.ToList();
                    var keyName = tmp[0];
                    tmp.RemoveAt(0);
                    var tmpPath = string.Join("\\", tmp);
                    var tmpParent = e.Parent.OpenSubKey(keyName, false);
                    var tmpParentFolder = e.ParentFolder;
                    var item = new HackFolder
                    {
                        Name = partName
                    };
                    var e1 = new GetRegistryKeyEventArgs(item, tmpParent, tmpPath);
                    Dispatcher.Invoke(new GetRegistryKeyHandler(StartupWindow_GetRegistryKey), new object[] { this, e1 });
                    item.RegKey = e1.Result;
                    e.ParentFolder.Items.Add(item);
                    result = e1.Result;
                }
            }
            e.Result = result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindow = new MainWindow();
            //minimum splash show time is 3 seconds
            _closeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            _closeTimer.Tick += _CloseTimer_Tick;
            _closeTimer.Start();

            Task.Factory.StartNew(() => StartRegistryRead());
        }
    }
}