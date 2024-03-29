using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Ookii.Dialogs.Wpf
{
    public sealed class AnimationResource
    {
        public AnimationResource(string resourceFile, int resourceId)
        {
            if( resourceFile == null )
                throw new ArgumentNullException("resourceFile");
            ResourceFile = resourceFile;
            ResourceId = resourceId;
        }
        public string ResourceFile { get; private set; }
        public int ResourceId { get; private set; }
        public static AnimationResource GetShellAnimation(ShellAnimation animation)
        {
            if( !Enum.IsDefined(typeof(ShellAnimation), animation) )
                throw new ArgumentOutOfRangeException("animation");
            return new AnimationResource("shell32.dll", (int)animation);
        }
        internal SafeModuleHandle LoadLibrary()
        {
            SafeModuleHandle handle = NativeMethods.LoadLibraryEx(ResourceFile, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
            if( handle.IsInvalid )
            {
                int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                if( error == NativeMethods.ErrorFileNotFound )
                    throw new FileNotFoundException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Properties.Resources.FileNotFoundFormat, ResourceFile));
                else
                    throw new System.ComponentModel.Win32Exception(error);
            }
            return handle;
        }
    }
}
