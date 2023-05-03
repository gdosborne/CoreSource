using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GregOsborne.MVVMFramework;
using sysio = System.IO;

namespace GregOsborne.Documater
{
    public class MainWindowView : ViewModelBase
    {
        public event ExecuteUiActionHandler ExecuteUiAction;

        private DelegateCommand _openFileCommand;
        public DelegateCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new DelegateCommand(OpenFile, ValidateOpenFileState));
        private void OpenFile(object state)
        {
            var p = new Dictionary<string, object> {
                { "cancel", true },
                { "filename", default(string) },
                { "initialdirectory", Application.Settings.GetSetting(App.ApplicationName, "FileAndFolder", "LastOpenFileFolder", default(string)) }
            };
            ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("OpenFile", p));
            if ((bool)p["cancel"])
                return;
            var fileName = (string)p["filename"];
            Application.Settings.SetSetting(App.ApplicationName, "FileAndFolder", "LastOpenFileFolder", sysio.Path.GetDirectoryName(fileName));
        }
        private bool ValidateOpenFileState(object state) => true;

    }
}