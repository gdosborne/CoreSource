namespace SDFManager
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Media3D;

	internal class DragCanvas : Canvas
	{
		#region Public Constructors
		static DragCanvas()
		{
			AllowDraggingProperty = DependencyProperty.Register("AllowDragging", typeof(bool), typeof(DragCanvas), new PropertyMetadata(true));
			CanBeDraggedProperty = DependencyProperty.RegisterAttached("CanBeDragged", typeof(bool), typeof(DragCanvas), new UIPropertyMetadata(true));
		}
		public DragCanvas()
		{
		}
		#endregion Public Constructors

		#region Public Methods
		public static bool GetCanBeDragged(UIElement uiElement)
		{
			if (uiElement == null)
				return false;

			return (bool)uiElement.GetValue(CanBeDraggedProperty);
		}
		public static void SetCanBeDragged(UIElement uiElement, bool value)
		{
			if (uiElement != null)
				uiElement.SetValue(CanBeDraggedProperty, value);
		}
		public void BringToFront(UIElement element)
		{
			this.UpdateZOrder(element, true);
		}
		public void Clear()
		{
			Children.Clear();
		}
		public void ClearSelection()
		{
			this.ElementBeingDragged = null;
		}
		public UIElement FindCanvasChild(DependencyObject depObj)
		{
			while (depObj != null)
			{
				UIElement elem = depObj as UIElement;
				if (elem != null && base.Children.Contains(elem))
					break;
				if (depObj is Visual || depObj is Visual3D)
					depObj = VisualTreeHelper.GetParent(depObj);
				else
					depObj = LogicalTreeHelper.GetParent(depObj);
			}
			return depObj as UIElement;
		}
		public double MaxChildPositionX()
		{
			var result = 0.0;
			foreach (FrameworkElement c in Children)
			{
				c.UpdateLayout();
				if (((double)c.GetValue(Canvas.LeftProperty)) + c.ActualWidth + 5 > result)
					result = (double)c.GetValue(Canvas.LeftProperty) + c.ActualWidth + 5;
			}
			return result;
		}
		public double MaxChildPositionY()
		{
			var result = 0.0;
			foreach (FrameworkElement c in Children)
			{
				if (((double)c.GetValue(Canvas.TopProperty)) + c.ActualHeight + 5 > result)
					result = (double)c.GetValue(Canvas.TopProperty) + c.ActualHeight + 5;
			}
			return result;
		}
		public void SendToBack(UIElement element)
		{
			this.UpdateZOrder(element, false);
		}

		#endregion Public Methods
		public event EventHandler DeselectItem;
		public event TableSelectedHandler TableSelected;
		#region Protected Methods
		private void DeselectAll()
		{
			foreach (var item in this.Children.Cast<FrameworkElement>().Where(x => x.Is<TableDefinitionItem>()).Cast<TableDefinitionItem>())
			{
				item.SelectionRectangleVisibility = Visibility.Collapsed;
			}
			if (DeselectItem != null)
				DeselectItem(this, EventArgs.Empty);
		}
		protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseRightButtonDown(e);
			DeselectAll();
		}
		private double originalLeft = 0;
		private double originalTop = 0;
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);
			DeselectAll();
			this.isDragInProgress = false;
			this.origCursorLocation = e.GetPosition(this);
			this.ElementBeingDragged = this.FindCanvasChild(e.Source as DependencyObject);
			if (this.ElementBeingDragged == null || !ElementBeingDragged.Is<TableDefinitionItem>())
				return;

			originalLeft = Canvas.GetLeft(this.ElementBeingDragged);
			double right = Canvas.GetRight(this.ElementBeingDragged);
			originalTop = Canvas.GetTop(this.ElementBeingDragged);
			double bottom = Canvas.GetBottom(this.ElementBeingDragged);
			this.origHorizOffset = ResolveOffset(originalLeft, right, out this.modifyLeftOffset);
			this.origVertOffset = ResolveOffset(originalTop, bottom, out this.modifyTopOffset);

			var tdi = ElementBeingDragged.As<TableDefinitionItem>();
			tdi.SelectionRectangleVisibility = Visibility.Visible;
			tdi.Definition.Width = tdi.ActualWidth;
			tdi.Definition.Height = tdi.ActualHeight;
			if (TableSelected != null)
				TableSelected(this, new TableSelectedEventArgs(tdi.Definition));

			this.isDragInProgress = true;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnPreviewMouseMove(e);
			if (this.ElementBeingDragged == null || !this.isDragInProgress || !ElementBeingDragged.Is<TableDefinitionItem>())
				return;
			Point cursorLocation = e.GetPosition(this);
			double newHorizontalOffset, newVerticalOffset;

			if (this.modifyLeftOffset)
				newHorizontalOffset = this.origHorizOffset + (cursorLocation.X - this.origCursorLocation.X);
			else
				newHorizontalOffset = this.origHorizOffset - (cursorLocation.X - this.origCursorLocation.X);
			if (this.modifyTopOffset)
				newVerticalOffset = this.origVertOffset + (cursorLocation.Y - this.origCursorLocation.Y);
			else
				newVerticalOffset = this.origVertOffset - (cursorLocation.Y - this.origCursorLocation.Y);
			if (this.modifyLeftOffset)
				Canvas.SetLeft(this.ElementBeingDragged, newHorizontalOffset);
			else
				Canvas.SetRight(this.ElementBeingDragged, newHorizontalOffset);

			if (this.modifyTopOffset)
				Canvas.SetTop(this.ElementBeingDragged, newVerticalOffset);
			else
				Canvas.SetBottom(this.ElementBeingDragged, newVerticalOffset);

			if (this.ElementBeingDragged.Is<TableDefinitionItem>())
			{
				var item = this.ElementBeingDragged.As<TableDefinitionItem>().PrimaryKeyItem != null
					? this.ElementBeingDragged.As<TableDefinitionItem>().PrimaryKeyItem
					: this.ElementBeingDragged.As<TableDefinitionItem>();
				DragForeignKeyLine(item);
			}
			e.Handled = true;
		}
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnPreviewMouseUp(e);
			if (ElementBeingDragged == null || !isDragInProgress || !ElementBeingDragged.Is<TableDefinitionItem>())
				return;
			if ((double)this.ElementBeingDragged.GetValue(Canvas.LeftProperty) == originalLeft && (double)this.ElementBeingDragged.GetValue(Canvas.TopProperty) == originalTop)
			{
				originalLeft = 0;
				originalTop = 0;
				ElementBeingDragged = null;
				this.isDragInProgress = false;
				return;
			}
			if (MoveComplete != null)
				MoveComplete(this, new MoveCompleteEventArgs(this.ElementBeingDragged.As<TableDefinitionItem>().Definition, e.GetPosition(this)));
			ClearSelection();
			e.Handled = true;
		}

		#endregion Protected Methods

		#region Private Methods
		private static double ResolveOffset(double side1, double side2, out bool useSide1)
		{
			useSide1 = true;
			double result;
			if (Double.IsNaN(side1))
			{
				if (Double.IsNaN(side2))
				{
					result = 0;
				}
				else
				{
					result = side2;
					useSide1 = false;
				}
			}
			else
			{
				result = side1;
			}
			return result;
		}
		private Rect CalculateDragElementRect(double newHorizOffset, double newVertOffset)
		{
			if (this.ElementBeingDragged == null)
				throw new InvalidOperationException("ElementBeingDragged is null.");
			Size elemSize = this.ElementBeingDragged.RenderSize;
			double x, y;
			if (this.modifyLeftOffset)
				x = newHorizOffset;
			else
				x = this.ActualWidth - newHorizOffset - elemSize.Width;
			if (this.modifyTopOffset)
				y = newVertOffset;
			else
				y = this.ActualHeight - newVertOffset - elemSize.Height;
			Point elemLoc = new Point(x, y);
			return new Rect(elemLoc, elemSize);
		}
		private void DragForeignKeyLine(TableDefinitionItem primary)
		{
			if (primary == null)
				return;
			if (primary.ConnectingLine == null)
				return;
			var p = primary.ConnectingLine;
			var x = (double)primary.GetValue(Canvas.LeftProperty) + primary.ActualWidth - 5;
			var y = (double)primary.GetValue(Canvas.TopProperty) + (primary.ActualHeight / 2);
			var p1 = new Point(x, y);

			p.Points[0] = new Point(x, y);
			p.Points[1] = new Point(x + 20, y);
			p.Points[2] = new Point((double)primary.ForeignKeyItem.GetValue(Canvas.LeftProperty) - 20, (double)primary.ForeignKeyItem.GetValue(Canvas.TopProperty) + primary.ForeignKeyItem.ActualHeight / 2);
			p.Points[3] = new Point((double)primary.ForeignKeyItem.GetValue(Canvas.LeftProperty) + 5, (double)primary.ForeignKeyItem.GetValue(Canvas.TopProperty) + primary.ForeignKeyItem.ActualHeight / 2);
		}
		private void UpdateZOrder(UIElement element, bool bringToFront)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			if (!base.Children.Contains(element))
				throw new ArgumentException("Must be a child element of the Canvas.", "element");
			int elementNewZIndex = -1;
			if (bringToFront)
			{
				foreach (UIElement elem in base.Children)
					if (elem.Visibility != Visibility.Collapsed)
						++elementNewZIndex;
			}
			else
			{
				elementNewZIndex = 0;
			}
			int offset = (elementNewZIndex == 0) ? +1 : -1;

			int elementCurrentZIndex = Canvas.GetZIndex(element);
			foreach (UIElement childElement in base.Children)
			{
				if (childElement == element)
					Canvas.SetZIndex(element, elementNewZIndex);
				else
				{
					int zIndex = Canvas.GetZIndex(childElement);
					if (bringToFront && elementCurrentZIndex < zIndex ||
						!bringToFront && zIndex < elementCurrentZIndex)
					{
						Canvas.SetZIndex(childElement, zIndex + offset);
					}
				}
			}
		}
		#endregion Private Methods

		#region Public Events
		public event MoveCompleteHandler MoveComplete;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty AllowDraggingProperty;
		public static readonly DependencyProperty CanBeDraggedProperty;
		#endregion Public Fields

		#region Private Fields
		private UIElement elementBeingDragged;
		private bool isDragInProgress;
		private bool modifyLeftOffset, modifyTopOffset;
		private Point origCursorLocation;
		private double origHorizOffset, origVertOffset;
		#endregion Private Fields

		#region Public Properties
		public bool AllowDragging
		{
			get { return (bool)base.GetValue(AllowDraggingProperty); }
			set { base.SetValue(AllowDraggingProperty, value); }
		}
		public UIElement ElementBeingDragged
		{
			get
			{
				if (!this.AllowDragging)
					return null;
				else
					return this.elementBeingDragged;
			}
			protected set
			{
				if (this.elementBeingDragged != null)
					this.elementBeingDragged.ReleaseMouseCapture();

				if (!this.AllowDragging)
					this.elementBeingDragged = null;
				else
				{
					if (DragCanvas.GetCanBeDragged(value))
					{
						this.elementBeingDragged = value;
						this.elementBeingDragged.CaptureMouse();
					}
					else
						this.elementBeingDragged = null;
				}
			}
		}
		#endregion Public Properties
	}
}
