// ----------------------------------------------------------------------- Copyright © Statistics & Controls, Inc 2016 Created by: Greg Osborne -----------------------------------------------------------------------
//
// Event Support
namespace ProjectFileManager {

	using System;
	using System.Collections.Generic;
	using System.IO;

	using System.Linq;

	public delegate void CancelHandler(object sender, EventArgs e);
	public delegate void OKHandler(object sender, FileSelectedEventArgs e);

	public class FileSelectedEventArgs : EventArgs {

		#region Public Constructors
		public FileSelectedEventArgs(string fileName, Stream fileStream, string address) {
			FileName = fileName;
			FileStream = fileStream;
			Address = address;
		}
		#endregion Public Constructors

		#region Public Properties
		public string Address {
			get;
			private set;
		}
		public string FileName {
			get;
			private set;
		}
		public Stream FileStream {
			get;
			private set;
		}
		#endregion Public Properties
	}
}
