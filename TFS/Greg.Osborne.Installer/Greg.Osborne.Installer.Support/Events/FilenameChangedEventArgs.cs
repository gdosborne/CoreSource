using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Osborne.Installer.Support.Events {
	public delegate void FilenameChangedHandler(object sender, FilenameChangedEventArgs e);
	public class FilenameChangedEventArgs : EventArgs{
		public FilenameChangedEventArgs(string oldFilename) {
			this.OldFilename = oldFilename;
		}

		public string OldFilename { get; private set; } = default;

	}
}
