// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// Tag
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Class Tag.
	/// </summary>
	public class TagItem
	{
		private TagItem(TagTypes tagType) {
			TagType = tagType;
			Classes = new List<string>();
			Tags = new List<TagItem>();
			Attributes = new Dictionary<string, string>();
			IsSvg = true;
			HasProperties = true;
		}
		private TagItem(TagTypes tagType, bool isInline)
			: this(tagType) {
			IsInline = isInline;
		}
		private TagItem(TagTypes tagType, string id)
			: this(tagType) {
			Id = id;
		}
		private TagItem(TagTypes tagType, string id, bool isInline)
			: this(tagType, isInline) {
			Id = id;
		}

		#region Public Methods
		/// <summary>
		/// Creates the tag.
		/// </summary>
		/// <param name="tagType">Type of the tag.</param>
		/// <returns>TagItem.</returns>
		public static TagItem CreateTag(TagTypes tagType) {
			return CreateTag(tagType, null, null, true, false, false, string.Empty);
		}
		/// <summary>
		/// Creates the tag.
		/// </summary>
		/// <param name="tagType">Type of the tag.</param>
		/// <param name="classes">The classes.</param>
		/// <param name="attributes">The attributes.</param>
		/// <returns>TagItem.</returns>
		public static TagItem CreateTag(TagTypes tagType, List<string> classes, Dictionary<string, string> attributes) {
			return CreateTag(tagType, classes, attributes, true, false, false, string.Empty);
		}
		/// <summary>
		/// Creates the tag.
		/// </summary>
		/// <param name="tagType">Type of the tag.</param>
		/// <param name="classes">The classes.</param>
		/// <param name="attributes">The attributes.</param>
		/// <param name="isSvg">if set to <c>true</c> [is SVG].</param>
		/// <param name="isInline">if set to <c>true</c> [is inline].</param>
		/// <param name="hasProperties">if set to <c>true</c> [has properties].</param>
		/// <returns>TagItem.</returns>
		public static TagItem CreateTag(TagTypes tagType, List<string> classes, Dictionary<string, string> attributes, bool isSvg, bool isInline, bool hasProperties) {
			return CreateTag(tagType, classes, attributes, isSvg, isInline, hasProperties, string.Empty);
		}
		/// <summary>
		/// Creates the tag.
		/// </summary>
		/// <param name="tagType">Type of the tag.</param>
		/// <param name="classes">The classes.</param>
		/// <param name="attributes">The attributes.</param>
		/// <param name="isSvg">if set to <c>true</c> [is SVG].</param>
		/// <param name="isInline">if set to <c>true</c> [is inline].</param>
		/// <param name="hasProperties">if set to <c>true</c> [has properties].</param>
		/// <param name="id">The identifier.</param>
		/// <returns>TagItem.</returns>
		public static TagItem CreateTag(TagTypes tagType, List<string> classes, Dictionary<string, string> attributes, bool isSvg, bool isInline, bool hasProperties, string id) {
			var result = new TagItem(tagType)
			{
				Id = id,
				IsInline = isInline,
				IsSvg = isSvg,
				HasProperties = hasProperties,
			};
			if (classes != null)
				result.Classes.AddRange(classes);
			if (attributes != null) {
				attributes.Keys.ToList().ForEach(x => result.Attributes.Add(x, attributes[x]));
			}
			return result;
		}
		public static TagItem GetDropShadowTagItem() {
			var defs = new TagItem(TagTypes.defs);
			var filter = new TagItem(TagTypes.filter);
			filter.Attributes.Add("id", "dropShadow");
			var gBlur = new TagItem(TagTypes.feGaussianBlur);
			gBlur.Attributes.Add("in", "SourceAlpha");
			gBlur.Attributes.Add("stdDeviation", "3");
			var offset = new TagItem(TagTypes.feOffset);
			offset.Attributes.Add("dx", "2");
			offset.Attributes.Add("dy", "4");
			var merge = new TagItem(TagTypes.feMerge);
			var node1 = new TagItem(TagTypes.feMergeNode);
			var node2 = new TagItem(TagTypes.feMergeNode);
			node2.Attributes.Add("in", "SourceGraphic");
			merge.Tags.Add(node1);
			merge.Tags.Add(node2);
			filter.Tags.Add(gBlur);
			filter.Tags.Add(offset);
			filter.Tags.Add(merge);
			defs.Tags.Add(filter);
			return defs;
		}
		
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() {
			var sb = new StringBuilder();
			sb.Append("<");
			sb.Append(TagType.ToString());
			if (!string.IsNullOrEmpty(Id))
				sb.Append(Id + " ");
			if (Classes.Any()) {
				sb.Append(" class=\"");
				for (int i = 0; i < Classes.Count; i++) {
					sb.Append(Classes[i]);
					if (i < Classes.Count - 1)
						sb.Append(" ");
				}
				sb.Append("\"");
			}
			if (Attributes.Any()) {
				foreach (var attr in Attributes) {
					sb.Append(string.Format(" {0}=\"{1}\"", attr.Key, attr.Value));
				}
			}
            if (TagType.ToString().Equals(TagTypes.input.ToString(), StringComparison.OrdinalIgnoreCase))
                sb.AppendLine(" />");
            else
            {
                sb.Append(">");
                if (!string.IsNullOrEmpty(Text))
                    sb.Append(Text);
                if (Tags.Any())
                {
                    sb.AppendLine();
                    Tags.ForEach(x => sb.Append(x.ToString()));
                }
                sb.Append("</");
                sb.Append(TagType.ToString().ToLower());
                if (IsInline)
                    sb.Append(">");
                else
                    sb.AppendLine(">");
            }
			return sb.ToString();
		}
		#endregion Public Methods

		#region Public Enums
		/// <summary>
		/// Enum TagTypes
		/// </summary>
		public enum TagTypes
		{
			defs,
			filter,
			feGaussianBlur,
			feOffset,
			feMerge,
			feMergeNode,
			a,
			li,
			ul,
			img,
			span,
			div,
			p,
			svg,
			rect,
			text,
			ellipse,
			image,
			table,
			tr,
			td,
			line,
			path,
			polyline,
            input,
            button
		}
		#endregion Public Enums

		#region Public Properties
		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <value>The attributes.</value>
		public Dictionary<string, string> Attributes { get; private set; }
		/// <summary>
		/// Gets the classes.
		/// </summary>
		/// <value>The classes.</value>
		public List<string> Classes { get; private set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance has properties.
		/// </summary>
		/// <value><c>true</c> if this instance has properties; otherwise, <c>false</c>.</value>
		public bool HasProperties { get; set; }
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public string Id { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is inline.
		/// </summary>
		/// <value><c>true</c> if this instance is inline; otherwise, <c>false</c>.</value>
		public bool IsInline { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is SVG.
		/// </summary>
		/// <value><c>true</c> if this instance is SVG; otherwise, <c>false</c>.</value>
		public bool IsSvg { get; set; }
		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <value>The tags.</value>
		public List<TagItem> Tags { get; private set; }
		/// <summary>
		/// Gets the type of the tag.
		/// </summary>
		/// <value>The type of the tag.</value>
		public TagTypes TagType { get; private set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text { get; set; }
		#endregion Public Properties
	}
}