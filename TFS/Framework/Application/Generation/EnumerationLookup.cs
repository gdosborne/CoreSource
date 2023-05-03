namespace GregOsborne.Application.Generation {
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;

	/// <summary>Enumeration Lookup</summary>
	public class EnumerationLookup {
		/// <summary>Initializes a new instance of the <a onclick="return false;" href="EnumerationLookup" originaltag="see">EnumerationLookup</a> class.</summary>
		public EnumerationLookup() => this.EnumItems = new Dictionary<object, string>();

		/// <summary>Initializes a new instance of the <a onclick="return false;" href="EnumerationLookup" originaltag="see">EnumerationLookup</a> class.</summary>
		/// <param name="connectionString">The connection string.</param>
		public EnumerationLookup(string connectionString)
			: this() => this.ConnectionString = connectionString;

		/// <summary>Gets or sets the connection string.</summary>
		/// <value>The connection string.</value>
		public string ConnectionString {
			get; set;
		}

		/// <summary>Gets or sets the type of the enum base.</summary>
		/// <value>The type of the enum base.</value>
		public Type EnumBaseType {
			get; set;
		}

		/// <summary>Gets the enum items.</summary>
		/// <value>The enum items.</value>
		public IDictionary<object, string> EnumItems {
			get; private set;
		}

		/// <summary>Gets or sets the name of the enum.</summary>
		/// <value>The name of the enum.</value>
		public string EnumName {
			get; set;
		}

		/// <summary>Gets or sets the name of the identifier field.</summary>
		/// <value>The name of the identifier field.</value>
		public string IdFieldName {
			get; set;
		}

		/// <summary>Gets or sets the name of the name field.</summary>
		/// <value>The name of the name field.</value>
		public string NameFieldName {
			get; set;
		}

		/// <summary>Gets or sets the name of the table.</summary>
		/// <value>The name of the table.</value>
		public string TableName {
			get; set;
		}

		/// <summary>Creates EnumerationLookup from an XElement</summary>
		/// <param name="elem">The elem.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <returns>
		///   <br />
		/// </returns>
		public static EnumerationLookup FromXElement(XElement elem, string connectionString) {
			var result = new EnumerationLookup(connectionString) {
				EnumBaseType = Type.GetType(elem.Attribute("EnumBaseType").Value),
				EnumName = elem.Attribute("EnumName").Value,
				IdFieldName = elem.Attribute("IdFieldName").Value,
				NameFieldName = elem.Attribute("NameFieldName").Value,
				TableName = elem.Attribute("TableName").Value
			};
			return result;
		}

		/// <summary>Gets the enumerations.</summary>
		/// <param name="normalize">if set to <c>true</c> [normalize].</param>
		public void GetEnumerations(bool normalize = false) {
			using (var conn = new SqlConnection(this.ConnectionString)) {
				conn.Open();
				var sql = $"SELECT [{this.IdFieldName}], [{this.NameFieldName}] FROM [{this.TableName}] ORDER BY [{this.IdFieldName}]";
				using (var cmd = new SqlCommand(sql, conn))
				using (var reader = cmd.ExecuteReader()) {
					if (reader.HasRows) {
						var index = 0;
						while (reader.Read()) {
							var id = reader.GetValue(0).CastTo(this.EnumBaseType);
							var name = reader.GetString(1);
							if (normalize) {
								name = name.Replace(" ", "_");
							}
							index = 0;
							var baseName = name;
							while (this.EnumItems.Any(x => x.Value.Equals(name))) {
								index++;
								name = $"{baseName}{index}";
							}
							this.EnumItems.Add(id, name);
						}
					}
				}

				conn.Close();
			}
		}
		/// <summary>Converts to string.</summary>
		/// <returns>A <a onclick="return false;" href="System.String" originaltag="see">System.String</a> that represents this instance.</returns>
		public override string ToString() => $"{base.ToString()} ({this.EnumName})";
	}
}
