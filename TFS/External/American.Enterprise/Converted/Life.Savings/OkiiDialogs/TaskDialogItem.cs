using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Ookii.Dialogs.Wpf.Properties;

namespace Ookii.Dialogs.Wpf {
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    public abstract partial class TaskDialogItem : Component {
        private bool _enabled = true;
        private int _id;
        private TaskDialog _owner;
        private string _text;

        protected TaskDialogItem() {
            InitializeComponent();
        }

        protected TaskDialogItem(IContainer container) {
            container?.Add(this);
            InitializeComponent();
        }

        internal TaskDialogItem(int id) {
            InitializeComponent();
            _id = id;
        }

        [Browsable(false)]
        public TaskDialog Owner {
            get => _owner;
            internal set {
                _owner = value;
                AutoAssignId();
            }
        }

        [Localizable(true)]
        [Category("Appearance")]
        [Description("The text of the item.")]
        [DefaultValue("")]
        public string Text {
            get => _text ?? string.Empty;
            set {
                _text = value;
                UpdateOwner();
            }
        }

        [Category("Behavior")]
        [Description("Indicates whether the item is enabled.")]
        [DefaultValue(true)]
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;
                Owner?.SetItemEnabled(this);
            }
        }

        [Category("Data")]
        [Description("The id of the item.")]
        [DefaultValue(0)]
        internal virtual int Id {
            get => _id;
            set {
                CheckDuplicateId(null, value);
                _id = value;
                UpdateOwner();
            }
        }

        protected abstract IEnumerable ItemCollection { get; }

        public void Click() {
            if (Owner == null)
                throw new InvalidOperationException(Resources.NoAssociatedTaskDialogError);
            Owner.ClickItem(this);
        }

        protected void UpdateOwner() {
            Owner?.UpdateDialog();
        }

        internal virtual void CheckDuplicate(TaskDialogItem itemToExclude) {
            CheckDuplicateId(itemToExclude, _id);
        }

        internal virtual void AutoAssignId() {
            if (ItemCollection == null) return;
            var highestId = (from TaskDialogItem item in ItemCollection select item.Id).Concat(new[] {9}).Max();
            Id = highestId + 1;
        }

        private void CheckDuplicateId(IDisposable itemToExclude, int id) {
            if (id == 0) return;
            var items = ItemCollection;
            if (items == null) return;
            if (items.Cast<TaskDialogItem>().Any(item => item != this && !Equals(item, itemToExclude) && item.Id == id)) {
                throw new InvalidOperationException(Resources.DuplicateItemIdError);
            }
        }
    }
}