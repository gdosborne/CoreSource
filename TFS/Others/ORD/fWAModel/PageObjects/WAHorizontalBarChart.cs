// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Horizontal Bar chart
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAHorizontalBarChart. This class cannot be inherited.
	/// </summary>
	public sealed class WAHorizontalBarChart : WABarChart
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAHorizontalBarChart" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		public WAHorizontalBarChart(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log)
		{
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags()
		{
			return null;
		}
		#endregion Public Methods
	}
}