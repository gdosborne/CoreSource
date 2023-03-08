using Common.Application.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MakeCompositeIcon {
    internal partial class MainWindowView : ViewModelBase {
        public MainWindowView() {
            Title = $"{App.Current.As<App>().ApplicationName} [designer]";
            CurrentFileName = "No file selected";
            PrimaryBrush = Brushes.Black;
            SurfaceBrush = System.Drawing.SystemColors.Window.ToMediaBrush();
        }

        public override void Initialize() {
            base.Initialize();

            Title = App.Current.As<App>().ApplicationName;
        }

        #region CurrentFileName Property
        private string _CurrentFileName = default;
        /// <summary>Gets/sets the CurrentFileName.</summary>
        /// <value>The CurrentFileName.</value>
        public string CurrentFileName {
            get => _CurrentFileName;
            set {
                _CurrentFileName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsSingleColorSelected Property
        private bool _IsSingleColorSelected = default;
        /// <summary>Gets/sets the IsSingleColorSelected.</summary>
        /// <value>The IsSingleColorSelected.</value>
        public bool IsSingleColorSelected {
            get => _IsSingleColorSelected;
            set {
                _IsSingleColorSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrimaryBrush Property
        private SolidColorBrush _PrimaryBrush = default;
        /// <summary>Gets/sets the PrimaryBrush.</summary>
        /// <value>The PrimaryBrush.</value>
        public SolidColorBrush PrimaryBrush {
            get => _PrimaryBrush;
            set {
                _PrimaryBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SurfaceBrush Property
        private SolidColorBrush _SurfaceBrush = default;
        /// <summary>Gets/sets the SurfaceBrush.</summary>
        /// <value>The SurfaceBrush.</value>
        public SolidColorBrush SurfaceBrush {
            get => _SurfaceBrush;
            set {
                _SurfaceBrush = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
