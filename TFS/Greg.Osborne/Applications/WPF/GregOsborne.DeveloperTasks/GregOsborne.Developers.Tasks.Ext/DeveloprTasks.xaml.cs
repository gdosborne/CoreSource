using System.Windows.Controls;
using GregOsborne.Application.Primitives;
using GregOsborne.Suite.Extender;

namespace GregOsborne.Developers.Tasks.Ext {
	public partial class DeveloperTasks : UserControl {
		public DeveloperTasks() => this.InitializeComponent();

		public DeveloperTasksView View => this.DataContext.As<DeveloperTasksView>();
	}
}
