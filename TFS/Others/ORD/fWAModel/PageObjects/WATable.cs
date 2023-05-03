// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Web Analytics Table
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WATable.
	/// </summary>
	public abstract class WATable : WAObject, IWAMultiValueDynamicObject
	{
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
		/// <exception cref="ApplicationException">Table element is not configured correctly</exception>
		public WATable(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			Columns = new List<WAColumn>();
			Tags = new Dictionary<string, VTSTag>();

			if (element.HasProperty(TypeIDs.FONT)) {
				Font = project.Fonts.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(TypeIDs.FONT, string.Empty)));
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public abstract override List<TagItem> ToTags();
		#endregion Public Methods

		#region Protected Methods
		/// <summary>
		/// Gets the unique identifier for tag path.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="parent">The VTS server.</param>
		/// <param name="path">The path.</param>
		/// <param name="tagElement">The tag element.</param>
		/// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
		protected int? GetUniqueIdForTagPath(IProject source, IElement parent, string path, out IElement tagElement, out string name) {
			tagElement = null;
			name = null;
			var pathParts = path.Split('.');
			int? result = null;
			if (parent == null)
				return result;
			var searchName = pathParts[0];
			name = searchName;
			for (int i = 0; i < pathParts.Length; i++) {
				if (result.HasValue)
					break;
				foreach (var id in parent.Children) {
					var element = source.GetElemById(id);
					if (element.Name.Equals(searchName)) {
						path = path.Replace(searchName, string.Empty);
						if (!string.IsNullOrEmpty(path)) {
							path = path.TrimStart('.');
							result = GetUniqueIdForTagPath(source, element, path, out tagElement, out name);
						}
						else {
							tagElement = element;
							result = element.GetPropertyValue<int>(TypeIDs.UID, -1);
						}
						break;
					}
				}
			}
			return result;
		}
		/// <summary>
		/// VTSs the server element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>IElement.</returns>
		protected IElement VtsServerElement(IElement element) {
			while (!element.Type.TypeID.Equals(TypeIDs.VTSSERVER)) {
				element = element.Parent;
			}
			return element;
		}
		#endregion Protected Methods

		#region Public Properties
		/// <summary>
		/// Gets the columns.
		/// </summary>
		/// <value>The columns.</value>
		public List<WAColumn> Columns { get; private set; }
		/// <summary>
		/// Gets or sets the corner radius.
		/// </summary>
		/// <value>The corner radius.</value>
		public double CornerRadius { get; set; }
		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public WAFont Font { get; set; }
		/// <summary>
		/// Gets the tags.
		/// </summary>
		/// <value>The tags.</value>
		public IDictionary<string, VTSTag> Tags { get; private set; }
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public WATitle Title { get; set; }
		/// <summary>
		/// Gets or sets the name of the trend chart template.
		/// </summary>
		/// <value>The name of the trend chart template.</value>
		public string TrendChartTemplateName { get; set; }
		#endregion Public Properties
	}
}