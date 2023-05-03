// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Translates internal object to Kendo piechart
//
namespace SNC.OptiRamp.Translators
{
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.PageObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Class KendoPieChart.
	/// </summary>
	public sealed class KendoPieChart : TranslatorBase
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="KendoPieChart"/> class.
		/// </summary>
		/// <param name="chart">The chart.</param>
		/// <param name="tagId">The tag identifier.</param>
		public KendoPieChart(WAPieChart chart, string tagId) {
			if (chart == null)
				return;

			DataPointIds = new Dictionary<string, string>();
			int pointNum = 0;
			foreach (var dp in chart.DataPoints) {
				dp.ObjectId = string.Format("{0}!{1}{2}", tagId, pointNum, dp.FixedValue > 0 ? "!" + dp.FixedValue : string.Empty);
				DataPointIds.Add(dp.ObjectId, !string.IsNullOrEmpty(dp.FriendlyName) ? dp.FriendlyName : dp.Legend);
				pointNum++;
			}

			var seriesFormat = "{{ category: \"{0}\", value: 1, color: \"#{1}\" }}";
			var sb = new StringBuilder();
			foreach (var key in DataPointIds.Keys) {
				if (sb.Length > 0)
					sb.Append(",");
				var point = chart.DataPoints.FirstOrDefault(x => x.Legend.Equals(DataPointIds[key]) || x.FriendlyName.Equals(DataPointIds[key]));
				if (point == null)
					continue;
				var color = point.Background.Color1.ToHexString(false);
				sb.AppendFormat(seriesFormat, DataPointIds[key], color);
			}

			var labelTemplate = chart.IsLegendVisible ? "\"#=value#\"" : "\"#=category#\\n(#=value#)\"";
			formatValues = new Dictionary<int, object>();
			formatValues.Add(0, tagId);
			formatValues.Add(1, chart.Header == null ? string.Empty : chart.Header.Text);
			formatValues.Add(2, sb);
			formatValues.Add(3, chart.IsLegendVisible.ToString().ToLower());
			formatValues.Add(4, chart.ArePointLabelsVisible.ToString().ToLower());
			formatValues.Add(5, labelTemplate);
			formatValues.Add(6, chart.Header.Brush.Color1.ToHexString(false));
			formatValues.Add(7, chart.Header.Font == null ? string.Empty : chart.Header.Font.FontSize.ToString() + "pt");
			formatValues.Add(8, chart.Header.Font == null ? string.Empty : string.Join(",", chart.Header.Font.FontFamilies));
			formatValues.Add(9, chart.Header.Font == null || chart.Header.Font.FontStyle == Enumerations.FontStyles.Normal ? string.Empty : chart.Header.Font.FontStyle.ToString());
			formatValues.Add(10, (!chart.IsLegendVisible).ToString().ToLower());
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() {
			var vals = formatValues.Values;
			return string.Format(_Template, vals.ToArray());
		}
		#endregion Public Methods

		#region Private Fields
		private string _Template = 
			"$(\"#{0}\").kendoChart({{" + nl +
				t1 + "transitions: false," + nl +
				t1 + "chartArea: {{" + nl +
					t2 + "opacity: 0" + nl +
				t1 + "}}," + nl +
				t1 + "title: {{" + nl +
					t2 + "position: \"bottom\"," + nl +
					t2 + "text: \"{1}\"," + nl +
					t2 + "align: \"center\"," + nl +
					t2 + "color: \"#{6}\"," + nl +
					t2 + "font: \"{9} {7} {8}\"" + nl +
				t1 + "}}," + nl +
				t1 + "legend: {{" + nl +
					t2 + "visible: {4}" + nl +
				t1 + "}}," + nl +
				t1 + "chartArea: {{" + nl +
					t2 + "background: \"\"," + nl +
					t2 + "margin: 15" + nl +
				t1 + "}}," + nl +
				t1 + "seriesDefaults: {{" + nl +
					t2 + "type: \"pie\"," + nl +
					t2 + "startAngle: 90," + nl +
					t2 + "labels: {{" + nl +
						t3 + "visible: {3}," + nl +
						t3 + "background: \"transparent\"," + nl +
						t3 + "template: {5}" + nl +
					t2 + "}}" + nl +
				t1 + "}}," + nl +
				t1 + "tooltip: {{" + nl +
					t2 + "visible: {10}" + nl +
				t1 + "}}," + nl +
				t1 + "series: [{{" + nl +
					t2 + "data: [{2}]" + nl +
				t1 + "}}]" + nl +
			"}});";
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the data point ids.
		/// </summary>
		/// <value>The data point ids.</value>
		public Dictionary<string, string> DataPointIds { get; set; }
		#endregion Public Properties
	}
}