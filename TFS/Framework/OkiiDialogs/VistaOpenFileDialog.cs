namespace GregOsborne.Dialogs {
	using System.ComponentModel;
	using System.IO;
	using GregOsborne.Dialogs.Interop;
	using Microsoft.Win32;

	[Description("Prompts the user to open a file.")]
	public sealed class VistaOpenFileDialog : VistaFileDialog {
		private const int OpenDropDownId = 0x4002;
		private const int OpenItemId = 0x4003;
		private const int ReadOnlyItemId = 0x4004;
		private bool readOnlyChecked;
		private bool showReadOnly;

		public VistaOpenFileDialog() {
			if (!IsVistaFileDialogSupported) {
				this.DownlevelDialog = new OpenFileDialog();
			}
		}

		[DefaultValue(true)]
		[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
		public override bool CheckFileExists {
			get => base.CheckFileExists;
			set => base.CheckFileExists = value;
		}

		[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Multiselect {
			get => ((OpenFileDialog)this.DownlevelDialog)?.Multiselect ?? this.GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
			set {
				if (this.DownlevelDialog != null) {
					((OpenFileDialog)this.DownlevelDialog).Multiselect = value;
				}

				this.SetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT, value);
			}
		}

		[Description("A value indicating whether the dialog box contains a read-only check box.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ShowReadOnly {
			get => ((OpenFileDialog)this.DownlevelDialog)?.ShowReadOnly ?? this.showReadOnly;
			set {
				if (this.DownlevelDialog != null) {
					((OpenFileDialog)this.DownlevelDialog).ShowReadOnly = value;
				} else {
					this.showReadOnly = value;
				}
			}
		}

		[DefaultValue(false)]
		[Description("A value indicating whether the read-only check box is selected.")]
		[Category("Behavior")]
		public bool ReadOnlyChecked {
			get => ((OpenFileDialog)this.DownlevelDialog)?.ReadOnlyChecked ?? this.readOnlyChecked;
			set {
				if (this.DownlevelDialog != null) {
					((OpenFileDialog)this.DownlevelDialog).ReadOnlyChecked = value;
				} else {
					this.readOnlyChecked = value;
				}
			}
		}

		public override void Reset() {
			base.Reset();
			if (this.DownlevelDialog != null) {
				return;
			}

			this.CheckFileExists = true;
			this.showReadOnly = false;
			this.readOnlyChecked = false;
		}

		public Stream OpenFile() {
			if (this.DownlevelDialog != null) {
				return ((OpenFileDialog)this.DownlevelDialog).OpenFile();
			}

			var fileName = this.FileName;
			return new FileStream(fileName, FileMode.Open, FileAccess.Read);
		}

		internal override IFileDialog CreateFileDialog() => new NativeFileOpenDialog();

		internal override void SetDialogProperties(IFileDialog dialog) {
			base.SetDialogProperties(dialog);
			if (!this.showReadOnly) {
				return;
			}
			// ReSharper disable once SuspiciousTypeConversion.Global
			var customize = (IFileDialogCustomize)dialog;
			customize.EnableOpenDropDown(OpenDropDownId);
			customize.AddControlItem(OpenDropDownId, OpenItemId, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.OpenButton));
			customize.AddControlItem(OpenDropDownId, ReadOnlyItemId, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ReadOnly));
		}

		internal override void GetResult(IFileDialog dialog) {
			if (!this.Multiselect) {
				this.FileNamesInternal = null;
			} else {
				((IFileOpenDialog)dialog).GetResults(out var results);
				results.GetCount(out var count);
				var fileNames = new string[count];
				for (uint x = 0; x < count; ++x) {
					results.GetItemAt(x, out var item);
					item.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out var name);
					fileNames[x] = name;
				}
				this.FileNamesInternal = fileNames;
			}
			if (this.ShowReadOnly) {
				// ReSharper disable once SuspiciousTypeConversion.Global
				var customize = (IFileDialogCustomize)dialog;
				customize.GetSelectedControlItem(OpenDropDownId, out var selected);
				this.readOnlyChecked = selected == ReadOnlyItemId;
			}
			base.GetResult(dialog);
		}
	}
}