using System;

namespace Common.Applicationn.Generation {
    public delegate void EnumerationStartGenerationHandler(object sender, EnumerationStartGenerationEventArgs e);
    public class EnumerationStartGenerationEventArgs : EventArgs {
        public EnumerationStartGenerationEventArgs(string enumerationName) => EnumerationName = enumerationName;

        public string EnumerationName { get; private set; } = default;
    }
}
