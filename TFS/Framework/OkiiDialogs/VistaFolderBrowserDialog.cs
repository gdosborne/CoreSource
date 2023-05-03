namespace GregOsborne.Dialogs {
	using System;
	using System.ComponentModel;
	using System.Drawing.Design;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows;
	using System.Windows.Interop;
	using GregOsborne.Dialogs.Interop;
	using Ookii.Dialogs.Wpf.Properties;

	[DefaultEvent("HelpRequest")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("SelectedPath")]
	[Description("Prompts the user to select a folder.")]
	public sealed class VistaFolderBrowserDialog {
		private string description;
		private string selectedPath;

		public VistaFolderBrowserDialog() => this.Reset();

		[Browsable(false)]
		public static bool IsVistaFolderDialogSupported => NativeMethods.IsWindowsVistaOrLater;

		[Category("Folder Browsing")]
		[DefaultValue("")]
		[Localizable(true)]
		[Browsable(true)]
		[Description("The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog.")]
		public string Description {
			get => this.description ?? string.Empty;
			set => this.description = value;
		}

		[Localizable(false)]
		[Description("The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.")]
		[Category("Folder Browsing")]
		[Browsable(true)]
		[DefaultValue(typeof(Environment.SpecialFolder), "Desktop")]
		public Environment.SpecialFolder RootFolder { get; set; }

		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Description("The path selected by the user.")]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Folder Browsing")]
		public string SelectedPath {
			get => this.selectedPath ?? string.Empty;
			set => this.selectedPath = value;
		}

		[Browsable(true)]
		[Localizable(false)]
		[Description("A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.")]
		[DefaultValue(true)]
		[Category("Folder Browsing")]
		public bool ShowNewFolderButton { get; set; }

		[Category("Folder Browsing")]
		[DefaultValue(false)]
		[Description("A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.")]
		public bool UseDescriptionForTitle { get; set; }

		public void Reset() {
			this.description = string.Empty;
			this.UseDescriptionForTitle = false;
			this.selectedPath = string.Empty;
			this.RootFolder = Environment.SpecialFolder.Desktop;
			this.ShowNewFolderButton = true;
		}

		public bool? ShowDialog() => this.ShowDialog(null);

		public bool? ShowDialog(Window owner) {
			var ownerHandle = owner == null ? NativeMethods.GetActiveWindow() : new WindowInteropHelper(owner).Handle;
			return IsVistaFolderDialogSupported ? this.RunDialog(ownerHandle) : this.RunDialogDownlevel(ownerHandle);
		}

		private bool RunDialog(IntPtr owner) {
			IFileDialog dialog = null;
			try {
				dialog = new NativeFileOpenDialog();
				this.SetDialogProperties(dialog);
				var result = dialog.Show(owner);
				if (result < 0) {
					if ((uint)result == (uint)HRESULT.ERROR_CANCELLED) {
						return false;
					} else {
						throw Marshal.GetExceptionForHR(result);
					}
				}

				this.GetResult(dialog);
				return true;
			}
			finally {
				if (dialog != null) {
					Marshal.FinalReleaseComObject(dialog);
				}
			}
		}

		private bool RunDialogDownlevel(IntPtr owner) {
			var rootItemIdList = IntPtr.Zero;
			var resultItemIdList = IntPtr.Zero;
			if (NativeMethods.SHGetSpecialFolderLocation(owner, this.RootFolder, ref rootItemIdList) != 0) {
				if (NativeMethods.SHGetSpecialFolderLocation(owner, 0, ref rootItemIdList) != 0) {
					throw new InvalidOperationException(Resources.FolderBrowserDialogNoRootFolder);
				}
			}

			try {
				var info = new NativeMethods.BROWSEINFO {
					hwndOwner = owner,
					lpfn = BrowseCallbackProc,
					lpszTitle = Description,
					pidlRoot = rootItemIdList,
					pszDisplayName = new string('\0', 260),
					ulFlags = NativeMethods.BrowseInfoFlags.NewDialogStyle | NativeMethods.BrowseInfoFlags.ReturnOnlyFsDirs
				};
				if (!this.ShowNewFolderButton) {
					info.ulFlags |= NativeMethods.BrowseInfoFlags.NoNewFolderButton;
				}

				resultItemIdList = NativeMethods.SHBrowseForFolder(ref info);
				if (resultItemIdList != IntPtr.Zero) {
					var path = new StringBuilder(260);
					NativeMethods.SHGetPathFromIDList(resultItemIdList, path);
					this.SelectedPath = path.ToString();
					return true;
				} else {
					return false;
				}
			}
			finally {
				if (rootItemIdList != null) {
					var malloc = NativeMethods.SHGetMalloc();
					malloc.Free(rootItemIdList);
					Marshal.ReleaseComObject(malloc);
				}
				if (resultItemIdList != null) {
					Marshal.FreeCoTaskMem(resultItemIdList);
				}
			}
		}

		private void SetDialogProperties(IFileDialog dialog) {
			if (!string.IsNullOrEmpty(this.description)) {
				if (this.UseDescriptionForTitle) {
					dialog.SetTitle(this.description);
				} else {
					// ReSharper disable once SuspiciousTypeConversion.Global
					var customize = (IFileDialogCustomize)dialog;
					customize.AddText(0, this.description);
				}
			}

			dialog.SetOptions(NativeMethods.FOS.FOS_PICKFOLDERS | NativeMethods.FOS.FOS_FORCEFILESYSTEM | NativeMethods.FOS.FOS_FILEMUSTEXIST);
			if (string.IsNullOrEmpty(this.selectedPath)) {
				return;
			}

			var parent = Path.GetDirectoryName(this.selectedPath);
			if (parent == null || !Directory.Exists(parent)) {
				dialog.SetFileName(this.selectedPath);
			} else {
				var folder = Path.GetFileName(this.selectedPath);
				dialog.SetFolder(NativeMethods.CreateItemFromParsingName(parent));
				dialog.SetFileName(folder);
			}
		}

		private void GetResult(IFileDialog dialog) {
			dialog.GetResult(out var item);
			item.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out this.selectedPath);
		}

		private int BrowseCallbackProc(IntPtr hwnd, NativeMethods.FolderBrowserDialogMessage msg, IntPtr lParam, IntPtr wParam) {
			// ReSharper disable once ConvertIfStatementToSwitchStatement
			if (msg == NativeMethods.FolderBrowserDialogMessage.Initialized) {
				if (this.SelectedPath.Length != 0) {
					NativeMethods.SendMessage(hwnd, NativeMethods.FolderBrowserDialogMessage.SetSelection, new IntPtr(1), this.SelectedPath);
				}
			} else if (msg == NativeMethods.FolderBrowserDialogMessage.SelChanged) {
				if (lParam == IntPtr.Zero) {
					return 0;
				}

				var path = new StringBuilder(260);
				var validPath = NativeMethods.SHGetPathFromIDList(lParam, path);
				NativeMethods.SendMessage(hwnd, NativeMethods.FolderBrowserDialogMessage.EnableOk, IntPtr.Zero, validPath ? new IntPtr(1) : IntPtr.Zero);
			}
			return 0;
		}
	}
}