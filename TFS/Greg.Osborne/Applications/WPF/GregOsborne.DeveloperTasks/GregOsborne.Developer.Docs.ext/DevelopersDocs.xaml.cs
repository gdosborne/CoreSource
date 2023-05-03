namespace GregOsborne.Developers.Docs.ext {
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;

	public partial class DevelopersDocs : UserControl {
		public DevelopersDocs() => this.InitializeComponent();

		public DevelopersDocsView View => this.DataContext.As<DevelopersDocsView>();
	}
}
