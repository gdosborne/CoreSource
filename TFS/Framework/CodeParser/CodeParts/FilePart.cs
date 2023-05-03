using CodeParser.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParser.CodeParts
{
    public class FilePart : CodePart
    {
        internal FilePart(string code, LanguageTypes language, LanguageSpecification spec)
            : base(code, CodePartTypes.File, language, spec)
        {
            NameSpaces = new List<NamespacePart>();
            var bodyItemCount = 0;
            var lineNumber = 0;
            var isFindingBodyStart = false;
            var body = new StringBuilder(5 * 1024);
            using (var sr = new StringReader(code))
            {
                while (sr.Peek() > -1)
                {
                    lineNumber++;
                    var line = sr.ReadLine();
                    if (line.StartsWith(Specification.CommentStart))
                        continue;
                    if (line.StartsWith(Specification.NamespaceStart))
                    {
                        body.AppendLine(line);
                        if (line.Contains(Specification.BodyStart))
                        {
                            bodyItemCount++;
                            isFindingBodyStart = true;
                        }
                    }
                    else if (isFindingBodyStart)
                    {
                        body.AppendLine(line);
                        if(line.Contains(Specification.BodyEnd) && bodyItemCount == 1)
                        {
                            //reached the end of the namespace
                            var namespaceCode = body.ToString();
                            NameSpaces.Add(new NamespacePart(namespaceCode, language, spec));
                            body.Clear();
                        }
                        else if(line.Contains(Specification.BodyStart))
                        {
                            bodyItemCount++;
                        }
                        else if (line.Contains(Specification.BodyEnd))
                        {
                            bodyItemCount--;
                        }
                    }
                }
            }
        }

        public IList<NamespacePart> NameSpaces { get; private set; }
    }
}
