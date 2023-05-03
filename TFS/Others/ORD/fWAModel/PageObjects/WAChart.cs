// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Chart base
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAChart.
	/// </summary>
	public abstract class WAChart : WAObject
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
		public WAChart(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
				ArePointLabelsVisible = element.GetPropertyValue<bool>(TypeIDs.SHOWBARVALUES, false);
				IsLegendVisible = element.GetPropertyValue<bool>(TypeIDs.SHOWPOINTLABELS, false);
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			return null;
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether this instance is legend visible.
		/// </summary>
		/// <value><c>true</c> if this instance is legend visible; otherwise, <c>false</c>.</value>
		public bool IsLegendVisible { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [are point labels visible].
		/// </summary>
		/// <value><c>true</c> if [are point labels visible]; otherwise, <c>false</c>.</value>
		public bool ArePointLabelsVisible { get; set; }
		#endregion Public Properties
	}
}