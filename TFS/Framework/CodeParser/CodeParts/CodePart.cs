using CodeParser.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.CodeParts
{
    public enum CodePartTypes
    {
        File,
        Namespace,
        Class
    }
    public abstract class CodePart
    {
        internal CodePart(string code, CodePartTypes type, LanguageTypes language, LanguageSpecification spec)
        {
            PartType = type;
            RawText = code;
            Language = language;
            Specification = spec;
        }

        public string RawText { get; private set; }
        public CodePartTypes PartType { get; private set; }

        public LanguageTypes Language { get; private set; }

        public LanguageSpecification Specification { get; internal set; }
    }
}
