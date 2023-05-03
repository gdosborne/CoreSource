// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Title
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WATitle. This class cannot be inherited.
	/// </summary>
	public sealed class WATitle
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the brush.
		/// </summary>
		/// <value>The brush.</value>
		public WABrush Brush { get; set; }
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public WAFont Font { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
		public bool IsVisible { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }
		#endregion Public Properties
	}
}