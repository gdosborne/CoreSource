namespace GregOsborne.Dialogs {
	using System;
	using System.ComponentModel.Design;
	using Ookii.Dialogs.Wpf.Properties;

	internal class TaskDialogDesigner : ComponentDesigner {
		public override DesignerVerbCollection Verbs {
			get {
				var verbs = new DesignerVerbCollection { new DesignerVerb(Resources.Preview, this.Preview) };
				return verbs;
			}
		}

		private void Preview(object sender, EventArgs e) => ((TaskDialog)this.Component).ShowDialog();
	}
}