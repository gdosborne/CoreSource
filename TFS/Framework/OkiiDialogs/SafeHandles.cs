namespace GregOsborne.Dialogs {
	using System;
	using System.Runtime.ConstrainedExecution;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;
	using Microsoft.Win32.SafeHandles;

	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal class SafeModuleHandle : SafeHandle {
		public SafeModuleHandle()
			: base(IntPtr.Zero, true) {
		}
		public override bool IsInvalid => this.handle == IntPtr.Zero;
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle() => NativeMethods.FreeLibrary(this.handle);
	}
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal class ActivationContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid {
		public ActivationContextSafeHandle()
			: base(true) {
		}
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle() {
			NativeMethods.ReleaseActCtx(this.handle);
			return true;
		}
	}
}
