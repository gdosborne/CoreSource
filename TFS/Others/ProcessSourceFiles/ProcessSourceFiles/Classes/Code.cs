using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProcessSourceFiles.Classes
{
	public abstract class BaseItem
	{
		[Flags]
		public enum Modifiers
		{
			Ignore = 0,
			Public = 1,
			Private = 2,
			Internal = 4,
			Protected = 8,
			Partial = 16,
			Static = 32
		}
		public BaseItem(string name, MemberDeclarationSyntax syntax)
		{
			Name = name;
			Syntax = syntax;
		}
		public string Name { get; protected set; }
		public Modifiers ModifierFlag { get; set; }
		public MemberDeclarationSyntax Syntax { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
	public class LocalNamespace : BaseItem
	{
		public LocalNamespace(string name, NamespaceDeclarationSyntax syntax)
			: base(name, syntax)
		{
			ModifierFlag = Modifiers.Ignore;
			Classes = new List<LocalClass>();
			Enumerations = new List<LocalEnumeration>();
		}
		public List<LocalClass> Classes { get; set; }
		public List<LocalEnumeration> Enumerations { get; set; }
		public List<LocalUsing> Usings { get; set; }
	}
	public class LocalClass : BaseItem
	{
		public LocalClass(string name, ClassDeclarationSyntax syntax)
			: base(name, syntax)
		{
			Classes = new List<LocalClass>();
			Enumerations = new List<LocalEnumeration>();
			Fields = new List<LocalField>();
		}
		public List<LocalClass> Classes { get; set; }
		public List<LocalEnumeration> Enumerations { get; set; }
		public List<LocalField> Fields { get; set; }
	}
	public class LocalEnumerationMember : BaseItem
	{
		public LocalEnumerationMember(string name, EnumMemberDeclarationSyntax syntax, object value)
			: base(name, syntax) 
		{
			Value = value;
		}
		public object Value { get; set; }
	}
	public class LocalEnumeration : BaseItem
	{
		public LocalEnumeration(string name, EnumDeclarationSyntax syntax)
			: base(name, syntax)
		{
			Members = new List<LocalEnumerationMember>();
		}
		public List<LocalEnumerationMember> Members { get; set; }
	}
	public class LocalField : BaseItem
	{
		public LocalField(string name, object value, Type type, FieldDeclarationSyntax syntax, int enumSequence)
			: this(name, value, type, syntax)
		{
			EnumSequence = enumSequence;
		}
		public LocalField(string name, object value, Type type, FieldDeclarationSyntax syntax)
			: base(name, syntax)
		{
			Value = value;
			Type = type;
		}
		public object Value { get; set; }
		public Type Type { get; set; }
		public int EnumSequence { get; set; }
	}
	public class LocalUsing : BaseItem
	{
		public LocalUsing(string name, UsingDirectiveSyntax syntax)
			: base(name, null)
		{
			Syntax = syntax;
		}
		public new UsingDirectiveSyntax Syntax { get; set; }
	}
}
