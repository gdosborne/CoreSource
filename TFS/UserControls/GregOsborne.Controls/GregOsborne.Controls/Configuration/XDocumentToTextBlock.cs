using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using GregOsborne.Application.Primitives;

namespace GregOsborne.Controls.Configuration {
    public class XDocumentToTextBlock : ToTextBlockConverter {
        public XDocumentToTextBlock(string data, List<BrushData> brushes, FontFamily fontFamily, double fontSize, FontStretch fontStretch, FontStyle fontStyle, int xmlXmlTabSize, int codeTabSize) {
            XDoc = XDocument.Parse(data);
            XmlTabSize = xmlXmlTabSize;
            CodeTabSize = codeTabSize;
            Brushes = brushes;
            TextBlock = new TextBlock {
                FontFamily = fontFamily,
                FontSize = fontSize,
                FontStretch = fontStretch,
                FontStyle = fontStyle,
                Margin = new Thickness(3)
            };
            TextBlock.MouseWheel += TextBlock_MouseWheel;
            TextBlock.Inlines.Add(GetDeclaration());
            TextBlock.Inlines.Add(new LineBreak());
            AddElement(TextBlock, XDoc.Root, 0);
        }

        public void UpdateCode(string code) {
            var node = XDoc.DescendantNodes().Single(el => el.NodeType == XmlNodeType.CDATA);
            var parent = node?.Parent;
            parent?.RemoveAll();
            parent?.Add(new XCData(code));
            CDataText = code;
            RawText = XDoc.ToString();
            //var cs = XDoc.Root.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("CodeSnippet"));
            //if (cs == null) return;
            //var sn = cs.Elements().FirstOrDefault(x=>x.Name.LocalName.Equals("Snippet"));
            //if (sn == null) return;
            //var co = sn.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("Code"));
            //if (co == null) return;
            //if (HasChildOfType<XCData>(co)) {
            //    var codeElement = co.Elements().First();
            //    codeElement.Remove();
            //    co.Add(new XCData(code));
            //}
        }

        public string RawText { get; set; }
        public string CDataText { get; set; }
        private Brush GetBrush(int key) {
            return Brushes.FirstOrDefault(x => x.Key == key)?.Value;
        }

        private Brush GetBrush(Enumerations.PalettePart key) {
            return GetBrush(key.Value);
        }

