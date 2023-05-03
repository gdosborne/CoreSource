namespace OptiRampDesigner.Windows
{
    using GregOsborne.Application.Primitives;
    using GregOsborne.Application.Windows;
    using MVVMFramework;
    using OptiRampDesignerModel;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class OptionsWindow : Window
    {
        #region Public Constructors

        public OptionsWindow()
        {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUIAction += View_ExecuteUIAction;
        }

        #endregion Public Constructors

        #region Public Properties

        public OptionsWindowView View { get { return LayoutRoot.GetView<OptionsWindowView>(); } }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.HideControlBox();
            this.HideMinimizeAndMaximizeButtons();
        }

        #endregion Protected Methods

        #region Private Methods

        private void AddCategoryTotree(IOptionCategory cat, TreeViewItem parent)
        {
            var ti = new TreeViewItem { Header = cat.Name };
            cat.Categories.ToList().ForEach(x =>
            {
                AddCategoryTotree(x, ti);
            });
            parent.Items.Add(ti);
        }

        private void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
        {
            switch (e.CommandToExecute)
            {
                case "ClearTree":
                    OptionsTree.Items.Clear();
                    break;

                case "AddOptionSet":
                    IOptionSet set = e.Parameters["optionset"].As<IOptionSet>();
                    var ti = new TreeViewItem { Header = set.ModuleName };
                    set.Categories.ToList().ForEach(x =>
                    {
                        AddCategoryTotree(x, ti);
                    });
                    OptionsTree.Items.Add(ti);
                    break;
            }
        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            View.Persist(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            View.Initialize(this);
            View.InitView();
        }

        #endregion Private Methods
    }
}