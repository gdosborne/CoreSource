using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.Language
{
    public class LanguageSpecification
    {
        public static IList<LanguageSpecification> Languages = null;

        static LanguageSpecification()
        {
            Languages = new List<LanguageSpecification>
            {
                CSharpSpecification(),
                VBSpecification()
            };
        }

        private static LanguageSpecification CSharpSpecification()
        {
            return new LanguageSpecification
            {
                Name = "CSharp",
                CommentStart = "//",
                NamespaceStart = "namespace",
                BodyStart = "{",
                BodyEnd = "}"
            };
        }
        private static LanguageSpecification VBSpecification()
        {
            return new LanguageSpecification
            {
                Name = "VB",
                CommentStart = "'",
                NamespaceStart = "Namespace",
                BodyStart = Environment.NewLine,
                BodyEnd = Environment.NewLine
            };
        }

        public string Name { get; private set; }

        public string CommentStart { get; private set; }

        public string NamespaceStart { get; private set; }

        public string BodyStart { get; private set; }
        public string BodyEnd { get; private set; }
    }
}
