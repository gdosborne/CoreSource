using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Counties.MVVM;
using Data.Classes;

namespace Counties {
	public class MainWindowView : ViewModelBase {
		public MainWindowView() {
		}

		public event ExecuteUiActionHandler ExecuteUiAction;
		public override void Initialize() {
			this.AllCounties = new ObservableCollection<County>();
			this.RefreshCounties();
		}

		private string countyNumber = default(string);
		public string CountyNumber {
			get => this.countyNumber;
			set {
				this.countyNumber = value;
				this.InvokePropertyChanged(nameof(this.CountyNumber));
			}
		}
		private void RefreshCounties() {
			if (!this.AllCounties.Any()) {
				var all = (App.Current as App).DataSource.GetAllCounties();
				all.ToList().ForEach(x => this.AllCounties.Add(x));
			}
			var temp = this.AllCounties.ToList();
			this.AllCounties.Clear();
			if (this.IsSortedByName) {
				temp.OrderBy(x => x.Name).ToList().ForEach(x => this.AllCounties.Add(x));
			} else {
				temp.OrderBy(x => x.ID).ToList().ForEach(x => this.AllCounties.Add(x));
			}
		}

		private DelegateCommand goCommand = default(DelegateCommand);
		public DelegateCommand GoCommand => this.goCommand ?? (this.goCommand = new DelegateCommand(this.Go, this.ValidateGoState));
		private bool ValidateGoState(object state) => true;
		private void Go(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("dofuction"));
		private DelegateCommand cancelCommand = default(DelegateCommand);
		public DelegateCommand CancelCommand => this.cancelCommand ?? (this.cancelCommand = new DelegateCommand(this.Cancel, this.ValidateCancelState));
		private bool ValidateCancelState(object state) => true;
		private void Cancel(object state) => (App.Current as App).Shutdown(0);

		private ObservableCollection<County> allCounties = default(ObservableCollection<County>);
		public ObservableCollection<County> AllCounties {
			get => this.allCounties;
			set {
				this.allCounties = value;
				this.InvokePropertyChanged(nameof(this.AllCounties));
			}
		}

		private bool isSortedByName = default(bool);
		public bool IsSortedByName {
			get => this.isSortedByName;
			set {
				this.isSortedByName = value;
				if (this.AdjacentCounties == null) {
					this.AdjacentCounties = new ObservableCollection<County>();
				}
				this.CleanUp();
				this.RefreshCounties();
				this.InvokePropertyChanged(nameof(this.IsSortedByName));
			}
		}

		private County selectedCounty = default(County);
		public County SelectedCounty {
			get => this.selectedCounty;
			set {
				this.selectedCounty = value;
				if (this.AdjacentCounties != null) {
					this.AdjacentCounties.Clear();
				}
				if (this.SelectedCounty == null) {
					return;
				}

				if (this.AdjacentCounties == null) {
					this.AdjacentCounties = new ObservableCollection<County>();
				}

				if (this.IsSortedByName) {
					this.SelectedCounty.AdjacentCounties.OrderBy(x => x.Name).ToList().ForEach(x => this.AdjacentCounties.Add(x));
				} else {
					this.SelectedCounty.AdjacentCounties.OrderBy(x => x.ID).ToList().ForEach(x => this.AdjacentCounties.Add(x));
				}

				this.InvokePropertyChanged(nameof(this.SelectedCounty));
			}
		}

		public void CleanUp() {
			if (this.AdjacentCounties != null) {
				this.AdjacentCounties.Clear();
			}
			this.SelectedCounty = null;
			this.CountyNumber = string.Empty;
			this.SelectedCounty = null;
		}

		private ObservableCollection<County> adjacentCounties = default(ObservableCollection<County>);
		public ObservableCollection<County> AdjacentCounties {
			get => this.adjacentCounties;
			set {
				this.adjacentCounties = value;
				this.InvokePropertyChanged(nameof(this.AdjacentCounties));
			}
		}
		private DelegateCommand queryCommand = default(DelegateCommand);
		public DelegateCommand QueryCommand => queryCommand ?? (queryCommand = new DelegateCommand(Query, ValidateQueryState));
		private bool ValidateQueryState(object state) => !string.IsNullOrWhiteSpace(this.CountyNumber);
		private void Query(object state) {
			if (!int.TryParse(this.CountyNumber, out var val)) {
				return;
			}
			if (val < this.AllCounties.Min(x => x.ID) || val > this.AllCounties.Max(x => x.ID)) {
				this.CleanUp();
				return;
			}
			this.ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("findcounty", new KeyValuePair<string, object>("value", val)));
		}

	}
}
