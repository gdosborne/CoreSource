using System;

namespace Life.Savings.Events {
    public delegate void ShowBrowseFolderHandler(object sender, ShowBrowseFolderEventArgs e);

    public class ShowBrowseFolderEventArgs : EventArgs {
        public ShowBrowseFolderEventArgs(string initialFolderPath, string prompt) {
            InitialFolderPath = initialFolderPath;
            Prompt = prompt;
        }
        public string Prompt { get; }
        public string InitialFolderPath { get; }
        public string SelectedFolderPath { get; set; }
        public bool IsCancel { get; set; }
    }
}