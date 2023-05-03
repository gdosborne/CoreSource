// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Web Analytics Column for table
//
namespace SNC.OptiRamp
{
	using SNC.OptiRamp.Services.fDefs;
	using System;
	using System.Collections.Generic;

	using System.Linq;

	/// <summary>
	/// Class WAColumn. This class cannot be inherited.
	/// </summary>
	public sealed class WAColumn : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAColumn"/> class.
		/// </summary>
		public WAColumn() {
			UniqueIds = new List<WARow>();
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the alignment.
		/// </summary>
		/// <value>The alignment.</value>
		public string Alignment { get; set; }
		/// <summary>
		/// Gets or sets the element.
		/// </summary>
		/// <value>The element.</value>
		public IElement Element { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is name column.
		/// </summary>
		/// <value><c>true</c> if this instance is name column; otherwise, <c>false</c>.</value>
		public bool IsNameColumn { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is total column.
		/// </summary>
		/// <value><c>true</c> if this instance is total column; otherwise, <c>false</c>.</value>
		public bool IsTotalColumn { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the unique ids.
		/// </summary>
		/// <value>The unique ids.</value>
		public List<WARow> UniqueIds { get; set; }
		/// <summary>
		/// Gets or sets the name of the VTS tag.
		/// </summary>
		/// <value>The name of the VTS tag.</value>
		public string VtsTagName { get; set; }
		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public double Width { get; set; }
		#endregion Public Properties
	}
}