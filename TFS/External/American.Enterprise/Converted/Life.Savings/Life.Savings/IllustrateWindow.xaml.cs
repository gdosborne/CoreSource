using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using GregOsborne.Application;
using GregOsborne.Application.Logging;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Windows;
using GregOsborne.Dialog;
using Life.Savings.Extensions;
using Ookii.Dialogs.Wpf;

namespace Life.Savings {
    public partial class IllustrateWindow : Window {
        private readonly bool _isInitializing;
        public IllustrateWindow() {
            InitializeComponent();
            Logger.LogMessage($"Loading illustration window.");
            _isInitializing = true;

            Loaded += IllustrateWindow_Loaded;
            SizeChanged += IllustrateWindow_SizeChanged;
            LocationChanged += IllustrateWindow_LocationChanged;

            var rect = App.GetWindowBounds("IllustrateWindow", 825, 500);

            this.Position(rect.Left, rect.Top);
            //Width = rect.Width;
            Height = rect.Height;

            DataContext.As<IllustrateWindowView>().PixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            _isInitializing = false;
        }

        private void IllustrateWindow_LocationChanged(object sender, EventArgs e) {
            App.SavePosition(_isInitializing, "IllustrateWindow", this);
        }

        private void IllustrateWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
            App.SavePosition(_isInitializing, "IllustrateWindow", this, true);
        }

        private void IllustrateWindow_Loaded(object sender, RoutedEventArgs e) {

        }
        protected override void OnSourceInitialized(EventArgs e) {
            this.HideMinimizeAndMaximizeButtons();
        }

        private DispatcherTimer selectTimer = null;
        private IList<string> propertyNames = null;
        private void IllustrateWindowView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("DialogResult"))
                DialogResult = DataContext.As<IllustrateWindowView>().DialogResult;
            else if (e.PropertyName.Equals("IsFirst25Checked") || e.PropertyName.Equals("IsFirst10Checked"))
            {
                if (selectTimer == null)
                {
                    propertyNames = new List<string>();
                    selectTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
                    selectTimer.Tick += SelectTimer_Tick;
                }
                propertyNames.Add(e.PropertyName);
                selectTimer.Start();
            }
        }

        private void SelectTimer_Tick(object sender, EventArgs e) {
            selectTimer.Stop();
            //this is necessary because we get a property change notification for 
            // turning one off and then the other on. The off is first, but the
            // propertychanged for the on is not triggered because of the speed at 
            // which these are set so we store them and run through them all
            propertyNames.ToList().ForEach(x => {
                if (x.Equals("IsFirst25Checked") && DataContext.As<IllustrateWindowView>().IsFirst25Checked)
                    First25FirstBox.Focus();
                else if (x.Equals("IsFirst10Checked") && DataContext.As<IllustrateWindowView>().IsFirst10Checked)
                    First10FirstBox.Focus();
            });
            propertyNames.Clear();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            sender.As<TextBox>().SelectAll();
        }

        private void IllustrateWindowView_GetIllustrationSaveParameters(object sender, Events.GetIllustrationSaveParametersEventArgs e) {
            var sfd = new VistaSaveFileDialog {
                AddExtension = true,
                DefaultExt = ".xps",
                CheckFileExists = false,
                CheckPathExists = true,
                CreatePrompt = false,
                Filter = "XPS Files|*.xps|PDF Files|*.pdf",
                InitialDirectory = Settings.GetSetting(App.ApplicationName, "Application", "IllustrationSaveLocation", string.Empty),
                OverwritePrompt = true,
                Title = "Save illustration..."
            };
            var result = sfd.ShowDialog(this);
            if (!result.HasValue || !result.Value)
            {
                e.IsCancel = true;
                return;
            }
            e.FileType = new FileInfo(sfd.FileName).Extension.EndsWith("xps", StringComparison.OrdinalIgnoreCase) ? Events.SaveFileTypes.Xps : Events.SaveFileTypes.Pdf;
            e.Document = IllustrationDocument;
            e.IsCancel = false;
            e.FileName = sfd.FileName;
        }

        private void IllustrateWindowView_GetIllustrationSaveParameters(object sender, object e) {

        }

        private void IllustrateWindowView_SetCursor(object sender, Events.SetCursorEventArgs e) {
            Cursor = e.Cursor;
        }

        private void IllustrateWindowView_ShowMessage(object sender, Events.ShowMessageEventArgs e) {
            var td = new GregOsborne.Dialog.TaskDialog {
                Image = e.ImageType,
                MessageText = e.Text,
                Title = e.Title,
                Width = e.Width,
                Height = e.Height
            };
            td.AddButtons(ButtonTypes.OK);
            e.Result = (ButtonTypes)td.ShowDialog(this);
        }

        private void IllustrateWindowView_CompleteIllustration(object sender, EventArgs e) {
            var pageNum = 1;
            foreach (var page in DataContext.As<IllustrateWindowView>().Pages)
            {
                var s = new Section();
                s.BreakPageBefore = pageNum > 1;
                var p = new Paragraph(new Run(page));
                s.Blocks.Add(p);
                IllustrationDocument.Blocks.Add(s);
                    pageNum++;
            }
        }

        private void IllustrateWindowView_PrintFlowDocument(object sender, EventArgs e) {
            var pw = IllustrationDocument.PageWidth;
            var ph = IllustrationDocument.PageHeight;
            var pa = IllustrationDocument.PagePadding;
            var cg = IllustrationDocument.ColumnGap;
            var cw = IllustrationDocument.ColumnWidth;

            PrintDialog pd = new PrintDialog();
            IllustrationDocument.PageHeight = pd.PrintableAreaHeight;
            IllustrationDocument.PageWidth = pd.PrintableAreaWidth;
            IllustrationDocument.PagePadding = new Thickness(50);
            IllustrationDocument.ColumnGap = 0;
            IllustrationDocument.ColumnWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource dps = IllustrationDocument;
            pd.PrintDocument(dps.DocumentPaginator, "flow doc");

            IllustrationDocument.PageHeight = ph;
            IllustrationDocument.PageWidth = pw;
            IllustrationDocument.PagePadding = pa;
            IllustrationDocument.ColumnGap = cg;
            IllustrationDocument.ColumnWidth = cw;
        }
    }
}
