using GregOsborne.MVVMFramework;
using GregOsborne.Application.Media;
using InnoData;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using GregOsborne.Application.Primitives;
using GregOsborne.Application.Theme;
using AppFramework.Theme;
using Castle.Core.Smtp;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BuildInno {
    public partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            BuildInnoProjects = [];
            OpenedProjects = [];
            Title = "Build Inno Setup [designer]";
        }

        private Window window = default;
        public void Initialize(Window window) {
            base.Initialize();
            this.window = window;
            Title = $"{App.ApplicationName}";
            App.CurrentThemeChanged += (s, e) => {

            };
        }

        #region BuildInnoProjects Property
        private ObservableCollection<BuildInnoProject> _BuildInnoProjects = default;
        public ObservableCollection<BuildInnoProject> BuildInnoProjects {
            get => _BuildInnoProjects;
            set {
                _BuildInnoProjects = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region OpenedProjects Property
        private ObservableCollection<BuildInnoProject> _OpenedProjects = default;
        public ObservableCollection<BuildInnoProject> OpenedProjects {
            get => _OpenedProjects;
            set {
                _OpenedProjects = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedProject Property
        private BuildInnoProject _SelectedProject = default;
        public BuildInnoProject SelectedProject {
            get => _SelectedProject;
            set {
                if (SelectedProject != null) {
                    SelectedProject.PropertyChanged -= (s, e) => { };
                }
                _SelectedProject = value;
                if (SelectedProject != null) {
                    SelectedProject.PropertyChanged += (s, e) => {
                        UpdateInterface();
                    };
                    Title = $"{App.ApplicationName} - {SelectedProject.Filename}";
                    if (!OpenedProjects.Contains(SelectedProject)) {
                        OpenedProjects.Add(SelectedProject);
                    }
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region RichDocument Property
        private FlowDocument _RichDocument = default;
        public FlowDocument RichDocument {
            get => _RichDocument;
            set {
                _RichDocument = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public bool IsTextSelected {
            get {
                var p = new Dictionary<string, object> {
                    { "IsSelected", false }
                };
                ExecuteAction(nameof(Actions.IsTextSelected), p);
                return p["IsSelected"].CastTo<bool>();
            }
        }
    }
}
