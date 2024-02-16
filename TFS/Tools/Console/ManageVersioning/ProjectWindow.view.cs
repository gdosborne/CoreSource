using GregOsborne.MVVMFramework;
using System.Collections.ObjectModel;
using System.Windows;
using VersionMaster;
using GregOsborne.Application.Linq;

namespace ManageVersioning {
    public partial class ProjectWindowView : ViewModelBase {
        public ProjectWindowView() {
            Title = "Project [designer]";
            TitleControlVisibility = Visibility.Visible;
            EditControlVisibility = Visibility.Collapsed;
            Methods = [];
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Project";
            IsOKDefault = true;
            Methods.AddRange(ProjectData.LoadAllMethods(App.DataFile));
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Project Property
        private ProjectData _Project = default;
        public ProjectData Project {
            get => _Project;
            set {
                _Project = value;
                if (Project != null) {
                    Project.PropertyChanged += (s, e) => UpdateInterface();
                    previousSchemaName = Project.SchemaName;
                    previousProjectPath = Project.FullPath;
                }
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

        internal bool itemHasChanges = false;

        internal string previousProjectPath = default;
        internal string previousSchemaName = default;

        #region EditControlVisibility Property
        private Visibility _EditControlVisibility = default;
        public Visibility EditControlVisibility {
            get => _EditControlVisibility;
            set {
                _EditControlVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region TitleControlVisibility Property
        private Visibility _TitleControlVisibility = default;
        public Visibility TitleControlVisibility {
            get => _TitleControlVisibility;
            set {
                _TitleControlVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsOKDefault Property
        private bool _IsOKDefault = default;
        public bool IsOKDefault {
            get => _IsOKDefault;
            set {
                _IsOKDefault = value;
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

    }
}
