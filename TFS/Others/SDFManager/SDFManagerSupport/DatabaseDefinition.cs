namespace SDFManagerSupport
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Linq;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(TypeName = "Database")]
	public class DatabaseDefinition : INotifyPropertyChanged
	{
		#region Public Constructors
		public DatabaseDefinition() { }
		public DatabaseDefinition(string name)
		{
			Name = name;
			Tables = new List<TableDefinition>();
		}
		public DatabaseDefinition(string name, params TableDefinition[] tables)
			: this(name)
		{
			if (tables != null && tables.Any())
				Tables.AddRange(tables);
		}
		#endregion Public Constructors

		#region Public Methods
		public static DatabaseDefinition Load(string fileName)
		{
			DatabaseDefinition result = null;
			var ser = new XmlSerializer(typeof(DatabaseDefinition));
			using (FileStream myFileStream = new FileStream(fileName, FileMode.Open))
			{
				result = (DatabaseDefinition)ser.Deserialize(myFileStream);
				result.FileName = fileName;
			}
			result.IsChanged = false;
			return result;
		}
		public void Save(string fileName)
		{
			XmlSerializer ser = new XmlSerializer(this.GetType());
			using (TextWriter writer = new StreamWriter(fileName))
			{
				ser.Serialize(writer, this);
			}
			FileName = fileName;
			IsChanged = false;
		}
		#endregion Public Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private bool _IsChanged;
		#endregion Private Fields

		#region Public Properties
		[XmlAttribute]
		private string _FileName;
		public string FileName
		{
			get { return _FileName; }
			set
			{
				_FileName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		[XmlIgnore]
		public bool IsChanged
		{
			get
			{
				return _IsChanged;
			}
			set
			{
				_IsChanged = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		[XmlAttribute]
		public string Name { get; set; }
		[XmlArray(ElementName = "Tables")]
		public List<TableDefinition> Tables { get; set; }
		#endregion Public Properties
	}
}
