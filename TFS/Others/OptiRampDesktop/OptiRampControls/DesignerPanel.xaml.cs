using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using MyApplication.Primitives;
using OptiRampControls.Classes;
using OptiRampControls.Classes.ElementHelpers;
using OptiRampControls.DesignerObjects;

namespace OptiRampControls
{
	public partial class DesignerPanel : UserControl
	{
		#region Public Fields
		public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(DesignerPanel), new PropertyMetadata(null, onBackgroundChanged));
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<OptiRampElement>), typeof(DesignerPanel), new PropertyMetadata(null, onItemsSourceChanged));
		public static readonly DependencyProperty ReduceOpacityOnMoveProperty = DependencyProperty.Register("ReduceOpacityOnMove", typeof(bool), typeof(DesignerPanel), new PropertyMetadata(true, onReduceOpacityOnMoveChanged));
		public ElementPropertyValueChangedHandler ValueChanged;
		#endregion

		#region Private Fields
		private OptiRampElement _ElementToSize = null;
		private Rectangle _SelectionBox = null;
		private DispatcherTimer checkTimer = null;
		private double firstXPos, firstYPos, firstArrowXPos, firstArrowYPos;
		private bool hasMoved = false;
		private bool mouseDown = false;
		private FrameworkElement movingObject = null;
		private OptiRampElement selectedElement = null;
		#endregion

		#region Public Constructors

		public DesignerPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Events
		public event ElementSelectedHandler ElementSelected;
		#endregion

		#region Public Properties
		public new Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}
		public IEnumerable<OptiRampElement> ItemsSource
		{
			get { return (IEnumerable<OptiRampElement>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public bool ReduceOpacityOnMove
		{
			get { return (bool)GetValue(ReduceOpacityOnMoveProperty); }
			set { SetValue(ReduceOpacityOnMoveProperty, value); }
		}
		#endregion

		#region Public Methods

		public void AddElement(OptiRampElement element)
		{
			if (ItemsSource == null)
				ItemsSource = new List<OptiRampElement>();
			element.ElementPropertyValueChanged += element_ValueChanged;
			ItemsSource.As<List<OptiRampElement>>().Add(element);
			ProcessElement(element);
		}

		#endregion

		#region Private Methods

		private static void onBackgroundChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (DesignerPanel)source;
			if (src == null)
				return;
			var value = (Brush)e.NewValue;
			src.AnalyticsCanvas.Background = value;
		}

		private static void onItemsSourceChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (DesignerPanel)source;
			if (src == null)
				return;
			var value = (IEnumerable<OptiRampElement>)e.NewValue;
			src.AnalyticsCanvas.Children.Clear();
			value.ToList().ForEach(x => src.ProcessElement(x));
		}

		private static void onReduceOpacityOnMoveChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (DesignerPanel)source;
			if (src == null)
				return;
			var value = (bool)e.NewValue;
		}

		private void _SelectionBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var mover = _SelectionBox;
			firstXPos = e.GetPosition(mover).X;
			firstYPos = e.GetPosition(mover).Y;
			firstArrowXPos = e.GetPosition(mover.Parent.As<Canvas>()).X - firstXPos;
			firstArrowYPos = e.GetPosition(mover.Parent.As<Canvas>()).Y - firstYPos;
			movingObject = mover;
			mouseDown = true;
			e.Handled = true;
		}

		private void _SelectionBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			_SelectionBox.Cursor = Cursors.Arrow;
			hasMoved = false;
			HideSelectionBox();
			movingObject = null;
			mouseDown = false;
			TestScrollbars();
			e.Handled = true;
		}

		private void AnalyticsScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (checkTimer != null)
				return;
			checkTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
			checkTimer.Tick += checkTimer_Tick;
			checkTimer.Start();
		}

		private void checkTimer_Tick(object sender, EventArgs e)
		{
			checkTimer.Stop();
			TestScrollbars();
			checkTimer = null;
		}

		private void DeselectAll()
		{
			HideSelectionBox();
			ItemsSource.ToList().ForEach(x => x.IsSelected = false);
		}

		private void DesignerPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			HideSelectionBox();
			var mover = sender.As<FrameworkElement>();
			if (!(mover.Parent is Canvas))
				return;
			SetOpacity(.5, mover);
			firstXPos = e.GetPosition(mover).X;
			firstYPos = e.GetPosition(mover).Y;
			firstArrowXPos = e.GetPosition(mover.Parent.As<Canvas>()).X - firstXPos;
			firstArrowYPos = e.GetPosition(mover.Parent.As<Canvas>()).Y - firstYPos;
			movingObject = mover;
			mouseDown = true;
			e.Handled = true;
		}

		private void DesignerPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			SetOpacity(1);
			var elem = ItemsSource.FirstOrDefault(x => x.Element == movingObject);
			if (!hasMoved && !elem.Is<OptiRampPolygon>())
			{
				_ElementToSize = elem;
				var location = new Point((double)elem.Element.GetValue(Canvas.LeftProperty) + elem.Element.ActualWidth, (double)elem.Element.GetValue(Canvas.TopProperty) + elem.Element.ActualHeight);
				ShowSelectionBox(location);
			}
			else
			{
				if (elem != null)
				{
					var left = (double)movingObject.GetValue(Canvas.LeftProperty);
					var top = (double)movingObject.GetValue(Canvas.TopProperty);
					elem.Properties.SetValue<Point>("Location", new Point(left, top));
				}
			}
			hasMoved = false;
			movingObject = null;
			mouseDown = false;
			TestScrollbars();
			e.Handled = true;
		}

		private void DesignerPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown)
			{
				Canvas parent = null;
				if (sender is Canvas)
					parent = sender.As<Canvas>();
				else
				{
					var mover = sender.As<FrameworkElement>();
					parent = mover.Parent.As<Canvas>();
				}
				hasMoved = true;
				var posX = e.GetPosition(parent).X - firstXPos;
				var posY = e.GetPosition(parent).Y - firstYPos;
				if (posX + movingObject.ActualWidth > parent.ActualWidth)
					parent.Width = posX + movingObject.ActualWidth + 20;
				if (posY + movingObject.ActualHeight > parent.ActualHeight)
					parent.Height = posY + movingObject.ActualHeight + 20;
				movingObject.SetValue(Canvas.LeftProperty, posX);
				movingObject.SetValue(Canvas.TopProperty, posY);
				if (movingObject == _SelectionBox)
				{
					_SelectionBox.Cursor = Cursors.SizeNWSE;
					var left = (double)_ElementToSize.Element.GetValue(Canvas.LeftProperty);
					var top = (double)_ElementToSize.Element.GetValue(Canvas.TopProperty);
					var width = posX - left;
					var height = posY - top;
					width = width < _ElementToSize.Properties.GetValue<Size>("MinimumSize").Width ? _ElementToSize.Properties.GetValue<Size>("MinimumSize").Width : width;
					height = height < _ElementToSize.Properties.GetValue<Size>("MinimumSize").Height ? _ElementToSize.Properties.GetValue<Size>("MinimumSize").Height : height;
					_ElementToSize.Properties.SetValue<Size>("Size", new Size(width, height));
					if (_ElementToSize.Is<OptiRampTriangle>() || _ElementToSize.Is<OptiRampLine>())
					{
						AnalyticsCanvas.Children.Remove(_ElementToSize.Element);
						ProcessElement(_ElementToSize);
					}
					else
					{
						_ElementToSize.Element.Width = width;
						_ElementToSize.Element.Height = height;
					}
				}
				e.Handled = true;
			}
		}

		private void element_ValueChanged(object sender, ElementPropertyValueChangedEventArgs e)
		{
			if (ValueChanged != null)
				ValueChanged(this, e);
		}

		private void HideSelectionBox()
		{
			if (_SelectionBox != null)
				_SelectionBox.Visibility = Visibility.Collapsed;
			_ElementToSize = null;
		}

		private void ProcessElement(OptiRampElement element)
		{
			if (element.Is<ShapeElement>())
				element.Element = ShapeHelper.CreateShape(element.As<ShapeElement>());
			if (element.IsMovable)
			{
				element.Element.MouseLeftButtonDown += DesignerPanel_MouseLeftButtonDown;
				element.Element.MouseLeftButtonUp += DesignerPanel_MouseLeftButtonUp;
				element.Element.MouseMove += DesignerPanel_MouseMove;
			}
			AnalyticsCanvas.Children.Add(element.Element);
		}

		private void SetOpacity(double value, FrameworkElement except = null)
		{
			if (!ReduceOpacityOnMove)
				return;
			ItemsSource.ToList().ForEach(x =>
			{
				if (x.Element != except)
					x.Element.Opacity = value;
			});
		}

		private void ShowSelectionBox(Point location)
		{
			if (_SelectionBox == null)
			{
				_SelectionBox = new Rectangle
				{
					Width = 8,
					Height = 8,
					Fill = new SolidColorBrush(Colors.Black),
					Stroke = new SolidColorBrush(Colors.White),
					StrokeThickness = .5,
					RadiusX = 2,
					RadiusY = 2,
				};
				_SelectionBox.MouseLeftButtonDown += _SelectionBox_MouseLeftButtonDown;
				_SelectionBox.MouseLeftButtonUp += _SelectionBox_MouseLeftButtonUp;
				_SelectionBox.MouseMove += DesignerPanel_MouseMove;
				AnalyticsCanvas.Children.Add(_SelectionBox);
			}
			_SelectionBox.SetValue(Canvas.LeftProperty, location.X);
			_SelectionBox.SetValue(Canvas.TopProperty, location.Y);
			_SelectionBox.Visibility = Visibility.Visible;
		}

		private void TestScrollbars()
		{
			if (ItemsSource == null)
				return;
			var maxWidth = AnalyticsScrollViewer.ViewportWidth;
			var maxHeight = AnalyticsScrollViewer.ViewportHeight;
			foreach (var item in ItemsSource)
			{
				var elem = item.Element;
				var left = (double)elem.GetValue(Canvas.LeftProperty);
				var top = (double)elem.GetValue(Canvas.TopProperty);
				var xPos = left + elem.ActualWidth;
				var yPos = top + elem.ActualHeight;
				maxWidth = maxWidth < xPos ? xPos : maxWidth;
				maxHeight = maxHeight < yPos ? yPos : maxHeight;
			}
			AnalyticsCanvas.Width = maxWidth;
			AnalyticsCanvas.Height = maxHeight;
		}

		#endregion
	}
}