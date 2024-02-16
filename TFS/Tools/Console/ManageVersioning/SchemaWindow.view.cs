using EnvDTE;
using System.Collections.ObjectModel;
using System.Windows;
using VersionMaster;

namespace ManageVersioning {
    public partial class SchemaWindowView : ViewModelBase {
        public SchemaWindowView() {
            Title = "Schema [designer]";
            TitleControlVisibility = Visibility.Visible;
            EditControlVisibility = Visibility.Collapsed;
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Schema";
        }

        internal bool itemHasChanges = false;
        internal string previousName = default;
        internal Enumerations.TransformTypes previousMajorMethod = default;
        internal Enumerations.TransformTypes previousMinorMethod = default;
        internal Enumerations.TransformTypes previousBuildMethod = default;
        internal Enumerations.TransformTypes previousRevisionMethod = default;
        internal string previousMajorParameter = default;
        internal string previousMinorParameter = default;
        internal string previousBuildParameter = default;
        internal string previousRevisionParameter = default;


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

        #region Schema Property
        private SchemaItem _Schema = default;
        public SchemaItem Schema {
            get => _Schema;
            set {
                _Schema = value;
                if (Schema != null) {
                    previousName = Schema.Name;

                    previousMajorMethod = Schema.MajorPart;
                    previousMinorMethod = Schema.MinorPart;
                    previousBuildMethod = Schema.BuildPart;
                    previousRevisionMethod = Schema.RevisionPart;

                    previousMajorParameter = Schema.MajorParameter;
                    previousMinorParameter = Schema.MinorParameter;
                    previousBuildParameter = Schema.BuildParameter;
                    previousRevisionParameter = Schema.RevisionParameter;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region IsDelete Property
        private bool _IsDelete = default;
        public bool IsDelete {
            get => _IsDelete;
            set {
                _IsDelete = value;
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

        #region DeleteVisibility Property
        private Visibility _DeleteVisibility = default;
        public Visibility DeleteVisibility {
            get => _DeleteVisibility;
            set {
                _DeleteVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}
