using System.ComponentModel;

namespace Ookii.Dialogs.Wpf {
    public class TaskDialogItemClickedEventArgs : CancelEventArgs {
        public TaskDialogItemClickedEventArgs(TaskDialogItem item) {
            Item = item;
        }

        public TaskDialogItem Item { get; }
    }
}