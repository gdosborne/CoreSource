namespace GregOsborne.Dialogs {
	using System;
	using System.IO;

	public sealed class AnimationResource {
		public AnimationResource(string resourceFile, int resourceId) {
			if (resourceFile == null) {
				throw new ArgumentNullException("resourceFile");
			}

			this.ResourceFile = resourceFile;
			this.ResourceId = resourceId;
		}
		public string ResourceFile { get; private set; }
		public int ResourceId { get; private set; }
		public static AnimationResource GetShellAnimation(ShellAnimation animation) {
			if (!Enum.IsDefined(typeof(ShellAnimation), animation)) {
				throw new ArgumentOutOfRangeException("animation");
			}

			return new AnimationResource("shell32.dll", (int)animation);
		}
		internal SafeModuleHandle LoadLibrary() {
			var handle = NativeMethods.LoadLibraryEx(this.ResourceFile, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			if (handle.IsInvalid) {
				var error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
				if (error == NativeMethods.ErrorFileNotFound) {
					throw new FileNotFoundException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Ookii.Dialogs.Wpf.Properties.Resources.FileNotFoundFormat, this.ResourceFile));
				} else {
					throw new System.ComponentModel.Win32Exception(error);
				}
			}
			return handle;
		}
	}
}
