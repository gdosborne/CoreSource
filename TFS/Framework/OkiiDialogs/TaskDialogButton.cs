namespace GregOsborne.Dialogs {
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.Drawing.Design;
	using System.Linq;
	using Ookii.Dialogs.Wpf.Properties;

	public class TaskDialogButton : TaskDialogItem {
		private string commandLinkNote;
		private bool @default;
		private bool elevationRequired;
		private ButtonType type;

		public TaskDialogButton() {
		}

		public TaskDialogButton(ButtonType type)
			: base((int)type) => this.type = type;

		public TaskDialogButton(IContainer container)
			: base(container) {
		}

		public TaskDialogButton(string text) => this.Text = text;

		[Category("Appearance")]
		[Description("The type of the button.")]
		[DefaultValue(ButtonType.Custom)]
		public ButtonType ButtonType {
			get => this.type;
			set {
				if (value != ButtonType.Custom) {
					this.CheckDuplicateButton(value, null);
					this.type = value;
					base.Id = (int)value;
				} else {
					this.type = value;
					this.AutoAssignId();
					this.UpdateOwner();
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the note associated with a command link button.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string CommandLinkNote {
			get => this.commandLinkNote ?? string.Empty;
			set {
				this.commandLinkNote = value;
				this.UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates if the button is the default button on the dialog.")]
		[DefaultValue(false)]
		public bool Default {
			get => this.@default;
			set {
				this.@default = value;
				if (value && this.Owner != null) {
					foreach (var button in this.Owner.Buttons) {
						if (button != this) {
							button.Default = false;
						}
					}
				}

				this.UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
		[DefaultValue(false)]
		public bool ElevationRequired {
			get => this.elevationRequired;
			set {
				this.elevationRequired = value;
				this.Owner?.SetButtonElevationRequired(this);
			}
		}

		internal override int Id {
			get => base.Id;
			set {
				if (base.Id == value) {
					return;
				}

				if (this.type != ButtonType.Custom) {
					throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
				}

				base.Id = value;
			}
		}

		internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag {
			get {
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch (this.type) {
					case ButtonType.Ok:
						return NativeMethods.TaskDialogCommonButtonFlags.OkButton;
					case ButtonType.Yes:
						return NativeMethods.TaskDialogCommonButtonFlags.YesButton;
					case ButtonType.No:
						return NativeMethods.TaskDialogCommonButtonFlags.NoButton;
					case ButtonType.Cancel:
						return NativeMethods.TaskDialogCommonButtonFlags.CancelButton;
					case ButtonType.Retry:
						return NativeMethods.TaskDialogCommonButtonFlags.RetryButton;
					case ButtonType.Close:
						return NativeMethods.TaskDialogCommonButtonFlags.CloseButton;
					default:
						return 0;
				}
			}
		}

		protected override IEnumerable ItemCollection => this.Owner?.Buttons;

		internal override void AutoAssignId() {
			if (this.type == ButtonType.Custom) {
				base.AutoAssignId();
			}
		}

		internal override void CheckDuplicate(TaskDialogItem itemToExclude) {
			this.CheckDuplicateButton(this.type, itemToExclude);
			base.CheckDuplicate(itemToExclude);
		}

		private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude) {
			if (type == ButtonType.Custom || this.Owner == null) {
				return;
			}

			if (this.Owner.Buttons.Any(button => button != this && button != itemToExclude && button.ButtonType == type)) {
				throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
			}
		}
	}
}