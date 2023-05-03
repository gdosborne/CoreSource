namespace ORDControls.ItemProperties
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Navigation;
	using System.Windows.Shapes;
	using MVVMFramework;
	using ORDControls.ItemProperties;

	public partial class ProjectProperties : UserControl, IItemProperties
	{
		public ProjectProperties() {
			InitializeComponent();
		}

		public ProjectPropertiesView View {
			get {
				return LayoutRoot.GetView<ProjectPropertiesView>();
			}
		}


		#region ProjectName
		public string ProjectName {
			get { return (string)GetValue(ProjectNameProperty); }
			set { SetValue(ProjectNameProperty, value); }
		}

		public static readonly DependencyProperty ProjectNameProperty = DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectProperties), new PropertyMetadata(string.Empty, onProjectNameChanged));
		private static void onProjectNameChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ProjectProperties)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.View.ProjectName = value;
		}
		#endregion


		#region ProjectDescription
		public string ProjectDescription {
			get { return (string)GetValue(ProjectDescriptionProperty); }
			set { SetValue(ProjectDescriptionProperty, value); }
		}

		public static readonly DependencyProperty ProjectDescriptionProperty = DependencyProperty.Register("ProjectDescription", typeof(string), typeof(ProjectProperties), new PropertyMetadata(string.Empty, onProjectDescriptionChanged));
		private static void onProjectDescriptionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ProjectProperties)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.View.ProjectDescription = value;
		}
		#endregion


		#region ProjectLocation
		public string ProjectLocation {
			get { return (string)GetValue(ProjectLocationProperty); }
			set { SetValue(ProjectLocationProperty, value); }
		}

		public static readonly DependencyProperty ProjectLocationProperty = DependencyProperty.Register("ProjectLocation", typeof(string), typeof(ProjectProperties), new PropertyMetadata(string.Empty, onProjectLocationChanged));
		private static void onProjectLocationChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ProjectProperties)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.View.ProjectLocation = value;
		}
		#endregion


		#region ProjectSize
		public long ProjectSize {
			get { return (long)GetValue(ProjectSizeProperty); }
			set { SetValue(ProjectSizeProperty, value); }
		}

		public static readonly DependencyProperty ProjectSizeProperty = DependencyProperty.Register("ProjectSize", typeof(long), typeof(ProjectProperties), new PropertyMetadata((long)0, onProjectSizeChanged));
		private static void onProjectSizeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ProjectProperties)source;
			if (src == null)
				return;
			var value = (long)e.NewValue;
			src.View.ProjectSize = value;
		}
		#endregion


		#region RevisionsItemsSource
		public IEnumerable<object> RevisionsItemsSource {
			get { return (IEnumerable<object>)GetValue(RevisionsItemsSourceProperty); }
			set { SetValue(RevisionsItemsSourceProperty, value); }
		}

		public static readonly DependencyProperty RevisionsItemsSourceProperty = DependencyProperty.Register("RevisionsItemsSource", typeof(IEnumerable<object>), typeof(ProjectProperties), new PropertyMetadata(null, onRevisionsItemsSourceChanged));
		private static void onRevisionsItemsSourceChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (ProjectProperties)source;
			if (src == null)
				return;
			var value = (IEnumerable<object>)e.NewValue;
			src.View.Revisions = new System.Collections.ObjectModel.ObservableCollection<object>(value);
		}
		#endregion

		private void RevisionsGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
			if (e.Column.Header.Equals("Major") || e.Column.Header.Equals("Minor"))
				e.Cancel = true;
			else if (e.Column.Header.Equals("Description"))
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
		}


	}
}
