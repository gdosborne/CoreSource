using GregOsborne.Application.Linq;
using GregOsborne.Application.Theme;
using GregOsborne.MVVMFramework;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using VersionMaster;

namespace ManageVersioning {
    public partial class MainWindowView : ViewModelBase, IThemedView {
        public enum OriginalActions {
            Delete,
            Cut,
            Copy,
            Insert,
            ChangeValue
        }

        public MainWindowView() {
            Title = "Manage Versions [designer]";
            WindowTextBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            WindowBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            ActiveCaptionBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 220));
            ActiveCaptionTextBrush = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            ControlBorderBrush = new SolidColorBrush(Color.FromArgb(255, 125, 125, 125));
            FontSize = 14.0;
            TitlebarFontSize = 18.0;
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Manage Versions";
            defaultForeground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
            originalValues = ProjectData.LoadAll(App.DataFile, defaultForeground).ToList();
            Projects = [];
            Schemas = [];
            Methods = [];
            DataFilePath = App.DataFile;
            Schemas.Clear();

            var defaultImageBrush = SysIO.Path.Combine(SysIO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Images", "4pfmphjd.png");
            if (string.IsNullOrEmpty(App.Settings.ConsoleBrushFilePath)) {
                App.Settings.ConsoleBrushFilePath = defaultImageBrush;
            }
            App.Settings.IsConsoleBackgroundBrushUsed &= SysIO.File.Exists(App.Settings.ConsoleBrushFilePath);
            ConsoleImageBrush = GregOsborne.Application.Media.Extensions.GetImageBrush(App.Settings.ConsoleBrushFilePath, ((double)App.Settings.ConsoleBrushOpacity) / 100.0);

            Projects.AddRange(ProjectData.LoadAll(App.DataFile, defaultForeground));
            Schemas.AddRange(ProjectData.LoadAllSchemas(App.DataFile));
            Methods.AddRange(ProjectData.LoadAllMethods(App.DataFile));

            window = App.Current.MainWindow;

            themeWatcher = new ThemeWatcher();
            themeWatcher.ThemeChanged += (s, e) => {
                DoThemeChange(e.Theme);
            };
            themeWatcher.WatchTheme();
            //DoThemeChange(ThemeWatcher.GetWindowsTheme());
            DoThemeChange(ThemeWatcher.WindowsTheme.Light);
        }

        private ThemeWatcher themeWatcher = default;
        private Window window = default;

        private void DoThemeChange(ThemeWatcher.WindowsTheme theme) {
            App.Current.Dispatcher.BeginInvoke(new Action(() => {
                if (theme == ThemeWatcher.WindowsTheme.Dark) {
                    Theme = App.ThemeManager.ByName("Dark");
                }
                else {
                    Theme = App.ThemeManager.ByName("Light");
                }
                Theme.Apply(window);
            }));
        }

