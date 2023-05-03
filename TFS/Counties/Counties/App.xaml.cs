namespace Counties {
	using System.Windows;
	using Data;

	public partial class App : Application {
		public DataSource DataSource { get; set; }
		protected override void OnStartup(StartupEventArgs e) => this.DataSource = new DataSource();
	}
}