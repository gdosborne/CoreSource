namespace GregOsborne.Dialogs.Interop {
	using System;
	using System.Text;

	internal class Win32Resources : IDisposable {
		private SafeModuleHandle moduleHandle;
		private const int bufferSize = 500;
		public Win32Resources(string module) {
			this.moduleHandle = NativeMethods.LoadLibraryEx(module, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			if (this.moduleHandle.IsInvalid) {
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}
		}
		public string LoadString(uint id) {
			this.CheckDisposed();
			var buffer = new StringBuilder(bufferSize);
			if (NativeMethods.LoadString(this.moduleHandle, id, buffer, buffer.Capacity + 1) == 0) {
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}

			return buffer.ToString();
		}
		public string FormatString(uint id, params string[] args) {
			this.CheckDisposed();
			var buffer = IntPtr.Zero;
			var source = this.LoadString(id);
			var flags = NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING;
			var sourcePtr = System.Runtime.InteropServices.Marshal.StringToHGlobalAuto(source);
			try {
				if (NativeMethods.FormatMessage(flags, sourcePtr, id, 0, ref buffer, 0, args) == 0) {
					throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}
			}
			finally {
				System.Runtime.InteropServices.Marshal.FreeHGlobal(sourcePtr);
			}
			var result = System.Runtime.InteropServices.Marshal.PtrToStringAuto(buffer);
			System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);
			return result;
		}
		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				this.moduleHandle.Dispose();
			}
		}
		private void CheckDisposed() {
			if (this.moduleHandle.IsClosed) {
				throw new ObjectDisposedException("Win32Resources");
			}
		}
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
