using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MyApplication.Primitives;
using MyApplication.Registry;
using MVVMFramework;
using OptiRampControls.Classes;
using OptiRampControls.DesignerObjects;
using OptiRampDesktop.Helpers;
using OptiRampDesktop.Views;

namespace OptiRampDesktop.Windows
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private MainWindowView View
		{
			get
			{
				return LayoutRoot.GetView<MainWindowView>();
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ApplicationSettings.ApplicationMode = View.ThisWindowKey.GetEnumValue<ApplicationModes>("ApplicationMode", ApplicationModes.Normal);
			if (ApplicationSettings.ApplicationMode == ApplicationModes.Kiosk)
			{
				KioskCloseRectangle.Visibility = Visibility.Visible;
				this.WindowState = WindowState.Maximized;
				this.WindowStyle = WindowStyle.None;
				this.ShowInTaskbar = false;
				this.Title = string.Empty;
				this.BorderThickness = new Thickness(0);
				this.ResizeMode = ResizeMode.NoResize;
#if !DEBUG
				this.Topmost = true;
#endif
			}
			else
			{
				KioskCloseRectangle.Visibility = Visibility.Collapsed;
				this.Left = View.ThisWindowKey.GetValue<double>("Left", this.Left);
				this.Top = View.ThisWindowKey.GetValue<double>("Top", this.Top);
				this.Width = View.ThisWindowKey.GetValue<double>("Width", this.Width);
				this.Height = View.ThisWindowKey.GetValue<double>("Height", this.Height);
				this.WindowState = View.ThisWindowKey.GetEnumValue<WindowState>("WindowState", this.WindowState);
			}
			try
			{
				AnalyticsDesignerPanel.AddElement(new OptiRampRectangle { Fill = new SolidColorBrush(Colors.DarkBlue), CornerRadius = 5 });
				AnalyticsDesignerPanel.AddElement(new OptiRampTriangle { Rotation = -45 });
				AnalyticsDesignerPanel.AddElement(new OptiRampEllipse { Size = new Size(300, 50) });
				AnalyticsDesignerPanel.AddElement(new OptiRampPolygon
				{
					Fill = new SolidColorBrush(Colors.Transparent),
					Stroke = new SolidColorBrush(Colors.Red),
					StrokeThickness = 2,
					OrderedPoints = new List<Point>
					{
						new Point(17.5,0),
						new Point(42.5,0),
						new Point(60,17.5),
						new Point(60,42.5),
						new Point(42.5,60),
						new Point(17.5,60),
						new Point(0,42.5),
						new Point(0,17.5),
					}
				});
				AnalyticsDesignerPanel.AddElement(new OptiRampLine { Stroke = new SolidColorBrush(Colors.DarkRed), StrokeThickness = 3, Size = new Size(300, 0) });
				var ctx = GetObjectContextMenu();
				AnalyticsDesignerPanel.ItemsSource.ToList().ForEach(x =>
				{
					x.Element.ContextMenu = ctx;
				});
				ctx.Opened += ctx_Opened;
				var desCtx = GetDesignerContentMenu();
				desCtx.Opened += ctx_Opened;
				AnalyticsDesignerPanel.ContextMenu = desCtx;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private ContextMenu GetDesignerContentMenu()
		{
			var result = new ContextMenu();
			var addMenuItem = new MenuItem
			{
				Header = "Add",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/insert.png")) }
			};
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Line",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/line.png")) }
			});
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Rectangle",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/rectangle.png")) }
			});
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Ellipse",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/ellipse.png")) }
			});
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Polygon",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/polygon.png")) }
			});
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Triangle",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/triangle.png")) }
			});
			addMenuItem.Items.Add(new MenuItem
			{
				Header = "Connector",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/connector.png")) }
			});
			result.Items.Add(addMenuItem);
			return result;
		}

		private ContextMenu GetObjectContextMenu()
		{
			var result = new ContextMenu();
			var propMenuItem = new MenuItem
			{
				Header = "Properties",
				Command = View.PropertiesCommand,
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/properties.png")) }
			};
			var deleteMenuItem = new MenuItem
			{
				Header = "Delete",
				Icon = new Image { Width = 16, Height = 16, Source = new BitmapImage(new Uri("pack://application:,,,/OptiRampDesktop;component/Images/delete.png")) },
				Command = View.DeleteElementCommand
			};
			result.Items.Add(deleteMenuItem);
			result.Items.Add(new Separator());
			result.Items.Add(propMenuItem);
			return result;
		}

		private void ctx_Opened(object sender, RoutedEventArgs e)
		{
			var ctx = (ContextMenu)e.OriginalSource;
			var ctrl = ctx.PlacementTarget;
			var orObject = AnalyticsDesignerPanel.ItemsSource.FirstOrDefault(x => x.Element == ctrl);
			if (orObject == null)
				return;
			foreach (var item in ctx.Items)
			{
				if (item is MenuItem)
					item.As<MenuItem>().CommandParameter = orObject;
			}
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (ApplicationSettings.ApplicationMode != ApplicationModes.Kiosk)
			{
				View.ThisWindowKey.SetValue<double>("Left", this.RestoreBounds.Left);
				View.ThisWindowKey.SetValue<double>("Top", this.RestoreBounds.Top);
				View.ThisWindowKey.SetValue<double>("Width", this.RestoreBounds.Width);
				View.ThisWindowKey.SetValue<double>("Height", this.RestoreBounds.Height);
				View.ThisWindowKey.SetValue<WindowState>("WindowState", this.WindowState);
				View.ThisWindowKey.SetValue<ApplicationModes>("ApplicationMode", ApplicationSettings.ApplicationMode);
			}
		}

		private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (ApplicationSettings.ApplicationMode == ApplicationModes.Kiosk && Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
				Close();
		}

		private void MainWindowView_OpenProperties(object sender, ElementEventArgs e)
		{
			var win = new PropertiesWindow { Owner = this };
			win.View.TypeName = e.Element.TypeName;
			win.View.Properties = e.Element.Properties;
			if (e.Element.ObjectImageSource != null)
				win.View.IconImageSource = e.Element.ObjectImageSource;
			var result = win.ShowDialog();
		}

		private void MainWindowView_DeleteItem(object sender, ElementEventArgs e)
		{
		}
	}
}