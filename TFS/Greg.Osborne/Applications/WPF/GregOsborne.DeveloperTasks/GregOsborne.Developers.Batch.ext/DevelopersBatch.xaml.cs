namespace GregOsborne.Developers.Batch.ext {
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;

	public partial class DevelopersBatch : UserControl {
		public DevelopersBatch() => this.InitializeComponent();

		public DevelopersBatchView View => this.DataContext.As<DevelopersBatchView>();
	}
}
