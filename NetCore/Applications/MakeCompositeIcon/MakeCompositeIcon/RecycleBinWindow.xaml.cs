using Common.Media;
using Common.Primitives;
using Common.Windows;
using Common.Windows.Controls;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MakeCompositeIcon {
    public partial class RecycleBinWindow : Window {
        public RecycleBinWindow() {
            InitializeComponent();
            View.PropertyChanged += View_PropertyChanged;
            View.ExecuteUiAction += View_ExecuteUiAction;
            View.Initialize();
        }

        private void View_ExecuteUiAction(object sender, Common.MVVMFramework.ExecuteUiActionEventArgs e) {
            if (Enum.TryParse(typeof(RecycleBinWindowView.Actions), e.CommandToExecute, out var action)) {
                var act = (RecycleBinWindowView.Actions)action;
                switch (act) {
                    case RecycleBinWindowView.Actions.ClearRecycleBin: {
                            App.ClearRecycleBin();
                            View.UpdateInterface();
                            break;
                        }
                    case RecycleBinWindowView.Actions.RestoreSelected: {
                            var td = new TaskDialog {
                                MainIcon = TaskDialogIcon.Shield,
                                MainInstruction = $"Restore recycle bin items?",
                                AllowDialogCancellation = true,
                                ButtonStyle = TaskDialogButtonStyle.Standard,
                                Content = $"You are about to restore {View.SelectedIcons.Count} icons(s) " +
                                    $"from the recycle bin.\n\nRestore icons?",
                                CenterParent = true,
                                MinimizeBox = false,
                                WindowTitle = "Restore icon(s)..."
                            };
                            View.ItemsForOtherFiles ??= new List<string>();
                            td.Buttons.Add(new TaskDialogButton(ButtonType.Yes));
                            td.Buttons.Add(new TaskDialogButton(ButtonType.No));
                            var result = td.ShowDialog(this);
                            iconsToRestore ??= new List<RecycledCompositeIcon>(View.SelectedIcons);

                            iconsToRestore.ForEach(icon => {
                                var restoreFilename = icon.FullPath;
                                var currentFilename = icon.RecycleBinFilename;

                                try {
                                    File.Move(currentFilename, restoreFilename, true);
                                    View.Icons.Remove(icon);
                                    if (!Path.GetDirectoryName(restoreFilename).Equals(App.ThisApp.FilesDirectory, StringComparison.OrdinalIgnoreCase)) {
                                        View.ItemsForOtherFiles.Add(restoreFilename);
                                    }
                                }
                                catch (Exception e) { App.HandleException(e); }
                            });
                            break;
                        }
                }
            }
        }

        private List<RecycledCompositeIcon> iconsToRestore { get; set; }

        private void View_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == "DialogResult") {
                DialogResult = View.DialogResult;
            }
        }

        internal RecycleBinWindowView View => DataContext.As<RecycleBinWindowView>();

        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            this.HideMinimizeAndMaximizeButtons();
            MainToolbar.RemoveOverflow();

            if (App.ThisApp.IsUseLastPositionChecked) {
                Left = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(RecycleBinWindow), "Left", double.IsInfinity(Left) ? 0 : Left);
                Top = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(RecycleBinWindow), "Top", double.IsInfinity(Top) ? 0 : Top);
                Width = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(RecycleBinWindow), "Width", Width);
                Height = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(RecycleBinWindow), "Height", Height);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(RecycleBinWindow), "Left", RestoreBounds.Left);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(RecycleBinWindow), "Top", RestoreBounds.Top);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(RecycleBinWindow), "Width", RestoreBounds.Width);
            App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(RecycleBinWindow), "Height", RestoreBounds.Height);
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            foreach (RecycledCompositeIcon item in e.RemovedItems) {
                if (View.SelectedIcons.Contains(item))
                    View.SelectedIcons.Remove(item);
            }
            foreach (RecycledCompositeIcon item in e.AddedItems) {
                View.SelectedIcons.Add(item);
            }
            View.UpdateInterface();
        }
    }
}
