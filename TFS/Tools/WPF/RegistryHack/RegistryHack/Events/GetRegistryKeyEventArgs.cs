using GregOsborne.RegistryHack.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryHack.Events
{
    public delegate void GetRegistryKeyHandler(object sender, GetRegistryKeyEventArgs e);

    public class GetRegistryKeyEventArgs : EventArgs
    {
        public GetRegistryKeyEventArgs(HackFolder parentFolder, RegistryKey parent, string path)
        {
            ParentFolder = parentFolder;
            Parent = parent;
            Path = path;
        }

        public RegistryKey Parent { get; private set; }
        public HackFolder ParentFolder { get; set; }
        public string Path { get; private set; }
        public RegistryKey Result { get; set; }
    }
}