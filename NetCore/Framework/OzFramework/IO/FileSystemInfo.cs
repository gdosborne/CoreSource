/* File="FileSystemInfo"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using Common.Primitives;
using Microsoft.VisualBasic.FileIO;
using System;
using SysIO = System.IO;

namespace Common.IO {
    public static class FileSystemInfo {
        public static void DeleteOrRecycle(this SysIO.FileSystemInfo item, bool toRecycleBin = false) {
            if (item.IsNull()) throw new ArgumentException(null, nameof(item));
            if (!item.Exists) {
                throw (item.Is<SysIO.DirectoryInfo>() ? new SysIO.DirectoryNotFoundException(nameof(item)) : new SysIO.FileNotFoundException(nameof(item)));
            }
            var recyOption = toRecycleBin ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;
            if (item.Is<SysIO.DirectoryInfo>()) FileSystem.DeleteDirectory(item.FullName, UIOption.OnlyErrorDialogs, recyOption, UICancelOption.DoNothing);
            if (item.Is<SysIO.FileInfo>()) FileSystem.DeleteFile(item.FullName, UIOption.OnlyErrorDialogs, recyOption, UICancelOption.DoNothing);
        }

    }
}
