using Common.MVVMFramework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congregation.Scheduler.Views {
    internal class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel () {
            Title = $"{App.ApplicationName} [Designer]";
        }

        public override void Initialize () {
            base.Initialize();
            Title = $"{App.ApplicationName}";
        }
    }
}
