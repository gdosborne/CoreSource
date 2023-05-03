using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GregOsborne.RegistryHack.Data
{
    public sealed class HackFolder : HackItemBase
    {
        private IList<HackItemBase> _items;
        private RegistryKey _regKey;

        public HackFolder(RegistryKey regKey)
            : this()
        {
            RegKey = regKey;
            var allNames = RegKey.Name.Split('\\');
            Name = allNames[allNames.Length - 1];
        }

        public HackFolder()
        {
            HackItemType = HackTypes.Folder;
            Items = new List<HackItemBase>();
        }

        public IList<HackItemBase> Items {
            get {
                return _items;
            }
            set {
                _items = value;
                InvokePropertyChanged(nameof(Items));
            }
        }

        public int NumberOfSubItems {
            get {
                if (Items == null)
                    return 0;
                return Items.Where(x => x.HackItemType == HackTypes.Folder)
                    .Cast<HackFolder>()
                    .Sum(x => x.NumberOfSubItems) + 1;
            }
        }

        public RegistryKey RegKey {
            get {
                return _regKey;
            }
            set {
                _regKey = value;
                InvokePropertyChanged(nameof(RegKey));
            }
        }
    }
}