        #region ConsoleImageBrush Property
        private ImageBrush _ConsoleImageBrush = default;
        public ImageBrush ConsoleImageBrush {
            get => _ConsoleImageBrush;
            set {
                _ConsoleImageBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private Brush defaultForeground = default;
        private List<ProjectData> originalValues = default;

        public bool HasChanges {
            get {
                if (Projects == null) return false;
                var result = Projects.Any(x => x.HasChanges) || Schemas.Any(x => x.HasChanges) || HasDeletedItems;
                return result;
            }
        }
        internal Stack<(ProjectData Item, OriginalActions Action, string PropertyName, object OriginalValue)> UndoItems = new();
        internal Stack<(ProjectData Item, OriginalActions Action, string PropertyName, object OriginalValue)> RedoItems = new();

        #region SelectedSchema Property
        private SchemaItem _SelectedSchema = default;
        public SchemaItem SelectedSchema {
            get => _SelectedSchema;
            set {
                _SelectedSchema = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasUndo Property
        private bool _HasUndo = false;
        public bool HasUndo {
            get {
                _HasUndo = UndoItems.Count > 0;
                return _HasUndo;
            }
        }
        #endregion

        #region HasRedo Property
        private bool _HasRedo = default;
        public bool HasRedo {
            get {
                _HasRedo = RedoItems.Count > 0;
                return _HasRedo;
            }
        }
        #endregion

        #region HasClipboardItem Property
        private bool _HasClipboardItem = default;
        public bool HasClipboardItem {
            get {
                _HasClipboardItem = UndoItems.Where(x => x.Action == OriginalActions.Copy || x.Action == OriginalActions.Cut).Count() > 0;
                return _HasClipboardItem;
            }
        }
        #endregion

        #region Projects Property
        private ObservableCollection<ProjectData> _Projects = default;
        public ObservableCollection<ProjectData> Projects {
            get => _Projects;
            set {
                _Projects = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods Property
        private ObservableCollection<Enumerations.TransformTypes> _Methods = default;
        public ObservableCollection<Enumerations.TransformTypes> Methods {
            get => _Methods;
            set {
                _Methods = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Schemas Property
        private ObservableCollection<SchemaItem> _Schemas = default;
        public ObservableCollection<SchemaItem> Schemas {
            get => _Schemas;
            set {
                _Schemas = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedProject Property
        private ProjectData _SelectedProject = default;
        public ProjectData SelectedProject {
            get => _SelectedProject;
            set {
                _SelectedProject = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DataFilePath Property
        private string _DataFilePath = default;
        public string DataFilePath {
            get => _DataFilePath;
            set {
                _DataFilePath = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConsoleText Property
        private string _ConsoleText = default;
        public string ConsoleText {
            get => _ConsoleText;
            set {
                _ConsoleText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region HasDeletedItems Property
        private bool _HasDeletedItems = default;
        public bool HasDeletedItems {
            get => _HasDeletedItems;
            set {
                _HasDeletedItems = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Theme Property
        private ApplicationTheme _Theme = default;
        public ApplicationTheme Theme {
            get => _Theme;
            set {
                _Theme = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TitlebarFontSize Property
        private double _TitlebarFontSize = default;
        public double TitlebarFontSize {
            get => _TitlebarFontSize;
            set {
                _TitlebarFontSize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontSize Property
        private double _FontSize = default;
        public double FontSize {
            get => _FontSize;
            set {
                _FontSize = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ActiveCaptionBrush Property
        private SolidColorBrush _ActiveCaptionBrush = default;
        public SolidColorBrush ActiveCaptionBrush {
            get => _ActiveCaptionBrush;
            set {
                _ActiveCaptionBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ActiveCaptionTextBrush Property
        private SolidColorBrush _ActiveCaptionTextBrush = default;
        public SolidColorBrush ActiveCaptionTextBrush {
            get => _ActiveCaptionTextBrush;
            set {
                _ActiveCaptionTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BorderBrush Property
        private SolidColorBrush _BorderBrush = default;
        public SolidColorBrush BorderBrush {
            get => _BorderBrush;
            set {
                _BorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ControlBorderBrush Property
        private SolidColorBrush _ControlBorderBrush = default;
        public SolidColorBrush ControlBorderBrush {
            get => _ControlBorderBrush;
            set {
                _ControlBorderBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region WindowBrush Property
        private SolidColorBrush _WindowBrush = default;
        public SolidColorBrush WindowBrush {
            get => _WindowBrush;
            set {
                _WindowBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region WindowTextBrush Property
        private SolidColorBrush _WindowTextBrush = default;
        public SolidColorBrush WindowTextBrush {
            get => _WindowTextBrush;
            set {
                _WindowTextBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public void ApplyVisualElement<T>(VisualElement<T> element) {
            switch (element.Name) {
                case nameof(ActiveCaptionBrush): ActiveCaptionBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(ActiveCaptionTextBrush): ActiveCaptionTextBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(BorderBrush): BorderBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(ControlBorderBrush): ControlBorderBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(WindowBrush): WindowBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(WindowTextBrush): WindowTextBrush = element.Value.As<SolidColorBrush>(); break;
                case nameof(FontSize): FontSize = (double)(object)element.Value; break;
            }
        }
    }
}
