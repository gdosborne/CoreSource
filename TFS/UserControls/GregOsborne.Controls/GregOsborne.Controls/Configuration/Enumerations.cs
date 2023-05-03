using System.Collections.Generic;
using System.Windows.Media;
using GregOsborne.Application;

namespace GregOsborne.Controls.Configuration {
    public static class Enumerations {
        static Enumerations() {

        }

        public class CodeLanguages {
            public static CodeLanguage CSharp = new CodeLanguage(1) {
                Name = "CSharp"
            };
        }

        public class DocumentTypes {
            public static DocumentType Text = new DocumentType(1) {
                Description = @"Plain text document",
                Palette = BrushPalette.TextPalette
            };
            public static DocumentType Xml = new DocumentType(2) {
                Description = @"Xml document",
                Palette = BrushPalette.XmlPalette
            };
            public static DocumentType CSharp = new DocumentType(3) {
                Description = @"CSharp document",
                Palette = BrushPalette.CSharpPalette
            };
        }

        public class PaletteParts {
            public static PalettePart Background = new PalettePart(1) {
                Description = @"Document background"
            };
            public static PalettePart Foreground = new PalettePart(2) {
                Description = @"Document foreground (default when other color not specified)"
            };
            public static PalettePart StringStartEnd = new PalettePart(3) {
                Description = @"Quote"
            };
            public static PalettePart ElementStartEnd = new PalettePart(4) {
                Description = @"Element braces"
            };
            public static PalettePart EqualsSign = new PalettePart(5) {
                Description = @"Equals sign"
            };
            public static PalettePart ElementName = new PalettePart(6) {
                Description = @"Element name"
            };
            public static PalettePart ElementValue = new PalettePart(7) {
                Description = @"Element value"
            };
            public static PalettePart AttributeName = new PalettePart(8) {
                Description = @"Attribute name"
            };
            public static PalettePart AttributeValue = new PalettePart(9) {
                Description = @"Attribute value"
            };
            public static PalettePart CData = new PalettePart(10) {
                Description = @"CData identifier"
            };
        }

        public class DocumentType : ThickEnum<int> {
            public DocumentType(int value) : base(value) { }

            public string Description { get; internal set; }
            public BrushPalette Palette { get; set; }
        }

        public class PalettePart : ThickEnum<int> {
            public PalettePart(int value) : base(value) { }
            public string Description { get; internal set; }
        }

        public class CodeLanguage : ThickEnum<int> {
            public CodeLanguage(int value) : base(value) { }
            public string Name { get; internal set; }
        }
    }
}