using ApplicationFramework.Media;
using Common.MVVMFramework;
using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MakeCompositeIcon {
    internal partial class OptionsWindowView : ViewModelBase {
        public OptionsWindowView() {
            Title = "Options [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Options";
            IsUseLastPositionChecked = App.ThisApp.IsUseLastPositionChecked;
            AreGuidesShown = App.ThisApp.AreGuidesShown;
            FontSizes = new ObservableCollection<double>();
            for (int i = 10; i < 40; i++) {
                FontSizes.Add(i);
            }
            SelectedFontSize = (double)App.ThisApp.Resources["RootFontSize"];
        }

        #region DialogResult Property
        private bool? _DialogResult = default;
        /// <summary>Gets/sets the DialogResult.</summary>
        /// <value>The DialogResult.</value>
        public bool? DialogResult {
            get => _DialogResult;
            set {
                _DialogResult = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsUseLastPositionChecked Property
        private bool _IsUseLastPositionChecked = default;
        /// <summary>Gets/sets the IsUseLastPositionChecked.</summary>
        /// <value>The IsUseLastPositionChecked.</value>
        public bool IsUseLastPositionChecked {
            get => _IsUseLastPositionChecked;
            set {
                _IsUseLastPositionChecked = value;
                App.ThisApp.IsUseLastPositionChecked = IsUseLastPositionChecked;
                OnPropertyChanged();
            }
        }
        #endregion

        #region AreGuidesShown Property
        private bool _AreGuidesShown = default;
        /// <summary>Gets/sets the AreGuidesShown.</summary>
        /// <value>The AreGuidesShown.</value>
        public bool AreGuidesShown {
            get => _AreGuidesShown;
            set {
                _AreGuidesShown = value;
                App.ThisApp.AreGuidesShown = AreGuidesShown;
                OnPropertyChanged();
            }
        }
        #endregion

        #region FontSizes Property
        private ObservableCollection<double> _FontSizes = default;
        /// <summary>Gets/sets the FontSizes.</summary>
        /// <value>The FontSizes.</value>
        public ObservableCollection<double> FontSizes {
            get => _FontSizes;
            set {
                _FontSizes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedFontSize Property
        private double _SelectedFontSize = default;
        /// <summary>Gets/sets the SelectedFontSize.</summary>
        /// <value>The SelectedFontSize.</value>
        public double SelectedFontSize {
            get => _SelectedFontSize;
            set {
                _SelectedFontSize = value;
                App.ThisApp.BaseFontSize = SelectedFontSize;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedIcon Property
        private RecycledCompositeIcon _SelectedIcon = default;
        /// <summary>Gets/sets the SelectedIcon.</summary>
        /// <value>The SelectedIcon.</value>
        public RecycledCompositeIcon SelectedIcon {
            get => _SelectedIcon;
            set {
                _SelectedIcon = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
