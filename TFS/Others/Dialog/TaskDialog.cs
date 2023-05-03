namespace GregOsborne.Dialog {
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public enum AdditionalControlPositions {
        BeforeAdditionalInformation,
        AfterAdditionalInformation,
        ButtonArea
    }
    public enum ButtonTypes {
        None,
        Yes,
        No,
        Cancel,
        OK,
        Custom
    }
    public enum ImagesTypes {
        Information,
        Question,
        Warning,
        Error,
        Custom
    }
    internal struct Button {
        public int CustomValue;
        public string Text;
        public ButtonTypes Type;
        public double Width;
        public Button(ButtonTypes type, string text, int customValue = -1, double width = 0) {
            this.Type = type;
            this.Text = text;
            this.CustomValue = customValue;
            this.Width = width;
        }
    }

    public sealed class TaskDialog {
        public TaskDialog() {
            Buttons = new List<Button>();
            Title = "Message Dialog";
            Width = 0;
            Height = 0;
            Image = ImagesTypes.Information;
            AdditionalControlPosition = AdditionalControlPositions.ButtonArea;
            DialogResult = (int)ButtonTypes.Cancel;
        }

        public FrameworkElement AdditionalControl {
            get; set;
        }

        public AdditionalControlPositions AdditionalControlPosition {
            get; set;
        }

        public string AdditionalInformation {
            get; set;
        }

        public bool AllowClose {
            get; set;
        }

        public ImageSource CustomImage {
            get; set;
        }

        public int DialogResult {
            get; private set;
        }

        public double Height {
            get; set;
        }

        public ImagesTypes Image {
            get; set;
        }

        public bool IsAdditionalInformationExpanded {
            get; set;
        }

        public string MessageText {
            get; set;
        }

        public string Title {
            get; set;
        }

        public double Width {
            get; set;
        }

        internal IList<Button> Buttons {
            get; set;
        }

        public void AddButton(ButtonTypes type) => Buttons.Add(new Button(type, type.ToString(), (int)type));

        public void AddButton(string text, int value, double width = -1) => Buttons.Add(new Button(ButtonTypes.Custom, text, value, width));

        public void AddButtons(params ButtonTypes[] types) => types.ToList().ForEach(x => AddButton(x));

        public void Show() {
            //var dlg = new MessageDialog {
            //    WindowStartupLocation = WindowStartupLocation.CenterScreen,
            //    AllowClose = AllowClose
            //};
            //Show(dlg);
        }

        public void Show(double left, double top) {
            //var dlg = new MessageDialog {
            //    Left = left,
            //    Top = top,
            //    WindowStartupLocation = WindowStartupLocation.Manual,
            //    AllowClose = AllowClose
            //};
            //Show(dlg);
        }

        public new int ShowDialog(Window owner) {
            //var dlg = new MessageDialog {
            //    WindowStartupLocation = owner != null && owner.IsLoaded ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen,
            //    Owner = owner != null && owner.IsLoaded ? owner : null,
            //    AllowClose = AllowClose
            //};
            //SetOther(dlg);
            //dlg.ShowDialog();
            //DialogResult = dlg.View.ButtonValue;
            //return DialogResult;
            return 0;
        }

        public int ShowDialog(Window owner, double left, double top) {
            //var dlg = new MessageDialog {
            //    Left = left,
            //    Top = top,
            //    WindowStartupLocation = WindowStartupLocation.Manual,
            //    Owner = owner != null && owner.IsLoaded ? owner : null,
            //    AllowClose = AllowClose
            //};
            //SetOther(dlg);
            //dlg.ShowDialog();
            //DialogResult = dlg.View.ButtonValue;
            //return DialogResult;
            return 0;
        }

        internal static ImageSource GetImageSourceByName(string resourceName) => Assembly.GetExecutingAssembly().GetImageSourceByName("Resources/" + resourceName);

        internal static ImageSource GetImageSourceByType(GregOsborne.Dialog.ImagesTypes type) => GetImageSourceByName(type + ".png");

        private void SetOther(MessageDialog dlg) {
            if (AdditionalControl != null) {
                if (AdditionalControlPosition == AdditionalControlPositions.BeforeAdditionalInformation) {
                    AdditionalControl.SetValue(Grid.RowProperty, 1);
                } else if (AdditionalControlPosition == AdditionalControlPositions.AfterAdditionalInformation) {
                    AdditionalControl.SetValue(Grid.RowProperty, 3);
                } else {
                    AdditionalControl.SetValue(Grid.RowProperty, 4);
                }
                AdditionalControl.SetValue(Grid.ColumnSpanProperty, 2);
                dlg.AdditionalBorder.Child = AdditionalControl;
            }
            if (Width == 0 && Height == 0) {
                dlg.SizeToContent = SizeToContent.WidthAndHeight;
                dlg.MaxWidth = Screen.PrimaryScreenWidth / 2;
                dlg.MaxHeight = Screen.PrimaryScreenHeight / 2;
            } else if (Width == 0) {
                dlg.SizeToContent = SizeToContent.Width;
                dlg.Height = Height;
            } else if (Height == 0) {
                dlg.SizeToContent = SizeToContent.Height;
                dlg.Width = Width;
            } else {
                dlg.SizeToContent = SizeToContent.Manual;
                dlg.Width = Width;
                dlg.Height = Height;
            }
            for (var i = 0; i < Buttons.Count; i++) {
                switch (i) {
                    case 0:
                        dlg.View.Button1 = Buttons[i];
                        break;
                    case 1:
                        dlg.View.Button2 = Buttons[i];
                        break;
                    case 2:
                        dlg.View.Button3 = Buttons[i];
                        break;
                }
            }
            dlg.View.IsAdditionalInformationExpanded = IsAdditionalInformationExpanded;
            if (Image != ImagesTypes.Custom) {
                dlg.View.Source = GetImageSourceByType(Image);
            } else {
                dlg.View.Source = CustomImage;
            }

            dlg.View.AdditionalInformation = AdditionalInformation;
            dlg.View.MessageText = MessageText;
            dlg.Title = Title;
        }

        private void Show(MessageDialog dlg) {
            SetOther(dlg);
            dlg.Show();
        }
    }
}
