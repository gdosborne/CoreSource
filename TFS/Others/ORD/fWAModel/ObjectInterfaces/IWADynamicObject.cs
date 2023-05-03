// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// [your comment here]
//
namespace SNC.OptiRamp.ObjectInterfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Interface IWADynamicObject
	/// </summary>
	public interface IWADynamicObject
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		VTSTag Tag { get; set; }
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		string TrendChartTemplateName { get; set; }
		#endregion Public Properties
	}
}