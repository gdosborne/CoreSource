using Common.Application.Linq;
using Common.Application.Media;
using Common.Application.Primitives;
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
            var aliasName = App.Current.Resources.BrushAliasNames();
            var temp = new List<SettingColor>();
            App.CurrentTheme.Values.ToList().ForEach(x => {
                temp.Add(new SettingColor {
                    Name = aliasName[x.Key],
                    ColorValue = x.Value.ToColor()
                });
            });
            Colors = new ObservableCollection<SettingColor>(temp.OrderBy(x => x.Name));
            temp.ForEach(color => {
                color.ColorClicked += Color_ColorClicked; ;
            });
            Colors.AddRange(temp.OrderBy(x => x.Name));
        }

        private void Color_ColorClicked(object? sender, System.EventArgs e) {
            var setColor = sender.As<SettingColor>();
            var p = new Dictionary<string, object> {
                { "Color", setColor.ColorString }
            };
            ExecuteAction(nameof(Actions.ChooseColor), p);
            //setColor.ColorString = (string)p["Color"];
            //App.Current.Resources[setColor.Name] = setColor.ToBrush();
            //App.ApplicationSession.ApplicationSettings.AddOrUpdateSetting("AlternateColors", setColor.Name, setColor.ColorString);
        }

        public override void Initialize() {
            base.Initialize();
            Title = "Theme Editor";
        }

        public enum Actions {
            CloseWindow,
            ChooseColor
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
    }
}
