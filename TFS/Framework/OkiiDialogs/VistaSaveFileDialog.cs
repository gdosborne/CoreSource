namespace GregOsborne.Dialogs {
	using System.ComponentModel;
	using System.IO;
	using System.Windows;
	using GregOsborne.Dialogs.Interop;
	using Microsoft.Win32;

	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Description("Prompts the user to open a file.")]
	public sealed class VistaSaveFileDialog : VistaFileDialog {
		public VistaSaveFileDialog() {
			if (!IsVistaFileDialogSupported) {
				this.DownlevelDialog = new SaveFileDialog();
			}
		}

		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.")]
		public bool CreatePrompt {
			get => ((SaveFileDialog)this.DownlevelDialog)?.CreatePrompt ?? this.GetOption(NativeMethods.FOS.FOS_CREATEPROMPT);
			set {
				if (this.DownlevelDialog != null) {
					((SaveFileDialog)this.DownlevelDialog).CreatePrompt = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_CREATEPROMPT, value);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
		public bool OverwritePrompt {
			get => ((SaveFileDialog)this.DownlevelDialog)?.OverwritePrompt ?? this.GetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT);
			set {
				if (this.DownlevelDialog != null) {
					((SaveFileDialog)this.DownlevelDialog).OverwritePrompt = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT, value);
				}
			}
		}

		public override void Reset() {
			base.Reset();
			if (this.DownlevelDialog == null) {
				this.OverwritePrompt = true;
			}
		}

		public Stream OpenFile() {
			if (this.DownlevelDialog != null) {
				return ((SaveFileDialog)this.DownlevelDialog).OpenFile();
			}
			var fileName = this.FileName;
			return new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
		}

		protected override void OnFileOk(CancelEventArgs e) {
			if (this.DownlevelDialog == null) {
				if (this.CheckFileExists && !File.Exists(this.FileName)) {
					this.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.FileNotFound, Path.GetFileName(this.FileName)), MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
					e.Cancel = true;
					return;
				}
				if (this.CreatePrompt && !File.Exists(this.FileName)) {
					if (!this.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.CreatePrompt, Path.GetFileName(this.FileName)), MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)) {
						e.Cancel = true;
						return;
					}
				}
			}
			base.OnFileOk(e);
		}

		internal override IFileDialog CreateFileDialog() => new NativeFileSaveDialog();
	}
}