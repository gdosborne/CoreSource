namespace GregOsborne.Dialogs {
	using System;
	using System.IO;
	using System.Runtime.InteropServices;

	internal sealed class ComCtlv6ActivationContext : IDisposable {
		private IntPtr cookie;
		private static NativeMethods.ACTCTX enableThemingActivationContext;
		private static ActivationContextSafeHandle activationContext;
		private static bool contextCreationSucceeded;
		private static readonly object contextCreationLock = new object();
		public ComCtlv6ActivationContext(bool enable) {
			if (enable && NativeMethods.IsWindowsXPOrLater) {
				if (EnsureActivateContextCreated()) {
					if (!NativeMethods.ActivateActCtx(activationContext, out this.cookie)) {
						this.cookie = IntPtr.Zero;
					}
				}
			}
		}
		~ComCtlv6ActivationContext() {
			this.Dispose(false);
		}
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing) {
			if (this.cookie != IntPtr.Zero) {
				if (NativeMethods.DeactivateActCtx(0, this.cookie)) {
					this.cookie = IntPtr.Zero;
				}
			}
		}
		private static bool EnsureActivateContextCreated() {
			lock (contextCreationLock) {
				if (!contextCreationSucceeded) {
					string assemblyLoc = null;
					assemblyLoc = typeof(object).Assembly.Location;
					string manifestLoc = null;
					string installDir = null;
					if (assemblyLoc != null) {
						installDir = Path.GetDirectoryName(assemblyLoc);
						const string manifestName = "XPThemes.manifest";
						manifestLoc = Path.Combine(installDir, manifestName);
					}
					if (manifestLoc != null && installDir != null) {
						enableThemingActivationContext = new NativeMethods.ACTCTX {
							cbSize = Marshal.SizeOf(typeof(NativeMethods.ACTCTX)),
							lpSource = manifestLoc,
							lpAssemblyDirectory = installDir,
							dwFlags = NativeMethods.ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID
						};
						activationContext = NativeMethods.CreateActCtx(ref enableThemingActivationContext);
						contextCreationSucceeded = !activationContext.IsInvalid;
					}
				}
				return contextCreationSucceeded;
			}
		}
	}
}
