using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Extensibility {
    public delegate void SaveExtensionDataHandler(object sender, SaveExtensionDataEventArgs e);
    public class SaveExtensionDataEventArgs : EventArgs {
        public SaveExtensionDataEventArgs(object obj) { 
            Object = obj;
        }

        /// <summary>Gets the Object.</summary>
        /// <value>The Object.</value>
        public object Object { get; private set; } = null;

    }
}
