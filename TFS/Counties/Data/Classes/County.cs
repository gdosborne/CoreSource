namespace Data.Classes {
	using System.Collections.ObjectModel;
	using System.ComponentModel;

	public class County : INotifyPropertyChanged {
		public County() {
			this.AdjacentCounties = new ObservableCollection<County>();
		}

		private int id = default(int);
		public int ID {
			get => this.id;
			set {
				this.id = value;
				this.InvokePropertyChanged(nameof(this.ID));
			}
		}

		private string name = default(string);
		public string Name {
			get => this.name;
			set {
				this.name = value;
				this.InvokePropertyChanged(nameof(this.Name));
			}
		}

		private ObservableCollection<County> adjacentCounties = default(ObservableCollection<County>);
		public ObservableCollection<County> AdjacentCounties {
			get => this.adjacentCounties;
			set {
				this.adjacentCounties = value;
				this.InvokePropertyChanged(nameof(this.AdjacentCounties));
			}
		}
		public override string ToString() => this.ID.ToString() + "/" + this.Name;

		public event PropertyChangedEventHandler PropertyChanged;
		protected void InvokePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
