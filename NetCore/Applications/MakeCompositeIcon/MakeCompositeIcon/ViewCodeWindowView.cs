using ApplicationFramework.Media;
using Common.Application.Primitives;
using Common.MVVMFramework;

namespace MakeCompositeIcon {
    internal partial class ViewCodeWindowView : ViewModelBase {

        public ViewCodeWindowView() {
            Title = "View Xaml [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "View Xaml";
            OutputIconSize = App.ThisApp.MySession.ApplicationSettings.GetValue("Application",
                nameof(OutputIconSize), 32);
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

        #region Icon Property
        private CompositeIcon _Icon = default;
        /// <summary>Gets/sets the Icon.</summary>
        /// <value>The Icon.</value>
        public CompositeIcon Icon {
            get => _Icon;
            set {
                _Icon = value;
                if (Icon != null)
                    XamlText = Icon.GetXaml(OutputIconSize.CastTo<int>());
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

        #region OutputIconSize Property
        private double _OutputIconSize = default;
        /// <summary>Gets/sets the OutputIconSize.</summary>
        /// <value>The OutputIconSize.</value>
        public double OutputIconSize {
            get => _OutputIconSize;
            set {
                _OutputIconSize = value < 10 ? 10 : value > 200 ? 200 : value;
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting("Application",
                    nameof(OutputIconSize), OutputIconSize.CastTo<int>());
                if (Icon != null)
                    XamlText = Icon.GetXaml(OutputIconSize.CastTo<int>());
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
