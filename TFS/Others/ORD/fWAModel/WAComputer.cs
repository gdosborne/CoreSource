// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Computer
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAComputer.
	/// </summary>
	public sealed class WAComputer : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAItem" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAComputer(int id, string name, int sequence)
			: base(id, name, sequence)
		{
			VTSServers = new List<WAServer>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WAComputer" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="address">The address.</param>
		public WAComputer(int id, string name, int sequence, string address)
			: this(id, name, sequence)
		{
			Address = address;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address { get; private set; }
		/// <summary>
		/// Gets the servers.
		/// </summary>
		/// <value>The servers.</value>
		public List<WAServer> VTSServers { get; private set; }
		#endregion Public Properties
	}
}