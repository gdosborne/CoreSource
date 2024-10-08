#define TESTER
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
namespace ProcessSourceFiles.Classes
{
	public class ProcessFile : INotifyPropertyChanged
	{
		public static List<string> disallowedExtensions = new List<string>
		{
			".g.i.cs",
			".g.cs",
			".designer.cs"
		};
		public static List<string> disallowedFileNames = new List<string>
		{
			"reference.cs",
			"assemblyinfo.cs"
		};
		public static List<string> disallowedFilePrefixes = new List<string>
		{
			"Temporary"
		};
		public void Format()
		{
			Namespaces = new List<LocalNamespace>();
			var tree = CSharpSyntaxTree.ParseText(ModifiedData);
			var root = tree.GetRoot() as CompilationUnitSyntax;
			foreach (var ns in root.Members)
			{
				if (ns is NamespaceDeclarationSyntax)
				{
					var nSpace = (ns as NamespaceDeclarationSyntax);
					var nName = nSpace.Name;
					var nameSpace = new LocalNamespace(nSpace.Name.ToString(), nSpace);
					foreach (var nsMember in nSpace.Members)
					{
						if (nsMember is ClassDeclarationSyntax)
						{
							GetClass(nameSpace, nsMember as ClassDeclarationSyntax);
						}
						else if (nsMember is EnumDeclarationSyntax)
						{
							GetEnumeration(nameSpace, nsMember as EnumDeclarationSyntax);
						}
						else if (nsMember is StructDeclarationSyntax)
						{
						}
					}
					Namespaces.Add(nameSpace);
				}
			}
		}
		public void Process(ProcessParameters parameters)
		{
			var result = new StringBuilder();
			bool removeLine = false;
			bool lastLineWasBlank = false;
			int lineNumber = 0;
			var usingBuilder = new StringBuilder();
			var otherLineFound = false;
			var namespaceFound = false;
			var usingFound = false;
			var nameSpaceFound = false;
			var nameSpaceStartFound = false;
			var usingsWritten = false;
			IEnumerator<string> enumerator = null;
			enumerator = GetLines(1).GetEnumerator();
			while(enumerator.MoveNext())
			{
				var x = enumerator.Current;
				lineNumber++;
				removeLine = false;
				var hasHtmlComment = x.Contains(HtmlComment);
				var hasComment = x.TrimStart().StartsWith(Comment);
				var hasRegion = x.Contains(Region) || x.Contains(EndRegion);
				var isBlankLine = string.IsNullOrWhiteSpace(x.Trim());
				if (hasHtmlComment)
					removeLine = parameters.RemoveHtmlComments;
				else if (hasComment)
					removeLine = parameters.RemoveFullLineComments;
				else if (hasRegion)
					removeLine = parameters.RemoveRegions;
				else if (isBlankLine)
					removeLine = parameters.RemoveAllBlankLines || lastLineWasBlank;
				if (!removeLine)
				{
					result.AppendLine(x);
					lastLineWasBlank = isBlankLine;
				}
				else if (isBlankLine)
					lastLineWasBlank = true;
			}
			pass2Data = result.ToString();
			result = new StringBuilder();
			lineNumber = 0;
			enumerator = GetLines(2).GetEnumerator();
			while (enumerator.MoveNext())
			{
				var x = enumerator.Current;
				lineNumber++;
				if (!otherLineFound && x.TrimStart().StartsWith("using", StringComparison.OrdinalIgnoreCase))
				{
					usingBuilder.AppendLine(x.TrimStart());
					usingFound = true;
				}
				else
				{
					result.AppendLine(x);
					if (x.StartsWith("namespace", StringComparison.OrdinalIgnoreCase))
						namespaceFound = true;
					else if (namespaceFound && usingFound)
						otherLineFound = true;
				}
			}
			pass3Data = result.ToString();
			result = new StringBuilder();
			ModifiedData = string.Empty;
			lineNumber = 0;
			if (usingBuilder.Length > 0 && parameters.UsingPosition == UsingPositions.OutsideNamespace)
			{
				ModifiedData = usingBuilder.ToString() + pass3Data;
			}
			else
			{
				enumerator = GetLines(3).GetEnumerator();
				while (enumerator.MoveNext())
				{
					var x = enumerator.Current;
					lineNumber++;
					if (nameSpaceFound && nameSpaceStartFound && !usingsWritten)
					{
						using (var sr1 = new StringReader(usingBuilder.ToString()))
						{
							while (sr1.Peek() > -1)
							{
								result.AppendLine("\t" + sr1.ReadLine());
							}
						}
						usingsWritten = true;
					}
					result.AppendLine(x);
					if (x.TrimStart().StartsWith("namespace", StringComparison.OrdinalIgnoreCase))
						nameSpaceFound = true;
					if (nameSpaceFound && !nameSpaceStartFound && x.TrimEnd().EndsWith("{", StringComparison.OrdinalIgnoreCase))
						nameSpaceStartFound = true;
				}
				ModifiedData = result.ToString();
			}
		}
		public void Save()
		{
			if (System.IO.File.Exists(FullPath))
				System.IO.File.Delete(FullPath);
			using (var fs = new FileStream(FullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs))
			{
				sw.Write(ModifiedData);
			}
			Data = ModifiedData;
			ModifiedData = string.Empty;
		}
		private void GetClass(LocalNamespace ns, ClassDeclarationSyntax cls)
		{
			var cName = cls.Identifier.Text;
			ns.Classes.Add(new LocalClass(cName, cls));
			foreach (var mbr in cls.Members)
			{
			}
		}
		private void GetEnumeration(LocalNamespace ns, EnumDeclarationSyntax enu)
		{
			var eName = enu.Identifier.Text;
			var localEnum = new LocalEnumeration(eName, enu);
			foreach (var mbr in enu.Members)
			{
				if (mbr is EnumMemberDeclarationSyntax)
				{
				}
			}
			ns.Enumerations.Add(localEnum);
		}
		private IEnumerable<string> GetLines(int passNumber)
		{
			var theData = passNumber == 1 ? Data 
				: passNumber == 2 ? pass2Data 
					: passNumber == 3 ? pass3Data 
						: string.Empty;
			using (var sr = new StringReader(theData))
			{
				while (sr.Peek() > -1)
				{
					yield return sr.ReadLine();
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly string Comment = "//";
		private string _Data;
		private string _FileName;
		private string _FullPath;
		private bool _HasChanges;
		private bool _IsSelected;
		private string _ModifiedData;
		private string pass2Data = string.Empty;
		private string pass3Data = string.Empty;
		public enum UsingPositions
		{
			InsideNamespace,
			OutsideNamespace
		}
		public string Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Data"));
			}
		}
		public string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
			}
		}
		public string FullPath
		{
			get
			{
				return _FullPath;
			}
			set
			{
				_FullPath = value;
				using (var fs = new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.None))
				using (var sr = new StreamReader(fs))
				{
					Data = sr.ReadToEnd();
				}
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("FullPath"));
			}
		}
		public bool HasChanges
		{
			get
			{
				return _HasChanges;
			}
			set
			{
				_HasChanges = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HasChanges"));
			}
		}
		public bool IsSelected
		{
			get
			{
				return _IsSelected;
			}
			set
			{
				_IsSelected = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
			}
		}
		public string ModifiedData
		{
			get
			{
				return _ModifiedData;
			}
			set
			{
				_ModifiedData = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("ModifiedData"));
			}
		}
		public List<LocalNamespace> Namespaces { get; set; }
	}
}
