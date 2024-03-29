namespace SNC.OptiRamp.Application.Developer.Views {
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Windows;

	internal partial class SettingsWindowView : INotifyPropertyChanged {
		public SettingsWindowView() {
			Categories = new ObservableCollection<Category>();
			Categories.CollectionChanged += Categories_CollectionChanged;
		}

		void Categories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void UpdateInterface() {
			OKCommand.RaiseCanExecuteChanged();
		}
		public void InitView() {

			UpdateInterface();
		}
		public void Initialize(Window window) {

		}
		public void Persist(Window window) {

		}
		private bool? _DialogResult;
		public bool? DialogResult {
			get {
				return _DialogResult;
			}
			set {
				_DialogResult = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("DialogResult"));
			}
		}
		private ObservableCollection<Category> _Categories;
		public ObservableCollection<Category> Categories {
			get {
				return _Categories;
			}
			set {
				_Categories = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Categories"));
			}
		}
		private Visibility _PromptVisibility;
		public Visibility PromptVisibility {
			get {
				return _PromptVisibility;
			}
			set {
				_PromptVisibility = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("PromptVisibility"));
			}
		}
		private string _SearchText;
		public string SearchText {
			get {
				return _SearchText;
			}
			set {
				_SearchText = value;
				PromptVisibility = string.IsNullOrEmpty(value) ? Visibility.Visible : Visibility.Collapsed;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("SearchText"));
			}
		}
	}
}
