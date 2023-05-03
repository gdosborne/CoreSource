using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Consoler {
    public partial class App : Application {
        public static StringWriter TheConsole = null;
        private static TextWriter oldConsole = null;
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            oldConsole = Console.Out;
            TheConsole = new StringWriter();
            TheConsole.Flush();
            Console.SetOut(TheConsole);
        }
        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);
            Console.SetOut(oldConsole);
        }
    }
}
