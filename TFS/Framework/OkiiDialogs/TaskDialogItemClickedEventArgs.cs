using System.ComponentModel;

namespace GregOsborne.Dialogs {
    public class TaskDialogItemClickedEventArgs : CancelEventArgs {
        public TaskDialogItemClickedEventArgs(TaskDialogItem item) {
            Item = item;
        }

        public TaskDialogItem Item { get; }
    }
}