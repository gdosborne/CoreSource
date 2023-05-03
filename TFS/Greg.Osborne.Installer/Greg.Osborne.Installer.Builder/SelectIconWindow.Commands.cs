using GregOsborne.MVVMFramework;

namespace Greg.Osborne.Installer.Builder {
	public partial class SelectIconWindowView {
		private DelegateCommand okCommand = default;
		public DelegateCommand OKCommand => this.okCommand ?? (this.okCommand = new DelegateCommand(this.OK, this.ValidateOKState));
		private bool ValidateOKState(object state) => true;
		private void OK(object state) => this.DialogResult = true;

		private DelegateCommand cancelCommand = default;
		public DelegateCommand CancelCommand => this.cancelCommand ?? (this.cancelCommand = new DelegateCommand(this.Cancel, this.ValidateCancelState));
		private bool ValidateCancelState(object state) => true;
		private void Cancel(object state) => this.DialogResult = false;

		private DelegateCommand nextPageCommand = default;
		public DelegateCommand NextPageCommand => this.nextPageCommand ?? (this.nextPageCommand = new DelegateCommand(this.NextPage, this.ValidateNextPageState));
		private bool ValidateNextPageState(object state) => this.PageNumber < this.TotalPages;
		private void NextPage(object state) => this.PageNumber++;

		private DelegateCommand previousPageCommand = default;
		public DelegateCommand PreviousPageCommand => this.previousPageCommand ?? (this.previousPageCommand = new DelegateCommand(this.PreviousPage, this.ValidatePreviousPageState));
		private bool ValidatePreviousPageState(object state) => this.PageNumber > 0;
		private void PreviousPage(object state) => this.PageNumber--;

	}
}
