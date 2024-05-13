/* File="EnumerationStartGenerationEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2023 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace CCC.ApplicationFramework.Generation {
    public delegate void EnumerationStartGenerationHandler(object sender, EnumerationStartGenerationEventArgs e);
    public class EnumerationStartGenerationEventArgs : EventArgs {
        public EnumerationStartGenerationEventArgs(string enumerationName) => EnumerationName = enumerationName;

        public string EnumerationName { get; private set; } = default;
    }
}
