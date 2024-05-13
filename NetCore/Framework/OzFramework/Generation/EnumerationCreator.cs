/* File="EnumerationCreator"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2023 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using CCC.ApplicationFramework.Generation;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Framework.Application.Generation {
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
            ConnectionString = connectionString;
            AssemblyName = assemblyName;
            AssemblyPath = assemblyPath;
            if (string.IsNullOrEmpty(ConnectionString)) {
                throw new ArgumentException("Parameter connectionstring must have a value");
            }
            if (string.IsNullOrEmpty(AssemblyName)) {
                throw new ArgumentException("Parameter assemblyName must have a value");
            }
            if (string.IsNullOrEmpty(AssemblyPath)) {
                throw new ArgumentException("Parameter assemblyPath must have a value");
            }
            Enumerations = new List<EnumerationLookup>();
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
            SourceFile = sourceFile;
            ConnectionString = connectionString;
            AssemblyName = assemblyName;
            AssemblyPath = assemblyPath;
            if (string.IsNullOrEmpty(SourceFile)) {
                throw new ArgumentException("Parameter source file must have a value");
            }
            if (string.IsNullOrEmpty(ConnectionString)) {
                throw new ArgumentException("Parameter connectionstring must have a value");
            }
            if (string.IsNullOrEmpty(AssemblyName)) {
                throw new ArgumentException("Parameter assemblyName must have a value");
            }
            if (string.IsNullOrEmpty(AssemblyPath)) {
                throw new ArgumentException("Parameter assemblyPath must have a value");
            }
            if (!File.Exists(SourceFile)) {
                throw new ArgumentException($"{SourceFile} is missing");
            }
            Namespace = $"{AssemblyName}.Enums";
            Enumerations = new List<EnumerationLookup>();
            doc = XDocument.Load(SourceFile);
            UpdateEnumerationDefinitions();
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
            get => sourceFile;
            private set {
                sourceFile = value;
                isSourceInitialized = true;
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
            var aName = AssemblyName;
            if (!aName.EndsWith("dll", StringComparison.OrdinalIgnoreCase) && !aName.EndsWith("exe", StringComparison.OrdinalIgnoreCase)) {
                aName += ".dll";
            }

            var assyName = new AssemblyName(aName) {
                CodeBase = Path.GetDirectoryName(AssemblyPath),
                CultureInfo = System.Globalization.CultureInfo.CurrentCulture,
                Version = version,
                VersionCompatibility = System.Configuration.Assemblies.AssemblyVersionCompatibility.SameDomain
            };
            Environment.CurrentDirectory = Path.GetDirectoryName(AssemblyPath);
            var currentDomain = AppDomain.CurrentDomain;
            var assemblyBuilder = currentDomain.DefineDynamicAssembly(assyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assyName.Name, aName);

            using (var conn = new SqlConnection(ConnectionString)) {
                conn.Open();
                using (var cmd = new SqlCommand(actualSql, conn)) {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        var tableName = reader.GetString(reader.GetOrdinal("TableName"));
                        for (var i = 0; i < columnNames.Length; i++) {
                            var e = new EnumerationLookup(ConnectionString) {
                                EnumBaseType = typeof(int),
                                EnumName = tableName,
                                IdFieldName = $"{tableName}ID",
                                NameFieldName = columnNames[i],
                                TableName = tableName
                            };
                            try {
                                if (!Enumerations.Any(x => x.TableName.Equals(tableName))) {
                                    e.GetEnumerations(true);
                                    //Debug.WriteLine($"{e.TableName} enums defined");
                                    Enumerations.Add(e);
                                    //Debug.WriteLine($"{tableName}.{e.NameFieldName} success");
                                }
                                break;
                            }
                            catch (System.Exception) {
                                //Debug.WriteLine($"{tableName}.{e.NameFieldName} failed");
                                //Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            BuildEnums(moduleBuilder, true);

            assemblyBuilder.DefineVersionInfoResource(productName, version.ToString(), companyName, copyright, trademark);
            assemblyBuilder.Save(aName);
            AssemblyGenerated?.Invoke(this, new AssemblyGeneratedEventArgs(Path.GetFileName(AssemblyPath), Path.GetDirectoryName(AssemblyPath), version));
            return true;
        }

        private void BuildEnums(ModuleBuilder moduleBuilder, bool areEnumsDefined = false) => Enumerations.ForEach(e => {
            if (!e.EnumItems.Any()) {
                return;
            }

            Debug.WriteLine($"{e.TableName} {new string('-', 20)}");
            var enumBuilder = moduleBuilder.DefineEnum($"{Namespace}.{e.EnumName}", TypeAttributes.Public, e.EnumBaseType);

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
                    catch (System.Exception ex) {
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
                var aName = AssemblyName;
                if (!aName.EndsWith("dll", StringComparison.OrdinalIgnoreCase) && !aName.EndsWith("exe", StringComparison.OrdinalIgnoreCase)) {
                    aName += ".dll";
                }

                var assyName = new AssemblyName(aName) {
                    CodeBase = Path.GetDirectoryName(AssemblyPath),
                    CultureInfo = System.Globalization.CultureInfo.CurrentCulture,
                    Version = version,
                    VersionCompatibility = System.Configuration.Assemblies.AssemblyVersionCompatibility.SameDomain
                };
                Environment.CurrentDirectory = Path.GetDirectoryName(AssemblyPath);
                var currentDomain = AppDomain.CurrentDomain;
                var assemblyBuilder = currentDomain.DefineDynamicAssembly(assyName, AssemblyBuilderAccess.RunAndSave);
                var moduleBuilder = assemblyBuilder.DefineDynamicModule(assyName.Name, aName);

                BuildEnums(moduleBuilder);

                assemblyBuilder.DefineVersionInfoResource(productName, version.ToString(), companyName, copyright, trademark);
                assemblyBuilder.Save(aName);
                AssemblyGenerated?.Invoke(this, new AssemblyGeneratedEventArgs(Path.GetFileName(AssemblyPath), Path.GetDirectoryName(AssemblyPath), version));
                return true;
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <summary>Updates the enumeration definitions.</summary>
        /// <param name="sourceFile">The source file.</param>
        public void UpdateEnumerationDefinitions(string sourceFile = default) {
            if (!isSourceInitialized && string.IsNullOrEmpty(SourceFile)) {
                return;
            }
            try {
                var temp = new List<EnumerationLookup>();
                var root = doc.Root;
                var resultAssemblyAttrib = root.Attribute("ResultantAssemblyName");
                var resultPathAttrib = root.Attribute("ResultantDirectory");
                if (resultAssemblyAttrib != null && !string.IsNullOrEmpty(resultAssemblyAttrib.Value)) {
                    AssemblyName = resultAssemblyAttrib.Value;
                }
                if (resultPathAttrib != null && !string.IsNullOrEmpty(resultPathAttrib.Value) && Directory.Exists(resultPathAttrib.Value)) {
                    AssemblyPath = resultPathAttrib.Value;
                }
                root.Elements().ToList().ForEach(elem => {
                    var el = EnumerationLookup.FromXElement(elem, ConnectionString);
                    temp.Add(el);
                });
                if (Enumerations == null) {
                    Enumerations = new List<EnumerationLookup>();
                }

                Enumerations.Clear();
                Enumerations.AddRange(temp);
            }
            catch (System.Exception) { throw; }
        }

        private bool isDisposed = false;
        protected virtual void Dispose(bool isDisposing) {
            if (!isDisposed) {
                if (isDisposing) {
                    doc = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                isDisposed = true;
            }
        }
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose() => Dispose(true);
    }
}
