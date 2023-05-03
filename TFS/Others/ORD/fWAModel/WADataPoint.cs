// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Datapoint
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WADataPoint.
	/// </summary>
	public sealed class WADataPoint
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the background.
		/// </summary>
		/// <value>The background.</value>
		public WABrush Background { get; set; }
		/// <summary>
		/// Gets or sets the fixed value.
		/// </summary>
		/// <value>The fixed value.</value>
		public double FixedValue { get; set; }
		/// <summary>
		/// Gets or sets the foreground.
		/// </summary>
		/// <value>The foreground.</value>
		public WABrush Foreground { get; set; }
		/// <summary>
		/// Gets or sets the name of the friendly.
		/// </summary>
		/// <value>The name of the friendly.</value>
		public string FriendlyName { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is persisted.
		/// </summary>
		/// <value><c>true</c> if this instance is persisted; otherwise, <c>false</c>.</value>
		public bool IsPersisted { get; set; }
		/// <summary>
		/// Gets or sets the legend.
		/// </summary>
		/// <value>The legend.</value>
		public string Legend { get; set; }
		/// <summary>
		/// Gets or sets the local identifier.
		/// </summary>
		/// <value>The local identifier.</value>
		public Guid LocalId { get; set; }
		/// <summary>
		/// Gets or sets the object identifier.
		/// </summary>
		/// <value>The object identifier.</value>
		public string ObjectId { get; set; }
		#endregion Public Properties
	}
}