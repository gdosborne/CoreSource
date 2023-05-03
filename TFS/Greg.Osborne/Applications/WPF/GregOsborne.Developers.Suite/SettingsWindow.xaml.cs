namespace GregOsborne.Developers.Suite {
	using System;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Windows;
	using GregOsborne.Suite.Extender.AppSettings;

	public partial class SettingsWindow : Window {
		public SettingsWindow() {
			this.InitializeComponent();
			this.View.Initialize();

			this.View.ExecuteUiAction += this.View_ExecuteUiAction;
			this.View.PropertyChanged += this.View_PropertyChanged;
		}

		private void SaveCategory(Category category) {
			if (category.Categories.Any()) {
				category.Categories.ForEach(x => this.SaveCategory(x));
			}
			if (category.SettingValues.Any()) {
				category.SettingValues.ForEach(x => {
					if (x is BoolSettingValue) {
						var val = ((BoolSettingValue)x).CurrentValue;
						var name = ((BoolSettingValue)x).Name;
						GregOsborne.Application.Settings.SetSetting(App.Current.As<App>().ApplicationName, category.Path, name, val);
					}
				});
			}
		}

		private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "DialogResult") {
				if (!this.View.DialogResult.HasValue || !this.View.DialogResult.Value) {
					this.DialogResult = this.View.DialogResult;
				} else {
					this.View.Categories.ToList().ForEach(x => this.SaveCategory(x));
					this.DialogResult = this.View.DialogResult;
				}
			}
		}

		private void View_ExecuteUiAction(object sender, MVVMFramework.ExecuteUiActionEventArgs e) {
			if (e.CommandToExecute == "addcategory") {
				var tvi = e.Parameters["item"].As<TreeViewItem>();
				var isTopLevel = (bool)e.Parameters["istopitem"];
				if (tvi != null) {
					tvi.Expanded += this.Tvi_Selected;
					tvi.Selected += this.Tvi_Selected;
					var phoneyTVI = new TreeViewItem {
						Header = "Phoney",
						Tag = "phoney"
					};
					tvi.Items.Add(phoneyTVI);
					if (isTopLevel) {
						this.tvCategories.Items.Add(tvi);
					}
				}
			} else if (e.CommandToExecute == "applysettings") {
				var generalCategory = this.View.Categories.First(x => x.Title == "General");
				var extensionsCategory = this.View.Categories.First(x => x.Title == "Extensions");
				Utilities.Shared.UpdateExtensionSettings(generalCategory, extensionsCategory);
				this.View.Categories.ToList().ForEach(x => this.SaveCategory(x));
			}
		}

		private void Tvi_Selected(object sender, RoutedEventArgs e) {
			var catTVI = sender.As<CategoryTreeViewItem>();
			if (catTVI.Items.Count == 1 && catTVI.Items[0].As<TreeViewItem>().Tag.As<string>() == "phoney") {
				catTVI.Items.Clear();
				var cat = catTVI.Category;
				cat.Categories.ToList().ForEach(subCat => {
					var tvi = this.View.GetCategory(subCat);
					catTVI.Items.Add(tvi);
				});
			}
			this.View.LoadSettings(catTVI.Category);
		}

		protected override void OnSourceInitialized(EventArgs e) {
			this.HideMinimizeAndMaximizeButtons();
			if (this.View.Categories.Any()) {
				this.tvCategories.Items[0].As<CategoryTreeViewItem>().IsSelected = true;
			}
			this.tvColumn.Width = new GridLength(250);
		}

		public SettingsWindowView View => this.DataContext.As<SettingsWindowView>();
	}
}
