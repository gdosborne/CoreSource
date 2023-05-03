// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Chart containing datapoints
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WADataPointsChart.
	/// </summary>
	public abstract class WADataPointsChart : WAChart, IWAMultiValueDynamicObject
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAObject" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		public WADataPointsChart(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			Tags = new Dictionary<string, VTSTag>();
			DataPoints = new List<WADataPoint>();
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public abstract override List<TagItem> ToTags();
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the data points.
		/// </summary>
		/// <value>The data points.</value>
		public List<WADataPoint> DataPoints { get; set; }
		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>The header.</value>
		public WATitle Header { get; set; }
		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <value>The tags.</value>
		public IDictionary<string, VTSTag> Tags { get; private set; }
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		public string TrendChartTemplateName { get; set; }
		#endregion Public Properties
	}
}