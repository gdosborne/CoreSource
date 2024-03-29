using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace OPF {
    public static class Initializer {
        public static void InitializeResizer(Grid mainGrid) {
            var rect = new Rectangle {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Transparent),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                Cursor = Cursors.SizeNWSE
            };
            rect.SetValue(Grid.RowProperty, 0);
            rect.SetValue(Grid.ColumnProperty, 0);
            rect.SetValue(Grid.RowSpanProperty, 99);
            rect.SetValue(Grid.ColumnSpanProperty, 99);
            rect.MouseDown += Rect_MouseDown;
            rect.MouseUp += Rect_MouseUp;
            rect.MouseMove += Rect_MouseMove;
            _MainGrid = mainGrid;
            mainGrid.Children.Add(rect);
        }
        public static void InitializeWindow(Window window, Border titleBorder, TextBlock titleTextBlock) {
            titleTextBlock.Text = window.Title;
            _Window = window;
            titleBorder.MouseDown += TitleBorder_MouseDown;
        }

        private static void TitleBorder_MouseDown(object sender, MouseButtonEventArgs e) {
            _Window.DragMove();
        }

        private static void Rect_MouseMove(object sender, MouseEventArgs e) {
            if(_MouseDown){
                var endingPoint = e.GetPosition(_MainGrid);
                var newOffsetX = endingPoint.X - _StartingPoint.X;
                var newOffsetY = endingPoint.Y - _StartingPoint.Y;
                Window.GetWindow(_MainGrid).Width = Window.GetWindow(_MainGrid).Width + newOffsetX;
                Window.GetWindow(_MainGrid).Height = Window.GetWindow(_MainGrid).Height + newOffsetY;
                _StartingPoint = e.GetPosition(_MainGrid);
            }
        }

        private static void Rect_MouseUp(object sender, MouseButtonEventArgs e) {
            _MouseDown = false;
            (sender as Rectangle).ReleaseMouseCapture();
        }
        private static Grid _MainGrid = null;
        private static bool _MouseDown = false;
        private static Point _StartingPoint;
        private static Window _Window = null;
        private static void Rect_MouseDown(object sender, MouseButtonEventArgs e) {
            _MouseDown = true;
            _StartingPoint = e.GetPosition(_MainGrid);
            (sender as Rectangle).CaptureMouse();
        }
    }
}
