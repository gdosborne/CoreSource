using System;
using System.Collections.Generic;
using GregOsborne.MVVMFramework;

namespace Greg.Osborne.Installer.Builder.Controls {
	public class FontItem {
		public string Item { get; set; }
		public double FontSize { get; set; }
		public int Index { get; set; }
		private DelegateCommand selectMeCommand = default;
		public DelegateCommand SelectMeCommand => this.selectMeCommand ?? (this.selectMeCommand = new DelegateCommand(this.SelectMe, this.ValidateSelectMeState));
		private bool ValidateSelectMeState(object state) => true;
		private void SelectMe(object state) {
			var p = new Dictionary<string, object> {
				{ "index",  (int)state }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("SelectImage", p));
		}
		public event ExecuteUiActionHandler ExecuteUiAction;
	}
}
