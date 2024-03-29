namespace SDFManagerSupport
{
	using System;
	using System.Collections.Generic;
	using System.Data.Linq;
	using System.Diagnostics;
	using System.IO;

	using System.Linq;

	public class SDFDatabase : DataContext, ISDFDatabase
	{
		#region Public Constructors
		public SDFDatabase(string fileName)
			: base(fileName)
		{
		}
		#endregion Public Constructors

		#region Private Constructors
		private SDFDatabase(string fileName, bool isNew)
			: base(fileName)
		{
			if (isNew)
			{
				if (base.DatabaseExists())
					throw new ApplicationException(string.Format("{0} already exists.", fileName));
				try
				{
					base.CreateDatabase();
				}
				catch (InvalidOperationException) { }
				catch (Exception) { throw; }
			}
			Name = Path.GetFileName(fileName);
		}
		#endregion Private Constructors

		#region Public Methods
		public static ISDFDatabase Create(string fileName, DatabaseDefinition dbDef)
		{
			return Create(fileName, dbDef.Tables);
		}
		public static ISDFDatabase Create(string fileName, List<TableDefinition> tables)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);
			var db = new SDFDatabase(fileName, true);
			foreach (var t in tables)
			{
				Debug.WriteLine(t.CreationScript());
				db.ExecuteCommand(t.CreationScript());
			}
			return db;
		}
		#endregion Public Methods

		#region Public Properties
		public string Name { get; private set; }
		#endregion Public Properties
	}
}
