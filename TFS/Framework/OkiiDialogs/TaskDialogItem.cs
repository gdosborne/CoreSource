namespace GregOsborne.Dialogs {
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Linq;
	using Ookii.Dialogs.Wpf.Properties;

	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	public abstract partial class TaskDialogItem : Component {
		private bool enabled = true;
		private int id;
		private TaskDialog owner;
		private string text;

		protected TaskDialogItem() => this.InitializeComponent();

		protected TaskDialogItem(IContainer container) {
			container?.Add(this);
			this.InitializeComponent();
		}

		internal TaskDialogItem(int id) {
			this.InitializeComponent();
			this.id = id;
		}

		[Browsable(false)]
		public TaskDialog Owner {
			get => this.owner;
			internal set {
				this.owner = value;
				this.AutoAssignId();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text of the item.")]
		[DefaultValue("")]
		public string Text {
			get => this.text ?? string.Empty;
			set {
				this.text = value;
				this.UpdateOwner();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the item is enabled.")]
		[DefaultValue(true)]
		public bool Enabled {
			get => this.enabled;
			set {
				this.enabled = value;
				this.Owner?.SetItemEnabled(this);
			}
		}

		[Category("Data")]
		[Description("The id of the item.")]
		[DefaultValue(0)]
		internal virtual int Id {
			get => this.id;
			set {
				this.CheckDuplicateId(null, value);
				this.id = value;
				this.UpdateOwner();
			}
		}

		protected abstract IEnumerable ItemCollection { get; }

		public void Click() {
			if (this.Owner == null) {
				throw new InvalidOperationException(Resources.NoAssociatedTaskDialogError);
			}

			this.Owner.ClickItem(this);
		}

		protected void UpdateOwner() => this.Owner?.UpdateDialog();

		internal virtual void CheckDuplicate(TaskDialogItem itemToExclude) => this.CheckDuplicateId(itemToExclude, this.id);

		internal virtual void AutoAssignId() {
			if (this.ItemCollection == null) {
				return;
			}

			var highestId = (from TaskDialogItem item in this.ItemCollection select item.Id).Concat(new[] { 9 }).Max();
			this.Id = highestId + 1;
		}

		private void CheckDuplicateId(IDisposable itemToExclude, int id) {
			if (id == 0) {
				return;
			}

			var items = this.ItemCollection;
			if (items == null) {
				return;
			}

			if (items.Cast<TaskDialogItem>().Any(item => item != this && !Equals(item, itemToExclude) && item.Id == id)) {
				throw new InvalidOperationException(Resources.DuplicateItemIdError);
			}
		}
	}
}