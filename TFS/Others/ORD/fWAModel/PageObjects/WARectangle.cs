// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Rectangle
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows.Media;
	using System.Xml.Linq;

	/// <summary>
	/// Class WARectangle.
	/// </summary>
	public class WARectangle : WASimpleShape, IWATag
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
		public WARectangle(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.Rectangle;
			CornerRadius = Defaults.ObjectDefaults.CornerRadius;

			CornerRadius = element.GetPropertyValue<double>(TypeIDs.CORNERRADIUS, 0);
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			var sb = new StringBuilder();
			sb.Append(GetStyleEntry<double>("stroke-width", Border));
			if (BackgroundBrush != null)
				sb.Append(GetStyleEntry<Color>("fill", BackgroundBrush.Color1));
			if (BorderBrush != null)
				sb.Append(GetStyleEntry<Color>("stroke", BorderBrush.Color1));

			var result = TagItem.CreateTag(TagItem.TagTypes.rect, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"x", Location.X.ToString()},
					{"y", Location.Y.ToString()},
					{"width", Size.Width.ToString()},
					{"height", Size.Height.ToString()},
					{"rx", CornerRadius.ToString()},
					{"selectionoffset", "2"},
					{"style", sb.ToString()}
				});
			if (ShowDropShadow)
				result.Attributes.Add("filter", "url(#dropShadow)");
			return new List<TagItem> { result };
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the corner radius.
		/// </summary>
		/// <value>The corner radius.</value>
		public double CornerRadius { get; set; }
		#endregion Public Properties
	}
}