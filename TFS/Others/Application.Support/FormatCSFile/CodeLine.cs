namespace Application.Support.FormatCSFile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CodeLine
    {
        private const string _usingKey = "using";
        private const string _defineDirectiveKey = "#define";
        private const string _namespaceKey = "namespace";
        private const string _regionKey = "#region";
        private const string _endRegionKey = "#endregion";
        private const string _ifDirectiveKey = "#if";
        private const string _elseDirectiveKey = "#else";
        private const string _elseIfDirectiveKey = "#elif";
        private const string _endIfDirectiveKey = "#endif";
        private const string _blockStartKey = "{";
        private const string _blockEndKey = "}";

        public CodeLine(string text, int sequence)
        {
            Text = text;
            Sequence = sequence;
            ProcessCodeLine();
        }

        private void ProcessCodeLine()
        {
            if (string.IsNullOrWhiteSpace(Text.Trim())) {
                ItemType = ItemTypes.BlankLine;
            }
            else if (Text.Trim().StartsWith(_usingKey)) {
                ItemType = ItemTypes.Using;
            }
            else if (Text.Trim().StartsWith(_defineDirectiveKey)) {
                ItemType = ItemTypes.CompilerDefineDirective;
            }
            else if (Text.Trim().StartsWith(_namespaceKey)) {
                ItemType = ItemTypes.NamespaceDefinition;
            }
            else if (Text.Trim().StartsWith(_regionKey)) {
                ItemType = ItemTypes.Region;
            }
            else if (Text.Trim().StartsWith(_endRegionKey)) {
                ItemType = ItemTypes.EndRegion;
            }
            else if (Text.Trim().StartsWith(_ifDirectiveKey)) {
                ItemType = ItemTypes.IfDirective;
            }
            else if (Text.Trim().StartsWith(_elseDirectiveKey)) {
                ItemType = ItemTypes.ElseDirective;
            }
            else if (Text.Trim().StartsWith(_elseIfDirectiveKey)) {
                ItemType = ItemTypes.ElseDirective;
            }
            else if (Text.Trim().StartsWith(_endIfDirectiveKey)) {
                ItemType = ItemTypes.EndIfDirective;
            }
            else if (Text.Trim().StartsWith(_blockStartKey)) {
                ItemType = ItemTypes.BlockStart;
            }
            else if (Text.Trim().StartsWith(_blockEndKey)) {
                ItemType = ItemTypes.BlockEnd;
            }
        }

        public enum ScopeTypes
        {
            None,
            Public,
            Protected,
            Internal,
            Private
        }

        public enum ItemTypes
        {
            None,
            BlankLine,
            Region,
            EndRegion,
            CompilerDefineDirective,
            IfDirective,
            ElseDirective,
            ElseIfDirective,
            EndIfDirective,
            BlockStart,
            BlockEnd,
            Using,
            NamespaceDefinition,
            ClassDefinition,
            Method,
            Property,
            Structure
        }


        public bool IsContinuingOnNextLine {
            get; private set;
        }

        public ScopeTypes Scope {
            get; private set;
        }

        public ItemTypes ItemType {
            get; private set;
        }

        public string Text {
            get; private set;
        }

        public string ModifiedText {
            get; set;
        }

        public int Sequence {
            get; set;
        }
        public override string ToString() => Text;
    }
}
