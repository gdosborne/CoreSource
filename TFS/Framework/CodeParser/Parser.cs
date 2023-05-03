using CodeParser.CodeParts;
using CodeParser.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysio = System.IO;

namespace CodeParser
{
    public enum LanguageTypes
    {
        CSharp,
        VB
    }
    public class Parser
    {
        public static Parser Create(string fileName, string collectionType, LanguageTypes language, LanguageSpecification spec)
        {
            return new Parser(fileName, collectionType, language, spec);
        }
        public static Parser Create(string fileName, string collectionType)
        {
            if (sysio.File.Exists(fileName))
            {
                if (GregOsborne.Application.IO.File.Extension(fileName).Equals(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    return Create(fileName, collectionType, LanguageTypes.CSharp, LanguageSpecification.Languages.First(x => x.Name == "CSharp"));
                }
                else if (GregOsborne.Application.IO.File.Extension(fileName).Equals(".vb", StringComparison.OrdinalIgnoreCase))
                {
                    return Create(fileName, collectionType, LanguageTypes.VB, LanguageSpecification.Languages.First(x => x.Name == "VB"));
                }
                else
                    throw new ApplicationException("Invalid code file (must be either cs or vb)");
            }
            else
                throw new ArgumentException("Filename is missing or file does not exist.");
        }

#pragma warning disable IDE0044 // Add readonly modifier
        private object _fileLock = new object();
#pragma warning restore IDE0044 // Add readonly modifier
        private Parser(string fileName, string collectionType, LanguageTypes language, LanguageSpecification spec)
        {
            if (sysio.File.Exists(fileName))
            {
                
                //CodeFile = new FilePart(code, language, spec);
            }
            else
                throw new ArgumentException("Filename is missing or file does not exist.");
        }
        
        public FilePart CodeFile { get; private set; }
    }
}
