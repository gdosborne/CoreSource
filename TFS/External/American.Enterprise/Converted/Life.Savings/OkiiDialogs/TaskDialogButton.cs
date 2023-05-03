using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using Ookii.Dialogs.Wpf.Properties;

namespace Ookii.Dialogs.Wpf {
    public class TaskDialogButton : TaskDialogItem {
        private string _commandLinkNote;
        private bool _default;
        private bool _elevationRequired;
        private ButtonType _type;

        public TaskDialogButton() {
        }

        public TaskDialogButton(ButtonType type)
            : base((int) type) {
            _type = type;
        }

        public TaskDialogButton(IContainer container)
            : base(container) {
        }

        public TaskDialogButton(string text) {
            Text = text;
        }

        [Category("Appearance")]
        [Description("The type of the button.")]
        [DefaultValue(ButtonType.Custom)]
        public ButtonType ButtonType {
            get { return _type; }
            set {
                if (value != ButtonType.Custom) {
                    CheckDuplicateButton(value, null);
                    _type = value;
                    base.Id = (int) value;
                }
                else {
                    _type = value;
                    AutoAssignId();
                    UpdateOwner();
                }
            }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [Description("The text of the note associated with a command link button.")]
        [DefaultValue("")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string CommandLinkNote {
            get => _commandLinkNote ?? string.Empty;
            set {
                _commandLinkNote = value;
                UpdateOwner();
            }
        }

        [Category("Behavior")]
        [Description("Indicates if the button is the default button on the dialog.")]
        [DefaultValue(false)]
        public bool Default {
            get => _default;
            set {
                _default = value;
                if (value && Owner != null)
                    foreach (var button in Owner.Buttons)
                        if (button != this)
                            button.Default = false;
                UpdateOwner();
            }
        }

        [Category("Behavior")]
        [Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
        [DefaultValue(false)]
        public bool ElevationRequired {
            get => _elevationRequired;
            set {
                _elevationRequired = value;
                Owner?.SetButtonElevationRequired(this);
            }
        }

        internal override int Id {
            get => base.Id;
            set {
                if (base.Id == value) return;
                if (_type != ButtonType.Custom)
                    throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
                base.Id = value;
            }
        }

        internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag {
            get {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (_type) {
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

        protected override IEnumerable ItemCollection => Owner?.Buttons;

        internal override void AutoAssignId() {
            if (_type == ButtonType.Custom)
                base.AutoAssignId();
        }

        internal override void CheckDuplicate(TaskDialogItem itemToExclude) {
            CheckDuplicateButton(_type, itemToExclude);
            base.CheckDuplicate(itemToExclude);
        }

        private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude) {
            if (type == ButtonType.Custom || Owner == null) return;
            if (Owner.Buttons.Any(button => button != this && button != itemToExclude && button.ButtonType == type)) {
                throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
            }
        }
    }
}