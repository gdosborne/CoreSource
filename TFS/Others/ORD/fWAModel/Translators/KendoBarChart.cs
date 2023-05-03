// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Translates internal object to Kendo barchart
//
namespace SNC.OptiRamp.Translators
{
	using SNC.OptiRamp.PageObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using SNC.OptiRamp.Application.Extensions.Media;

	/// <summary>
	/// Class KendoBarChart.
	/// </summary>
	public sealed class KendoBarChart : TranslatorBase
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="KendoBarChart"/> class.
		/// </summary>
		/// <param name="chart">The chart.</param>
		/// <param name="tagId">The tag identifier.</param>
		public KendoBarChart(WABarChart chart, string tagId) {
			if (chart == null)
				return;

			DataPointIds = new Dictionary<string, string>();
			int pointNum = 0;
			foreach (var dp in chart.DataPoints) {
				dp.ObjectId = string.Format("{0}!{1}", tagId, pointNum);
				DataPointIds.Add(!string.IsNullOrEmpty(dp.FriendlyName) ? dp.FriendlyName : dp.ObjectId, dp.Legend);
				pointNum++;
			}

			var seriesFormat = "{{ name: \"{0}\", data: [0], color: \"#{1}\", labels: {{ visible: {2} }}}}";
			var sb = new StringBuilder();
			foreach (var key in DataPointIds.Keys) {
				if (sb.Length > 0)
					sb.Append(",");
				var color = chart.DataPoints.FirstOrDefault(x => x.Legend.Equals(DataPointIds[key])).Background.Color1.ToHexString(false);
				sb.AppendFormat(seriesFormat, DataPointIds[key], color, chart.ArePointLabelsVisible.ToString().ToLower());
			}

			formatValues = new Dictionary<int, object>();
			formatValues.Add(0, tagId);
			formatValues.Add(1, chart.Header.Text);
			formatValues.Add(2, chart.Minimum);
			formatValues.Add(3, chart.Maximum);
			formatValues.Add(4, sb);
			formatValues.Add(5, chart.XAxis.Text);
			formatValues.Add(6, chart.XAxis.Brush.Color1.ToHexString(false));
			formatValues.Add(7, chart.Header.Brush.Color1.ToHexString(false));
			formatValues.Add(8, chart.YAxis.Text);
			formatValues.Add(9, chart.YAxis.Brush.Color1.ToHexString(false));
			formatValues.Add(10, chart.Header.Font.FontSize.ToString() + "pt");
			formatValues.Add(11, string.Join(",", chart.Header.Font.FontFamilies));
			formatValues.Add(12, chart.YAxis.Font.FontSize.ToString() + "pt");
			formatValues.Add(13, string.Join(",", chart.YAxis.Font.FontFamilies));
			formatValues.Add(14, chart.XAxis.Font.FontSize.ToString() + "pt");
			formatValues.Add(15, string.Join(",", chart.XAxis.Font.FontFamilies));
			formatValues.Add(16, chart.AreGridlinesVisible.ToString().ToLower());
			formatValues.Add(17, chart.GridLineThickness);
			formatValues.Add(18, chart.GridLineThickness / 2);
			formatValues.Add(19, chart.GridLineBrush.Color1.ToHexString(false));
			formatValues.Add(20, chart.IsLegendVisible.ToString().ToLower());
			formatValues.Add(21, chart.Header.Font.FontStyle == Enumerations.FontStyles.Bold ? chart.Header.Font.FontStyle.ToString() : string.Empty);
			formatValues.Add(22, chart.XAxis.Font.FontStyle == Enumerations.FontStyles.Bold ? chart.XAxis.Font.FontStyle.ToString() : string.Empty);
			formatValues.Add(23, chart.YAxis.Font.FontStyle == Enumerations.FontStyles.Bold ? chart.YAxis.Font.FontStyle.ToString() : string.Empty);
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
					t2 + "text: \"{1}\"," + nl +
					t2 + "color: \"#{7}\"," + nl +
					t2 + "font: \"{21} {10} {11}\"" + nl +
				t1 + "}}," + nl +
				t1 + "legend: {{" + nl +
					t2 + "visible: {20}" + nl +
				t1 + "}}," + nl +
				t1 + "series: [{4}]," + nl +
				t1 + "categoryAxis: {{" + nl +
					t2 + "title: {{" + nl +
						t3 + "text: \"{8}\"," + nl +
						t3 + "color: \"#{9}\"," + nl +
						t3 + "font: \"{23} {12} {13}\"" + nl +
					t2 + "}}," + nl +
					t2 + "line: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {17}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}," + nl +
					t2 + "majorGridLines: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {17}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}," + nl +
					t2 + "minorGridLines: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {18}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}," + nl +
				t1 + "}}," + nl +
				t1 + "valueAxis: {{" + nl +
					t2 + "title: {{" + nl +
						t3 + "text: \"{5}\"," + nl +
						t3 + "color: \"#{6}\"," + nl +
						t3 + "font: \"{22} {14} {15}\"" + nl +
					t2 + "}}," + nl +
					t2 + "min: {2}," + nl +
					t2 + "max: {3}," + nl +
					t2 + "line: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {17}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}," + nl +
					t2 + "majorGridLines: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {17}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}," + nl +
					t2 + "minorGridLines: {{" + nl +
						t3 + "visible: {16}," + nl +
						t3 + "width: {18}," + nl +
						t3 + "color: \"#{19}\"" + nl +
					t2 + "}}" + nl +
				t1 + "}}" + nl +
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