// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Template
//
namespace SNC.OptiRamp
{
	using SNC.OptiRamp.PageObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WATemplate.
	/// </summary>
	public sealed class WATemplate : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WATemplate" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WATemplate(int id, string name, int sequence)
			: base(id, name, sequence)
		{
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the background brush.
		/// </summary>
		/// <value>The background brush.</value>
		public WABrush BackgroundBrush { get; set; }
		/// <summary>
		/// Gets or sets the border brush.
		/// </summary>
		/// <value>The border brush.</value>
		public WABrush BorderBrush { get; set; }
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public WAFont Font { get; set; }
		/// <summary>
		/// Gets or sets the type of the object.
		/// </summary>
		/// <value>The type of the object.</value>
		public SNC.OptiRamp.Enumerations.ObjectTypes ObjectType { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [show drop shadow].
		/// </summary>
		/// <value><c>true</c> if [show drop shadow]; otherwise, <c>false</c>.</value>
		public bool ShowDropShadow { get; set; }
		/// <summary>
		/// Gets or sets the thickness.
		/// </summary>
		/// <value>The thickness.</value>
		public double Thickness { get; set; }
		#endregion Public Properties
	}
}