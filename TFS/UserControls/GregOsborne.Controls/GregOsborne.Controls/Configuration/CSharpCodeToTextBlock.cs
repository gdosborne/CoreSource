using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace GregOsborne.Controls.Configuration {
    public class CSharpCodeToTextBlock : ToTextBlockConverter {
        public CSharpCodeToTextBlock(string data, List<BrushData> brushes, FontFamily fontFamily, double fontSize, FontStretch fontStretch, FontStyle fontStyle, int xmlTabSize, int codeTabSize) {
            XDoc = XDocument.Parse(data);
            XmlTabSize = xmlTabSize;
            CodeTabSize = codeTabSize;
            Brushes = brushes;
            TextBlock = new TextBlock {
                FontFamily = fontFamily,
                FontSize = fontSize,
                FontStretch = fontStretch,
                FontStyle = fontStyle
            };
            TextBlock.MouseWheel += TextBlock_MouseWheel;
            //TextBlock.Inlines.Add(GetDeclaration());
            //TextBlock.Inlines.Add(new LineBreak());
            //AddElement(TextBlock, XDoc.Root, 0);
        }
    }
}
