/* File="EnumerationGeneratedEventArgs"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2023 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;

namespace CCC.ApplicationFramework.Generation {
    /// <summary>Enumeration Generated Handler</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <a onclick="return false;" href="EnumerationGeneratedEventArgs" originaltag="see">EnumerationGeneratedEventArgs</a> instance containing the event data.</param>
    public delegate void EnumerationGeneratedHandler(object sender, EnumerationGeneratedEventArgs e);
    /// <summary>Enumeration Geterated EventArgs</summary>
    public class EnumerationGeneratedEventArgs : EventArgs {
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="EnumerationGeneratedEventArgs" originaltag="see">EnumerationGeneratedEventArgs</a> class.</summary>
        /// <param name="enumerationName">Name of the enumeration.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="numberOfEnumItems">The number of enum items.</param>
        /// <param name="status">The status.</param>
        public EnumerationGeneratedEventArgs(string enumerationName, string tableName, int numberOfEnumItems, string status) {
            EnumerationName = enumerationName;
            TableName = tableName;
            NumberOfEnumItems = numberOfEnumItems;
            Status = status;
        }

        /// <summary>Gets the name of the enumeration.</summary>
        /// <value>The name of the enumeration.</value>
        public string EnumerationName { get; private set; } = default;

        /// <summary>Gets the number of enum items.</summary>
        /// <value>The number of enum items.</value>
        public int NumberOfEnumItems { get; private set; } = default;

        /// <summary>Gets the status.</summary>
        /// <value>The status.</value>
        public string Status { get; private set; } = default;

        /// <summary>Gets the name of the table.</summary>
        /// <value>The name of the table.</value>
        public string TableName { get; private set; } = default;
    }
}
