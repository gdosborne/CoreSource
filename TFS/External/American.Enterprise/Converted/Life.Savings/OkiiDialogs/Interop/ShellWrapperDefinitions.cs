using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace Ookii.Dialogs.Wpf.Interop
{
    internal interface NativeCommonFileDialog
    { }
    [ComImport,
    Guid(IIDGuid.IFileOpenDialog), 
    CoClass(typeof(FileOpenDialogRCW))]
    internal interface NativeFileOpenDialog : IFileOpenDialog
    {
    }
    [ComImport,
    Guid(IIDGuid.IFileSaveDialog),
    CoClass(typeof(FileSaveDialogRCW))]
    internal interface NativeFileSaveDialog : IFileSaveDialog
    {
    }
    [ComImport,
    Guid(IIDGuid.IKnownFolderManager),
    CoClass(typeof(KnownFolderManagerRCW))]
    internal interface KnownFolderManager : IKnownFolderManager
    {
    }
    [ComImport,
    ClassInterface(ClassInterfaceType.None),
    TypeLibType(TypeLibTypeFlags.FCanCreate),
    Guid(CLSIDGuid.FileOpenDialog)]
    internal class FileOpenDialogRCW
    {
    }
    [ComImport,
    ClassInterface(ClassInterfaceType.None),
    TypeLibType(TypeLibTypeFlags.FCanCreate),
    Guid(CLSIDGuid.FileSaveDialog)]
    internal class FileSaveDialogRCW
    {
    }
    [ComImport,
    ClassInterface(ClassInterfaceType.None),
    TypeLibType(TypeLibTypeFlags.FCanCreate),
    Guid(CLSIDGuid.KnownFolderManager)]
    internal class KnownFolderManagerRCW
    {
    }
}
