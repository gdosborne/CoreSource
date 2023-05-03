namespace GregOsborne.Suite.Extender.AppSettings {
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Controls;

	[Description("Container for application and extension settings")]
	public class Category {
		public Category(string title)
			: this(null, title) { }

		public Category(Category parent, string title) {
			this.Categories = new List<Category>();
			this.SettingValues = new List<dynamic>();
			this.Parent = parent;
			this.Title = title;
		}

		public int IndentLevel {
			get {
				var result = 0;
				if (this.Parent == null) {
					return result;
				}

				return this.Parent.IndentLevel + 1;
			}
		}

		public int TabSize { get; set; } = 8;
		public string Title { get; set; }
		public Category Parent { get; set; }
		public List<Category> Categories { get; set; }
		public List<dynamic> SettingValues { get; set; }
		public string Path {
			get {
				var result = this.Title;
				if (this.Parent == null) {
					return result;
				}

				result = $"{this.Parent.Path}/{result}";
				return result;
			}
		}
		public UserControl Control { get; set; } = default;

		private void View_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			var setting = this.SettingValues.FirstOrDefault(x => x.PropertyName == e.PropertyName);
		}
		public Grid DisplayGrid {
			get {
				var g = new Grid();
				g.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) });
				g.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(0, System.Windows.GridUnitType.Auto) });
				g.ColumnDefinitions.Add(new ColumnDefinition { Width = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
				return g;
			}
		}
	}
}