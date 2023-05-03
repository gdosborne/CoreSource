using GregOsborne.MVVMFramework;
using GregOsborne.RegistryHack.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryHack
{
    internal class MainWindowView : ViewModelBase
    {
        private ObservableCollection<HackItemBase> _HackItems;

        private HackFolder _SelectedFolder;

        private string _SelectedFolderPath;

        public MainWindowView()
        {
            HackItems = new ObservableCollection<HackItemBase>(App.HackItemsTree);
        }

        public ObservableCollection<HackItemBase> HackItems {
            get {
                return _HackItems;
            }
            set {
                _HackItems = value;
                InvokePropertyChanged(nameof(HackItems));
            }
        }

        public HackFolder SelectedFolder {
            get {
                return _SelectedFolder;
            }
            set {
                _SelectedFolder = value;
                if (SelectedFolder != null)
                    SelectedFolderPath = SelectedFolder.RegKey.Name;
                InvokePropertyChanged(nameof(SelectedFolder));
            }
        }

        public string SelectedFolderPath {
            get {
                return _SelectedFolderPath;
            }
            set {
                _SelectedFolderPath = value;
                InvokePropertyChanged(nameof(SelectedFolderPath));
            }
        }
    }
}