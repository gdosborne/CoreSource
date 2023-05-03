// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Linear meter
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
	/// Class WALinearMeter. This class cannot be inherited.
	/// </summary>
	public sealed class WALinearMeter : WAMeter
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
		public WALinearMeter(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.LinearMeter;

			element.Children.ToList().ForEach(child =>
			{
				var elem = source.GetElemById(child);
				IsArrow = elem.GetPropertyValue<string>(TypeIDs.THUMB, "Bar").Equals("Arrow", StringComparison.OrdinalIgnoreCase);
			});
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			return base.ToTags();
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether this instance is arrow.
		/// </summary>
		/// <value><c>true</c> if this instance is arrow; otherwise, <c>false</c>.</value>
		public bool IsArrow { get; set; }
		#endregion Public Properties
	}
}