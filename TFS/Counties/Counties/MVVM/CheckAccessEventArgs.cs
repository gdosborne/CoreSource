using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Counties.MVVM {
	public delegate void CheckAccessEventHandler(object sender, CheckAccessEventArgs e);

	public class CheckAccessEventArgs : EventArgs {
		#region Public Properties

		public Dispatcher Dispatcher { get; set; }

		public bool HasAccess { get; set; }

		#endregion Public Properties
	}
}
