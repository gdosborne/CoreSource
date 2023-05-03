using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ServiceModel;
using ServiceDataModel.Services;
using System.ServiceModel.Description;

namespace WpfApp1 {
    public partial class App : System.Windows.Application {
        public static IDataServiceWcf DataService { get; private set; }

        protected override void OnStartup(StartupEventArgs e) {
            var contractDescription = new ContractDescription("MyDataChannel", "IDataServiceWcf");
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            var ep = new EndpointAddress("http://localhost:60476/DataServiceWcf");
            var sep = new ServiceEndpoint(contractDescription, binding, ep);
            var factory = new ChannelFactory<IDataServiceWcf>(sep);
            DataService = factory.CreateChannel();
        }
    }
}
