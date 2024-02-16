namespace GregOsborne.Application.Generation {
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Reflection.Emit;
	using System.Xml.Linq;

	/// <summary>Enumeration Creator</summary>
	public class EnumerationCreator : IDisposable {
		private bool isSourceInitialized = default;
		private string sourceFile = default;

		/// <summary>Initializes a new instance of the <a onclick="return false;" href="EnumerationCreator" originaltag="see">EnumerationCreator</a> class.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <param name="assemblyPath">The assembly path.</param>
		/// <exception cref="ArgumentException">Parameter connectionstring must have a value
		/// or
		/// Parameter assemblyName must have a value
		/// or
		/// Parameter assemblyPath must have a value</exception>
		/// <remarks>Use this constructor when generating from all tables with particular fields</remarks>
		public EnumerationCreator(string connectionString, string assemblyName, string assemblyPath) {
			this.ConnectionString = connectionString;
			this.AssemblyName = assemblyName;
			this.AssemblyPath = assemblyPath;
			if (string.IsNullOrEmpty(this.ConnectionString)) {
				throw new ArgumentException("Parameter connectionstring must have a value");
			}
			if (string.IsNullOrEmpty(this.AssemblyName)) {
				throw new ArgumentException("Parameter assemblyName must have a value");
			}
			if (string.IsNullOrEmpty(this.AssemblyPath)) {
				throw new ArgumentException("Parameter assemblyPath must have a value");
			}
			this.Enumerations = new List<EnumerationLookup>();
		}

		/// <summary>Initializes a new instance of the <a onclick="return false;" href="EnumerationCreator" originaltag="see">EnumerationCreator</a> class.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="assemblyPath">The assembly path.</param>
		/// <exception cref="ArgumentException">
		/// Parameter source file must have a value
		/// or
		/// Parameter connectionstring must have a value
		/// or
		/// Parameter assemblyName must have a value
		/// or
		/// Parameter assemblyPath must have a value
		/// or
		/// </exception>
		/// <remarks>Use this constructor when you are controlling generation from a file</remarks>
		public EnumerationCreator(string connectionString, string assemblyName, string sourceFile, string assemblyPath) {
			this.SourceFile = sourceFile;
			this.ConnectionString = connectionString;
			this.AssemblyName = assemblyName;
			this.AssemblyPath = assemblyPath;
			if (string.IsNullOrEmpty(this.SourceFile)) {
				throw new ArgumentException("Parameter source file must have a value");
			}
			if (string.IsNullOrEmpty(this.ConnectionString)) {
				throw new ArgumentException("Parameter connectionstring must have a value");
			}
			if (string.IsNullOrEmpty(this.AssemblyName)) {
				throw new ArgumentException("Parameter assemblyName must have a value");
			}
			if (string.IsNullOrEmpty(this.AssemblyPath)) {
				throw new ArgumentException("Parameter assemblyPath must have a value");
			}
			if (!File.Exists(this.SourceFile)) {
				throw new ArgumentException($"{this.SourceFile} is missing");
			}
			this.Namespace = $"{this.AssemblyName}.Enums";
			this.Enumerations = new List<EnumerationLookup>();
			this.doc = XDocument.Load(this.SourceFile);
			this.UpdateEnumerationDefinitions();
		}

		/// <summary>Occurs when [assembly generated].</summary>
		public event AssemblyGeneratedHandler AssemblyGenerated;
		/// <summary>Occurs when [enumeration generated].</summary>
		public event EnumerationGeneratedHandler EnumerationGenerated;
		/// <summary>Occurs when [enumeration member generated].</summary>
		public event EnumerationMemberGeneratedHandler EnumerationMemberGenerated;
		/// <summary>Occurs when [enumeration started].</summary>
		public event EnumerationStartGenerationHandler EnumerationStarted;
		/// <summary>Gets or sets the name of the assembly.</summary>
		/// <value>The name of the assembly.</value>
		public string AssemblyName { get; set; } = default;
		/// <summary>Gets or sets the namespace.</summary>
		/// <value>The namespace.</value>
		public string Namespace { get; set; }
		/// <summary>Gets or sets the assembly path.</summary>
		/// <value>The assembly path.</value>
		public string AssemblyPath { get; set; } = default;

		/// <summary>Gets or sets the connection string.</summary>
		/// <value>The connection string.</value>
		public string ConnectionString { get; set; } = default;
		private XDocument doc = default;
		/// <summary>Gets the enumerations.</summary>
		/// <value>The enumerations.</value>
		public List<EnumerationLookup> Enumerations {
			get; private set;
		}

		/// <summary>Gets the source file.</summary>
		/// <value>The source file.</value>
		public string SourceFile {
			get => this.sourceFile;
			private set {
				this.sourceFile = value;
				this.isSourceInitialized = true;
			}
		}

		/// <summary>Creates the enumerations.</summary>
		/// <param name="version">The version.</param>
		/// <param name="lastUpdate">The last update.</param>
		/// <param name="productName">Name of the product.</param>
		/// <param name="companyName">Name of the company.</param>
		/// <param name="copyright">The copyright.</param>
		/// <param name="trademark">The trademark.</param>
		/// <param name="commonColumnName">Name of the common column.</param>
		/// <returns>
		///   <br />
		/// </returns>
		public bool Create(Version version, DateTime lastUpdate, string productName, string companyName, string copyright, string trademark, string commonColumnName) {
			var sql = @"select DISTINCT TableName from (
							select st.[name] as TableName, sc.[name] as ColumnName from sys.tables st 
								inner join sys.columns sc on sc.object_id = st.object_id 
							where sc.[name] like '%[column1]%' or sc.[name] like '%[column2]%'
						) as temp
						Group By TableName
						Having Count(*) > 1
						order by TableName";

			var columnNames = commonColumnName.Split('|');
			var actualSql = sql;
			for (var i = 0; i < columnNames.Length; i++) {
				actualSql = actualSql.Replace($"[column{i + 1}]", columnNames[i]);
			}
			var aName = this.AssemblyName;
			if (!aName.EndsWith("dll", StringComparison.OrdinalIgnoreCase) && !aName.EndsWith("exe", StringComparison.OrdinalIgnoreCase)) {
				aName += ".dll";
			}

			var assyName = new AssemblyName(aName) {
				CodeBase = Path.GetDirectoryName(this.AssemblyPath),
				CultureInfo = System.Globalization.CultureInfo.CurrentCulture,
				Version = version,
				VersionCompatibility = System.Configuration.Assemblies.AssemblyVersionCompatibility.SameDomain
			};
			Environment.CurrentDirectory = Path.GetDirectoryName(this.AssemblyPath);
			var currentDomain = AppDomain.CurrentDomain;
			var assemblyBuilder = currentDomain.DefineDynamicAssembly(assyName, AssemblyBuilderAccess.RunAndSave);
			var moduleBuilder = assemblyBuilder.DefineDynamicModule(assyName.Name, aName);

			using (var conn = new SqlConnection(this.ConnectionString)) {
				conn.Open();
				using (var cmd = new SqlCommand(actualSql, conn)) {
					var reader = cmd.ExecuteReader();
					while (reader.Read()) {
						var tableName = reader.GetString(reader.GetOrdinal("TableName"));
						for (var i = 0; i < columnNames.Length; i++) {
							var e = new EnumerationLookup(this.ConnectionString) {
								EnumBaseType = typeof(int),
								EnumName = tableName,
								IdFieldName = $"{tableName}ID",
								NameFieldName = columnNames[i],
								TableName = tableName
							};
							try {
								if (!this.Enumerations.Any(x => x.TableName.Equals(tableName))) {
									e.GetEnumerations(true);
									//Debug.WriteLine($"{e.TableName} enums defined");
									this.Enumerations.Add(e);
									//Debug.WriteLine($"{tableName}.{e.NameFieldName} success");
								}
								break;
							}
							catch (Exception) {
								//Debug.WriteLine($"{tableName}.{e.NameFieldName} failed");
								//Debug.WriteLine(ex);
							}
						}
					}
				}
			}
			this.BuildEnums(moduleBuilder, true);

			assemblyBuilder.DefineVersionInfoResource(productName, version.ToString(), companyName, copyright, trademark);
			assemblyBuilder.Save(aName);
			AssemblyGenerated?.Invoke(this, new AssemblyGeneratedEventArgs(Path.GetFileName(this.AssemblyPath), Path.GetDirectoryName(this.AssemblyPath), version));
			return true;
		}

		private void BuildEnums(ModuleBuilder moduleBuilder, bool areEnumsDefined = false) => this.Enumerations.ForEach(e => {
			if (!e.EnumItems.Any()) {
				return;
			}

			Debug.WriteLine($"{e.TableName} {new string('-', 20)}");
			var enumBuilder = moduleBuilder.DefineEnum($"{this.Namespace}.{e.EnumName}", TypeAttributes.Public, e.EnumBaseType);

			if (!areEnumsDefined) {
				e.GetEnumerations();
				Debug.WriteLine($"{e.TableName} enums defined");
			}
			var status = "Success";
			if (e.EnumItems.Any()) {
				EnumerationStarted?.Invoke(this, new EnumerationStartGenerationEventArgs(e.EnumName));
				foreach (var item in e.EnumItems) {
					try {
						var literalName = item.Value.Replace(" ", "_");
						enumBuilder.DefineLiteral(literalName, item.Key);
						Debug.WriteLine($"Literal {e.TableName}.{literalName} defined");
					}
					catch (Exception ex) {
						status = $"Failure: {ex.Message}";
						break;
					}
					EnumerationMemberGenerated?.Invoke(this, new EnumerationMemberGeneratedEventArgs(e.EnumName, item.Value, item.Key));
				}
				enumBuilder.CreateType();
			}
			EnumerationGenerated?.Invoke(this, new EnumerationGeneratedEventArgs(e.EnumName, e.TableName, e.EnumItems.Count, status));
		});

		/// <summary>Creates the enumerations.</summary>
		/// <param name="version">The version.</param>
		/// <param name="lastUpdate">The last update.</param>
		/// <param name="productName">Name of the product.</param>
		/// <param name="companyName">Name of the company.</param>
		/// <param name="copyright">The copyright.</param>
		/// <param name="trademark">The trademark.</param>
		/// <returns>
		///   <br />
		/// </returns>
		public bool Create(Version version, DateTime lastUpdate, string productName, string companyName, string copyright, string trademark) {
			try {
				var aName = this.AssemblyName;
				if (!aName.EndsWith("dll", StringComparison.OrdinalIgnoreCase) && !aName.EndsWith("exe", StringComparison.OrdinalIgnoreCase)) {
					aName += ".dll";
				}

				var assyName = new AssemblyName(aName) {
					CodeBase = Path.GetDirectoryName(this.AssemblyPath),
					CultureInfo = System.Globalization.CultureInfo.CurrentCulture,
					Version = version,
					VersionCompatibility = System.Configuration.Assemblies.AssemblyVersionCompatibility.SameDomain
				};
				Environment.CurrentDirectory = Path.GetDirectoryName(this.AssemblyPath);
				var currentDomain = AppDomain.CurrentDomain;
				var assemblyBuilder = currentDomain.DefineDynamicAssembly(assyName, AssemblyBuilderAccess.RunAndSave);
				var moduleBuilder = assemblyBuilder.DefineDynamicModule(assyName.Name, aName);

				this.BuildEnums(moduleBuilder);

				assemblyBuilder.DefineVersionInfoResource(productName, version.ToString(), companyName, copyright, trademark);
				assemblyBuilder.Save(aName);
				AssemblyGenerated?.Invoke(this, new AssemblyGeneratedEventArgs(Path.GetFileName(this.AssemblyPath), Path.GetDirectoryName(this.AssemblyPath), version));
				return true;
			}
			catch (Exception ex) {
				Console.WriteLine(ex);
				throw;
			}
		}

		/// <summary>Updates the enumeration definitions.</summary>
		/// <param name="sourceFile">The source file.</param>
		public void UpdateEnumerationDefinitions(string sourceFile = default) {
			if (!this.isSourceInitialized && string.IsNullOrEmpty(this.SourceFile)) {
				return;
			}
			try {
				var temp = new List<EnumerationLookup>();
				var root = this.doc.Root;
				var resultAssemblyAttrib = root.Attribute("ResultantAssemblyName");
				var resultPathAttrib = root.Attribute("ResultantDirectory");
				if (resultAssemblyAttrib != null && !string.IsNullOrEmpty(resultAssemblyAttrib.Value)) {
					this.AssemblyName = resultAssemblyAttrib.Value;
				}
				if (resultPathAttrib != null && !string.IsNullOrEmpty(resultPathAttrib.Value) && Directory.Exists(resultPathAttrib.Value)) {
					this.AssemblyPath = resultPathAttrib.Value;
				}
				root.Elements().ToList().ForEach(elem => {
					var el = EnumerationLookup.FromXElement(elem, this.ConnectionString);
					temp.Add(el);
				});
				if (this.Enumerations == null) {
					this.Enumerations = new List<EnumerationLookup>();
				}

				this.Enumerations.Clear();
				this.Enumerations.AddRange(temp);
			}
			catch (Exception) { throw; }
		}

		private bool isDisposed = false;
		protected virtual void Dispose(bool isDisposing) {
			if (!this.isDisposed) {
				if (isDisposing) {
					this.doc = null;
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				this.isDisposed = true;
			}
		}
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose() => this.Dispose(true);
	}
}
