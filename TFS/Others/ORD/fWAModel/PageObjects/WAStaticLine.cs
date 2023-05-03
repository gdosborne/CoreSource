// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Static Line
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Application.Extensions.Media;
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
	/// Enum JoinCaps
	/// </summary>
	public enum JoinCaps {
		/// <summary>
		/// The miter
		/// </summary>
		Miter,
		/// <summary>
		/// The round
		/// </summary>
		Round,
		/// <summary>
		/// The bevel
		/// </summary>
		Bevel
	}
	/// <summary>
	/// Enum LineCaps
	/// </summary>
	public enum LineCaps {
		/// <summary>
		/// The round
		/// </summary>
		Round,
		/// <summary>
		/// The square
		/// </summary>
		Square,
		/// <summary>
		/// The butt
		/// </summary>
		Butt
	}

	/// <summary>
	/// Class WAStaticLine. This class cannot be inherited.
	/// </summary>
	public sealed class WAStaticLine : WASimpleShape {
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
		public WAStaticLine(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.StaticLine;
			Segments = new List<Point>();
			IsDashed = element.GetPropertyValue<bool>(TypeIDs.ISDASHED, false);
			Thickness = element.GetPropertyValue<double>(TypeIDs.THICKNESS, 1);
			var cap = element.GetPropertyValue<string>(TypeIDs.STARTENDCAP, "Round");
			if (cap.Equals("Miter", StringComparison.OrdinalIgnoreCase))
				cap = "Butt";
			LineCap = (LineCaps)Enum.Parse(typeof(LineCaps), cap, true);
			JoinCap = JoinCaps.Round; // always
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			List<TagItem> result = new List<TagItem>();
			TagItem polyLine = null;
			StringBuilder data = null;
			StringBuilder sb = null;
			var dashNum = Thickness * 2.5;
			if (Border > 0) {
				data = new StringBuilder();
				data.AppendFormat("{0},{1} ", Location.X, Location.Y);
				foreach (var seg in Segments) {
					data.AppendFormat("{0},{1} ", seg.X, seg.Y);
				}
				sb = new StringBuilder();
				sb.Append(GetStyleEntry<string>("fill", "none"));
				sb.Append(GetStyleEntry<Color>("stroke", BorderBrush.Color1));
				sb.Append(GetStyleEntry<double>("stroke-width", (Thickness + (2 * Border))));
				sb.Append(GetStyleEntry<object>("stroke-linecap", LineCap));
				sb.Append(GetStyleEntry<object>("stroke-linejoin", JoinCap));
				if (IsDashed) {
					sb.AppendFormat("{0}:{1} {1};", "stroke-dasharray", dashNum);
				}
				char sequence = 'A';
				polyLine = TagItem.CreateTag(TagItem.TagTypes.polyline, null,
					new Dictionary<string, string>
					{
						{"id", PageId + "-" + sequence},
						{"points", data.ToString()},
						{"style", sb.ToString()}
					}, true, false, false);
				result.Add(polyLine);
			}
			data = new StringBuilder();
			data.AppendFormat("{0},{1} ", Location.X, Location.Y);
			foreach (var seg in Segments) {
				data.AppendFormat("{0},{1} ", seg.X, seg.Y);
			}
			sb = new StringBuilder();
			sb.Append(GetStyleEntry<string>("fill", "none"));
			sb.Append(GetStyleEntry<Color>("stroke", BackgroundBrush.Color1));
			sb.Append(GetStyleEntry<double>("stroke-width", Thickness));
			sb.Append(GetStyleEntry<object>("stroke-linecap", LineCap));
			sb.Append(GetStyleEntry<object>("stroke-linejoin", JoinCap));
			if (IsDashed) {
				sb.AppendFormat("{0}:{1} {1};", "stroke-dasharray", dashNum);
			}
			polyLine = TagItem.CreateTag(TagItem.TagTypes.polyline, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"points", data.ToString()},
					{"style", sb.ToString()},
					{"borderthickness", Border.ToString()}
				});

			result.Add(polyLine);
			if (StartConnector == null)
				result.AddRange(StartConnector.ToTags());
			if (EndConnector == null)
				result.AddRange(EndConnector.ToTags());
			return result;
		}
		#endregion Public Methods

		#region Private Methods
		private TagItem GetLineSegment(Point p1, Point p2) {
			var line = TagItem.CreateTag(TagItem.TagTypes.line, null,
				new Dictionary<string, string>
				{
					{"x1", p1.X.ToString()},
					{"y1", p1.Y.ToString()},
					{"x2", p2.X.ToString()},
					{"y2", p2.Y.ToString()},
					{"stroke-width", Border.ToString()},
					{"stroke", BorderBrush.Color1.ToHexString(true)}
				});
			return line;
		}
		#endregion Private Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the end connector.
		/// </summary>
		/// <value>The end connector.</value>
		public WAConnector EndConnector {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is dashed.
		/// </summary>
		/// <value><c>true</c> if this instance is dashed; otherwise, <c>false</c>.</value>
		public bool IsDashed {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the end line cap.
		/// </summary>
		/// <value>The end line cap.</value>
		public JoinCaps JoinCap {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the segments.
		/// </summary>
		/// <value>The segments.</value>
		public List<Point> Segments {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the start connector.
		/// </summary>
		/// <value>The start connector.</value>
		public WAConnector StartConnector {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the start line cap.
		/// </summary>
		/// <value>The start line cap.</value>
		public LineCaps LineCap {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the thickness.
		/// </summary>
		/// <value>The thickness.</value>
		public double Thickness {
			get;
			set;
		}
		#endregion Public Properties
	}
}