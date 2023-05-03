// ----------------------------------------------------------------------- Copyright (c) Statistics & Controls, Inc.. All rights reserved. Created by: Greg -----------------------------------------------------------------------
//
// Base object
namespace SNC.OptiRamp.PageObjects {

	using SNC.OptiRamp.Application.Extensions.Media;
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
	using SNC.OptiRamp.Application.Extensions.Primitives;

	/// <summary>
	/// Class WAObject.
	/// </summary>
	public abstract class WAObject : WAItem {

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAObject"/> class.
		/// </summary>
		/// <param name="id">      The identifier.</param>
		/// <param name="name">    The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element"> The element.</param>
		/// <param name="project"> The project.</param>
		/// <param name="page">    The page.</param>
		/// <param name="source">  The source.</param>
		/// <param name="log">     The log.</param>
		public WAObject(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence) {
			Log = log;
			SourceElement = element;
			Project = project;
			Page = page;
			DocumentReadyScripts = new StringBuilder();
			AdditionalScriptLines = new StringBuilder();
			TrendChartArgs = new StringBuilder();
			NonCriticalErrors = new List<string>();

			PageId = string.Format("object_{0}", ObjectNumber);

			//set defaults
			BackgroundBrush = Defaults.ObjectDefaults.Background;
			BorderBrush = Defaults.ObjectDefaults.BorderBrush;
			Border = Defaults.ObjectDefaults.Border;
			ShowDropShadow = false;

			double? xValue = null;
			double? yValue = null;
			double? width = 0;
			double? height = 0;
			//set values

			PropertyGroups.Background.ForEach(idName => {
				if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(idName, string.Empty))) {
					BackgroundBrush = project.Brushes.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(idName, string.Empty), StringComparison.OrdinalIgnoreCase));
					return;
				}
			});
			PropertyGroups.Border.ForEach(idName => {
				if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(idName, string.Empty))) {
					BorderBrush = project.Brushes.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(idName, string.Empty), StringComparison.OrdinalIgnoreCase));
					return;
				}
			});
			PropertyGroups.BorderSize.ForEach(idName => {
				if (idName.Equals(TypeIDs.BORDER, StringComparison.OrdinalIgnoreCase) &&
					element.Type.TypeID.Equals(TypeIDs.STATICLINE, StringComparison.OrdinalIgnoreCase)) {
					Border = element.GetPropertyValue<double>(TypeIDs.BORDER, 0);
				}
				else if (idName.Equals(TypeIDs.THICKNESS, StringComparison.OrdinalIgnoreCase) &&
					element.Type.TypeID.Equals(TypeIDs.STATICLINE, StringComparison.OrdinalIgnoreCase)) {
					this.As<WAStaticLine>().Thickness = element.GetPropertyValue<double>(TypeIDs.THICKNESS, 0);
				}
				else if (idName.Equals(TypeIDs.BORDER, StringComparison.OrdinalIgnoreCase) &&
					(element.Type.TypeID.Equals(TypeIDs.CHART, StringComparison.OrdinalIgnoreCase) ||
					 element.Type.TypeID.Equals(TypeIDs.TRENDCHART, StringComparison.OrdinalIgnoreCase))) {

					// Inconsistent use of TypeIDs.BORDER in Developer program. It represents a brush for TypeIDs.CHART and TypeIDs.TRENDCHART. Use TypeIDs.THICKNESS instead.
					if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(TypeIDs.THICKNESS, string.Empty))) {
						Border = element.GetPropertyValue<double>(TypeIDs.THICKNESS, 0);
						return;
					}
				}
				else if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(idName, string.Empty))) {
					Border = element.GetPropertyValue<double>(idName, 0);
					return;
				}
			});
			ShowDropShadow = element.GetPropertyValue<bool>(TypeIDs.SHOWDROPSHADOW, false);
			xValue = element.GetPropertyValue<double>(TypeIDs.X, 0.0);
			yValue = element.GetPropertyValue<double>(TypeIDs.Y, 0.0);
			Location = GetLocation(xValue, yValue);
			width = element.GetPropertyValue<double>(TypeIDs.WIDTH, 0.0);
			height = element.GetPropertyValue<double>(TypeIDs.HEIGHT, 0.0);
			Size = GetSize(width, height);
			HelpLink = element.GetPropertyValue<string>(TypeIDs.HELPLINK, string.Empty);
			Reference = element.GetPropertyValue<string>(TypeIDs.REFERENCE, string.Empty);
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Resets the object number.
		/// </summary>
		public static void ResetObjectNumber() {
			ResetObjectNumber(-1);
		}
		/// <summary>
		/// Resets the object number.
		/// </summary>
		/// <param name="value">The value.</param>
		public static void ResetObjectNumber(int value) {
			_ObjectNumber = value;
		}
		public VTSTag GetVTSTag() {
			if (this.GetType() == typeof(IWADynamicObject))
				return ((IWADynamicObject)this).Tag;
			return null;
		}
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public abstract List<TagItem> ToTags();
		#endregion Public Methods

		#region Protected Methods
		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns>Point.</returns>
		protected Point GetLocation(double? x, double? y) {
			if (!x.HasValue || !y.HasValue)
				return new Point();
			return new Point(x.Value, y.Value);
		}
		/// <summary>
		/// Gets the reference tag.
		/// </summary>
		/// <returns>TagItem.</returns>
		protected string[] GetReference() {
			if (string.IsNullOrEmpty(Reference))
				return null;
			var result = new List<string>();
			result.Add("'" + Project.Name.Replace("'", "\\'") + "'");
			Reference.Split('|').ToList().ForEach(x => {
				result.Add("'" + x + "'");
			});
			return result.ToArray();
		}
		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <param name="width"> The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>Size.</returns>
		protected Size GetSize(double? width, double? height) {
			if (!width.HasValue || !height.HasValue)
				return new Size();
			return new Size(width.Value, height.Value);
		}
		/// <summary>
		/// Gets the style entry.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <param name="addPx">if set to <c>true</c> [add px].</param>
		/// <returns>System.String.</returns>
		protected string GetStyleEntry<T>(string name, T value, string format = "", bool addPx = false) {
			if (typeof(T) == typeof(string))
				return string.Format("{0}:{1}{2};", name, value, addPx ? "px" : string.Empty);
			else if (typeof(T) == typeof(Color))
				return string.Format("{0}:#{1};", name, ((Color)(object)value).ToHexString(false), addPx ? "px" : string.Empty);
			else if (IsFormatAllowed(typeof(T)) && !string.IsNullOrEmpty(format)) {
				if (typeof(T) == typeof(DateTime))
					return string.Format("{0}:{1}{2};", name, ((DateTime)(object)value).ToString(format), addPx ? "px" : string.Empty);
				else if (typeof(T) == typeof(TimeSpan))
					return string.Format("{0}:{1}{2};", name, ((TimeSpan)(object)value).ToString(format), addPx ? "px" : string.Empty);
				else
					return string.Format("{0}:{1}{2};", name, ((double)(object)value).ToString(format), addPx ? "px" : string.Empty);
			}
			return string.Format("{0}:{1}{2};", name, value.ToString(), addPx ? "px" : string.Empty);
		}
		#endregion Protected Methods

		#region Private Methods
		private bool IsFormatAllowed(Type t) {
			return t == typeof(int) ||
				t == typeof(double) ||
				t == typeof(Single) ||
				t == typeof(long) ||
				t == typeof(short) ||
				t == typeof(byte) ||
				t == typeof(uint) ||
				t == typeof(ulong) ||
				t == typeof(ushort) ||
				t == typeof(float) ||
				t == typeof(DateTime) ||
				t == typeof(TimeSpan);
		}
		#endregion Private Methods

		#region Private Fields
		private static int _ObjectNumber = -1;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the additional script lines.
		/// </summary>
		/// <value>The additional script lines.</value>
		public StringBuilder AdditionalScriptLines {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the background.
		/// </summary>
		/// <value>The background.</value>
		public WABrush BackgroundBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the border.
		/// </summary>
		/// <value>The border.</value>
		public double Border {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the border brush.
		/// </summary>
		/// <value>The border brush.</value>
		public WABrush BorderBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the document ready scripts.
		/// </summary>
		/// <value>The document ready scripts.</value>
		public StringBuilder DocumentReadyScripts {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the help link.
		/// </summary>
		/// <value>The help link.</value>
		public string HelpLink {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public Point Location {
			get;
			set;
		}
		/// <summary>
		/// Gets the log.
		/// </summary>
		/// <value>The log.</value>
		public IOptiRampLog Log {
			get;
			private set;
		}
		/// <summary>
		/// Gets the non critical errors.
		/// </summary>
		/// <value>The non critical errors.</value>
		public List<string> NonCriticalErrors {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the name of the object template.
		/// </summary>
		/// <value>The name of the object template.</value>
		public string ObjectTemplateName {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the type of the object.
		/// </summary>
		/// <value>The type of the object.</value>
		public Enumerations.ObjectTypes ObjectType {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the page.
		/// </summary>
		/// <value>The page.</value>
		public WAPage Page {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the page identifier.
		/// </summary>
		/// <value>The page identifier.</value>
		public string PageId {
			get;
			set;
		}
		/// <summary>
		/// Gets the project.
		/// </summary>
		/// <value>The project.</value>
		public WAProject Project {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>The reference.</value>
		public string Reference {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether [show drop shadow].
		/// </summary>
		/// <value><c>true</c> if [show drop shadow]; otherwise, <c>false</c>.</value>
		public bool ShowDropShadow {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public Size Size {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the source element.
		/// </summary>
		/// <value>The source element.</value>
		public IElement SourceElement {
			get;
			set;
		}
		/// <summary>
		/// Trend chart arguments: t1, t2, and channels.
		/// </summary>
		public StringBuilder TrendChartArgs {
			get;
			set;
		}
		#endregion Public Properties

		#region Protected Properties
		/// <summary>
		/// Gets the object number.
		/// </summary>
		/// <value>The object number.</value>
		protected static int ObjectNumber {
			get {
				_ObjectNumber++;
				return _ObjectNumber;
			}
		}
		#endregion Protected Properties
	}
}