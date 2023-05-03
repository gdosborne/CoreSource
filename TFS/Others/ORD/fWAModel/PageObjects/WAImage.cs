// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// [your comment here]
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;

	/// <summary>
	/// Class WAImage.
	/// </summary>
	public abstract class WAImage : WAObject {
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
		public WAImage(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			var referenceData = GetReference();
			var result = TagItem.CreateTag(TagItem.TagTypes.image, null,
				new Dictionary<string, string>{
					{"id", PageId},
					{"x", Location.X.ToString()},
					{"y", Location.Y.ToString()},
					{"width", Size.Width.ToString("0.0px")},
					{"height", Size.Height.ToString("0.0px")},
					{"selectionoffset", "2"},
					{"xlink:href", string.Empty},
					{"datatype", "discreet"}					
				}, true, false, true);
			if (referenceData != null && referenceData.Any()) {
				result.Attributes.Add("onclick", "selectTreeItemWithPath([" + string.Join(",", referenceData) + "]);");
				result.Attributes.Add("style", GetStyleEntry<string>("cursor", "pointer"));
			}
			if (ShowDropShadow)
				result.Attributes.Add("filter", "url(#dropShadow)");
			return new List<TagItem> { result };
		}
		#endregion Public Methods
	}
}