        private TextBlock GetDeclaration() {
            var result = new TextBlock {
                Margin = new Thickness(0)
            };
            result.Inlines.Add(GetRun("<", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            result.Inlines.Add(GetRun("?xml", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            result.Inlines.Add(GetRun(" ", GetBrush(Enumerations.PaletteParts.ElementName)));
            result.Inlines.Add(GetRun("version", GetBrush(Enumerations.PaletteParts.AttributeName)));
            result.Inlines.Add(GetRun("=", GetBrush(Enumerations.PaletteParts.EqualsSign)));
            result.Inlines.Add(GetRun("\"", GetBrush(Enumerations.PaletteParts.StringStartEnd)));
            if (XDoc.Declaration != null)
                result.Inlines.Add(GetRun(XDoc.Declaration.Version, GetBrush(Enumerations.PaletteParts.AttributeValue)));
            result.Inlines.Add(GetRun("\"", GetBrush(Enumerations.PaletteParts.StringStartEnd)));

            result.Inlines.Add(GetRun(" ", GetBrush(Enumerations.PaletteParts.ElementName)));
            result.Inlines.Add(GetRun("encoding", GetBrush(Enumerations.PaletteParts.AttributeName)));
            result.Inlines.Add(GetRun("=", GetBrush(Enumerations.PaletteParts.EqualsSign)));
            result.Inlines.Add(GetRun("\"", GetBrush(Enumerations.PaletteParts.StringStartEnd)));
            if (XDoc.Declaration != null)
                result.Inlines.Add(GetRun(XDoc.Declaration.Encoding, GetBrush(Enumerations.PaletteParts.AttributeValue)));
            result.Inlines.Add(GetRun("\"", GetBrush(Enumerations.PaletteParts.StringStartEnd)));

            result.Inlines.Add(GetRun("?", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            result.Inlines.Add(GetRun(">", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            return result;
        }

        private Run SpaceRun(int amount) {
            return GetRun(new string(' ', amount), GetBrush(Enumerations.PaletteParts.Background));
        }

        private Run EqualsRun() {
            return GetRun("=", GetBrush(Enumerations.PaletteParts.EqualsSign));
        }

        private Run QuoteRun() {
            return GetRun("\"", GetBrush(Enumerations.PaletteParts.StringStartEnd));
        }

        private void AddAttribute(TextBlock textBlock, XAttribute attribute) {
            textBlock.Inlines.Add(SpaceRun(1));
            textBlock.Inlines.Add(GetRun(attribute.Name.LocalName, GetBrush(Enumerations.PaletteParts.AttributeName)));
            textBlock.Inlines.Add(EqualsRun());
            textBlock.Inlines.Add(QuoteRun());
            textBlock.Inlines.Add(GetRun(attribute.Value, GetBrush(Enumerations.PaletteParts.AttributeValue)));
            textBlock.Inlines.Add(QuoteRun());
        }

        private static bool HasChildOfType<T>(XElement element) {
            return element.Nodes().Any(x => x is T);
        }

        private Thickness GetMargin(int tabPosition) {
            return new Thickness(tabPosition * XmlTabSize, 0, 0, 0);
        }

        private void AddElement(TextBlock result, XElement element, int tabPosition) {
            var hasValue = HasChildOfType<XText>(element);
            var isCData = HasChildOfType<XCData>(element);

            var textBlock = new TextBlock {
                Margin = GetMargin(tabPosition)
            };
            textBlock.Inlines.Add(GetRun("<", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            textBlock.Inlines.Add(GetRun(element.Name.LocalName, GetBrush(Enumerations.PaletteParts.ElementName)));
            if (element.HasAttributes) for (var i = 0; i < element.Attributes().Count(); i++) AddAttribute(textBlock, element.Attributes().ToArray()[i]);
            textBlock.Inlines.Add(GetRun(">", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
            if (hasValue && !element.HasElements) {
                if (isCData) {
                    textBlock.Inlines.Add(GetRun("<![CDATA[", GetBrush(Enumerations.PaletteParts.CData)));
                    CDataText = element.Value;
                }
                textBlock.Inlines.Add(GetRun(element.Value, GetBrush(Enumerations.PaletteParts.ElementValue)));
                if (isCData)
                    textBlock.Inlines.Add(GetRun("]]>", GetBrush(Enumerations.PaletteParts.CData)));
                textBlock.Inlines.Add(GetRun("</", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
                textBlock.Inlines.Add(GetRun(element.Name.LocalName, GetBrush(Enumerations.PaletteParts.ElementName)));
                textBlock.Inlines.Add(GetRun(">", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
                result.Inlines.Add(textBlock);
            }
            else if (element.HasElements) {
                result.Inlines.Add(textBlock);
                result.Inlines.Add(new LineBreak());
                for (var i = 0; i < element.Elements().Count(); i++) AddElement(result, element.Elements().ToArray()[i], tabPosition + 1);
                textBlock = new TextBlock {
                    Margin = GetMargin(tabPosition)
                };
                textBlock.Inlines.Add(GetRun("</", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
                textBlock.Inlines.Add(GetRun(element.Name.LocalName, GetBrush(Enumerations.PaletteParts.ElementName)));
                textBlock.Inlines.Add(GetRun(">", GetBrush(Enumerations.PaletteParts.ElementStartEnd)));
                result.Inlines.Add(textBlock);
            }
            result.Inlines.Add(new LineBreak());
        }

        private static Run GetRun(string text, Brush brush) {
            return new Run {
                Text = text,
                Foreground = brush
            };
        }
    }
}