namespace GregOsborne.Controls
{
    using GregOsborne.Application.Primitives;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class DockIndicator : UserControl
    {
        public static readonly DependencyProperty LinesBrushDependencyProperty = DependencyProperty.Register("LinesBrush", typeof(Brush), typeof(DockIndicator), new FrameworkPropertyMetadata(Brushes.White, OnLinesBrushPropertyChanged, CoerceLinesBrushProperty));
        public static readonly DependencyProperty MouseOverBrushDependencyProperty = DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(DockIndicator), new FrameworkPropertyMetadata(Brushes.LightGray, OnMouseOverBrushPropertyChanged, CoerceMouseOverBrushProperty));
        public static readonly DependencyProperty SelectedBrushDependencyProperty = DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(DockIndicator), new FrameworkPropertyMetadata(Brushes.Green, OnSelectedBrushPropertyChanged, CoerceSelectedBrushProperty));
        public static readonly DependencyProperty SideDockDependencyProperty = DependencyProperty.Register("SideDock", typeof(Dock), typeof(DockIndicator), new FrameworkPropertyMetadata(Dock.Top, OnSideDockPropertyChanged, CoerceSideDockProperty));
        public static readonly DependencyProperty UnselectedBrushDependencyProperty = DependencyProperty.Register("UnselectedBrush", typeof(Brush), typeof(DockIndicator), new FrameworkPropertyMetadata(Brushes.Gray, OnUnselectedBrushPropertyChanged, CoerceUnselectedBrushProperty));
        private Polygon _bottomPolygon = null;
        private Polygon _leftPolygon = null;
        private Polygon _rightPolygon = null;
        private Polygon _topPolygon = null;

        public DockIndicator()
        {
            InitializeComponent();
        }

        public Brush LinesBrush {
            get => (Brush)GetValue(LinesBrushDependencyProperty);
            set => SetValue(LinesBrushDependencyProperty, value);
        }

        public Brush MouseOverBrush {
            get => (Brush)GetValue(MouseOverBrushDependencyProperty);
            set => SetValue(MouseOverBrushDependencyProperty, value);
        }

        public Brush SelectedBrush {
            get => (Brush)GetValue(SelectedBrushDependencyProperty);
            set => SetValue(SelectedBrushDependencyProperty, value);
        }

        public Dock SideDock {
            get => (Dock)GetValue(SideDockDependencyProperty);
            set => SetValue(SideDockDependencyProperty, value);
        }

        public Brush UnselectedBrush {
            get => (Brush)GetValue(UnselectedBrushDependencyProperty);
            set => SetValue(UnselectedBrushDependencyProperty, value);
        }

        private static object CoerceLinesBrushProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceMouseOverBrushProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSelectedBrushProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceSideDockProperty(DependencyObject d, object value)
        {
            var val = (Dock)value;
            //coerce value here if necessary
            return val;
        }

        private static object CoerceUnselectedBrushProperty(DependencyObject d, object value)
        {
            var val = (Brush)value;
            //coerce value here if necessary
            return val;
        }

        private static void OnLinesBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DockIndicator)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private static void OnMouseOverBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DockIndicator)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private static void OnSelectedBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DockIndicator)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private static void OnSideDockPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DockIndicator)d;
            var val = (Dock)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private static void OnUnselectedBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (DockIndicator)d;
            var val = (Brush)e.NewValue;
            //modify the value here, i.e., obj.ctrl.Value = val
        }

        private void AddHandlers()
        {
            _topPolygon.PreviewMouseDown += Polygon_PreviewMouseDown;
            _topPolygon.MouseLeave += Polygon_MouseLeave;
            _topPolygon.MouseEnter += Polygon_MouseEnter;
            _rightPolygon.PreviewMouseDown += Polygon_PreviewMouseDown;
            _rightPolygon.MouseLeave += Polygon_MouseLeave;
            _rightPolygon.MouseEnter += Polygon_MouseEnter;
            _bottomPolygon.PreviewMouseDown += Polygon_PreviewMouseDown;
            _bottomPolygon.MouseLeave += Polygon_MouseLeave;
            _bottomPolygon.MouseEnter += Polygon_MouseEnter;
            _leftPolygon.PreviewMouseDown += Polygon_PreviewMouseDown;
            _leftPolygon.MouseLeave += Polygon_MouseLeave;
            _leftPolygon.MouseEnter += Polygon_MouseEnter;
        }

        private void Polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            var poly = sender.As<Polygon>();
            poly.Fill = MouseOverBrush;
        }

        private void Polygon_MouseLeave(object sender, MouseEventArgs e)
        {
            var poly = sender.As<Polygon>();
            var dock = (Dock)poly.Tag;
            if ((dock == Dock.Top && SideDock == Dock.Top)
                || (dock == Dock.Right && SideDock == Dock.Right)
                || (dock == Dock.Bottom && SideDock == Dock.Bottom)
                || (dock == Dock.Left && SideDock == Dock.Left))
                poly.Fill = SelectedBrush;
            else
                poly.Fill = UnselectedBrush;
        }

        private void Polygon_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _topPolygon.Fill = UnselectedBrush;
            _rightPolygon.Fill = UnselectedBrush;
            _bottomPolygon.Fill = UnselectedBrush;
            _leftPolygon.Fill = UnselectedBrush;
            var dock = (Dock)sender.As<Polygon>().Tag;
            if ((dock) == Dock.Top)
                SideDock = Dock.Top;
            else if ((dock) == Dock.Right)
                SideDock = Dock.Right;
            else if ((dock) == Dock.Bottom)
                SideDock = Dock.Bottom;
            else if ((dock) == Dock.Left)
                SideDock = Dock.Left;
            sender.As<Polygon>().Fill = SelectedBrush;
        }

        private void RemoveHandlers()
        {
            if (_topPolygon != null)
            {
                _topPolygon.PreviewMouseDown -= Polygon_PreviewMouseDown;
                _topPolygon.MouseLeave -= Polygon_MouseLeave;
                _topPolygon.MouseEnter -= Polygon_MouseEnter;
            }
            if (_rightPolygon != null)
            {
                _rightPolygon.PreviewMouseDown -= Polygon_PreviewMouseDown;
                _rightPolygon.MouseLeave -= Polygon_MouseLeave;
                _rightPolygon.MouseEnter -= Polygon_MouseEnter;
            }
            if (_bottomPolygon != null)
            {
                _bottomPolygon.PreviewMouseDown -= Polygon_PreviewMouseDown;
                _bottomPolygon.MouseLeave -= Polygon_MouseLeave;
                _bottomPolygon.MouseEnter -= Polygon_MouseEnter;
            }
            if (_leftPolygon != null)
            {
                _leftPolygon.PreviewMouseDown -= Polygon_PreviewMouseDown;
                _leftPolygon.MouseLeave -= Polygon_MouseLeave;
                _leftPolygon.MouseEnter -= Polygon_MouseEnter;
            }
        }

        private void thisControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePolygons();
        }

        private void thisControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePolygons();
        }

        private void UpdatePolygons()
        {
            RemoveHandlers();
            outerGrid.Children.Clear();
            _topPolygon = new Polygon
            {
                Tag = Dock.Top,
                //Style = Resources["TabPositionPolygon"].As<Style>(),
                Fill = SideDock == Dock.Top ? SelectedBrush : UnselectedBrush,
                StrokeThickness = .75,
                Stroke = LinesBrush
            };
            _topPolygon.Points.Add(new Point(0, 0));
            _topPolygon.Points.Add(new Point(ActualWidth, 0));
            _topPolygon.Points.Add(new Point(ActualWidth / 2, ActualHeight / 2));
            _rightPolygon = new Polygon
            {
                Tag = Dock.Right,
                //Style = Resources["TabPositionPolygon"].As<Style>(),
                Fill = SideDock == Dock.Right ? SelectedBrush : UnselectedBrush,
                StrokeThickness = .75,
                Stroke = LinesBrush
            };
            _rightPolygon.Points.Add(new Point(ActualWidth, 0));
            _rightPolygon.Points.Add(new Point(ActualWidth, ActualHeight));
            _rightPolygon.Points.Add(new Point(ActualWidth / 2, ActualHeight / 2));
            _bottomPolygon = new Polygon
            {
                Tag = Dock.Bottom,
                //Style = Resources["TabPositionPolygon"].As<Style>(),
                Fill = SideDock == Dock.Bottom ? SelectedBrush : UnselectedBrush,
                StrokeThickness = .75,
                Stroke = LinesBrush
            };
            _bottomPolygon.Points.Add(new Point(ActualWidth, ActualHeight));
            _bottomPolygon.Points.Add(new Point(0, ActualHeight));
            _bottomPolygon.Points.Add(new Point(ActualWidth / 2, ActualHeight / 2));
            _leftPolygon = new Polygon
            {
                Tag = Dock.Left,
                //Style = Resources["TabPositionPolygon"].As<Style>(),
                Fill = SideDock == Dock.Left ? SelectedBrush : UnselectedBrush,
                StrokeThickness = .75,
                Stroke = LinesBrush
            };
            _leftPolygon.Points.Add(new Point(0, ActualHeight));
            _leftPolygon.Points.Add(new Point(0, 0));
            _leftPolygon.Points.Add(new Point(ActualWidth / 2, ActualHeight / 2));

            outerGrid.Children.Add(_topPolygon);
            outerGrid.Children.Add(_rightPolygon);
            outerGrid.Children.Add(_bottomPolygon);
            outerGrid.Children.Add(_leftPolygon);
            AddHandlers();
        }
    }
}
