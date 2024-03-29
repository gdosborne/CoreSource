namespace Imaginator.Views
{
    using GregOsborne.Application.Media;
    using GregOsborne.Application.Primitives;
    using Imaginator.Data;
    using MVVMFramework;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Threading;

    public class MainWindowView : INotifyPropertyChanged
    {
        private DispatcherTimer clipboardTimer = null;
        public MainWindowView()
        {
            ProgressMinimum = 0;
            ProgressMaximum = 100;
            ProgressValue = 0;
            ErrorVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Collapsed;
            LastImageOpenFolder = App.GetSetting<string>("App.Last Images OpenFolder", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
            this.LoadComplete += MainWindowView_LoadComplete;
            clipboardTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
            clipboardTimer.Tick += (object sender, EventArgs e) =>
            {
                PasteImageCommand.RaiseCanExecuteChanged();
            };
            clipboardTimer.Start();
        }

        private DelegateCommand _PasteImageCommand = null;
        public DelegateCommand PasteImageCommand
        {
            get
            {
                if (_PasteImageCommand == null)
                    _PasteImageCommand = new DelegateCommand(PasteImage, ValidatePasteImageState);
                return _PasteImageCommand as DelegateCommand;
            }
        }
        private void PasteImage(object state)
        {
            if (ExecuteUIAction != null)
            {
                var p = new Dictionary<string, object> { 
                    { "result", false }, 
                    { "filename", null }, 
                    { "sizes", null }, 
                    { "tempfilename", null } 
                };
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowPasteDialog", p));
                if (!((bool?)p["result"]).GetValueOrDefault())
                    return;
                var sizes = (int[])p["sizes"];
                var filename = (string)p["tempfilename"];
                var oldSelectedItem = SelectedItem;
                SelectedItem = new FileItem { Name = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".png", FullPath = filename };
                sizes.ToList().ForEach(x =>
                {                    
                    MakeScaledImage(x, true);
                });
                SelectedItem = oldSelectedItem;
            }
        }
        private bool ValidatePasteImageState(object state)
        {
            return System.Windows.Clipboard.GetData(System.Windows.DataFormats.Bitmap) != null;
        }

        private DelegateCommand _OpenImageFileCommand = null;
        public DelegateCommand OpenImageFileCommand
        {
            get
            {
                if (_OpenImageFileCommand == null)
                    _OpenImageFileCommand = new DelegateCommand(OpenImageFile, ValidateOpenImageFileState);
                return _OpenImageFileCommand as DelegateCommand;
            }
        }
        private DelegateCommand _ConvertAllFilesCommand = null;
        public DelegateCommand ConvertAllFilesCommand
        {
            get
            {
                if (_ConvertAllFilesCommand == null)
                    _ConvertAllFilesCommand = new DelegateCommand(ConvertAllFiles, ValidateConvertAllFilesState);
                return _ConvertAllFilesCommand as DelegateCommand;
            }
        }
        private void ConvertAllFiles(object state)
        {
            var items = SelectedDirectory.Files.ToList();
            Task.Factory.StartNew(() => StartAllConversion(items));
        }
        private bool ValidateConvertAllFilesState(object state)
        {
            return true;
        }

        private void OpenImageFile(object state)
        {
            if (ExecuteUIAction != null)
            {
                var p = new Dictionary<string, object> { 
                    { "result", false }, 
                    { "filename", null }, 
                    { "sizes", null }, 
                    { "tempfilename", null } 
                };
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowImageOpenDialog", p));
                if (!((bool?)p["result"]).GetValueOrDefault())
                    return;
                LastImageOpenFolder = Path.GetDirectoryName((string)p["filename"]);
                p["result"] = false;
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowImageImporterDialog", p));
                if (!((bool?)p["result"]).GetValueOrDefault())
                    return;
                var sizes = (int[])p["sizes"];
                var filename = (string)p["tempfilename"];
                var oldSelectedItem = SelectedItem;
                sizes.ToList().ForEach(x =>
                {
                    SelectedItem = new FileItem { Name = Guid.NewGuid().ToString() + ".png", FullPath = filename };
                    MakeScaledImage(x, true);
                });
                SelectedItem = oldSelectedItem;
            }
        }
        private DelegateCommand _SettingsCommand = null;
        public DelegateCommand SettingsCommand {
            get {
                if (_SettingsCommand == null)
                    _SettingsCommand = new DelegateCommand(Settings, ValidateSettingsState);
                return _SettingsCommand as DelegateCommand;
            }
        }
        private void Settings(object state)
        {
            if (ExecuteUIAction != null)
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ShowSettingsDialog"));
        }
        private bool ValidateSettingsState(object state)
        {
            return true;
        }

        private string _LastImageOpenFolder;
        public string LastImageOpenFolder
        {
            get { return _LastImageOpenFolder; }
            set
            {
                _LastImageOpenFolder = value;
                App.SetSetting<string>("App.Last Images OpenFolder", value != null ? value : string.Empty);
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool ValidateOpenImageFileState(object state)
        {
            return true;
        }

        void MainWindowView_LoadComplete(object sender, EventArgs e)
        {
            LoadingVisibility = Visibility.Collapsed;
        }
        private Visibility _LoadingVisibility;
        public Visibility LoadingVisibility
        {
            get { return _LoadingVisibility; }
            set
            {
                _LoadingVisibility = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private DelegateCommand _ConvertCommand = null;
        public DelegateCommand ConvertCommand
        {
            get
            {
                if (_ConvertCommand == null)
                    _ConvertCommand = new DelegateCommand(Convert, ValidateConvertState);
                return _ConvertCommand as DelegateCommand;
            }
        }
        public void PerformConvertAll()
        {
            Convert("All");
        }
        private void MakeScaledImage(int size, bool ignoreSubDir = false)
        {
            var outputDir = System.IO.Path.Combine(RootFolder, string.Format("Toolbar {0}", size));
            if (!System.IO.Directory.Exists(outputDir))
                System.IO.Directory.CreateDirectory(outputDir);
            if (!ignoreSubDir)
            {
                var subDir = SelectedItem.FullPath.Replace(RootFolder, string.Empty);
                var parts = subDir.TrimStart('\\').Split('\\');
                parts[0] = string.Empty;
                subDir = string.Join("\\", parts);
                subDir = System.IO.Path.GetDirectoryName(subDir).TrimStart('\\');
                outputDir = System.IO.Path.Combine(outputDir, subDir);
                if (!System.IO.Directory.Exists(outputDir))
                    System.IO.Directory.CreateDirectory(outputDir);
            }
            var outputFileName = System.IO.Path.Combine(outputDir, SelectedItem.Name);
            SelectedItem.FullPath.GetThumbnailImage(size, size, outputFileName, System.Drawing.Imaging.ImageFormat.Png, true);
            ProgressMaximum++;
        }
        private void UpdateExtensions()
        {
            if (Directories == null)
                return;
            foreach (var item in Directories)
            {
                item.Extensions = GetExtensionFlags();
            }
        }
        private bool _UsePng;
        public bool UsePng
        {
            get { return _UsePng; }
            set
            {
                _UsePng = value;
                App.SetSetting<bool>("Extension.Png", UsePng);
                UpdateExtensions();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _UseJpg;
        public bool UseJpg
        {
            get { return _UseJpg; }
            set
            {
                _UseJpg = value;
                App.SetSetting<bool>("Extension.Jpg", UseJpg);
                UpdateExtensions();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _UseBmp;
        public bool UseBmp
        {
            get { return _UseBmp; }
            set
            {
                _UseBmp = value;
                App.SetSetting<bool>("Extension.Bmp", UseBmp);
                UpdateExtensions();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private bool _UseIco;
        public bool UseIco
        {
            get { return _UseIco; }
            set
            {
                _UseIco = value;
                App.SetSetting<bool>("Extension.Ico", UseIco);
                UpdateExtensions();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public delegate void ConversionHandler(double value);
        public event ConversionHandler BeginAllConversion;
        public event ConversionHandler UpdateProgress;
        private void StartAllConversion(List<FileItem> items)
        {
            if (BeginAllConversion != null)
                BeginAllConversion(items.Count);
            var count = 0;
            foreach (var fileItem in items)
            {
                count++;
                if (UpdateProgress != null)
                    UpdateProgress(count);
                SelectedItem = fileItem;
                Convert("All");
            }
        }
        private double _AllProgressMinimum;
        public double AllProgressMinimum
        {
            get { return _AllProgressMinimum; }
            set
            {
                _AllProgressMinimum = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _AllProgressMaximum;
        public double AllProgressMaximum
        {
            get { return _AllProgressMaximum; }
            set
            {
                _AllProgressMaximum = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _AllProgressValue;
        public double AllProgressValue
        {
            get { return _AllProgressValue; }
            set
            {
                _AllProgressValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private void StartIndividualConversion(FileItem item)
        {
            Convert("All");
        }
        private void Convert(object state)
        {
            ProgressValue = 0;
            ErrorVisibility = Visibility.Collapsed;
            try
            {
                switch ((string)state)
                {
                    case "Custom":
                        break;
                    case "All":
                        var sizes = new List<int> { 128, 64, 48, 32, 24, 16 };
                        ProgressMaximum = sizes.Count;
                        sizes.ForEach(x => Task.Factory.StartNew(() => MakeScaledImage(x)));
                        break;
                    default:
                        ProgressMaximum = 1;
                        Task.Factory.StartNew(() => MakeScaledImage(int.Parse((string)state)));
                        break;
                }
                Directories.Clear();
            }
            catch (Exception ex)
            {
                ErrorVisibility = Visibility.Visible;
                ErrorText = ex.Message;
            }
        }
        private bool ValidateConvertState(object state)
        {
            return true;
        }
        private Visibility _ErrorVisibility;
        public Visibility ErrorVisibility
        {
            get { return _ErrorVisibility; }
            set
            {
                _ErrorVisibility = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _ErrorText;
        public string ErrorText
        {
            get { return _ErrorText; }
            set
            {
                _ErrorText = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private FileItem _SelectedItem;
        public FileItem SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                if (value != null)
                {
                    var bitmapImage = value.FullPath.GetBitmapImage();
                    SelectedItemName = value.Name;
                    SelectedItemDimension = string.Format("Dimension: {0}x{1}", System.Convert.ToInt32(bitmapImage.Width), System.Convert.ToInt32(bitmapImage.Height));
                    SelectedItemSize = string.Format("Size: {0}", GregOsborne.Application.IO.File.Size(value.FullPath).Value.ToKBString<long>(true));
                }
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _SelectedItemName;
        public string SelectedItemName
        {
            get { return _SelectedItemName; }
            set
            {
                _SelectedItemName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _SelectedItemDimension;
        public string SelectedItemDimension
        {
            get { return _SelectedItemDimension; }
            set
            {
                _SelectedItemDimension = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private string _SelectedItemSize;
        public string SelectedItemSize
        {
            get { return _SelectedItemSize; }
            set
            {
                _SelectedItemSize = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private DirectoryItem _SelectedDirectory;
        public DirectoryItem SelectedDirectory
        {
            get { return _SelectedDirectory; }
            set
            {
                if (SelectedDirectory != null)
                    SelectedDirectory.MustReloadFiles -= value_MustReloadFiles;

                _SelectedDirectory = value;
                if (value == null)
                    return;
                value.MustReloadFiles += value_MustReloadFiles;
                value.LoadFiles();
                RefreshFiles();
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private void RefreshFiles()
        {
            if (ExecuteUIAction != null)
                ExecuteUIAction(this, new ExecuteUIActionEventArgs("ClearImages"));
            var dInfo = new DirectoryInfo(SelectedDirectory.FullPath);
            SelectedDirectory.Files.ToList().ForEach(x =>
            {
                if (ExecuteUIAction != null)
                    ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddImage", new Dictionary<string, object> { { "item", x } }));
            });
        }
        void value_MustReloadFiles(object sender, EventArgs e)
        {
            RefreshFiles();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event ExecuteUIActionHandler ExecuteUIAction;
        public void UpdateInterface()
        {
            RootFolderCommand.RaiseCanExecuteChanged();
        }
        public void InitView()
        {
            UsePng = App.GetSetting<bool>("Extension.Png", true);
            UseJpg = App.GetSetting<bool>("Extension.Jpg", true);
            UseBmp = App.GetSetting<bool>("Extension.Bmp", true);
            UseIco = App.GetSetting<bool>("Extension.Ico", false);
            Directories = new ObservableCollection<DirectoryItem>();
            RootFolder = App.GetSetting<string>("Last.App.Folder", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }
        public void Initialize(Window window)
        {
            window.Left = App.GetSetting<double>("MainWindow.Left", window.Left);
            window.Top = App.GetSetting<double>("MainWindow.Top", window.Top);
            window.Width = App.GetSetting<double>("MainWindow.Width", window.Width);
            window.Height = App.GetSetting<double>("MainWindow.Height", window.Height);
            window.As<MainWindow>().TreeColumn.Width = new GridLength(App.GetSetting<double>("MainWindow.Splitter.Position", 147.0) + 3);
        }
        private ObservableCollection<DirectoryItem> _Directories;
        public ObservableCollection<DirectoryItem> Directories
        {
            get { return _Directories; }
            set
            {
                _Directories = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private DirectoryItem.ExtensionFlags GetExtensionFlags()
        {
            var result = DirectoryItem.ExtensionFlags.None;
            if (UsePng)
                result = result | DirectoryItem.ExtensionFlags.Png;
            if (UseJpg)
                result = result | DirectoryItem.ExtensionFlags.Jpg;
            if (UseBmp)
                result = result | DirectoryItem.ExtensionFlags.Bmp;
            if (UseIco)
                result = result | DirectoryItem.ExtensionFlags.Ico;
            return result;
        }
        private event EventHandler LoadComplete;
        private void RefreshRoot()
        {
            if (!Directories.Any())
            {
                if (ExecuteUIAction != null)
                    ExecuteUIAction(this, new ExecuteUIActionEventArgs("ClearTree"));
                var dInfo = new DirectoryInfo(RootFolder);
                Imaginator.Data.DirectoryItem.ExtensionFlags flags = GetExtensionFlags();
                var di = DirectoryItem.Create(RootFolder, flags);
                if (ExecuteUIAction != null)
                    ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddTreeItem", new Dictionary<string, object> { { "item", di } }));
                Directories.Add(di);
            }
            if (SelectedDirectory == null)
                SelectedDirectory = Directories.FirstOrDefault();
            var currentName = SelectedDirectory.FullPath;
            SelectedDirectory = SelectLastDirectory(currentName);
            if (LoadComplete != null)
                LoadComplete(this, EventArgs.Empty);
        }
        private DirectoryItem FindDirectoryItemByPath(List<DirectoryItem> items, string path)
        {
            var item = items.FirstOrDefault(x => x.FullPath.Equals(path, StringComparison.OrdinalIgnoreCase));
            if (item != null)
                return item;
            foreach (var x in items)
            {
                if (x.Directories.Any())
                {
                    item = FindDirectoryItemByPath(x.Directories.ToList(), path);
                    if (item != null)
                        break;
                }
            }
            return item;
        }
        private DirectoryItem SelectLastDirectory(string path)
        {
            DirectoryItem result = null;
            if (ExecuteUIAction != null)
            {
                result = FindDirectoryItemByPath(Directories.ToList(), path);
                if (result != null)
                    ExecuteUIAction(this, new ExecuteUIActionEventArgs("SelectTreeItem", new Dictionary<string, object> { { "item", result } }));
            }
            return result;
        }
        private string _RootFolder;
        public string RootFolder
        {
            get { return _RootFolder; }
            set
            {
                if (value == null || (value != null && Directory.Exists(value)))
                {
                    _RootFolder = value;
                    App.SetSetting<string>("Last.App.Folder", string.IsNullOrEmpty(value) ? string.Empty : value);
                    if (Directories == null)
                        Directories = new ObservableCollection<DirectoryItem>();
                    Directories.Clear();
                    LoadingVisibility = Visibility.Visible;
                    Task.Factory.StartNew(() => RefreshRoot());
                    UpdateInterface();
                }
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _SplitterPosition;
        public double SplitterPosition
        {
            get { return _SplitterPosition; }
            set
            {
                if (double.IsNaN(value))
                    return;
                _SplitterPosition = value;
                UpdateInterface();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        public void Persist(Window window)
        {
            App.SetSetting<double>("MainWindow.Splitter.Position", window.As<MainWindow>().ImagesTreeView.ActualWidth);
            App.SetSetting<double>("MainWindow.Left", window.RestoreBounds.Left);
            App.SetSetting<double>("MainWindow.Top", window.RestoreBounds.Top);
            App.SetSetting<double>("MainWindow.Width", window.RestoreBounds.Width);
            App.SetSetting<double>("MainWindow.Height", window.RestoreBounds.Height);
        }
        private DelegateCommand _RootFolderCommand = null;
        public DelegateCommand RootFolderCommand
        {
            get
            {
                if (_RootFolderCommand == null)
                    _RootFolderCommand = new DelegateCommand(RootFolderAction, ValidateRootFolderState);
                return _RootFolderCommand as DelegateCommand;
            }
        }
        private void RootFolderAction(object state)
        {
            var fbd = new FolderBrowserDialog
            {
                Description = "Select the root image folder...",
                RootFolder = Environment.SpecialFolder.Desktop,
                SelectedPath = RootFolder,
                ShowNewFolderButton = false
            };
            if (fbd.ShowDialog() == DialogResult.Cancel)
                return;
            RootFolder = fbd.SelectedPath;
        }
        private bool ValidateRootFolderState(object state)
        {
            return true;
        }

        private double _ProgressMinimum;
        public double ProgressMinimum
        {
            get { return _ProgressMinimum; }
            set
            {
                _ProgressMinimum = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _ProgressMaximum;
        public double ProgressMaximum
        {
            get { return _ProgressMaximum; }
            set
            {
                _ProgressMaximum = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
        private double _ProgressValue;
        public double ProgressValue
        {
            get { return _ProgressValue; }
            set
            {
                _ProgressValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
            }
        }
    }
}
