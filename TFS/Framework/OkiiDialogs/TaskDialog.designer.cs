namespace GregOsborne.Dialogs {
	partial class TaskDialog {
		private System.ComponentModel.IContainer components = null;
		protected override void Dispose(bool disposing) {
			try {
				if (disposing) {
					if (components != null) {
						components.Dispose();
						components = null;
					}
					if (buttons != null) {
						foreach (TaskDialogButton button in buttons) {
							button.Dispose();
						}
						buttons.Clear();
					}
					if (radioButtons != null) {
						foreach (TaskDialogRadioButton radioButton in radioButtons) {
							radioButton.Dispose();
						}
						radioButtons.Clear();
					}
				}
			}
			finally {
				base.Dispose(disposing);
			}
		}
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
		}
	}
}
