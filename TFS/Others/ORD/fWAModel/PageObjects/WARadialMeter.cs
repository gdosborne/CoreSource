// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Radial meter
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Class WARadialMeter. This class cannot be inherited.
	/// </summary>
	public sealed class WARadialMeter : WAMeter
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
		public WARadialMeter(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log)
		{
			ObjectType = Enumerations.ObjectTypes.RadialMeter;
			element.Children.ToList().ForEach(child =>
			{
				var elem = source.GetElemById(child);
				IsTickVisible = elem.GetPropertyValue<bool>(TypeIDs.FACEPLATETICKVISIBILITY, true);
				if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.FACEPLATETICKFOREGROUND, string.Empty)))
					TickForeground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.FACEPLATETICKFOREGROUND, string.Empty));
				if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.SPEEDOMETERBACKGROUND, string.Empty)))
					BackgroundBrush = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.SPEEDOMETERBACKGROUND, string.Empty));
				if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.NUMBEROFMAJORTICKS, string.Empty)))
					NumberOfMajorTicks = elem.GetPropertyValue<double>(TypeIDs.NUMBEROFMAJORTICKS, 10.0);


			});
		}
		#endregion Public Constructors
		/// <summary>
		/// Gets or sets the tick foreground.
		/// </summary>
		/// <value>The tick foreground.</value>
		public WABrush TickForeground { get; set; }
		/// <summary>
		/// Gets or sets the is tick visible.
		/// </summary>
		/// <value>The is tick visible.</value>
		public bool IsTickVisible { get; set; }
		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			return base.ToTags();
		}
		#endregion Public Methods
	}
}