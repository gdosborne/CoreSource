//------------------------------------------------------------------------------
// <copyright file="VersionerEditorFormat.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Versioner {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "VersionerEditor")]
    [Name("VersionerEditor")]
    [UserVisible(true)] // This should be visible to the end user
    [Order(Before = Priority.Default)] // Set the priority to be after the default classifiers
    internal sealed class VersionerEditorFormat : ClassificationFormatDefinition {
        public VersionerEditorFormat() {
            this.DisplayName = "VersionerEditor"; // Human readable version of the name
            this.BackgroundColor = Colors.BlueViolet;
            this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
}
