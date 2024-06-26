//------------------------------------------------------------------------------
// <copyright file="VersionerEditorProvider.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Versioner {
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")] // This classifier applies to all text files.
    internal class VersionerEditorProvider : IClassifierProvider {
        // Disable "Field is never assigned to..." compiler's warning. Justification: the field is assigned by MEF.
#pragma warning disable 649

        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

#pragma warning restore 649

        #region IClassifierProvider

        public IClassifier GetClassifier(ITextBuffer buffer) {
            return buffer.Properties.GetOrCreateSingletonProperty<VersionerEditor>(creator: () => new VersionerEditor(this.classificationRegistry));
        }

        #endregion
    }
}
