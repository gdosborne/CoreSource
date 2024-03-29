namespace Imaginator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using GregOsborne.Application.Primitives;
    using System.Windows.Shapes;
    using System.ComponentModel;
    using Imaginator.Views;
    using MVVMFramework;

    public class InternalWindow : Window, INotifyPropertyChanged
    {
        private Type TheType { get; set; }
        protected void Initialize(Type theType, Canvas canvas, Image displayer, Rectangle selectionRectangle, Rectangle sizeRectangle, Grid layoutRoot)
        {
            ImageCanvas = canvas;
            Displayer = displayer;
            SelectionRectangle = selectionRectangle;
            SizeRectangle = sizeRectangle;
            LayoutRoot = layoutRoot;
            TheType = theType;
            if (theType == typeof(ImageWindow))
                View1.CropRectangle = new System.Drawing.Rectangle(0, 0, 128, 128);
            else
                View2.CropRectangle = new System.Drawing.Rectangle(0, 0, 128, 128);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private double ResolveOffset(double side1, double side2, out bool useSide1)
        {
            useSide1 = true;
            double result;
            if (double.IsNaN(side1))
            {
                if (double.IsNaN(side2))
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

        private UIElement elementDragged = null;
        private bool isDragging = false;
        private Point origCursorLocation;
        private double originalLeft = 0;
        private double originalTop = 0;
        private double origHorizOffset = 0;
        private double origVertOffset = 0;
        private bool modifyLeftOffset = true;
        private bool modifyTopOffset = true;
        protected void Rectangle_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = true;
            origCursorLocation = e.GetPosition(this);
            elementDragged = (UIElement)sender;
            originalLeft = Canvas.GetLeft(elementDragged);
            originalTop = Canvas.GetTop(elementDragged);
            double right = Canvas.GetRight(elementDragged);
            double bottom = Canvas.GetBottom(elementDragged);
            this.origHorizOffset = ResolveOffset(originalLeft, right, out this.modifyLeftOffset);
            this.origVertOffset = ResolveOffset(originalTop, bottom, out this.modifyTopOffset);
        }
        private Grid _LayoutRoot;
        public Grid LayoutRoot
        {
            get { return _LayoutRoot; }
            set
            {
                _LayoutRoot = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private Canvas _ImageCanvas;
        public Canvas ImageCanvas
        {
            get { return _ImageCanvas; }
            set
            {
                _ImageCanvas = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private Image _Displayer;
        public Image Displayer
        {
            get { return _Displayer; }
            set
            {
                _Displayer = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private Rectangle _SizeRectangle;
        public Rectangle SizeRectangle
        {
            get { return _SizeRectangle; }
            set
            {
                _SizeRectangle = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private Rectangle _SelectionRectangle;
        public Rectangle SelectionRectangle
        {
            get { return _SelectionRectangle; }
            set
            {
                _SelectionRectangle = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private System.Drawing.Rectangle _CropRectangle;
        public System.Drawing.Rectangle CropRectangle
        {
            get { return _CropRectangle; }
            set
            {
                _CropRectangle = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        protected void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (elementDragged == null || !isDragging)
                return;
            Point cursorLocation = e.GetPosition(this);
            double newHorizontalOffset, newVerticalOffset;

            if (modifyLeftOffset)
            {
                newHorizontalOffset = origHorizOffset + (cursorLocation.X - origCursorLocation.X);
                if (newHorizontalOffset < 0)
                    newHorizontalOffset = 0;
                else if (newHorizontalOffset + elementDragged.As<FrameworkElement>().ActualWidth > Displayer.ActualWidth)
                    newHorizontalOffset = Displayer.ActualWidth - elementDragged.As<FrameworkElement>().ActualWidth;
                Canvas.SetLeft(elementDragged, newHorizontalOffset);
            }
            else
            {
                newHorizontalOffset = origHorizOffset - (cursorLocation.X - origCursorLocation.X);
                Canvas.SetRight(elementDragged, newHorizontalOffset);
            }

            if (modifyTopOffset)
            {
                newVerticalOffset = origVertOffset + (cursorLocation.Y - origCursorLocation.Y);
                if (newVerticalOffset < 0)
                    newVerticalOffset = 0;
                else if (newVerticalOffset + elementDragged.As<FrameworkElement>().ActualHeight > Displayer.ActualHeight)
                    newVerticalOffset = Displayer.ActualHeight - elementDragged.As<FrameworkElement>().ActualHeight;
                Canvas.SetTop(elementDragged, newVerticalOffset);
            }
            else
            {
                newVerticalOffset = origVertOffset - (cursorLocation.Y - origCursorLocation.Y);
                Canvas.SetBottom(elementDragged, newVerticalOffset);
            }

            if (elementDragged == SizeRectangle)
            {
                var w = newHorizontalOffset + SizeRectangle.ActualWidth - Canvas.GetLeft(SelectionRectangle);
                var h = newVerticalOffset + SizeRectangle.ActualHeight - Canvas.GetTop(SelectionRectangle);
                w = w > h ? w : h;
                h = w;
                SelectionRectangle.Width = w;
                SelectionRectangle.Height = h;
            }
            else if (elementDragged == SelectionRectangle)
            {
                if (modifyLeftOffset)
                    Canvas.SetLeft(SizeRectangle, Canvas.GetLeft(SelectionRectangle) + SelectionRectangle.ActualWidth - SizeRectangle.ActualWidth);
                else
                    Canvas.SetRight(SizeRectangle, Canvas.GetLeft(SelectionRectangle) + SelectionRectangle.ActualWidth - SizeRectangle.ActualWidth);

                if (modifyTopOffset)
                    Canvas.SetTop(SizeRectangle, Canvas.GetTop(SelectionRectangle) + SelectionRectangle.ActualHeight - SizeRectangle.ActualHeight);
                else
                    Canvas.SetBottom(SizeRectangle, Canvas.GetTop(SelectionRectangle) + SelectionRectangle.ActualHeight - SizeRectangle.ActualHeight);
            }
            e.Handled = true;
        }

        protected void Canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (elementDragged == null || !isDragging)
                return;
            if ((double)elementDragged.GetValue(Canvas.LeftProperty) == originalLeft && (double)elementDragged.GetValue(Canvas.TopProperty) == originalTop)
            {
                originalLeft = 0;
                originalTop = 0;
                elementDragged = null;
            }
            if (elementDragged == SizeRectangle)
            {
                if (modifyLeftOffset)
                    Canvas.SetLeft(SizeRectangle, (double)SelectionRectangle.GetValue(Canvas.LeftProperty) + SelectionRectangle.ActualWidth - SizeRectangle.ActualWidth);
                else
                    Canvas.SetRight(SizeRectangle, (double)SelectionRectangle.GetValue(Canvas.LeftProperty) + SelectionRectangle.ActualWidth - SizeRectangle.ActualWidth);

                if (modifyTopOffset)
                    Canvas.SetTop(SizeRectangle, (double)SelectionRectangle.GetValue(Canvas.TopProperty) + SelectionRectangle.ActualHeight - SizeRectangle.ActualHeight);
                else
                    Canvas.SetBottom(SizeRectangle, (double)SelectionRectangle.GetValue(Canvas.TopProperty) + SelectionRectangle.ActualHeight - SizeRectangle.ActualHeight);
            }
            var r = new System.Drawing.Rectangle(
                    System.Convert.ToInt32(Canvas.GetLeft(SelectionRectangle)),
                    System.Convert.ToInt32(Canvas.GetTop(SelectionRectangle)),
                    System.Convert.ToInt32(SelectionRectangle.ActualWidth),
                    System.Convert.ToInt32(SelectionRectangle.ActualHeight));
            if (TheType == typeof(ImageWindow))
                View1.CropRectangle = r;
            else
                View2.CropRectangle = r;
            isDragging = false;
            e.Handled = true;
        }

        public ImageWindowView View1 { get { return LayoutRoot.GetView<ImageWindowView>(); } }
        public PastedWindowView View2 { get { return LayoutRoot.GetView<PastedWindowView>(); } }

        private bool initializing = true;
        protected void SelectionRectangle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!initializing)
                return;
            Canvas.SetLeft(SizeRectangle, sender.As<Rectangle>().ActualWidth - SizeRectangle.ActualWidth);
            Canvas.SetTop(SizeRectangle, sender.As<Rectangle>().ActualHeight - SizeRectangle.ActualHeight);
            initializing = false;
        }
    }
}
