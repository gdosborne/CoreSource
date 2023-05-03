namespace GregOsborne.ContinuousDeliverer {
    using GregOsborne.Application.Primitives;
    using GregOsborne.ApplicationData;
    using GregOsborne.ApplicationData.ContinuousDeliverer;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;

    public partial class App : System.Windows.Application {
        public static string ApplicationName { get; set; } = "Web Application Continuous Deliverer";
        public static string DataLocation { get; set; } = $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName, "Data")}";
        public static List<ApplicationData.ContinuousDeliverer.Model.Application> Applications { get; set; } = default(List<ApplicationData.ContinuousDeliverer.Model.Application>);
        public static IRepository DataRepository { get; private set; }

        protected override void OnStartup(StartupEventArgs e) {

            //Repository.Create(DataLocation, "Another One");

            DataRepository = new Repository();
            DataRepository.Connect(DataLocation);
            DataRepository.Open();
            Applications = DataRepository.As<Repository>().GetApplications().ToList();
        }

        protected override void OnExit(ExitEventArgs e) {
            DataRepository.Close();
            DataRepository.As<IDisposable>().Dispose();
        }
    }
}
