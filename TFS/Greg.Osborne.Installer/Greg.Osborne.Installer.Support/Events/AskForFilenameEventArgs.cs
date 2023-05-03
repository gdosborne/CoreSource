using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greg.Osborne.Installer.Support.Events {
	public delegate void AskForFilenameHandler(object sender, AskForFilenameEventArgs e);

	public class AskForFilenameEventArgs : EventArgs {
		public string Filename { get; set; }
	}
}
