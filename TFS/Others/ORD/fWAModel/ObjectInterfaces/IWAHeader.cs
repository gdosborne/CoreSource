// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
//  Header
//
namespace SNC.OptiRamp.ObjectInterfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Interface IWAHeader
	/// </summary>
	public interface IWAHeader
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the header brush.
		/// </summary>
		/// <value>The header brush.</value>
		WABrush HeaderBrush { get; set; }
		/// <summary>
		/// Gets or sets the header font.
		/// </summary>
		/// <value>The header font.</value>
		WAFont HeaderFont { get; set; }
		/// <summary>
		/// Gets or sets the header text.
		/// </summary>
		/// <value>The header text.</value>
		string HeaderText { get; set; }
		#endregion Public Properties
	}
}