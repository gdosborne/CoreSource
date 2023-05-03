namespace GregOsborne.Developers.Suite {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Suite.Extender.AppSettings;

	public class SettingsWindowView : ViewModelBase {
		public event ExecuteUiActionHandler ExecuteUiAction;

		public override void Initialize() {
			this.Categories = new ObservableCollection<Category>();
			this.SettingsControls = new ObservableCollection<FrameworkElement>();
		}

		private DelegateCommand okCommand = default;
		public DelegateCommand OKCommand => this.okCommand ?? (this.okCommand = new DelegateCommand(this.OK, this.ValidateOKState));
		private bool ValidateOKState(object state) => true;
		private void OK(object state) => this.DialogResult = true;

		private DelegateCommand cancelCommand = default;
		public DelegateCommand CancelCommand => this.cancelCommand ?? (this.cancelCommand = new DelegateCommand(this.Cancel, this.ValidateCancelState));
		private bool ValidateCancelState(object state) => true;
		private void Cancel(object state) => this.DialogResult = false;

		private DelegateCommand applyCommand = default;
		public DelegateCommand ApplyCommand => this.applyCommand ?? (this.applyCommand = new DelegateCommand(this.Apply, this.ValidateApplyState));
		private bool ValidateApplyState(object state) => true;
		private void Apply(object state) => ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("applysettings"));

		private bool? dialogResult = default;
		public bool? DialogResult {
			get => this.dialogResult;
			set {
				this.dialogResult = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		private ObservableCollection<Category> categories = default;
		public ObservableCollection<Category> Categories {
			get => this.categories;
			set {
				this.categories = value;
				if (this.Categories != null && this.Categories.Any()) {
					this.Categories.ToList().ForEach(cat => {
						this.GetCategory(cat, true);
					});
				}
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public CategoryTreeViewItem GetCategory(Category category, bool isTopItem = false) {
			var tvi = this.GetCategoryItem(category);
			var p = new Dictionary<string, object> {
				{ "item", tvi },
				{ "istopitem", isTopItem }
			};
			ExecuteUiAction?.Invoke(this, new ExecuteUiActionEventArgs("addcategory", p));
			return tvi;
		}

		private CategoryTreeViewItem GetCategoryItem(Category category) {
			var sp = new StackPanel {
				Orientation = Orientation.Horizontal
			};
			var icon = new TextBlock {
				Text = char.ConvertFromUtf32(61739),
				Margin = new System.Windows.Thickness(3),
				Style = App.Current.As<App>().Resources["basicUserControl"].As<Style>()
			};
			var title = new TextBlock {
				Text = category.Title,
				Margin = new System.Windows.Thickness(3),
				Style = App.Current.As<App>().Resources["basicUserControl"].As<Style>()
			};
			sp.Children.Add(icon);
			sp.Children.Add(title);
			return new CategoryTreeViewItem {
				Header = sp,
				Category = category
			};
		}

		private ObservableCollection<FrameworkElement> settingsControls = default;
		public ObservableCollection<FrameworkElement> SettingsControls {
			get => this.settingsControls;
			set {
				this.settingsControls = value;
				this.InvokePropertyChanged(Reflection.GetPropertyName());
			}
		}

		public void LoadSettings(Category category) {
			this.SettingsControls.Clear();
			if (!category.SettingValues.Any()) {
				var tb = new TextBlock {
					Text = $"No settings for {category.Title}",
					Margin = new Thickness(10),
					Style = App.Current.As<App>().Resources["basicUserControl"].As<Style>()
				};
				this.SettingsControls.Add(tb);
				return;
			}

			if (category.Control != null) {
				this.SettingsControls.Add(category.Control);
			}
		}
	}
}
