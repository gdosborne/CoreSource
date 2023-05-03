namespace GregOsborne.Dialogs {
	using System;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Windows;
	using System.Windows.Interop;
	using GregOsborne.Dialogs.Interop;
	using Microsoft.Win32;
	using Ookii.Dialogs.Wpf.Properties;

	[DefaultEvent("FileOk")]
	[DefaultProperty("FileName")]
	public abstract class VistaFileDialog {
		internal const int HelpButtonId = 0x4001;
		private bool addExtension;
		private string defaultExt;
		private FileDialog downlevelDialog;
		private string[] fileNames;
		private string filter;
		private int filterIndex;
		private string initialDirectory;
		private NativeMethods.FOS options;
		private Window owner;
		private string title;

		protected VistaFileDialog() => this.Reset();

		[Browsable(false)]
		public static bool IsVistaFileDialogSupported => NativeMethods.IsWindowsVistaOrLater;

		[Description("A value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool AddExtension {
			get => this.DownlevelDialog?.AddExtension ?? this.addExtension;
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.AddExtension = value;
				} else {
					this.addExtension = value;
				}
			}
		}

		[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool CheckFileExists {
			get => this.DownlevelDialog?.CheckFileExists ?? this.GetOption(NativeMethods.FOS.FOS_FILEMUSTEXIST);
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.CheckFileExists = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_FILEMUSTEXIST, value);
				}
			}
		}

		[Description("A value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool CheckPathExists {
			get => this.DownlevelDialog?.CheckPathExists ?? this.GetOption(NativeMethods.FOS.FOS_PATHMUSTEXIST);
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.CheckPathExists = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_PATHMUSTEXIST, value);
				}
			}
		}

		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The default file name extension.")]
		public string DefaultExt {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.DefaultExt;
				}

				return this.defaultExt ?? string.Empty;
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.DefaultExt = value;
				} else {
					if (value != null) {
						if (value.StartsWith(".", StringComparison.CurrentCulture)) {
							value = value.Substring(1);
						} else if (value.Length == 0) {
							value = null;
						}
					}

					this.defaultExt = value;
				}
			}
		}

		[Category("Behavior")]
		[Description("A value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).")]
		[DefaultValue(true)]
		public bool DereferenceLinks {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.DereferenceLinks;
				}

				return !this.GetOption(NativeMethods.FOS.FOS_NODEREFERENCELINKS);
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.DereferenceLinks = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_NODEREFERENCELINKS, !value);
				}
			}
		}

		[DefaultValue("")]
		[Category("Data")]
		[Description("A string containing the file name selected in the file dialog box.")]
		public string FileName {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.FileName;
				}

				if (this.fileNames == null || this.fileNames.Length == 0 || string.IsNullOrEmpty(this.fileNames[0])) {
					return string.Empty;
				}

				return this.fileNames[0];
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.FileName = value;
				}

				this.fileNames = new string[1];
				this.fileNames[0] = value;
			}
		}

		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Description("The file names of all selected files in the dialog box.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] FileNames => this.DownlevelDialog != null ? this.DownlevelDialog.FileNames : this.FileNamesInternal;

		[Description("The current file name filter string, which determines the choices that appear in the \"Save as file type\" or \"Files of type\" box in the dialog box.")]
		[Category("Behavior")]
		[Localizable(true)]
		[DefaultValue("")]
		public string Filter {
			get => this.DownlevelDialog != null ? this.DownlevelDialog.Filter : this.filter;
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.Filter = value;
				} else {
					if (value != this.filter) {
						if (!string.IsNullOrEmpty(value)) {
							var filterElements = value.Split('|');
							if (filterElements.Length % 2 != 0) {
								throw new ArgumentException(Resources.InvalidFilterString);
							}
						} else {
							value = null;
						}
						this.filter = value;
					}
				}
			}
		}

		[Description("The index of the filter currently selected in the file dialog box.")]
		[Category("Behavior")]
		[DefaultValue(1)]
		public int FilterIndex {
			get => this.DownlevelDialog?.FilterIndex ?? this.filterIndex;
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.FilterIndex = value;
				} else {
					this.filterIndex = value;
				}
			}
		}

		[Description("The initial directory displayed by the file dialog box.")]
		[DefaultValue("")]
		[Category("Data")]
		public string InitialDirectory {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.InitialDirectory;
				}

				return this.initialDirectory ?? string.Empty;
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.InitialDirectory = value;
				} else {
					this.initialDirectory = value;
				}
			}
		}

		[DefaultValue(false)]
		[Description("A value indicating whether the dialog box restores the current directory before closing.")]
		[Category("Behavior")]
		public bool RestoreDirectory {
			get => this.DownlevelDialog?.RestoreDirectory ?? this.GetOption(NativeMethods.FOS.FOS_NOCHANGEDIR);
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.RestoreDirectory = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_NOCHANGEDIR, value);
				}
			}
		}

		[Description("The file dialog box title.")]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Title {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.Title;
				}

				return this.title ?? string.Empty;
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.Title = value;
				} else {
					this.title = value;
				}
			}
		}

		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box accepts only valid Win32 file names.")]
		public bool ValidateNames {
			get {
				if (this.DownlevelDialog != null) {
					return this.DownlevelDialog.ValidateNames;
				}

				return !this.GetOption(NativeMethods.FOS.FOS_NOVALIDATE);
			}
			set {
				if (this.DownlevelDialog != null) {
					this.DownlevelDialog.ValidateNames = value;
				} else {
					this.SetOption(NativeMethods.FOS.FOS_NOVALIDATE, !value);
				}
			}
		}

		[Browsable(false)]
		protected FileDialog DownlevelDialog {
			get => this.downlevelDialog;
			set {
				this.downlevelDialog = value;
				if (value != null) {
					value.FileOk += this.DownlevelDialog_FileOk;
				}
			}
		}

		internal string[] FileNamesInternal {
			private get {
				if (this.fileNames == null) {
					return new string[0];
				}

				return (string[])this.fileNames.Clone();
			}
			set => this.fileNames = value;
		}

		[Description("Event raised when the user clicks on the Open or Save button on a file dialog box.")]
		[Category("Action")]
		public event CancelEventHandler FileOk;

		public virtual void Reset() {
			if (this.DownlevelDialog != null) {
				this.DownlevelDialog.Reset();
			} else {
				this.fileNames = null;
				this.filter = null;
				this.filterIndex = 1;
				this.addExtension = true;
				this.defaultExt = null;
				this.options = 0;
				this.title = null;
				this.CheckPathExists = true;
			}
		}

		public bool? ShowDialog() => this.ShowDialog(null);

		public bool? ShowDialog(Window owner) {
			this.owner = owner;
			if (this.DownlevelDialog != null) {
				return this.DownlevelDialog.ShowDialog(owner);
			}
			var ownerHandle = owner == null ? NativeMethods.GetActiveWindow() : new WindowInteropHelper(owner).Handle;
			return this.RunFileDialog(ownerHandle);
		}

		internal void SetOption(NativeMethods.FOS option, bool value) {
			if (value) {
				this.options |= option;
			} else {
				this.options &= ~option;
			}
		}

		internal bool GetOption(NativeMethods.FOS option) => (this.options & option) != 0;

		internal virtual void GetResult(IFileDialog dialog) {
			if (this.GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT)) {
				return;
			}

			this.fileNames = new string[1];
			dialog.GetResult(out var result);
			result.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out this.fileNames[0]);
		}

		protected virtual void OnFileOk(CancelEventArgs e) {
			var handler = FileOk;
			handler?.Invoke(this, e);
		}

		internal bool PromptUser(string text, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defaultResult) {
			var caption = string.IsNullOrEmpty(this.title) ? (this is VistaOpenFileDialog ? ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.Open) : ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ConfirmSaveAs)) : this.title;
			MessageBoxOptions options = 0;
			if (Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft) {
				options |= MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
			}

			return MessageBox.Show(this.owner, text, caption, buttons, icon, defaultResult, options) == MessageBoxResult.Yes;
		}

		internal virtual void SetDialogProperties(IFileDialog dialog) {
			dialog.Advise(new VistaFileDialogEvents(this), out var cookie);
			if (!(this.fileNames == null || this.fileNames.Length == 0 || string.IsNullOrEmpty(this.fileNames[0]))) {
				var parent = Path.GetDirectoryName(this.fileNames[0]);
				if (parent == null || !Directory.Exists(parent)) {
					dialog.SetFileName(this.fileNames[0]);
				} else {
					var folder = Path.GetFileName(this.fileNames[0]);
					dialog.SetFolder(NativeMethods.CreateItemFromParsingName(parent));
					dialog.SetFileName(folder);
				}
			}
			if (!string.IsNullOrEmpty(this.filter)) {
				var filterElements = this.filter.Split('|');
				var filter = new NativeMethods.COMDLG_FILTERSPEC[filterElements.Length / 2];
				for (var x = 0; x < filterElements.Length; x += 2) {
					filter[x / 2].pszName = filterElements[x];
					filter[x / 2].pszSpec = filterElements[x + 1];
				}
				dialog.SetFileTypes((uint)filter.Length, filter);
				if (this.filterIndex > 0 && this.filterIndex <= filter.Length) {
					dialog.SetFileTypeIndex((uint)this.filterIndex);
				}
			}
			if (this.addExtension && !string.IsNullOrEmpty(this.defaultExt)) {
				dialog.SetDefaultExtension(this.defaultExt);
			}

			if (!string.IsNullOrEmpty(this.initialDirectory) && Directory.Exists(this.initialDirectory)) {
				var item = NativeMethods.CreateItemFromParsingName(this.initialDirectory);
				dialog.SetDefaultFolder(item);
			}
			if (!string.IsNullOrEmpty(this.title)) {
				dialog.SetTitle(this.title);
			}

			dialog.SetOptions(this.options | NativeMethods.FOS.FOS_FORCEFILESYSTEM);
		}

		internal abstract IFileDialog CreateFileDialog();

		internal bool DoFileOk(IFileDialog dialog) {
			this.GetResult(dialog);
			var e = new CancelEventArgs();
			this.OnFileOk(e);
			return !e.Cancel;
		}

		private bool RunFileDialog(IntPtr hwndOwner) {
			IFileDialog dialog = null;
			try {
				dialog = this.CreateFileDialog();
				this.SetDialogProperties(dialog);
				var result = dialog.Show(hwndOwner);
				if (result >= 0) {
					return true;
				}

				if ((uint)result == (uint)HRESULT.ERROR_CANCELLED) {
					return false;
				} else {
					throw Marshal.GetExceptionForHR(result);
				}
			}
			finally {
				if (dialog != null) {
					Marshal.FinalReleaseComObject(dialog);
				}
			}
		}

		private void DownlevelDialog_FileOk(object sender, CancelEventArgs e) => this.OnFileOk(e);
	}
}