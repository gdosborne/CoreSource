// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Meter base class
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using SNC.OptiRamp.Translators;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows.Media;

	/// <summary>
	/// Class WAMeter.
	/// </summary>
	public abstract class WAMeter : WAObject, IWAMultiValueDynamicObject {
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
		public WAMeter(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			Tags = new Dictionary<string, VTSTag>();

			SetDefaults();
			SetValueTag(element, project, source, true);

			element.Children.ToList().ForEach(child => {
				var elem = source.GetElemById(child);
				if (elem.Name.Equals("HEADER", StringComparison.OrdinalIgnoreCase))
					SetHeader(project, elem);
				else {
					SetValues(elem, source);
					SetTags(project, source, elem);
					SetProperties(project, elem);
				}
			});
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			var control = new KendoMeter(this, PageId + "-meter").ToString();
			DocumentReadyScripts.Append(control);

			var result = new List<TagItem>();
			var styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<string>("position", "absolute"));
			styleSb.Append(GetStyleEntry<double>("left", Location.X, "0.0", true));
			styleSb.Append(GetStyleEntry<double>("top", Location.Y, "0.0", true));
			if (BackgroundBrush != null)
				styleSb.Append(GetStyleEntry<Color>("background-color", BackgroundBrush.Color1));
			if (Border > 0) {
				styleSb.Append(GetStyleEntry("border", Border.ToString("0.0px") + " Solid #" + BorderBrush.Color1.ToHexString(false)));
				if (CornerRadius > 0)
					styleSb.Append(GetStyleEntry<double>("border-radius", CornerRadius, "0.0", true));
			}
			var outerDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"defaultcolor", "#" + BackgroundBrush.Color1.ToHexString(false)},
					{"style", styleSb.ToString()},
					{"selectionoffset", "5"},
					{"oncontextmenu", "return false;"}
				}, false, false, true);
			styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<double>("width", (Size.Width - 10), "0.0", true));
			styleSb.Append(GetStyleEntry<double>("height", (Size.Height - 10), "0.0", true));
			var meterDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"id", PageId + "-meter"},
					{"style", styleSb.ToString()}
				});

			outerDiv.Tags.Add(meterDiv);
			result.Add(outerDiv);
			return result;
		}
		#endregion Public Methods

		#region Private Methods
		private IElement GetPeerForValue(IProject source, IElement parent, string name, bool ignoreName = false) {
			IElement result = null;
			if (parent == null)
				return result;
			parent.Peers.ToList().ForEach(x => {
				if (result != null)
					return;
				var temp = source.GetElemById(x.elemID);
				if (temp != null) {
					if (ignoreName) {
						result = temp;
						return;
					}
					else if (temp.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) {
						result = temp;
						return;
					}
				}
			});
			return result;
		}
		private VTSTag GetTag(WAProject project, IProject source, IElement parent, string name, bool ignoreName = false) {
			if (parent == null)
				return null;
			var pElem = GetPeerForValue(source, parent, name, ignoreName);
			if (pElem == null)
				return null;
			WAComputer c = null;
			WAServer s = null;
			string u = null;
			project.GetComputerForTag(pElem, out c, out s, out u);
			var result = new VTSTag {
				ElementId = pElem.Id,
				Name = name,
				UniqueId = pElem.GetPropertyValue<int>(TypeIDs.UID, 0),
				LocalId = Guid.NewGuid(),
				Owner = this,
				Computer = c,
				Server = s,
				APIUrl = u
			};
			return result;
		}
		private void SetDefaults() {
			ThumbBrush = Defaults.ObjectDefaults.Foreground;
			NumberOfMajorTicks = 10;
			IsMinorTicksVisible = false;
			Minimum = 0;
			Maximum = 100;
			LowLow = 5;
			Low = 15;
			High = 85;
			HighHigh = 95;
			LowLowBackground = Defaults.ObjectDefaults.Foreground;
			LowBackground = Defaults.ObjectDefaults.Foreground;
			HighHighBackground = Defaults.ObjectDefaults.Foreground;
			HighBackground = Defaults.ObjectDefaults.Foreground;
		}
		private void SetHeader(WAProject project, IElement elem) {
			Header = new WATitle();
			Header.Font = project.GetFontByName(elem.GetPropertyValue<string>(TypeIDs.FONT, string.Empty));
			Header.Brush = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.TEXTBRUSH, string.Empty));
			Header.Text = elem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty);
			Header.IsVisible = elem.GetPropertyValue<bool>(TypeIDs.TITLEVISIBILITY, false);
		}
		private void SetProperties(WAProject project, IElement elem) {
			Border = elem.GetPropertyValue<double>(TypeIDs.BORDERTHICKNESS, 0.0);
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.BORDERBRUSH, string.Empty)))
				BorderBrush = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.BORDERBRUSH, string.Empty));
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.THUMBFILL, string.Empty)))
				ThumbBrush = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.THUMBFILL, string.Empty));
			CornerRadius = elem.GetPropertyValue<double>(TypeIDs.CORNERRADIUS, 0.0);

			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.SCALEFONT, string.Empty)))
				MeterFont = project.Fonts.FirstOrDefault(x => x.Name.Equals(elem.GetPropertyValue<string>(TypeIDs.SCALEFONT, string.Empty), StringComparison.OrdinalIgnoreCase));
			IsAdjustable = elem.GetPropertyValue<bool>(TypeIDs.ISADJUSTABLE, false);
			IsValueVisible = elem.GetPropertyValue<bool>(TypeIDs.VALUEVISIBILITY, true);
			IsMinorTicksVisible = elem.GetPropertyValue<bool>(TypeIDs.SHOWMINORTICKS, true);
			NumberOfMajorTicks = elem.GetPropertyValue<double>(TypeIDs.NUMBEROFMAJORTICKS, 10.0);
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.VALUEFOREGROUNDBRUSH, string.Empty)))
				ValueForeground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.VALUEFOREGROUNDBRUSH, string.Empty));
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.LOWBRUSH, string.Empty)))
				LowBackground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.LOWBRUSH, string.Empty));
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.LOWLOWBRUSH, string.Empty)))
				LowLowBackground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.LOWLOWBRUSH, string.Empty));
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.HIGHBRUSH, string.Empty)))
				HighBackground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.HIGHBRUSH, string.Empty));
			if (!string.IsNullOrEmpty(elem.GetPropertyValue<string>(TypeIDs.HIGHHIGHBRUSH, string.Empty)))
				HighHighBackground = project.GetBrushByName(elem.GetPropertyValue<string>(TypeIDs.HIGHHIGHBRUSH, string.Empty));
			IsLowVisible = elem.GetPropertyValue<bool>(TypeIDs.LOWBRUSHVISIBILITY, true);
			IsLowLowVisible = elem.GetPropertyValue<bool>(TypeIDs.LOWLOWBRUSHVISIBILITY, true);
			IsHighVisible = elem.GetPropertyValue<bool>(TypeIDs.HIGHBRUSHVISIBILITY, true);
			IsHighHighVisible = elem.GetPropertyValue<bool>(TypeIDs.HIGHHIGHBRUSHVISIBILITY, true);
		}
		private void SetTags(WAProject project, IProject source, IElement elem) {
			VTSTag t = GetTag(project, source, elem, TypeIDs.MINIMUM);
			if (t != null)
				this.Tags.Add(TypeIDs.MINIMUM.ToLower(), t);
			t = GetTag(project, source, elem, TypeIDs.MAXIMUM);
			if (t != null)
				this.Tags.Add(TypeIDs.MAXIMUM.ToLower(), t);
			t = GetTag(project, source, elem, TypeIDs.LOW);
			if (t != null)
				this.Tags.Add(TypeIDs.LOW.ToLower(), t);
			t = GetTag(project, source, elem, TypeIDs.LOWLOW);
			if (t != null)
				this.Tags.Add(TypeIDs.LOWLOW.ToLower(), t);
			t = GetTag(project, source, elem, TypeIDs.HIGH);
			if (t != null)
				this.Tags.Add(TypeIDs.HIGH.ToLower(), t);
			t = GetTag(project, source, elem, TypeIDs.HIGHHIGH);
			if (t != null)
				this.Tags.Add(TypeIDs.HIGHHIGH.ToLower(), t);
		}
		private void SetValues(IElement elem, IProject source) {
			IProperty minProp = null;
			IProperty maxProp = null;
			IProperty lProp = null;
			IProperty llProp = null;
			IProperty hProp = null;
			IProperty hhProp = null;

			minProp = elem.GetPropertyByName(TypeIDs.MINIMUM);
			maxProp = elem.GetPropertyByName(TypeIDs.MAXIMUM);
			lProp = elem.GetPropertyByName(TypeIDs.LOW);
			llProp = elem.GetPropertyByName(TypeIDs.LOWLOW);
			hProp = elem.GetPropertyByName(TypeIDs.HIGH);
			hhProp = elem.GetPropertyByName(TypeIDs.HIGHHIGH);

			if (lProp == null)
				lProp = elem.GetPropertyByName(TypeIDs.LOWVALUE);
			if (llProp == null)
				llProp = elem.GetPropertyByName(TypeIDs.LOWLOWVALUE);
			if (hProp == null)
				hProp = elem.GetPropertyByName(TypeIDs.HIGHVALUE);
			if (hhProp == null)
				hhProp = elem.GetPropertyByName(TypeIDs.HIGHHIGHVALUE);

			if (minProp is IPropertyString)
				SetTag(elem, TypeIDs.MINIMUM, Project, source);
			else
				Minimum = double.Parse((string)minProp.rowValue);
			if (maxProp is IPropertyString)
				SetTag(elem, TypeIDs.MAXIMUM, Project, source);
			else
				Maximum = double.Parse((string)maxProp.rowValue);
			if (lProp is IPropertyString)
				SetTag(elem, TypeIDs.LOW, Project, source);
			else
				Low = double.Parse((string)lProp.rowValue);
			if (llProp is IPropertyString)
				SetTag(elem, TypeIDs.LOWLOW, Project, source);
			else
				LowLow = double.Parse((string)llProp.rowValue);
			if (hProp is IPropertyString)
				SetTag(elem, TypeIDs.HIGH, Project, source);
			else
				High = double.Parse((string)hProp.rowValue);
			if (hhProp is IPropertyString)
				SetTag(elem, TypeIDs.HIGHHIGH, Project, source);
			else
				HighHigh = double.Parse((string)hhProp.rowValue);
		}
		private void SetTag(IElement elem, string name, WAProject project, IProject source, bool ignoreName = false) {
			var tag = GetTag(project, source, elem, name, ignoreName);
			if (tag != null)
				Tags.Add(name, tag);
		}
		private void SetValueTag(IElement element, WAProject project, IProject source, bool ignoreName = false) {
			var tag = GetTag(project, source, element, "Value", ignoreName);
			if (tag != null)
				Tags.Add("Value", tag);
		}
		#endregion Private Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the corner radius.
		/// </summary>
		/// <value>The corner radius.</value>
		public double CornerRadius {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>The header.</value>
		public WATitle Header {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the high.
		/// </summary>
		/// <value>The high.</value>
		public double High {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the high background.
		/// </summary>
		/// <value>The high background.</value>
		public WABrush HighBackground {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the high high.
		/// </summary>
		/// <value>The high high.</value>
		public double HighHigh {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the high high background.
		/// </summary>
		/// <value>The high high background.</value>
		public WABrush HighHighBackground {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is adjustable.
		/// </summary>
		/// <value><c>true</c> if this instance is adjustable; otherwise, <c>false</c>.</value>
		public bool IsAdjustable {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is high high visible.
		/// </summary>
		/// <value><c>true</c> if this instance is high high visible; otherwise, <c>false</c>.</value>
		public bool IsHighHighVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is high visible.
		/// </summary>
		/// <value><c>true</c> if this instance is high visible; otherwise, <c>false</c>.</value>
		public bool IsHighVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is low low visible.
		/// </summary>
		/// <value><c>true</c> if this instance is low low visible; otherwise, <c>false</c>.</value>
		public bool IsLowLowVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is low visible.
		/// </summary>
		/// <value><c>true</c> if this instance is low visible; otherwise, <c>false</c>.</value>
		public bool IsLowVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is minor ticks visible.
		/// </summary>
		/// <value><c>true</c> if this instance is minor ticks visible; otherwise, <c>false</c>.</value>
		public bool IsMinorTicksVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is value visible.
		/// </summary>
		/// <value><c>true</c> if this instance is value visible; otherwise, <c>false</c>.</value>
		public bool IsValueVisible {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the low.
		/// </summary>
		/// <value>The low.</value>
		public double Low {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the low background.
		/// </summary>
		/// <value>The low background.</value>
		public WABrush LowBackground {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the low low.
		/// </summary>
		/// <value>The low low.</value>
		public double LowLow {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the low low background.
		/// </summary>
		/// <value>The low low background.</value>
		public WABrush LowLowBackground {
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
		/// Gets or sets the meter font.
		/// </summary>
		/// <value>The meter font.</value>
		public WAFont MeterFont {
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
		/// Gets or sets the number of major ticks.
		/// </summary>
		/// <value>The number of major ticks.</value>
		public double NumberOfMajorTicks {
			get;
			set;
		}
		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <value>The tags.</value>
		public IDictionary<string, VTSTag> Tags {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the thumb brush.
		/// </summary>
		/// <value>The thumb brush.</value>
		public WABrush ThumbBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		public string TrendChartTemplateName {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the value foreground.
		/// </summary>
		/// <value>The value foreground.</value>
		public WABrush ValueForeground {
			get;
			set;
		}
		#endregion Public Properties
	}
}