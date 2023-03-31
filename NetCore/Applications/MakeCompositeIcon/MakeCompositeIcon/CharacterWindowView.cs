using Common.MVVMFramework;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using static Common.Application.Media.Extensions;

namespace MakeCompositeIcon {
    internal partial class CharacterWindowView : ViewModelBase {
        public CharacterWindowView() {
            Title = "Font Character Window [designer]";
        }

        public override void Initialize() {
            base.Initialize();

            Title = "Font Character Window";
            CharacterSize = App.ThisApp.MySession.ApplicationSettings.GetValue(nameof(CharacterWindow),
                nameof(CharacterSize), 64.0);
            Characters = new ObservableCollection<CharInfo>();
        }

        private async Task RefreshAsync() {
            var chars = App.ThisApp.CachedCharacters[Font.Source];
            chars?.ForEach(x => {
                if (x.Index == 0 || x.Index == 13 || x.Index == 10)
                    return;
                x.FontSize = CharacterSize;
                Characters.Add(x);
            });
        }

        #region CharacterSize Property
        private double _CharacterSize = default;
        /// <summary>Gets/sets the CharacterSize.</summary>
        /// <value>The CharacterSize.</value>
        public double CharacterSize {
            get => _CharacterSize;
            set {
                _CharacterSize = value < 24 ? 24 : value > 100 ? 100 : value;
                if (Characters != null) {
                    Characters.ToList().ForEach(c => c.FontSize = _CharacterSize);
                }
                App.ThisApp.MySession.ApplicationSettings.AddOrUpdateSetting(nameof(CharacterWindow),
                    nameof(CharacterSize), CharacterSize);
                BoxSize = CharacterSize + 10;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BoxSize Property
        private double _BoxSize = default;
        /// <summary>Gets/sets the BoxSize.</summary>
        /// <value>The BoxSize.</value>
        public double BoxSize {
            get => _BoxSize;
            set {
                _BoxSize = value;
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

        #region Characters Property
        private ObservableCollection<CharInfo> _Characters = default;
        /// <summary>Gets/sets the Characters.</summary>
        /// <value>The Characters.</value>
        public ObservableCollection<CharInfo> Characters {
            get => _Characters;
            set {
                _Characters = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedCharacter Property
        private CharInfo _SelectedCharacter = default;
        /// <summary>Gets/sets the SelectedCharacter.</summary>
        /// <value>The SelectedCharacter.</value>
        public CharInfo SelectedCharacter {
            get => _SelectedCharacter;
            set {
                _SelectedCharacter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Font Property
        private FontFamily _Font = default;
        /// <summary>Gets/sets the Font.</summary>
        /// <value>The Font.</value>
        public FontFamily Font {
            get => _Font;
            set {
                _Font = value;
                RefreshAsync();
                Title += $" - {Font.Source}";
                OnPropertyChanged();
            }
        }
        #endregion

        #region IsPrimary Property
        private bool _IsPrimary = default;
        /// <summary>Gets/sets the IsPrimary.</summary>
        /// <value>The IsPrimary.</value>
        public bool IsPrimary {
            get => _IsPrimary;
            set {
                _IsPrimary = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
