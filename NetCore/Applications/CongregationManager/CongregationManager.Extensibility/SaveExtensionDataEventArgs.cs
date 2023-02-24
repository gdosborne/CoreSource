using System;

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
