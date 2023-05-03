using Windows.UI.Popups;

namespace MVVMFramework {
	public class UIDelegateCommand : IUICommand {
		public UIDelegateCommand()
			: this(null) { }

		public UIDelegateCommand(object id)
			: this(id, null) {
		}

		public UIDelegateCommand(object id, string label)
			: this(id, label, null) {
		}

		public UIDelegateCommand(object id, string label, UICommandInvokedHandler invoked) {
			this.Id = id;
			this.Label = label;
			this.Invoked = invoked;
		}

		public object Id { get; set; }
		public UICommandInvokedHandler Invoked { get; set; }
		public string Label { get; set; }
	}
}
