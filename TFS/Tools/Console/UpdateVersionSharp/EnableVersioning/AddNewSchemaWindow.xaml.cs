namespace EnableVersioning {
	using System.Windows;
	using GregOsborne.Application.Primitives;

	public partial class AddNewSchemaWindow : Window {

		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "DialogResult": {
					this.DialogResult = this.View.DialogResult;
					break;
				}
			}
		}

		public AddNewSchemaWindow() {
			this.InitializeComponent();

			this.View.PropertyChanged += this.View_PropertyChanged;
			this.View.Initialize();
		}

		public AddNewSchemaWindowView View => this.DataContext.As<AddNewSchemaWindowView>();
	}
}