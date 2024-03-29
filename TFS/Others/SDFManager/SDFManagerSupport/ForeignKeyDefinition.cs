namespace SDFManagerSupport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Serialization;

	[Serializable]
	public class ForeignKeyDefinition
	{
		#region Public Constructors
		public ForeignKeyDefinition() { }
		public ForeignKeyDefinition(string table, string field)
		{
			TableName = table;
			FieldName = field;
		}
		#endregion Public Constructors

		#region Public Methods
		public string CreationScript()
		{
			return "REFERENCES [" + TableName + "]([" + FieldName + "])";
		}
		#endregion Public Methods

		#region Public Properties
		[XmlAttribute]
		public string FieldName { get; set; }
		[XmlAttribute]
		public string TableName { get; set; }
		#endregion Public Properties
	}
}
