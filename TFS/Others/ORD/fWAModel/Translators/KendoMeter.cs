// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Translates internal object to kendo meter
//
namespace SNC.OptiRamp.Translators
{
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Application.Extensions.Primitives;
	using SNC.OptiRamp.PageObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class KendoMeter.
	/// </summary>
	public sealed class KendoMeter : TranslatorBase
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="KendoMeter"/> class.
		/// </summary>
		/// <param name="meter">The meter.</param>
		/// <param name="tagId">The tag identifier.</param>
		public KendoMeter(WAMeter meter, string tagId) {
			if (meter == null)
				return;

			double max = 100;
			if (meter.Tags.Any(x => x.Key.Equals("Maximum", StringComparison.OrdinalIgnoreCase))) {
				max = meter.NumberOfMajorTicks * 10;
			}
			else {
				max = meter.Maximum;
			}

			formatValues = new Dictionary<int, object>();
			formatValues.Add(0, tagId);
			formatValues.Add(1, meter.ObjectType == Enumerations.ObjectTypes.LinearMeter ? "Linear" : "Radial");
			formatValues.Add(2, meter.Minimum);
			formatValues.Add(3, meter.ThumbBrush.Color1.ToHexString(false));
			formatValues.Add(4, meter is WALinearMeter && meter.As<WALinearMeter>().IsArrow ? "shape: \"arrow\"" : string.Empty);
			formatValues.Add(5, meter.NumberOfMajorTicks);
			formatValues.Add(6, !meter.IsMinorTicksVisible ? meter.NumberOfMajorTicks : meter.NumberOfMajorTicks / 2);
			formatValues.Add(7, meter.Minimum);
			formatValues.Add(8, max);
			formatValues.Add(9, meter.Minimum);
			formatValues.Add(10, meter.LowLow);
			formatValues.Add(11, meter.LowLowBackground.Color1.ToHexString(false));
			formatValues.Add(12, meter.LowLow);
			formatValues.Add(13, meter.Low);
			formatValues.Add(14, meter.LowBackground.Color1.ToHexString(false));
			formatValues.Add(15, meter.High);
			formatValues.Add(16, meter.HighHigh);
			formatValues.Add(17, meter.HighBackground.Color1.ToHexString(false));
			formatValues.Add(18, meter.HighHigh);
			formatValues.Add(19, max);
			formatValues.Add(20, meter.HighHighBackground.Color1.ToHexString(false));
			formatValues.Add(21, meter.MeterFont == null ? string.Empty : meter.MeterFont.FontSize.ToString() + "pt");
			formatValues.Add(22, meter.MeterFont == null ? string.Empty : string.Join(",", meter.MeterFont.FontFamilies));
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
			"$(\"#{0}\").kendo{1}Gauge({{" + nl +
				t1 + "transitions: false," + nl +
				t1 + "pointer: {{" + nl +
					t2 + "value: {2}," + nl +
					t2 + "color: \"#{3}\"," + nl +
					t2 + "{4}" + nl +
				t1 + "}}," + nl +
				t1 + "scale: {{" + nl +
					t2 + "mirror: true," + nl +
					t2 + "majorUnit: {5}," + nl +
					t2 + "minorUnit: {6}," + nl +
					t2 + "min: {7}," + nl +
					t2 + "max: {8}," + nl +
					t2 + "vertical: true," + nl +
					t2 + "labels: {{" + nl +
						t3 + "font: \"{21} {22}\"" + nl +
					t2 + "}}," + nl +
					t2 + "ranges: [" + nl +
						t3 + "{{" + nl +
							t4 + "from: {9}," + nl +
							t4 + "to: {10}," + nl +
							t4 + "color: \"#{11}\"" + nl +
						t3 + "}},{{" + nl +
							t4 + "from: {12}," + nl +
							t4 + "to: {13}," + nl +
							t4 + "color: \"#{14}\"" + nl +
						t3 + "}},{{" + nl +
							t4 + "from: {15}," + nl +
							t4 + "to: {16}," + nl +
							t4 + "color: \"#{17}\"" + nl +
						t3 + "}},{{" + nl +
							t4 + "from: {18}," + nl +
							t4 + "to: {19}," + nl +
							t4 + "color: \"#{20}\"" + nl +
						t3 + "}}" + nl +
					t2 + "]" + nl +
				t1 + "}}" + nl +
			"}});";
		#endregion Private Fields
	}
}