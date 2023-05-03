// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// MultiValue dynamic objetc interface - controls that contain multiple tags
//
namespace SNC.OptiRamp.ObjectInterfaces
{
	using System;
	using System.Collections.Generic;

	using System.Linq;

	/// <summary>
	/// Interface IWAMultiValueDynamicObject
	/// </summary>
	public interface IWAMultiValueDynamicObject
	{
		#region Public Properties
		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <value>The tags.</value>
		IDictionary<string, VTSTag> Tags { get; }
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		string TrendChartTemplateName { get; set; }
		#endregion Public Properties
	}
}