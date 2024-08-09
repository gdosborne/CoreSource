using Common.Linq;
using Common.Media;
using Common.Primitives;
using Common.MVVMFramework;
using CongregationManager.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace CongregationManager.ViewModels {
    public class ThemeEditorWindowViewModel : ViewModelBase {
        public ThemeEditorWindowViewModel() {
            Title = "Theme Editor [designer]";
           
            Themes = new ObservableCollection<ApplicationTheme>(App.AppThemes);
            if (Themes.Any()){
                SelectedTheme = App.CurrentTheme;
            }
        }

        private void Color_ColorClicked(object? sender, System.EventArgs e) {
            var setColor = sender.As<SettingColor>();
            var p = new Dictionary<string, object> {
                { "Color", setColor.ColorString }
            };
            ExecuteAction(nameof(Actions.ChooseColor), p);
            setColor.ColorString = (string)p["Color"];
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Theme Editor";
        }

        public enum Actions {
            CloseWindow,
            ChooseColor,
            CreateTheme
        }

        #region CloseWindowCommand
        private DelegateCommand _CloseWindowCommand = default;
        /// <summary>Gets the CloseWindow command.</summary>
        /// <value>The CloseWindow command.</value>
        public DelegateCommand CloseWindowCommand => _CloseWindowCommand ??= new DelegateCommand(CloseWindow, ValidateCloseWindowState);
        private bool ValidateCloseWindowState(object state) => true;
        private void CloseWindow(object state) {
            ExecuteAction(nameof(Actions.CloseWindow));
        }
        #endregion

        #region Colors Property
        private ObservableCollection<SettingColor> _Colors = default;
        /// <summary>Gets/sets the Colors.</summary>
        /// <value>The Colors.</value>
        public ObservableCollection<SettingColor> Colors {
            get => _Colors;
            set {
                _Colors = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Themes Property
        private ObservableCollection<ApplicationTheme> _Themes = default;
        /// <summary>Gets/sets the Themes.</summary>
        /// <value>The Themes.</value>
        public ObservableCollection<ApplicationTheme> Themes {
            get => _Themes;
            set {
                _Themes = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SelectedTheme Property
        private ApplicationTheme _SelectedTheme = default;
        /// <summary>Gets/sets the SelectedTheme.</summary>
        /// <value>The SelectedTheme.</value>
        public ApplicationTheme SelectedTheme {
            get => _SelectedTheme;
            set {
                _SelectedTheme = value;
                if(SelectedTheme!=null) {
                    var aliasName = App.Current.Resources.BrushAliasNames();
                    var temp = new List<SettingColor>();

                    SelectedTheme.Values.ToList().ForEach(x => {
                        temp.Add(new SettingColor {
                            Name = aliasName[x.Key],
                            ColorValue = x.Value.ToColor(),
                            Key = x.Key
                        });
                    });
                    temp.ForEach(color => {
                        color.ColorClicked += Color_ColorClicked;
                    });
                    Colors = new ObservableCollection<SettingColor>();
                    Colors.AddRange(temp.OrderBy(x => x.Name));

                    App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("Application", "Theme", SelectedTheme.Name);
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region ApplyThemeCommand
        private DelegateCommand _ApplyThemeCommand = default;
        /// <summary>Gets the ApplyTheme command.</summary>
        /// <value>The ApplyTheme command.</value>
        public DelegateCommand ApplyThemeCommand => _ApplyThemeCommand ??= new DelegateCommand(ApplyTheme, ValidateApplyThemeState);
        private bool ValidateApplyThemeState(object state) => SelectedTheme != null;
        private void ApplyTheme(object state) {
            Colors.ToList().ForEach(x => {
                SelectedTheme.Values[x.Key] = x.ColorValue.ToHexValue();
            });
            SelectedTheme.Save();
            SelectedTheme.Apply(App.Current.Resources);
        }
        #endregion

        #region CreateThemeCommand
        private DelegateCommand _CreateThemeCommand = default;
        /// <summary>Gets the CreateTheme command.</summary>
        /// <value>The CreateTheme command.</value>
        public DelegateCommand CreateThemeCommand => _CreateThemeCommand ??= new DelegateCommand(CreateTheme, ValidateCreateThemeState);
        private bool ValidateCreateThemeState(object state) => true;
        private void CreateTheme(object state) {
            ExecuteAction(nameof(Actions.CreateTheme));
        }
        #endregion


    }
}
