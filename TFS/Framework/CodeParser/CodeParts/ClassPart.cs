using CodeParser.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.CodeParts
{
    public class ClassPart : CodePart
    {
        internal ClassPart(string code, LanguageTypes language, LanguageSpecification spec)
            : base(code, CodePartTypes.Class, language, spec)
        {

        }
    }
}
