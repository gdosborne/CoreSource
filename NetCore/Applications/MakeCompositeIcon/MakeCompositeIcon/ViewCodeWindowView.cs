using ApplicationFramework.Media;
using Common.MVVMFramework;

namespace MakeCompositeIcon {
    internal partial class ViewCodeWindowView : ViewModelBase {

        public ViewCodeWindowView() {
            Title = "View Xaml [designer]";
            IsDesktopChecked = true;
        }

        public override void Initialize() {
            base.Initialize();

            Title = "View Xaml";
        }

        #region IsUWPChecked Property
        private bool _IsUWPChecked = default;
        /// <summary>Gets/sets the IsUWPChecked.</summary>
        /// <value>The IsUWPChecked.</value>
        public bool IsUWPChecked {
            get => _IsUWPChecked;
            set {
                _IsUWPChecked = value;
                if (Icon != null)
                    XamlText = Icon.GetXaml(IsUWPChecked);
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsDesktopChecked Property
        private bool _IsDesktopChecked = default;
        /// <summary>Gets/sets the IsDesktopChecked.</summary>
        /// <value>The IsDesktopChecked.</value>
        public bool IsDesktopChecked {
            get => _IsDesktopChecked;
            set {
                _IsDesktopChecked = value;
                if (Icon != null)
                    XamlText = Icon.GetXaml(IsUWPChecked);
                OnPropertyChanged();
            }
        }
        #endregion

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

        #region Icon Property
        private CompositeIcon _Icon = default;
        /// <summary>Gets/sets the Icon.</summary>
        /// <value>The Icon.</value>
        public CompositeIcon Icon {
            get => _Icon;
            set {
                _Icon = value;
                if (Icon !=null) 
                    XamlText = Icon.GetXaml(IsUWPChecked);
                OnPropertyChanged();
            }
        }
        #endregion

        #region XamlText Property
        private string _XamlText = default;
        /// <summary>Gets/sets the XamlText.</summary>
        /// <value>The XamlText.</value>
        public string XamlText {
            get => _XamlText;
            set {
                _XamlText = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
