namespace SDFManagerSupport
{
	using System;
	using System.Collections.Generic;
	using System.Data.Linq.Mapping;
	using System.Linq;

	[Table(Name = "DatabaseProperties")]
	public class DatabaseProperty
	{
		#region Public Properties
		[Column(IsPrimaryKey = true, DbType = "NVARCHAR(50) NOT NULL UNIQUE")]
		public string Name { get; set; }
		[Column(DbType = "NVARCHAR(100)")]
		public string Value { get; set; }
		#endregion Public Properties
	}
}
