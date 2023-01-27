using System;

namespace Common.Applicationn.Generation {
    /// <summary>Assembly Generated handler</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <a onclick="return false;" href="AssemblyGeneratedEventArgs" originaltag="see">AssemblyGeneratedEventArgs</a> instance containing the event data.</param>
    public delegate void AssemblyGeneratedHandler(object sender, AssemblyGeneratedEventArgs e);
    /// <summary>Assembly Generated EvewntArgs</summary>
    public class AssemblyGeneratedEventArgs : EventArgs {
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="AssemblyGeneratedEventArgs" originaltag="see">AssemblyGeneratedEventArgs</a> class.</summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="path">The path.</param>
        /// <param name="version">The version.</param>
        public AssemblyGeneratedEventArgs(string assemblyName, string path, Version version) {
            AssemblyName = assemblyName;
            Path = path;
            Version = version;
        }

        /// <summary>Gets the name of the assembly.</summary>
        /// <value>The name of the assembly.</value>
        public string AssemblyName { get; private set; } = default;

        /// <summary>Gets the path.</summary>
        /// <value>The path.</value>
        public string Path { get; private set; } = default;

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        public Version Version { get; private set; } = null;
    }
}
