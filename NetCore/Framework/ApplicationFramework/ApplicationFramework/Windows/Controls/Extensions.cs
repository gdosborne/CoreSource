﻿using Common.OzApplication.Media;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Common.OzApplication.Windows.Controls {
    public static class Extensions {
        private static Delegate conversionHandler;

        public static bool IsWithinControl(this Point point, FrameworkElement c) {
            if (c == null) {
                throw new ArgumentException("Missing the control");
            }
            if (point == null) {
                throw new ArgumentException("Missing the point");
            }
            
            return false;
        }

        public static async void GetElementAsImageSourceAsync(this FrameworkElement rootElement, Delegate handler) {
            conversionHandler = handler;
            var converter = new XamlToPngConverter();
            converter.ConversionComplete += Converter_ConversionComplete;
            await converter.Convert(rootElement);
        }

        public static void RemoveOverflow(this ToolBar value) {
            var overflowGrid = value.Template.FindName("OverflowGrid", value) as Grid;
            var overflowButton = value.Template.FindName("OverflowButton", value) as ButtonBase;
            if (overflowButton != null) {
                overflowButton.Visibility = Visibility.Collapsed;
            }
        }

        private static void Converter_ConversionComplete(object sender, ConversionCompleteEventArgs e) => conversionHandler?.DynamicInvoke(sender, e);

        public static T GetBoundElement<T>(FrameworkElement namedElement, DependencyProperty property, object source) {
            var binding = new Binding(property.Name) {
                BindsDirectlyToSource = true,
                Mode = BindingMode.TwoWay,
                Source = source,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            var result = new Line();
            BindingOperations.SetBinding(result, property, binding);
            return (T)(object)result;
        }

        public static void BindElement(FrameworkElement namedElement, DependencyProperty property, object source) {
            var binding = new Binding(property.Name) {
                BindsDirectlyToSource = true,
                Mode = BindingMode.TwoWay,
                Source = source,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(namedElement, property, binding);
        }

        public static void ScrollToCenterOfView(this ItemsControl itemsControl, object item) {
            // Scroll immediately if possible
            if (!itemsControl.TryScrollToCenterOfView(item)) {
                // Otherwise wait until everything is loaded, then scroll
                if (itemsControl is ListBox) ((ListBox)itemsControl).ScrollIntoView(item);
                itemsControl.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => {
                    itemsControl.TryScrollToCenterOfView(item);
                }));
            }
        }

        private static bool TryScrollToCenterOfView(this ItemsControl itemsControl, object item) {
            // Find the container
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
            if (container == null) return false;

            // Find the ScrollContentPresenter
            ScrollContentPresenter presenter = null;
            for (Visual vis = container; vis != null && vis != itemsControl; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if ((presenter = vis as ScrollContentPresenter) != null)
                    break;
            if (presenter == null) return false;

            // Find the IScrollInfo
            var scrollInfo =
                !presenter.CanContentScroll ? presenter :
                presenter.Content as IScrollInfo ??
                FirstVisualChild(presenter.Content as ItemsPresenter) as IScrollInfo ??
                presenter;

            // Compute the center point of the container relative to the scrollInfo
            Size size = container.RenderSize;
            Point center = container.TransformToAncestor((Visual)scrollInfo).Transform(new Point(size.Width / 2, size.Height / 2));
            center.Y += scrollInfo.VerticalOffset;
            center.X += scrollInfo.HorizontalOffset;

            // Adjust for logical scrolling
            if (scrollInfo is StackPanel || scrollInfo is VirtualizingStackPanel) {
                double logicalCenter = itemsControl.ItemContainerGenerator.IndexFromContainer(container) + 0.5;
                Orientation orientation = scrollInfo is StackPanel ? ((StackPanel)scrollInfo).Orientation : ((VirtualizingStackPanel)scrollInfo).Orientation;
                if (orientation == Orientation.Horizontal)
                    center.X = logicalCenter;
                else
                    center.Y = logicalCenter;
            }

            // Scroll the center of the container to the center of the viewport
            if (scrollInfo.CanVerticallyScroll) scrollInfo.SetVerticalOffset(CenteringOffset(center.Y, scrollInfo.ViewportHeight, scrollInfo.ExtentHeight));
            if (scrollInfo.CanHorizontallyScroll) scrollInfo.SetHorizontalOffset(CenteringOffset(center.X, scrollInfo.ViewportWidth, scrollInfo.ExtentWidth));
            return true;
        }

        private static double CenteringOffset(double center, double viewport, double extent) {
            return Math.Min(extent - viewport, Math.Max(0, center - viewport / 2));
        }
        private static DependencyObject FirstVisualChild(Visual visual) {
            if (visual == null) return null;
            if (VisualTreeHelper.GetChildrenCount(visual) == 0) return null;
            return VisualTreeHelper.GetChild(visual, 0);
        }
    }


}