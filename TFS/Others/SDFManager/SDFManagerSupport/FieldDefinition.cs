namespace SDFManagerSupport
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Data;
	using System.Linq;
	using System.Text;
	using System.Xml.Serialization;

	[Serializable]
	[XmlType(TypeName = "Field")]
	public class FieldDefinition
	{
		#region Public Constructors
		public FieldDefinition()
		{
			DbTypes = new ObservableCollection<DbType>();
			Flags = FieldFlags.None;
			Enum.GetNames(typeof(DbType)).ToList().ForEach(x => DbTypes.Add((DbType)Enum.Parse(typeof(DbType), x, false)));
		}
		public FieldDefinition(string name)
			: this()
		{
			Name = name;
			DbType = System.Data.DbType.Int32;
			Length = 0;
		}
		public FieldDefinition(string name, DbType type)
			: this(name)
		{
			DbType = type;
		}
		public FieldDefinition(string name, DbType type, int length)
			: this(name, type)
		{
			Length = length;
		}
		public FieldDefinition(string name, DbType type, int length, FieldFlags flags)
			: this(name, type, length)
		{
			Flags = flags;
			Length = length;
		}
		public FieldDefinition(string name, DbType type, FieldFlags flags)
			: this(name, type)
		{
			Flags = flags;
		}
		#endregion Public Constructors

		#region Public Methods
		public string CreationScript()
		{
			var isPrimaryKey = Flags.HasFlag(FieldFlags.IsPrimaryKey);
			var isIdentity = Flags.HasFlag(FieldFlags.IsIdentity);
			var isUnique = Flags.HasFlag(FieldFlags.IsUniqueValue);
			var isNullAllowed = Flags.HasFlag(FieldFlags.IsNullAllowed);

			var result = new StringBuilder("\t");
			result.Append(Name + " ");
			result.Append(DbTypeTranslate());
			result.Append(isNullAllowed ? "NULL " : "NOT NULL ");
			if (ForeignKey != null)
				result.Append(ForeignKey.CreationScript() + " ");
			if (isIdentity)
				result.Append("IDENTITY(1,1) ");
			if (DefaultValue != null)
			{
				var q = DefaultValue is string ? "'" : string.Empty;
				result.Append("DEFAULT (" + q + DefaultValue.ToString() + q + ") WITH VALUES ");
			}
			if (isPrimaryKey)
				result.Append("PRIMARY KEY");
			else
			{
				if (isUnique)
					result.Append("UNIQUE");
			}
			return result.ToString();
		}
		public string DbTypeTranslate()
		{
			var result = new StringBuilder();
			if (DbType == System.Data.DbType.AnsiString || DbType == System.Data.DbType.String)
			{
				if (Length == 0 || Length > 4000)
					result.Append("NTEXT");
				else
				{
					result.Append("NVARCHAR(");
					result.Append(Length.ToString());
					result.Append(")");
				}
			}
			else if (DbType == System.Data.DbType.AnsiStringFixedLength || DbType == System.Data.DbType.StringFixedLength)
			{
				if (Length > 4000)
					result.Append("NTEXT");
				else
				{
					result.Append("NCHAR(");
					result.Append(Length.ToString());
					result.Append(")");
				}
			}
			else if (DbType == System.Data.DbType.Binary)
			{
				result.Append("BINARY(");
				if (Length == 0)
					Length = 1;
				result.Append(Length.ToString());
				result.Append(")");
			}
			else if (DbType == System.Data.DbType.Binary || DbType == System.Data.DbType.Boolean)
				result.Append("BIT");
			else if (DbType == System.Data.DbType.Byte)
				result.Append("TINYINT");
			else if (DbType == System.Data.DbType.Int16 || DbType == System.Data.DbType.SByte)
				result.Append("SMALLINT");
			else if (DbType == System.Data.DbType.Int32 || DbType == System.Data.DbType.UInt16)
				result.Append("INT");
			else if (DbType == System.Data.DbType.Int64 || DbType == System.Data.DbType.UInt32)
				result.Append("BIGINT");
			else if (DbType == System.Data.DbType.Decimal || DbType == System.Data.DbType.UInt64 || DbType == System.Data.DbType.Double || DbType == System.Data.DbType.Single || DbType == System.Data.DbType.VarNumeric)
				result.Append("FLOAT");
			else if (DbType == System.Data.DbType.Currency)
				result.Append("MONEY");
			else if (DbType == System.Data.DbType.Guid)
				result.Append("UNIQUEIDENTIFIER");
			else if (DbType == System.Data.DbType.Object)
				result.Append("SQL_VARIANT");
			else if (DbType == System.Data.DbType.Xml)
				result.Append("NVARCHAR(MAX)");
			else
				result.Append(DbType.ToString().ToUpper());
			result.Append(" ");
			return result.ToString();
		}
		#endregion Public Methods

		#region Public Enums
		[Flags]
		public enum FieldFlags
		{
			[XmlEnum]
			None = 0,
			[XmlEnum]
			IsPrimaryKey = 1,
			[XmlEnum]
			IsIdentity = 2,
			[XmlEnum]
			IsUniqueValue = 4,
			[XmlEnum]
			IsNullAllowed = 8
		}
		#endregion Public Enums

		#region Public Properties
		[XmlAttribute]
		public DbType DbType { get; set; }
		public object DefaultValue { get; set; }
		[XmlAttribute]
		public FieldFlags Flags { get; set; }
		[XmlElement(ElementName = "FKey")]
		public ForeignKeyDefinition ForeignKey { get; set; }
		[XmlAttribute]
		public int Length { get; set; }
		[XmlAttribute]
		public string Name { get; set; }
		[XmlIgnore]
		public ObservableCollection<DbType> DbTypes { get; set; }
		[XmlIgnore]
		public bool IsPrimaryKey
		{
			get { return Flags.HasFlag(FieldFlags.IsPrimaryKey); }
			set
			{
				Flags = value
					? (Flags | FieldFlags.IsPrimaryKey)
					: (Flags & ~FieldFlags.IsPrimaryKey);
			}
		}
		[XmlIgnore]
		public bool IsUnique
		{
			get { return Flags.HasFlag(FieldFlags.IsUniqueValue); }
			set
			{
				Flags = value
					? (Flags | FieldFlags.IsUniqueValue)
					: (Flags & ~FieldFlags.IsUniqueValue);
			}
		}
		[XmlIgnore]
		public bool IsNullable
		{
			get { return Flags.HasFlag(FieldFlags.IsNullAllowed); }
			set
			{
				Flags = value
					? (Flags | FieldFlags.IsNullAllowed)
					: (Flags & ~FieldFlags.IsNullAllowed);
			}
		}
		[XmlIgnore]
		public bool IsIdentity
		{
			get { return Flags.HasFlag(FieldFlags.IsIdentity); }
			set
			{
				Flags = value
					? (Flags | FieldFlags.IsIdentity)
					: (Flags & ~FieldFlags.IsIdentity);
			}
		}
		#endregion Public Properties
	}
}
