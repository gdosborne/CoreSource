﻿using GregOsborne.Application.Linq;
using GregOsborne.MVVMFramework;
using System.Collections.ObjectModel;
using System.Windows.Media;
using VersionMaster;

namespace ManageVersioning {
    public partial class MainWindowView : ViewModelBase {
        public enum OriginalActions {
            Delete,
            Cut,
            Copy,
            Insert,
            ChangeValue
        }

        public MainWindowView() {
            defaultForeground = App.GetResourceItem<SolidColorBrush>(App.Current.Resources, "WindowText");
            originalValues = ProjectData.LoadAll(App.DataFile, defaultForeground).ToList();
            Title = "Manage Versions [designer]";
            Projects = [];
            Schemas = [];
            Methods = [];
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Manage Versions";
            DataFilePath = App.DataFile;
            Schemas.Clear();
            Projects.AddRange(ProjectData.LoadAll(App.DataFile, defaultForeground));
            Schemas.AddRange(ProjectData.LoadAllSchemas(App.DataFile));
            Methods.AddRange(ProjectData.LoadAllMethods(App.DataFile));
        }

        private Brush defaultForeground = default;
        private List<ProjectData> originalValues = default;

        public bool HasChanges => Projects.Any(x => x.HasChanges) || Schemas.Any(x => x.HasChanges) || HasDeletedItems;
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

    }
}
