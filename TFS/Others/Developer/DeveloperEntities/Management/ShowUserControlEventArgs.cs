namespace SNC.OptiRamp.Application.DeveloperEntities.Management {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Controls;

	public delegate void ShowUserControlHandler(object sender, ShowUserControlEventArgs e);
	public class ShowUserControlEventArgs : EventArgs {
		public ShowUserControlEventArgs(UserControl control) {
			Control = control;
		}
		public UserControl Control {
			get;
			private set;
		}
	}
}
