// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Web Analytics Snapshot Table
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Translators;
	using SNC.OptiRamp.Application.Extensions.Media;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System.Windows.Media;

	/// <summary>
	/// Class WASnapshotTable.
	/// </summary>
	public class WASnapshotTable : WATable {
		/// <summary>
		/// Initializes a new instance of the <see cref="WASnapshotTable" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		public WASnapshotTable(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.SnapshotTable;


		}
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>System.Collections.Generic.List&lt;SNC.OptiRamp.TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			//var grid = new KendoGrid(this, string.Format("grid{0}", PageId.Replace("[", string.Empty).Replace("]", string.Empty)));
			//var control = grid, );
			//DocumentReadyScripts.Append(control);
			//grid.NamedReplacements.Where(x => x.Key.StartsWith("global")).ToList().ForEach(x => AdditionalScriptLines.AppendLine(x.Value));

			var result = new List<TagItem>();
			var sb = new StringBuilder();
			sb.Append(GetStyleEntry<string>("position", "absolute"));
			sb.Append(GetStyleEntry<double>("left", Location.X, "0.0", true));
			sb.Append(GetStyleEntry<double>("top", Location.Y, "0.0", true));
			if (BackgroundBrush != null)
				sb.Append(GetStyleEntry<Color>("background-color", BackgroundBrush.Color1));
			if (Border > 0) {
				sb.Append(GetStyleEntry<double>("padding", 5));
				sb.Append(GetStyleEntry<string>("border", Border.ToString("0.0px") + " Solid #" + BorderBrush.Color1.ToHexString(false)));
				if (CornerRadius > 0)
					sb.Append(GetStyleEntry<double>("border-radius", CornerRadius, "0.0", true));
			}
			var outerDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"style", sb.ToString()},
					{"oncontextmenu", "return false;"}
				}, false, false, true);
			sb = new StringBuilder();
			sb.Append(GetStyleEntry<double>("width", (Size.Width - 10), "0.0", true));
			sb.Append(GetStyleEntry<double>("height", (Size.Height - 10), "0.0", true));
			var tableDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"style", sb.ToString()}
				});

			outerDiv.Tags.Add(tableDiv);
			result.Add(outerDiv);
			return result;
		}
	}
}
