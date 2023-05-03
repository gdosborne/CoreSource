// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Datasource
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WADataSource.
	/// </summary>
	public sealed class WADataSource : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WADataSource" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WADataSource(int id, string name, int sequence)
			: base(id, name, sequence)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WADataSource" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="type">The type.</param>
		/// <param name="address">The address.</param>
		/// <param name="database">The database.</param>
		public WADataSource(int id, string name, int sequence, string type, string address, string database)
			: base(id, name, sequence)
		{
			Type = type;
			Address = address;
			Database = database;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WADataSource" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="type">The type.</param>
		/// <param name="address">The address.</param>
		/// <param name="database">The database.</param>
		/// <param name="channelsTable">The channels table.</param>
		/// <param name="valuesTable">The values table.</param>
		public WADataSource(int id, string name, int sequence, string type, string address, string database, string channelsTable, string valuesTable)
			: this(id, name, sequence, type, address, database)
		{
			ChannelsTable = channelsTable;
			ValuesTable = valuesTable;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address { get; set; }
		/// <summary>
		/// Gets or sets the channels table.
		/// </summary>
		/// <value>The channels table.</value>
		public string ChannelsTable { get; set; }
		/// <summary>
		/// Gets or sets the database.
		/// </summary>
		/// <value>The database.</value>
		public string Database { get; set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public string Type { get; set; }
		/// <summary>
		/// Gets or sets the values table.
		/// </summary>
		/// <value>The values table.</value>
		public string ValuesTable { get; set; }
		#endregion Public Properties
	}
}