// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Pie chart
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fRT;
	using SNC.OptiRamp.Translators;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System.Windows.Media;

	/// <summary>
	/// Class WAPieChart.
	/// </summary>
	public sealed class WAPieChart : WADataPointsChart
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAPieChart" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		public WAPieChart(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.PieChart;

			if (!element.Children.Any()) {
				NonCriticalErrors.Add(string.Format("Piechart at {0} on page {1} not configured", Location, Page.Name));
				return;
			}
			var totalValue = element.GetPropertyValue<double>(TypeIDs.FIXEDTOTAL, 0.0);
			element.Children.ToList().ForEach(child =>
			{
				var elem = source.GetElemById(child);
				if (elem.Type.TypeID.Equals(TypeIDs.TITLE, StringComparison.OrdinalIgnoreCase)) {
					WABrush brush = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.TEXTBRUSH, string.Empty));
					WAFont font = project.GetFontByName(elem.GetPropertyValue<string>(TypeIDs.FONT, string.Empty));
					string text = elem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty);
					if (elem.Name.Equals("Header", StringComparison.OrdinalIgnoreCase)) {
						Header = new WATitle {
							Brush = brush,
							Font = font,
							IsVisible = true,
							Text = text
						};
					}
				}
				else if (elem.Type.TypeID.Equals(TypeIDs.DATAPOINT, StringComparison.OrdinalIgnoreCase)) {
					var dp = new WADataPoint();
					dp.Background = project.Brushes.FirstOrDefault(x => x.Name.Equals(elem.GetPropertyValue<string>(TypeIDs.FILLBRUSH, string.Empty), StringComparison.OrdinalIgnoreCase));
					dp.Foreground = project.Brushes.FirstOrDefault(x => x.Name.Equals(elem.GetPropertyValue<string>(TypeIDs.TEXTBRUSH, string.Empty), StringComparison.OrdinalIgnoreCase));
					dp.IsPersisted = elem.GetPropertyValue<bool>(TypeIDs.PERSIST, false);
					dp.FriendlyName = elem.GetPropertyValue<string>(TypeIDs.FRIENDLYNAME, string.Empty);
					VTSTag tag = null;
					if (elem.Peers.Any()) {
						var elem1 = source.GetElemById(elem.Peers.FirstOrDefault().elemID);
						dp.Legend = elem1.Name;
						WAComputer c = null;
						WAServer s = null;
						string u = null;
						project.GetComputerForTag(elem1, out c, out s, out u);
						tag = new VTSTag {
							Computer = c,
							Server = s,
							APIUrl = u,
							ElementId = elem1.Id,
							Name = elem1.Name,
							UniqueId = elem1.GetPropertyValue<int>(TypeIDs.UID, 0),
							LocalId = Guid.NewGuid(),
							Owner = this
						};
					}
					else if (totalValue > 0) {
						dp.FixedValue = totalValue;
						dp.FriendlyName = "Remainder**";
						dp.Legend = dp.FriendlyName;
						tag = new VTSTag {
							APIUrl = "ignore",
							ElementId = elem.Id,
							Name = elem.Name,
							UniqueId = elem.GetPropertyValue<int>(TypeIDs.UID, 0),
							LocalId = Guid.NewGuid(),
							Owner = this
						};
					}
					if (tag != null) {
						Tags.Add(tag.Name, tag);
						dp.LocalId = tag.LocalId;
						DataPoints.Add(dp);
					}
				}
			});
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			var control = new KendoPieChart(this, PageId).ToString();
			DocumentReadyScripts.Append(control);

			var result = new List<TagItem>();
			var styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<string>("position", "absolute"));
			styleSb.Append(GetStyleEntry<double>("left", Location.X, "0.0", true));
			styleSb.Append(GetStyleEntry<double>("top", Location.Y, "0.0", true));
			if (BackgroundBrush != null)
				styleSb.Append(GetStyleEntry<Color>("background-color", BackgroundBrush.Color1));
			if (Border > 0) {
				styleSb.Append(GetStyleEntry("border", Border.ToString() + "px Solid #" + BorderBrush.Color1.ToHexString(false)));
			}
			var outerDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"style", styleSb.ToString()},
					{"selectionoffset", "5"},
					{"oncontextmenu", "return false;"}
				}, false, false, true);
			styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<double>("width", Size.Width, "0.0", true));
			styleSb.Append(GetStyleEntry<double>("height", Size.Height, "0.0", true));
			var chartDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"style", styleSb.ToString()}
				});

			outerDiv.Tags.Add(chartDiv);
			result.Add(outerDiv);
			return result;
		}
		#endregion Public Methods
	}
}