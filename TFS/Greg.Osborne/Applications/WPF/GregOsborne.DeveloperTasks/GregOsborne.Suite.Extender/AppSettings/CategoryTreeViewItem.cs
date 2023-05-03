namespace GregOsborne.Suite.Extender.AppSettings {
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>Enhanced TreeViewItem</summary>
	public class CategoryTreeViewItem : TreeViewItem {

		/// <summary>Initializes the <see cref="CategoryTreeViewItem" /> class.</summary>
		static CategoryTreeViewItem() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CategoryTreeViewItem), new FrameworkPropertyMetadata(typeof(CategoryTreeViewItem)));
		}

		private Category category = default;
		public Category Category {
			get => this.category;
			set {
				this.category = value;
				this.Header = this.Category.Title;
			}
		}
	}
}
