// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Barchart base
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using SNC.OptiRamp.Translators;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Windows.Media;

	/// <summary>
	/// Class WABarChart.
	/// </summary>
	public abstract class WABarChart : WADataPointsChart {
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
		/// <exception cref="ApplicationException">Barchart not configured</exception>
		public WABarChart(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.BarChart;

			AreGridlinesVisible = element.GetPropertyValue<bool>(TypeIDs.SHOWGRIDLINES, false);
			GridLineThickness = element.GetPropertyValue<double>(TypeIDs.GRIDLINETHICKNESS, 0.0);
			if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(TypeIDs.GRIDLINEBRUSH, string.Empty)))
				GridLineBrush = project.Brushes.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(TypeIDs.GRIDLINEBRUSH, string.Empty), StringComparison.OrdinalIgnoreCase));
			Minimum = element.GetPropertyValue<double>(TypeIDs.MINIMUM, 0.0);
			Maximum = element.GetPropertyValue<double>(TypeIDs.MAXIMUM, 100.0);

			if (!element.Children.Any()) {
				NonCriticalErrors.Add(string.Format("Barchart at {0} on page {1} not configured", Location, Page.Name));
				return;
			}
			element.Children.ToList().ForEach(child => {
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
					else if (elem.Name.Equals("XAxis", StringComparison.OrdinalIgnoreCase)) {
						XAxis = new WATitle {
							Brush = brush,
							Font = font,
							IsVisible = true,
							Text = text
						};
					}
					else if (elem.Name.Equals("YAxis", StringComparison.OrdinalIgnoreCase)) {
						YAxis = new WATitle {
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

					if (elem.Peers.Any()) {
						var elem1 = source.GetElemById(elem.Peers.FirstOrDefault().elemID);
						dp.Legend = elem1.Name;
						WAComputer c = null;
						WAServer s = null;
						string u = null;
						project.GetComputerForTag(elem1, out c, out s, out u);
						var tag = new VTSTag {
							Computer = c,
							Server = s,
							APIUrl = u,
							ElementId = elem1.Id,
							Name = elem1.Name,
							UniqueId = elem1.GetPropertyValue<int>(TypeIDs.UID, 0),
							LocalId = Guid.NewGuid(),
							Owner = this
						};

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
			var control = new KendoBarChart(this, PageId).ToString();
			DocumentReadyScripts.Append(control);

			var result = new List<TagItem>();
			var styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<string>("position", "absolute"));
			styleSb.Append(GetStyleEntry<double>("left", Location.X, "0.0", true));
			styleSb.Append(GetStyleEntry<double>("top", Location.Y, "0.0", true));
			if (BackgroundBrush != null)
				styleSb.Append(GetStyleEntry<Color>("background-color", BackgroundBrush.Color1));
			if (Border > 0)
				styleSb.Append(GetStyleEntry<string>("border", Border.ToString("0.0px") + " Solid #" + BorderBrush.Color1.ToHexString(false)));

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

		#region Public Properties
		/// <summary>
		/// Gets or sets a value indicating whether [are gridlines visible].
		/// </summary>
		/// <value><c>true</c> if [are gridlines visible]; otherwise, <c>false</c>.</value>
		public bool AreGridlinesVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the grid line brush.
		/// </summary>
		/// <value>The grid line brush.</value>
		public WABrush GridLineBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the grid line thickness.
		/// </summary>
		/// <value>The grid line thickness.</value>
		public double GridLineThickness {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the maximum.
		/// </summary>
		/// <value>The maximum.</value>
		public double Maximum {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the minimum.
		/// </summary>
		/// <value>The minimum.</value>
		public double Minimum {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the x axis.
		/// </summary>
		/// <value>The x axis.</value>
		public WATitle XAxis {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the y axis.
		/// </summary>
		/// <value>The y axis.</value>
		public WATitle YAxis {
			get;
			set;
		}
		#endregion Public Properties
	}
}