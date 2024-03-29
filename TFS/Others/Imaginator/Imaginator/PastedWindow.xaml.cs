namespace Imaginator
{
    using Imaginator.Views;
    using MVVMFramework;
    using System.Windows;
    using System.Windows.Input;

    public partial class PastedWindow : InternalWindow
    {
        public PastedWindow()
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

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public PastedWindowView View { get { return View2; } }

        private void ThisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.InitView();
        }


    }
}
