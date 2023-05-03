namespace OptiRampDesigner.Windows
{
    using GregOsborne.Application.Windows;
    using MVVMFramework;
    using Ookii.Dialogs.Wpf;
    using System;
    using System.Windows;

    public partial class LoadModulesWindow : Window
    {
        #region Public Constructors

        public LoadModulesWindow()
        {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUIAction += View_ExecuteUIAction;
        }

        #endregion Public Constructors

        #region Public Properties

        public LoadModulesWindowView View { get { return LayoutRoot.GetView<LoadModulesWindowView>(); } }

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

        private void View_ExecuteUIAction(object sender, ExecuteUIActionEventArgs e)
        {
            switch (e.CommandToExecute)
            {
                case "ShowYesNoDialog":
                    var yesNoDialog = new TaskDialog
                    {
                        AllowDialogCancellation = false,
                        ButtonStyle = TaskDialogButtonStyle.Standard,
                        CenterParent = true,
                        MainIcon = TaskDialogIcon.Warning,
                        MainInstruction = (string)e.Parameters["message"]
                    };
                    yesNoDialog.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                    yesNoDialog.Buttons.Add(new TaskDialogButton(ButtonType.No));
                    var msgResult = yesNoDialog.ShowDialog(this).ButtonType == ButtonType.Yes;
                    e.Parameters["result"] = msgResult;
                    break;

                case "ShowFileSelectDialog":
                    var fsDialog = new VistaOpenFileDialog
                    {
                        AddExtension = false,
                        CheckFileExists = true,
                        Filter = (string)e.Parameters["filter"],
                        InitialDirectory = App.GetSetting<string>("Last.Module.Directory", Environment.CurrentDirectory),
                        Multiselect = false,
                        Title = (string)e.Parameters["title"]
                    };
                    var fsResult = fsDialog.ShowDialog(this);
                    e.Parameters["result"] = fsResult;
                    if (fsResult.GetValueOrDefault())
                        e.Parameters["filename"] = fsDialog.FileName;
                    break;
            }
        }

        private void View_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "DialogResult":
                    this.DialogResult = View.DialogResult;
                    break;
            }
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