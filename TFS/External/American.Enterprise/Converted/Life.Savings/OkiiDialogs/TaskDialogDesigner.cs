using System;
using System.ComponentModel.Design;
using Ookii.Dialogs.Wpf.Properties;

namespace Ookii.Dialogs.Wpf {
    internal class TaskDialogDesigner : ComponentDesigner {
        public override DesignerVerbCollection Verbs {
            get {
                var verbs = new DesignerVerbCollection {new DesignerVerb(Resources.Preview, Preview)};
                return verbs;
            }
        }

        private void Preview(object sender, EventArgs e) {
            ((TaskDialog) Component).ShowDialog();
        }
    }
}