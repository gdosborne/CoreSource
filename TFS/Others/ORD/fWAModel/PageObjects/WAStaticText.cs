// ----------------------------------------------------------------------- Copyright (c) Statistics & Controls, Inc.. All rights reserved. Created by: Greg -----------------------------------------------------------------------
//
// STatic text
namespace SNC.OptiRamp.PageObjects {

	using SNC.OptiRamp.ObjectInterfaces;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAStaticText.
	/// </summary>
	public class WAStaticText : WAText, IWATag {

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
		public WAStaticText(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.StaticText;
			PropertyGroups.Foreground.ForEach(idName => {
				if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(idName, string.Empty))) {
					ForegroundBrush = project.Brushes.FirstOrDefault(x => x.Name.Equals(element.GetPropertyValue<string>(idName, string.Empty), StringComparison.OrdinalIgnoreCase));
					return;
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
			return base.ToTags();
		}
		#endregion Public Methods
	}
}