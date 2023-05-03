namespace SDFManager
{
	using SDFManagerSupport;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;
	using GregOsborne.Application.Primitives;

	public partial class TableDefinitionItem : UserControl
	{
		#region Public Constructors
		public TableDefinitionItem()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Private Methods
		private static void onConnectingLineChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (Polyline)e.NewValue;
			//implementation code goes here
		}
		private static void onDefinitionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (TableDefinition)e.NewValue;
			value.Fields.ForEach(x =>
			{
				src.FieldGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) });
				var fb = x.Flags.HasFlag(SDFManagerSupport.FieldDefinition.FieldFlags.IsPrimaryKey)
					? Application.Current.Resources["Visual.Theme.Table.Primary.Key.Foreground"].As<SolidColorBrush>()
					: Application.Current.Resources["Visual.Theme.Table.Foreground"].As<SolidColorBrush>();
				var bb = x.Flags.HasFlag(SDFManagerSupport.FieldDefinition.FieldFlags.IsPrimaryKey)
					? Application.Current.Resources["Visual.Theme.Table.Primary.Key.Background"].As<SolidColorBrush>()
					: Application.Current.Resources["Visual.Theme.Table.Background"].As<SolidColorBrush>();
				var tb1 = new TextBlock
				{
					Text = x.Name,
					Margin = new Thickness(5, 2, 5, 2),
					Foreground = fb
				};
				var tb2 = new TextBlock
				{
					Text = x.DbTypeTranslate(),
					Margin = new Thickness(5, 2, 5, 2),
					Foreground = fb
				};
				var bdr1 = new Border
				{
					Background = bb,
					Child = tb1
				};
				var bdr2 = new Border
				{
					Background = bb,
					Child = tb2
				};
				bdr1.SetValue(Grid.RowProperty, src.FieldGrid.RowDefinitions.Count - 1);
				bdr1.SetValue(Grid.ColumnProperty, 0);
				bdr2.SetValue(Grid.RowProperty, src.FieldGrid.RowDefinitions.Count - 1);
				bdr2.SetValue(Grid.ColumnProperty, 1);
				src.FieldGrid.Children.Add(bdr1);
				src.FieldGrid.Children.Add(bdr2);
			});
		}
		private static void onForeignKeyItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (TableDefinitionItem)e.NewValue;
			//implementation code goes here
		}
		private static void onPrimaryKeyItemChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (TableDefinitionItem)e.NewValue;
			//implementation code goes here
		}
		private static void onSelectionRectangleVisibilityChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (Visibility)e.NewValue;
			src.SelectionRectangle.Visibility = value;
		}
		private static void onTitleChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (TableDefinitionItem)source;
			if (src == null)
				return;
			var value = (string)e.NewValue;
			src.TitleTextBlock.Text = value;
		}
		#endregion Private Methods

		#region Public Fields
		public static readonly DependencyProperty ConnectingLineProperty = DependencyProperty.Register("ConnectingLine", typeof(Polyline), typeof(TableDefinitionItem), new PropertyMetadata(null, onConnectingLineChanged));
		public static readonly DependencyProperty DefinitionProperty = DependencyProperty.Register("Definition", typeof(TableDefinition), typeof(TableDefinitionItem), new PropertyMetadata(null, onDefinitionChanged));
		public static readonly DependencyProperty ForeignKeyItemProperty = DependencyProperty.Register("ForeignKeyItem", typeof(TableDefinitionItem), typeof(TableDefinitionItem), new PropertyMetadata(null, onForeignKeyItemChanged));
		public static readonly DependencyProperty PrimaryKeyItemProperty = DependencyProperty.Register("PrimaryKeyItem", typeof(TableDefinitionItem), typeof(TableDefinitionItem), new PropertyMetadata(null, onPrimaryKeyItemChanged));
		public static readonly DependencyProperty SelectionRectangleVisibilityProperty = DependencyProperty.Register("SelectionRectangleVisibility", typeof(Visibility), typeof(TableDefinitionItem), new PropertyMetadata(Visibility.Collapsed, onSelectionRectangleVisibilityChanged));
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TableDefinitionItem), new PropertyMetadata(string.Empty, onTitleChanged));
		#endregion Public Fields

		#region Public Properties
		public Polyline ConnectingLine
		{
			get { return (Polyline)GetValue(ConnectingLineProperty); }
			set { SetValue(ConnectingLineProperty, value); }
		}
		public TableDefinition Definition
		{
			get { return (TableDefinition)GetValue(DefinitionProperty); }
			set { SetValue(DefinitionProperty, value); }
		}
		public TableDefinitionItem ForeignKeyItem
		{
			get { return (TableDefinitionItem)GetValue(ForeignKeyItemProperty); }
			set { SetValue(ForeignKeyItemProperty, value); }
		}
		public TableDefinitionItem PrimaryKeyItem
		{
			get { return (TableDefinitionItem)GetValue(PrimaryKeyItemProperty); }
			set { SetValue(PrimaryKeyItemProperty, value); }
		}
		public Visibility SelectionRectangleVisibility
		{
			get { return (Visibility)GetValue(SelectionRectangleVisibilityProperty); }
			set { SetValue(SelectionRectangleVisibilityProperty, value); }
		}
		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		#endregion Public Properties
	}
}
