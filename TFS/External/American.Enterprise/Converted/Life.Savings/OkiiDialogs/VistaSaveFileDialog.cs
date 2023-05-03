using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf.Interop;

namespace Ookii.Dialogs.Wpf {
    [Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Description("Prompts the user to open a file.")]
    public sealed class VistaSaveFileDialog : VistaFileDialog {
        public VistaSaveFileDialog() {
            if (!IsVistaFileDialogSupported)
                DownlevelDialog = new SaveFileDialog();
        }

        [DefaultValue(false)]
        [Category("Behavior")]
        [Description("A value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.")]
        public bool CreatePrompt {
            get {
                return ((SaveFileDialog) DownlevelDialog)?.CreatePrompt ?? GetOption(NativeMethods.FOS.FOS_CREATEPROMPT);
            }
            set {
                if (DownlevelDialog != null)
                    ((SaveFileDialog) DownlevelDialog).CreatePrompt = value;
                else
                    SetOption(NativeMethods.FOS.FOS_CREATEPROMPT, value);
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
        public bool OverwritePrompt {
            get {
                return ((SaveFileDialog) DownlevelDialog)?.OverwritePrompt ?? GetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT);
            }
            set {
                if (DownlevelDialog != null)
                    ((SaveFileDialog) DownlevelDialog).OverwritePrompt = value;
                else
                    SetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT, value);
            }
        }

        public override void Reset() {
            base.Reset();
            if (DownlevelDialog == null)
                OverwritePrompt = true;
        }

        public Stream OpenFile() {
            if (DownlevelDialog != null) {
                return ((SaveFileDialog) DownlevelDialog).OpenFile();
            }
            var fileName = FileName;
            return new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
        }

        protected override void OnFileOk(CancelEventArgs e) {
            if (DownlevelDialog == null) {
                if (CheckFileExists && !File.Exists(FileName)) {
                    PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.FileNotFound, Path.GetFileName(FileName)), MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                    e.Cancel = true;
                    return;
                }
                if (CreatePrompt && !File.Exists(FileName))
                    if (!PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.CreatePrompt, Path.GetFileName(FileName)), MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No)) {
                        e.Cancel = true;
                        return;
                    }
            }
            base.OnFileOk(e);
        }

        internal override IFileDialog CreateFileDialog() {
            return new NativeFileSaveDialog();
        }
    }
}