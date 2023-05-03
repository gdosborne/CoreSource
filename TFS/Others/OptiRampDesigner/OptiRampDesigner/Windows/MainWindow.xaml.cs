namespace OptiRampDesigner.Windows
{
    using GregOsborne.Application.Primitives;
    using MVVMFramework;
    using OptiRampDesignerModel;
    using OptiRampDesignerModel.Events;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Ribbon;

    public partial class MainWindow : RibbonWindow
    {
        #region Private Fields

        private Dictionary<RibbonTab, FrameworkElement> elementsForTab = null;

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();
            elementsForTab = new Dictionary<RibbonTab, FrameworkElement>();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUIAction += View_ExecuteUIAction;
            View.ModuleInitializationComplete += View_ModuleInitializationComplete;
        }

        #endregion Public Constructors

        #region Public Properties

        public MainWindowView View { get { return LayoutRoot.GetView<MainWindowView>(); } }

        #endregion Public Properties

        #region Private Methods

        private void Ribbon_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            elementsForTab.ToList().ForEach(x =>
            {
                x.Value.Visibility = Visibility.Collapsed;
            });
            if (e.AddedItems.Count == 0)
                return;
            var tab = e.AddedItems[0].As<RibbonTab>();
            if (tab == MainTab)
                return;
            elementsForTab[tab].Visibility = Visibility.Visible;
        }

        private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            View.Persist(this);
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            View.Initialize(this);
            View.InitView();
        }

        private void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
        {
            switch (e.CommandToExecute)
            {
                case "BeginRelease":
                    foreach (var item in View.Modules)
                    {
                        Ribbon.Items.Remove(item.Tab);
                    }
                    break;

                case "ShowLoadModulesDialog":
                    var modulesWin = new LoadModulesWindow
                    {
                        Owner = this
                    };
                    modulesWin.ShowDialog();
                    foreach (var item in View.Modules)
                    {
                        Ribbon.Items.Remove(item.Tab);
                        item.Release();
                    }
                    View.Modules = new ObservableCollection<IModule>(App.GetModules());
                    foreach (var item in View.Modules)
                    {
                        item.InitializationComplete += View_ModuleInitializationComplete;
                        item.Initialize();
                    }
                    break;

                case "ShowOptionsDialog":
                    var optionsWin = new OptionsWindow
                    {
                        Owner = this
                    };
                    optionsWin.View.Options = View.Options;
                    optionsWin.ShowDialog();

                    break;
            }
        }

        private void View_ModuleInitializationComplete(object sender, InitializationCompleteEventArgs e)
        {
            Ribbon.Items.Add(e.Tab);
            elementsForTab.Add(e.Tab, e.UserControlContainer);
            e.UserControlContainer.Visibility = Visibility.Collapsed;
            ContainerGrid.Children.Add(e.UserControlContainer);
        }

        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        #endregion Private Methods
    }
}