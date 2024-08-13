using Common.Primitives;
using Common.Windows;

using Congregation.Scheduler.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Congregation.Scheduler {
    internal partial class PersonsWindow : Window {
        public PersonsWindow () {
            InitializeComponent();
            View.Initialize();

            SourceInitialized += (s, e) => {
                Left = App.Session.ApplicationSettings.GetValue(nameof(PersonsWindow), nameof(Left), Left);
                Top = App.Session.ApplicationSettings.GetValue(nameof(PersonsWindow), nameof(Top), Top);
                Width = App.Session.ApplicationSettings.GetValue(nameof(PersonsWindow), nameof(Width), Width);
                Height = App.Session.ApplicationSettings.GetValue(nameof(PersonsWindow), nameof(Height), Height);

                this.HideMinimizeAndMaximizeButtons();
            };

            Closing += (s, e) => {
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(PersonsWindow), nameof(Left), RestoreBounds.Left);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(PersonsWindow), nameof(Top), RestoreBounds.Top);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(PersonsWindow), nameof(Width), RestoreBounds.Width);
                App.Session.ApplicationSettings.AddOrUpdateSetting(nameof(PersonsWindow), nameof(Height), RestoreBounds.Height);
            };

            View.ExecuteUiAction += (s, e) => {
                if (Enum.TryParse(typeof(PersonsWindowViewModel.Actions), e.CommandToExecute, out var action)) {
                    switch (action) {
                        case PersonsWindowViewModel.Actions.AddMember:
                            
                            break;
                        default:
                            break;
                    }
                }
            };

        }

        internal PersonsWindowViewModel View => DataContext.As<PersonsWindowViewModel>();
    }
}
