namespace Imaginator
{
    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;
    using Imaginator.Data;
    using Imaginator.Views;
    using MVVMFramework;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            View.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {

            };
            View.ExecuteUIAction += View_ExecuteUIAction;
            View.BeginAllConversion += View_BeginAllConversion;
            View.UpdateProgress += View_UpdateProgress;
        }

        void View_UpdateProgress(double value)
        {
            if (this.Dispatcher.CheckAccess())
                View.AllProgressValue = value;
            else
                Dispatcher.BeginInvoke(new Imaginator.Views.MainWindowView.ConversionHandler(View_UpdateProgress), new object[] { value });

        }

        void View_BeginAllConversion(double value)
        {
            if (this.Dispatcher.CheckAccess())
            {
                View.AllProgressMinimum = 0;
                View.AllProgressMaximum = value;
                View.AllProgressValue = 0;
            }
            else
                Dispatcher.BeginInvoke(new Imaginator.Views.MainWindowView.ConversionHandler(View_BeginAllConversion), new object[] { value });
        }
        private bool SelectTreeViewItemByDirectoryItem(DirectoryItem item, TreeViewItem parent)
        {
            var result = false;
            ItemCollection items = ImagesTreeView.Items;
            if (parent != null)
                items = parent.Items;
            foreach (TreeViewItem tvi in items)
            {
                var theItem = tvi.Tag.As<DirectoryItem>();
                if (item.FullPath.Equals(theItem.FullPath))
                {
                    var looper = tvi;
                    while (looper.Parent != null && looper.Parent.Is<TreeViewItem>())
                    {
                        looper = looper.Parent.As<TreeViewItem>();
                        looper.IsExpanded = true;
                    }
                    tvi.IsSelected = true;
                    return true;
                }
                else
                    result = SelectTreeViewItemByDirectoryItem(item, tvi);
                if (result)
                    break;
            }
            return result;
        }
        void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
        {
            if (this.Dispatcher.CheckAccess())
            {
                switch (e.CommandToExecute)
                {
                    case "ShowSettingsDialog":
                        var win2 = new SettingsWindow
                        {
                            Owner = this
                        };
                        var imgResult2 = win2.ShowDialog();
                        break;
                    case "ShowPasteDialog":
                        var win1 = new PastedWindow
                        {
                            Owner = this
                        };
                        var imgResult1 = win1.ShowDialog();
                        e.Parameters["result"] = imgResult1;
                        e.Parameters["sizes"] = win1.View.Sizes;
                        e.Parameters["tempfilename"] = win1.View.TemporaryFileName;
                        break;
                    case "ShowImageImporterDialog":
                        var win = new ImageWindow
                        {
                            Owner = this
                        };
                        win.View.FileName = (string)e.Parameters["filename"];
                        var imgResult = win.ShowDialog();
                        e.Parameters["result"] = imgResult;
                        e.Parameters["sizes"] = win.View.Sizes;
                        e.Parameters["tempfilename"] = win.View.TemporaryFileName;
                        break;
                    case "ShowImageOpenDialog":
                        string filter = string.Empty;
                        string allFilters = string.Empty;
                        if (View.UsePng)
                        {
                            filter += "Portable Network Graphics Files (*.png)|*.png|";
                            allFilters += "*.png";
                        }
                        if (View.UseJpg)
                        {
                            filter += "Joint Photo Expert Group Files (*.jpg, *.jpeg)|*.jpg;*.jpeg|";
                            allFilters = allFilters + (allFilters.Length > 0 ? ";" : string.Empty) + "*.jpg;*.jpeg";
                        }
                        if (View.UseBmp)
                        {
                            filter += "Bitmap Files (*.bmp)|*.bmp|";
                            allFilters = allFilters + (allFilters.Length > 0 ? ";" : string.Empty) + "*.bmp";
                        }
                        if (View.UseIco)
                        {
                            filter += "Icon Files (*.ico)|*.ico|";
                            allFilters = allFilters + (allFilters.Length > 0 ? ";" : string.Empty) + "*.ico";
                        }
                        filter = "All image files|" + allFilters + "|" + filter.TrimEnd('|');
                        var openFileDlg = new Microsoft.Win32.OpenFileDialog
                        {
                            CheckFileExists = true,
                            Filter = filter,
                            InitialDirectory = View.LastImageOpenFolder,
                            Multiselect = false,
                            Title = "Select image file..."
                        };
                        var result = openFileDlg.ShowDialog(this);
                        e.Parameters["result"] = result;
                        e.Parameters["filename"] = openFileDlg.FileName;
                        break;
                    case "SelectTreeItem":
                        SelectTreeViewItemByDirectoryItem(e.Parameters["item"].As<DirectoryItem>(), null);
                        break;
                    case "ClearImages":
                        ImagesWrapPanel.Children.Clear();
                        break;
                    case "ClearTree":
                        if (ImagesTreeView.Items.Count > 0)
                            ImagesTreeView.Items.RemoveAt(0);
                        break;
                    case "AddTreeItem":
                        AddDirectoryTreeItem(e.Parameters["item"].As<DirectoryItem>(), null);
                        break;
                    case "AddImage":
                        AddImage(e.Parameters["item"].As<FileItem>());
                        break;
                }
            }
            else
                Dispatcher.BeginInvoke(new ExecuteUIActionHandler(View_ExecuteUIAction), new object[] { sender, e });
        }
        private void AddImage(FileItem item)
        {
            if (item == null)
                return;
            if (System.IO.Path.GetExtension(item.FullPath).Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                System.IO.Path.GetExtension(item.FullPath).Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                System.IO.Path.GetExtension(item.FullPath).Equals(".bmp", StringComparison.OrdinalIgnoreCase) ||
                System.IO.Path.GetExtension(item.FullPath).Equals(".ico", StringComparison.OrdinalIgnoreCase))
            {
                var bitmapImage = item.FullPath.GetBitmapImage();
                if (bitmapImage == null || bitmapImage.Width != bitmapImage.Height)
                    return;
                var sp = new StackPanel { Orientation = Orientation.Vertical };
                var tb1 = new TextBlock { Text = item.Name };
                sp.Children.Add(tb1);
                tb1 = new TextBlock { Text = string.Format("Dimension: {0}x{1}", Convert.ToInt32(bitmapImage.Width), Convert.ToInt32(bitmapImage.Height)) };
                sp.Children.Add(tb1);
                tb1 = new TextBlock { Text = string.Format("Size: {0}", GregOsborne.Application.IO.File.Size(item.FullPath).Value.ToKBString<long>(true)) };
                sp.Children.Add(tb1);
                var g = new Grid { Margin = new Thickness(5), VerticalAlignment = VerticalAlignment.Top, Tag = item };
                var rect = new Rectangle { StrokeThickness = 1, Stroke = App.Current.Resources["SelectedOutlineBrush"].As<Brush>(), Visibility = Visibility.Hidden, StrokeDashArray = new DoubleCollection { 5, 5 } };
                g.PreviewMouseLeftButtonDown += g_PreviewMouseLeftButtonDown;
                g.PreviewMouseRightButtonDown += g_PreviewMouseRightButtonDown;

                g.Children.Add(rect);
                var img = new Image { Width = bitmapImage.Width, Height = bitmapImage.Height, Source = bitmapImage, ToolTip = sp, Margin = new Thickness(5) };
                g.Children.Add(img);
                ImagesWrapPanel.Children.Add(g);
            }
        }

        void g_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            g_PreviewMouseLeftButtonDown(sender, e);
        }

        private void g_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender.As<Grid>().Tag.As<FileItem>();
            View.SelectedItem = item;
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                if (App.GetSetting<string>("App.Selected.Double.Click.Action", "Convert to all sizes").Equals("Convert to all sizes"))
                {
                    View.PerformConvertAll();
                    MessageBox.Show("Converted to 128, 64, 48, 32, 24, & 16 pixel sizes.", "Conversion", MessageBoxButton.OK);
                }
                else if (App.GetSetting<string>("App.Selected.Double.Click.Action", "Convert to all sizes").Equals("Open default application"))
                {
                    new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = item.FullPath,
                            WindowStyle = ProcessWindowStyle.Normal
                        }
                    }.Start();
                }
                return;
            }
            
            ImagesWrapPanel.Children.Cast<Grid>().ToList().ForEach(x =>
            {
                x.Background = new SolidColorBrush(Colors.Transparent);
                x.GetFirstChild<Rectangle>().Visibility = Visibility.Hidden;
            });
            sender.As<Grid>().Background = App.Current.Resources["SelectedImageBrush"].As<Brush>();
            sender.As<Grid>().GetFirstChild<Rectangle>().Visibility = Visibility.Visible;
        }
        private void AddDirectoryTreeItem(DirectoryItem item, TreeViewItem parent)
        {
            var sp = new StackPanel { Orientation = Orientation.Horizontal };
            var img = new Image { Width = 24, Height = 24, Margin = new Thickness(2, 1, 2, 1), Source = this.GetType().Assembly.GetImageSourceByName("Resources/openFolder.png") };
            sp.Children.Add(img);
            var tb = new TextBlock { Text = item.Name, Style = App.Current.Resources["TitleTextBlock"].As<Style>() };
            sp.Children.Add(tb);
            var ti = new TreeViewItem
            {
                Header = sp,
                Tag = item,
                Style = App.Current.Resources["MyTreeViewItem"].As<Style>()
            };
            if (parent == null)
            {
                ImagesTreeView.Items.Add(ti);
                ti.IsExpanded = true;
            }
            else
                parent.Items.Add(ti);
            if (item.Directories.Any())
                item.Directories.ToList().ForEach(x => AddDirectoryTreeItem(x, ti));
        }
        public MainWindowView View {
            get { return LayoutRoot.GetView<MainWindowView>(); }
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Minimize_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.Initialize(this);
            View.InitView();
        }

        private void ThisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            View.Persist(this);
        }

        private void RootFolderTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            sender.As<TextBox>().SelectAll();
        }

        private void ImagesTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null)
                View.SelectedDirectory = e.NewValue.As<TreeViewItem>().Tag.As<DirectoryItem>();
        }

    }
}
