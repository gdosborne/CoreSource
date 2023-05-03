using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.RegistryHack.Data
{
    public enum HackTypes { Folder, Item }

    public abstract class HackItemBase : INotifyPropertyChanged
    {
        private const string _hKCC = "HKEY_CURRENT_CONFIG";
        private const string _hKCR = "HKEY_CLASSES_ROOT";
        private const string _hKLM = "HKEY_LOCAL_MACHINE";
        private const string _hKUS = "HKEY_USERS";
        private const string _kHCU = "HKEY_CURRENT_USER";
        private HackTypes _hackItemType;
        private int _id;
        private bool _isValid;
        private string _name;

        public event PropertyChangedEventHandler PropertyChanged;

        public HackTypes HackItemType {
            get {
                return _hackItemType;
            }
            protected set {
                _hackItemType = value;
                InvokePropertyChanged(nameof(HackItemType));
            }
        }

        public int Id {
            get {
                return _id;
            }
            set {
                _id = value;
                InvokePropertyChanged(nameof(Id));
            }
        }

        public bool IsValid {
            get {
                return _isValid;
            }
            protected set {
                _isValid = value;
                InvokePropertyChanged(nameof(IsValid));
            }
        }

        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
                InvokePropertyChanged(nameof(Name));
            }
        }

        public static HackFolder[] GetHackFoldersFromData(string data)
        {
            var hackItems = new List<HackFolder>();
            var lines = new List<string>();
            using (var sr = new StringReader(data))
            {
                while (sr.Peek() > -1)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line.Trim()))
                        lines.Add(line);
                }
            }
            //must have header (i.e., "Windows Registry Editor Version 5.00") before first path
            var hasHeader = false;
            var i = 0;
            for (i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("Windows Registry Editor Version", StringComparison.OrdinalIgnoreCase))
                {
                    hasHeader = true;
                    break;
                }
            }
            if (!hasHeader)
            {
                //bad file
                return null;
            }
            i++;
            var keyValue = default(string);
            var hiveKey = default(RegistryKey);
            var subKeyName = default(string);
            var hackItem = default(HackFolder);
            while (i < lines.Count)
            {
                if (string.IsNullOrEmpty(lines[i].Trim()))
                    continue;
                if (lines[i].StartsWith("["))
                {
                    if (hackItem != null)
                    {
                        hackItems.Add(hackItem);
                        hackItems = null;
                    }
                    keyValue = lines[i].Substring(1, lines[i].Length - 2);
                    if (keyValue.StartsWith(_hKLM))
                    {
                        subKeyName = keyValue.Substring(_hKLM.Length + 1);
                        hiveKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                            Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    }
                    else if (keyValue.StartsWith(_hKCR))
                    {
                        subKeyName = keyValue.Substring(_hKCR.Length + 1);
                        hiveKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot,
                            Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    }
                    else if (keyValue.StartsWith(_hKCC))
                    {
                        subKeyName = keyValue.Substring(_hKCC.Length + 1);
                        hiveKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig,
                            Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    }
                    else if (keyValue.StartsWith(_hKUS))
                    {
                        subKeyName = keyValue.Substring(_hKUS.Length + 1);
                        hiveKey = RegistryKey.OpenBaseKey(RegistryHive.Users,
                            Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    }
                    else if (keyValue.StartsWith(_kHCU))
                    {
                        subKeyName = keyValue.Substring(_kHCU.Length + 1);
                        hiveKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
                            Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
                    }
                    if (hiveKey == null)
                        return null;
                }
                else if (lines[i].StartsWith("\""))
                {
                    var lineParts = lines[i].Replace("\"", string.Empty).Split('=');
                    var itemName = lineParts[0];
                    var entry = default(HackItemBase);
                    if (!string.IsNullOrEmpty(itemName))
                    {
                        var itemDesiredValue = lineParts[1];
                        itemDesiredValue = itemDesiredValue.Replace("\\\\", "\\");
                        var kind = default(RegistryValueKind);
                        var itemCurrentValue = default(object);
                        var localKey = default(RegistryKey);
                        localKey = hiveKey.OpenSubKey(subKeyName);
                        if (localKey != null)
                            itemCurrentValue = localKey.GetValue(itemName);
                        if (hackItem == null)
                            hackItem = new HackFolder(hiveKey);

                        if (itemCurrentValue != null)
                        {
                            kind = localKey.GetValueKind(itemName);
                            switch (kind)
                            {
                                case RegistryValueKind.Binary:
                                    entry = new HackEntry<bool>(hackItem, itemName, Convert.ToBoolean(itemCurrentValue));
                                    break;

                                case RegistryValueKind.DWord:
                                    entry = new HackEntry<int>(hackItem, itemName, Convert.ToInt32(itemCurrentValue));
                                    break;

                                case RegistryValueKind.QWord:
                                    entry = new HackEntry<long>(hackItem, itemName, Convert.ToInt64(itemCurrentValue));
                                    break;

                                case RegistryValueKind.Unknown:
                                    //ignore
                                    break;

                                case RegistryValueKind.MultiString:
                                    entry = new HackEntry<string[]>(hackItem, itemName, (string[])itemCurrentValue);
                                    break;

                                case RegistryValueKind.ExpandString:
                                case RegistryValueKind.String:
                                case RegistryValueKind.None:
                                    entry = new HackEntry<string>(hackItem, itemName, Convert.ToString(itemCurrentValue));
                                    break;
                            }
                            hackItem.Items.Add(entry);
                        }
                    }
                }
                i++;
            }
            if (hackItem != null)
                hackItems.Add(hackItem);
            return hackItems.ToArray();
        }

        public static HackFolder[] GetHackFoldersFromFile(FileInfo file)
        {
            //file must be an export from regedit (i.e., .reg file)
            if (!file.Exists)
                return null;

            var data = default(string);
            using (var sr = new StreamReader(file.OpenRead()))
            {
                data = sr.ReadToEnd();
            }
            return GetHackFoldersFromData(data);
        }

        public static IList<HackItemBase> GetInitialItems()
        {
            var result = new List<HackItemBase>();
            var classesRoot = new HackFolder(Registry.ClassesRoot) { Id = 1, Name = _hKCR };
            var currentUser = new HackFolder(Registry.CurrentUser) { Id = 2, Name = _kHCU };
            var localMachine = new HackFolder(Registry.LocalMachine) { Id = 3, Name = _hKLM };
            var users = new HackFolder(Registry.Users) { Id = 4, Name = _hKUS };
            var currentConfig = new HackFolder(Registry.CurrentConfig) { Id = 5, Name = _hKCC };

            result.Add(classesRoot);
            result.Add(currentUser);
            result.Add(localMachine);
            result.Add(users);
            result.Add(currentConfig);
            return result;
        }

        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}