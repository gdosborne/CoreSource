// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Text base
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Application.Extensions.Primitives;
	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows;
	using System.Windows.Media;

	/// <summary>
	/// Class WAText.
	/// </summary>
	public abstract class WAText : WAObject {
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
		public WAText(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			//set defaults
			Alignment = Defaults.TextDefaults.Alignment;
			ForegroundBrush = Defaults.TextDefaults.Foreground;
			ShowEngineeringUnitWhenValueIsNaN = Defaults.TextDefaults.ShowEngineeringUnitWhenValueIsNaN;
			Text = Defaults.TextDefaults.Text;

			double? xValue = null;
			double? yValue = null;
			double? width = null;
			double? height = null;

			//actual values
			xValue = element.GetPropertyValue<double>(TypeIDs.X, 0.0);
			yValue = element.GetPropertyValue<double>(TypeIDs.Y, 0.0);
			width = element.GetPropertyValue<double>(TypeIDs.WIDTH, 0.0);
			height = element.GetPropertyValue<double>(TypeIDs.HEIGHT, 0.0);
			Size = GetSize(width, height);
			Location = GetLocation(xValue, yValue + Size.Height);
			Text = element.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty);
			var font = element.GetPropertyValue<string>(TypeIDs.FONT, string.Empty);
			if (!string.IsNullOrEmpty(font) && project.Fonts.Any(x => x.Name.Equals(font, StringComparison.OrdinalIgnoreCase)))
				Font = project.Fonts.First(x => x.Name.Equals(font, StringComparison.OrdinalIgnoreCase));
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			TagItem t1 = null;
			char sequence = 'A';
			var sb = new StringBuilder();
			sb.Append(GetStyleEntry<double>("stroke-width", Border));
			if (BackgroundBrush != null) {
				sb.Append(GetStyleEntry<Color>("fill", BackgroundBrush.Color1));
				var alpha = BackgroundBrush.Color1.A;
				sb.Append(GetStyleEntry<double>("opacity", alpha / 255));
			}
			if (BorderBrush != null)
				sb.Append(GetStyleEntry<Color>("stroke", BorderBrush.Color1));
			t1 = TagItem.CreateTag(TagItem.TagTypes.rect, null,
				new Dictionary<string, string>{
					{"id", string.Format("{0}-{1}", PageId, sequence)},
					{"x", (Location.X - Border).ToString()},
					{"y", (Location.Y - Border).ToString()},
					{"width", (Size.Width + (Border * 2)).ToString()},
					{"height", (Size.Height + (Border * 2)).ToString()},
					{"rx", "2"},
					{"selectionoffset", "2"},
					{"datatype", "analog"},
					{"style", sb.ToString()}
				});
			if (ShowDropShadow)
				t1.Attributes.Add("filter", "url(#dropShadow)");

			DocumentReadyScripts.AppendFormat("resizeTextElement(\"{0}\", \"{0}-A\", \"#{1}\", \"#{2}\", {3}, {4});", new object[]
			{ 
				PageId, 
				BackgroundBrush.Color1.ToHexString(false), 
				BorderBrush.Color1.ToHexString(false), 
				Border, 
				Size.Width
			});

			sb = new StringBuilder();
			if (ForegroundBrush != null)
				sb.Append(GetStyleEntry<Color>("fill", ForegroundBrush.Color1));
			if (Font != null) {
				sb.Append(GetStyleEntry<string>("font-size", Font.FontSize + Font.Unit.ToString().ToLower()));
				sb.Append(GetStyleEntry<string>("font-family", string.Join(",", Font.FontFamilies)));
				sb.Append(GetStyleEntry<string>("font-weight", Font.FontStyle.ToString()));
			}
			var t2 = TagItem.CreateTag(TagItem.TagTypes.text, null,
				new Dictionary<string, string>{
					{"id", PageId},
					{"x", (Location.X + Border).ToString()},
					{"y", (Location.Y + Border).ToString()},
					{"width", Size.Width.ToString()},
					{"height", Size.Height.ToString()},
					{"rx", "2"},
					{"style", sb.ToString()}
				}, true, false, true);

			if (ShowDropShadow && BackgroundBrush == null)
				t2.Attributes.Add("filter", "url(#dropShadow)");
			t2.Text = string.IsNullOrEmpty(Text) ? "---" : Text;
			if (t1 != null)
				return new List<TagItem> { t1, t2 };
			else
				return new List<TagItem> { t2 };
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the alignment.
		/// </summary>
		/// <value>The alignment.</value>
		public Enumerations.DynamicAlignments Alignment {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public WAFont Font {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the foreground.
		/// </summary>
		/// <value>The foreground.</value>
		public WABrush ForegroundBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether [show engineering unit when value is na n].
		/// </summary>
		/// <value><c>true</c> if [show engineering unit when value is na n]; otherwise, <c>false</c>.</value>
		public bool ShowEngineeringUnitWhenValueIsNaN {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get;
			set;
		}
		#endregion Public Properties
	}
}