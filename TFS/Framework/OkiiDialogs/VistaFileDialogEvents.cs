namespace GregOsborne.Dialogs {
	using System;

	internal class VistaFileDialogEvents : Interop.IFileDialogEvents, Interop.IFileDialogControlEvents {
		public static uint SOk { get; } = 0;
		public static uint SFalse { get; } = 1;
		public static uint ENotimpl { get; } = 0x80004001;
		private readonly VistaFileDialog dialog;
		public VistaFileDialogEvents(VistaFileDialog dialog) => this.dialog = dialog ?? throw new ArgumentNullException("dialog");
		public Interop.HRESULT OnFileOk(Interop.IFileDialog pfd) => this.dialog.DoFileOk(pfd) ? Interop.HRESULT.S_OK : Interop.HRESULT.S_FALSE;
		public Interop.HRESULT OnFolderChanging(Interop.IFileDialog pfd, Interop.IShellItem psiFolder) => Interop.HRESULT.S_OK;
		public void OnFolderChange(Interop.IFileDialog pfd) {
		}
		public void OnSelectionChange(Interop.IFileDialog pfd) {
		}
		public void OnShareViolation(Interop.IFileDialog pfd, Interop.IShellItem psi, out NativeMethods.FDE_SHAREVIOLATION_RESPONSE pResponse) => pResponse = NativeMethods.FDE_SHAREVIOLATION_RESPONSE.FDESVR_DEFAULT;
		public void OnTypeChange(Interop.IFileDialog pfd) {
		}
		public void OnOverwrite(Interop.IFileDialog pfd, Interop.IShellItem psi, out NativeMethods.FDE_OVERWRITE_RESPONSE pResponse) => pResponse = NativeMethods.FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
		public void OnItemSelected(Interop.IFileDialogCustomize pfdc, int dwIdCtl, int dwIdItem) {
		}
		public void OnButtonClicked(Interop.IFileDialogCustomize pfdc, int dwIdCtl) {
		}
		public void OnCheckButtonToggled(Interop.IFileDialogCustomize pfdc, int dwIdCtl, bool bChecked) {
		}
		public void OnControlActivating(Interop.IFileDialogCustomize pfdc, int dwIdCtl) {
		}
	}
}
