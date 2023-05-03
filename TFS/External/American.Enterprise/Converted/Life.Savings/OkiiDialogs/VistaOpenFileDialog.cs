using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf.Interop;

namespace Ookii.Dialogs.Wpf {
    [Description("Prompts the user to open a file.")]
    public sealed class VistaOpenFileDialog : VistaFileDialog {
        private const int OpenDropDownId = 0x4002;
        private const int OpenItemId = 0x4003;
        private const int ReadOnlyItemId = 0x4004;
        private bool _readOnlyChecked;
        private bool _showReadOnly;

        public VistaOpenFileDialog() {
            if (!IsVistaFileDialogSupported)
                DownlevelDialog = new OpenFileDialog();
        }

        [DefaultValue(true)]
        [Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
        public override bool CheckFileExists {
            get => base.CheckFileExists;
            set => base.CheckFileExists = value;
        }

        [Description("A value indicating whether the dialog box allows multiple files to be selected.")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool Multiselect {
            get => ((OpenFileDialog) DownlevelDialog)?.Multiselect ?? GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
            set {
                if (DownlevelDialog != null)
                    ((OpenFileDialog) DownlevelDialog).Multiselect = value;
                SetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT, value);
            }
        }

        [Description("A value indicating whether the dialog box contains a read-only check box.")]
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ShowReadOnly {
            get => ((OpenFileDialog) DownlevelDialog)?.ShowReadOnly ?? _showReadOnly;
            set {
                if (DownlevelDialog != null)
                    ((OpenFileDialog) DownlevelDialog).ShowReadOnly = value;
                else
                    _showReadOnly = value;
            }
        }

        [DefaultValue(false)]
        [Description("A value indicating whether the read-only check box is selected.")]
        [Category("Behavior")]
        public bool ReadOnlyChecked {
            get => ((OpenFileDialog) DownlevelDialog)?.ReadOnlyChecked ?? _readOnlyChecked;
            set {
                if (DownlevelDialog != null)
                    ((OpenFileDialog) DownlevelDialog).ReadOnlyChecked = value;
                else
                    _readOnlyChecked = value;
            }
        }

        public override void Reset() {
            base.Reset();
            if (DownlevelDialog != null) return;
            CheckFileExists = true;
            _showReadOnly = false;
            _readOnlyChecked = false;
        }

        public Stream OpenFile() {
            if (DownlevelDialog != null) return ((OpenFileDialog) DownlevelDialog).OpenFile();
            var fileName = FileName;
            return new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }

        internal override IFileDialog CreateFileDialog() {
            return new NativeFileOpenDialog();
        }

        internal override void SetDialogProperties(IFileDialog dialog) {
            base.SetDialogProperties(dialog);
            if (!_showReadOnly) return;
            // ReSharper disable once SuspiciousTypeConversion.Global
            var customize = (IFileDialogCustomize) dialog;
            customize.EnableOpenDropDown(OpenDropDownId);
            customize.AddControlItem(OpenDropDownId, OpenItemId, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.OpenButton));
            customize.AddControlItem(OpenDropDownId, ReadOnlyItemId, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ReadOnly));
        }

        internal override void GetResult(IFileDialog dialog) {
            if (!Multiselect) {
                FileNamesInternal = null;
            }
            else {
                ((IFileOpenDialog) dialog).GetResults(out var results);
                results.GetCount(out var count);
                var fileNames = new string[count];
                for (uint x = 0; x < count; ++x) {
                    results.GetItemAt(x, out var item);
                    item.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out var name);
                    fileNames[x] = name;
                }
                FileNamesInternal = fileNames;
            }
            if (ShowReadOnly) {
                // ReSharper disable once SuspiciousTypeConversion.Global
                var customize = (IFileDialogCustomize) dialog;
                customize.GetSelectedControlItem(OpenDropDownId, out var selected);
                _readOnlyChecked = selected == ReadOnlyItemId;
            }
            base.GetResult(dialog);
        }
    }
}