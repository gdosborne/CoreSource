// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Connector
//
namespace SNC.OptiRamp.PageObjects {
	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows;
	using System.Windows.Media;

	/// <summary>
	/// Class WAConnector.
	/// </summary>
	public abstract class WAConnector : WAObject {
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
		public WAConnector(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the SVG.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			var connectorWidth = LineWidth + 4;
			var x = Location.X - (connectorWidth / 2);
			var y = Location.Y - (connectorWidth / 2);
			var sb = new StringBuilder();
			sb.Append(GetStyleEntry<double>("stroke-width", 1));
			sb.Append(GetStyleEntry<Color>("fill", Colors.White));
			sb.Append(GetStyleEntry<Color>("stroke", Colors.Black));
			var result = TagItem.CreateTag(TagItem.TagTypes.rect, null,
				new Dictionary<string, string>{
					{"x", x.ToString()},
					{"y", y.ToString()},
					{"width", connectorWidth.ToString()},
					{"height", connectorWidth.ToString()},
					{"rx", "2"},
					{"style", sb.ToString()}
				}, true, false, true);
			return new List<TagItem> { result };
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the width of the line.
		/// </summary>
		/// <value>The width of the line.</value>
		public double LineWidth {
			get;
			set;
		}
		#endregion Public Properties
	}
}