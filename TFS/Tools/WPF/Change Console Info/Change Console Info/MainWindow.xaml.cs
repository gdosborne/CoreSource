using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialogs;
using System;
using System.Windows;

namespace Greg.Osborne.ChangeConsoleInfo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowView();
            View.Initialize();

            DataContext.As<MainWindowView>().PropertyChanged += MainWindow_PropertyChanged;
            DataContext.As<MainWindowView>().ExecuteUIAction += MainWindow_ExecuteUIAction;
            Height = 300;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }

        public MainWindowView View => DataContext.As<MainWindowView>();

        private void MainWindow_ExecuteUIAction(object sender, GregOsborne.MVVMFramework.ExecuteUiActionEventArgs e)
        {
            switch (e.CommandToExecute)
            {
                case "Save Settings":
                    var td = new TaskDialog
                    {
                        AllowDialogCancellation = false,
                        ButtonStyle = TaskDialogButtonStyle.CommandLinks,
                        WindowTitle = (string)e.Parameters["WindowTitle"],
                        MainInstruction = (string)e.Parameters["WindowTitle"],
                        Content = (string)e.Parameters["Message"],
                        MainIcon = (TaskDialogIcon)e.Parameters["Icon"],
                        MinimizeBox = false,
                        CenterParent = true,
                    };
                    if (e.Parameters.ContainsKey("Button1Text"))
                    {
                        var button = new TaskDialogButton((string)e.Parameters["Button1Text"]);
                        if (e.Parameters.ContainsKey("Button1NoteText"))
                            button.CommandLinkNote = (string)e.Parameters["Button1NoteText"];
                        td.Buttons.Add(button);
                    }
                    if (e.Parameters.ContainsKey("Button2Text"))
                    {
                        var button = new TaskDialogButton((string)e.Parameters["Button2Text"]);
                        if (e.Parameters.ContainsKey("Button2NoteText"))
                            button.CommandLinkNote = (string)e.Parameters["Button2NoteText"];
                        td.Buttons.Add(button);
                    }
                    if (e.Parameters.ContainsKey("Button3Text"))
                    {
                        var button = new TaskDialogButton((string)e.Parameters["Button3Text"]);
                        if (e.Parameters.ContainsKey("Button3NoteText"))
                            button.CommandLinkNote = (string)e.Parameters["Button3NoteText"];
                        td.Buttons.Add(button);
                    }
                    var result = td.ShowDialog(this);
                    if (result.Text.Equals("No"))
                        e.Parameters["Cancel"] = true;
                    break;
            }
        }

        private void MainWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
    }
}