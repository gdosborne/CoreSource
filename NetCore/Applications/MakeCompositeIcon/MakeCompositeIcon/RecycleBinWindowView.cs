using ApplicationFramework.Media;
using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace MakeCompositeIcon {
    internal partial class RecycleBinWindowView : ViewModelBase {
        public RecycleBinWindowView() {
            Title = "Recycle Bin [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Recycle Bin";
            SelectedIcons = new List<RecycledCompositeIcon>();
            Icons = new ObservableCollection<RecycledCompositeIcon>();
            var dir = new DirectoryInfo(App.ThisApp.RecycleDirectory);
            if (dir.Exists) {
                dir.GetFiles("*.compo").ToList().ForEach(file => {
                    var icon = RecycledCompositeIcon.FromFileAsRecycled(file.FullName, .3);
                    if(icon != null) {                        
                        Icons.Add(icon);
                    }
                });
            }
        }

        #region DialogResult Property
        private bool _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Icons Property
        private ObservableCollection<RecycledCompositeIcon> _Icons = default;
        /// <summary>Gets/sets the Icons.</summary>
        /// <value>The Icons.</value>
        public ObservableCollection<RecycledCompositeIcon> Icons {
            get => _Icons;
            set {
                _Icons = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedIcons Property
        private List<RecycledCompositeIcon> _SelectedIcons = default;
        /// <summary>Gets/sets the SelectedIcons.</summary>
        /// <value>The SelectedIcon.</value>
        public List<RecycledCompositeIcon> SelectedIcons {
            get => _SelectedIcons;
            set {
                _SelectedIcons = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public List<string> ItemsForOtherFiles { get; set; }
    }
}
