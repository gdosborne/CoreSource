// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// Server
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;

	using System.Linq;

	/// <summary>
	/// Enum ServerTypes
	/// </summary>
	public enum ServerTypes
	{
		/// <summary>
		/// The wa
		/// </summary>
		WA,
		/// <summary>
		/// The opc
		/// </summary>
		OPC,
		/// <summary>
		/// The VTS
		/// </summary>
		VTS,
		/// <summary>
		/// The wiki
		/// </summary>
		WIKI
	}

	/// <summary>
	/// Class WAServer.
	/// </summary>
	public sealed class WAServer : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAItem" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAServer(int id, string name, int sequence)
			: base(id, name, sequence) {
			TextColors = new Dictionary<string, WABrush>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WAServer" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="type">The type.</param>
		public WAServer(int id, string name, int sequence, ServerTypes type)
			: this(id, name, sequence) {
			Type = type;
		}
		#endregion Public Constructors

		#region Public Fields
		/// <summary>
		/// The defaultalarmbrushkey
		/// </summary>
		public static readonly string ALARMBRUSHKEY = "DynamicTextAlarm";
		/// <summary>
		/// The defaultdynamictextbrushkey
		/// </summary>
		public static readonly string MANUALBRUSHKEY = "DynamicTextField";
		/// <summary>
		/// The defaultmanualbrushkey
		/// </summary>
		public static readonly string FIELDBRUSHKEY = "DynamicTextDefault";
		/// <summary>
		/// The defaultundefinedbrushkey
		/// </summary>
		public static readonly string SIMULATEDBRUSHKEY = "DynamicTextUndefined";
		#endregion Public Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>The port.</value>
		public int Port { get; set; }
		/// <summary>
		/// Gets or sets the text colors.
		/// </summary>
		/// <value>The text colors.</value>
		public Dictionary<string, WABrush> TextColors { get; private set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type.</value>
		public ServerTypes Type { get; set; }
		#endregion Public Properties
	}
}