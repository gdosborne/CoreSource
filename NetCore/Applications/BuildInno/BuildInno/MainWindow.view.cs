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
                _SelectedProject = value;
                if (SelectedProject != null) {
                    Title = $"{App.ApplicationName} - {SelectedProject.Filename}";
                    if (!OpenedProjects.Contains(SelectedProject)) {
                        OpenedProjects.Add(SelectedProject);
                    }
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region EditorData Property
        private string _EditorData = default;
        public string EditorData {
            get => _EditorData;
            set {
                _EditorData = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}
