namespace GregOsborne.Dialogs {
	using System.Collections;
	using System.ComponentModel;

	public class TaskDialogRadioButton : TaskDialogItem {
        private bool @checked;

        public TaskDialogRadioButton() {
        }

        public TaskDialogRadioButton(IContainer container)
            : base(container) {
        }

        [Category("Appearance")]
        [Description("Indicates whether the radio button is checked.")]
        [DefaultValue(false)]
        public bool Checked {
            get => @checked;
            set {
                @checked = value;
                if (!value || Owner == null) return;
                foreach (var radioButton in Owner.RadioButtons)
                    if (radioButton != this)
                        radioButton.Checked = false;
            }
        }

        protected override IEnumerable ItemCollection => Owner?.RadioButtons;
    }
}