using System;
using System.Windows;

namespace CongregationManager.Extensibility {
    public delegate void RetrieveResourcesHandler(object sender, RetrieveResourcesEventArgs e);
    public class RetrieveResourcesEventArgs : EventArgs {
        public RetrieveResourcesEventArgs() { }
        
        public ResourceDictionary Dictionary { get; set; }
   }
}
