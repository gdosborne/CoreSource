//------------------------------------------------------------------------------
// <copyright file="VersionerEditorClassificationDefinition.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Versioner {
    internal static class VersionerEditorClassificationDefinition {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("VersionerEditor")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169
    }
}
