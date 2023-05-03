namespace SDFManagerSupport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(TypeName = "Table")]
	public class TableDefinition
	{
		#region Public Constructors
		public TableDefinition()
		{
			Fields = new List<FieldDefinition>();
		}
		public TableDefinition(string name)
			:this()
		{
			Name = name;
		}
		#endregion Public Constructors

		#region Public Methods
		public string CreationScript()
		{
			var result = new StringBuilder();
			result.Append("CREATE TABLE ");
			result.AppendLine("[" + Name + "] ");
			result.AppendLine("(");
			for (int i = 0; i < Fields.Count; i++)
			{
				if (i > 0 && i < Fields.Count)
					result.AppendLine(",");
				result.Append(Fields[i].CreationScript());
			}
			result.AppendLine();
			result.AppendLine(");");
			return result.ToString();
		}
		#endregion Public Methods

		#region Public Properties
		[XmlArray(ElementName = "Fields")]
		public List<FieldDefinition> Fields { get; set; }
		[XmlAttribute]
		public double LeftPosition { get; set; }
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public double TopPosition { get; set; }
		[XmlIgnore]
		public double Width { get; set; }
		[XmlIgnore]
		public double Height { get; set; }
		#endregion Public Properties
	}
}
