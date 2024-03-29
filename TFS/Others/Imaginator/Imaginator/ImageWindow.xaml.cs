namespace Imaginator
{
    using Imaginator.Views;
    using MVVMFramework;
    using System.Windows;
    using System.Windows.Controls;
    using GregOsborne.Application.Primitives;
    using System.Windows.Shapes;

    public partial class ImageWindow : InternalWindow
    {
        public ImageWindow()
        {
            InitializeComponent();

            Initialize(this.GetType(), ImageCanvas, Displayer, SelectionRectangle, SizeRectangle, LayoutRoot);

            View.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                switch (e.PropertyName)
                {
                    case "DialogResult":
                        DialogResult = View.DialogResult;
                        break;
                }
            };
            View.ExecuteUIAction += (object sender, ExecuteUIActionEventArgs e) =>
            {
                switch (e.CommandToExecute)
                {
                    case "GetConversionSizes":
                        var win = new SizesWindow
                        {
                            Owner = this
                        };
                        win.View.TemporaryFileName = (string)e.Parameters["tempfilename"];
                        var result = win.ShowDialog();
                        e.Parameters["result"] = result;
                        if (result.GetValueOrDefault())
                            e.Parameters["sizes"] = win.View.Sizes;
                        break;
                }
            };
        }

        public ImageWindowView View { get { return View1; } }

        private void Close_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }            
    }
}
