// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// Static image
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
	/// Class WAStaticImage. This class cannot be inherited.
	/// </summary>
	public sealed class WAStaticImage : WAImage
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAStaticImage" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		public WAStaticImage(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log)
		{
			ObjectType = Enumerations.ObjectTypes.StaticImage;
			if (element.GetPropertyValue<string>(TypeIDs.IMAGE, string.Empty) != null && project.Pictures.Any())
			{
				var pic = project.Pictures.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(TypeIDs.IMAGE, string.Empty), StringComparison.OrdinalIgnoreCase));
				if (pic != null)
					Picture = pic;
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags()
		{
			var svgs = base.ToTags();
			if (Picture != null)
			{
				var svg = FindImageTag(svgs);
				if (svg != null)
					svg.Attributes["xlink:href"] = Picture.ImageUrl;
			}
			return svgs;
		}
		private TagItem FindImageTag(List<TagItem> svgs)
		{
			foreach (var svg in svgs)
			{
				if (svg.TagType == TagItem.TagTypes.image)
					return svg;
				if (svg.Tags != null && svg.Tags.Any())
					return FindImageTag(svg.Tags);
			}
			return null;
		}
		public WAPicture Picture { get; set; }
		#endregion Public Methods
	}
}