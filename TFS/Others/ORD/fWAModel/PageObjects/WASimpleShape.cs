// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Simple Shape
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
	/// Class WASimpleShape.
	/// </summary>
	public abstract class WASimpleShape : WAObject
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
		public WASimpleShape(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log)
		{
			//set values
			TrendChartTemplateName = element.GetPropertyValue<string>(TypeIDs.TEMPLATENAME, string.Empty);
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		public string TrendChartTemplateName { get; set; }
		#endregion Public Properties
	}
}