// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Web Analytics Realtime table
//
namespace SNC.OptiRamp.PageObjects
{
	using SNC.OptiRamp.Application.Extensions.Media;
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
	/// Class WARealtimeTable. This class cannot be inherited.
	/// </summary>
	public sealed class WARealtimeTable : WATable
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WARealtimeTable" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="element">The element.</param>
		/// <param name="project">The project.</param>
		/// <param name="page">The page.</param>
		/// <param name="source">The source.</param>
		/// <param name="log">The log.</param>
		/// <exception cref="ApplicationException">
		/// </exception>
		public WARealtimeTable(int id, string name, int sequence, IElement element, WAProject project, WAPage page, IProject source, IOptiRampLog log)
			: base(id, name, sequence, element, project, page, source, log) {
			ObjectType = Enumerations.ObjectTypes.RealtimeTable;

			IsTablePivoted = element.GetPropertyValue<bool>(TypeIDs.PIVOT, false);
			if (element.Children == null || !element.Children.Any()) {
				NonCriticalErrors.Add(string.Format("Table located at {0} on page {1} is not configured correctly", Location, page.Name));
				return;
			}
			var valueNum = 0;

			IElement vtsServerElement = null;
			//var holders = new List<valueHolder>();
			//var matrix = new List<List<IElement>>();
			var rows = new List<WARow>();
			var cols = new List<WAColumn>();
			var colNum = 0;

			foreach (var childId in element.Children) {
				var elem = source.GetElemById(childId);
				if (elem.Type.TypeID.Equals(TypeIDs.TITLE, StringComparison.OrdinalIgnoreCase)) {
					Title = new WATitle
					{
						Text = elem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty),
						Brush = project.Brushes.FirstOrDefault(x => x.Name.Equals(elem.GetPropertyValue<string>(TypeIDs.TEXTBRUSH, string.Empty))),
						Font = project.Fonts.FirstOrDefault(x => x.Name.Equals(elem.GetPropertyValue<string>(TypeIDs.FONT, string.Empty))),
						IsVisible = true
					};
				}
				else if (elem.Type.TypeID.Equals(TypeIDs.COLUMNS, StringComparison.OrdinalIgnoreCase)) {
					if (IsTablePivoted) {
						//read rows
						foreach (var rowId in elem.Children) {
							var rowElem = source.GetElemById(rowId);
							rows.Add(new WARow
							{
								Element = rowElem,
								ElementId = rowElem.Id,
								Id = 0,
								Sequence = 0,
							});
						}
					}
					else {
						foreach (var colId in elem.Children) {
							var colElem = source.GetElemById(colId);
							if (colElem == null)
								continue;
							var colName = colElem.GetPropertyValue<string>(TypeIDs.NAME, string.Empty);
							var isNameColumn = colName.Equals("Name", StringComparison.OrdinalIgnoreCase);
							var isTotalColumn = colName.Equals("Total", StringComparison.OrdinalIgnoreCase);
							var width = colElem.GetPropertyValue<double>(TypeIDs.WIDTH, 100.0);
							var alignment = colElem.GetPropertyValue<string>(TypeIDs.ALIGNMENT, "Left");
							var vtsFolderName = colElem.GetPropertyValue<string>(TypeIDs.LIST, string.Empty);
							var text = !string.IsNullOrEmpty(colElem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty))
								? colElem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty)
								: colName;
							if (!isNameColumn && !isTotalColumn && colElem.Peers != null && colElem.Peers.Any()) {
								var folderElem = source.GetElemById(colElem.Peers.First().elemID);
								vtsServerElement = VtsServerElement(folderElem);
								vtsFolderName = folderElem.GetPropertyValue<string>(TypeIDs.VTSFOLDERFULLNAME, string.Empty);
							}
							Columns.Add(new WAColumn
							{
								Id = colNum,
								Name = name,
								IsNameColumn = isNameColumn,
								IsTotalColumn = isTotalColumn,
								Text = text,
								Width = width,
								Alignment = alignment,
								VtsTagName = vtsFolderName
							});
							colNum++;
						}
					}
				}
				else if (elem.Type.TypeID.Equals(TypeIDs.VALUE, StringComparison.OrdinalIgnoreCase)) {
					if (IsTablePivoted) {
						if (cols.Count == 0) {
							cols.Add(new WAColumn
							{
								Element = null,
								ElementId = 0,
								IsNameColumn = true,
								Name = "Name",
								Width = -1,
								Alignment = "Left",
								Text = "Name",
								Id = colNum,
							});
						}
						colNum++;
						cols.Add(new WAColumn
						{
							Element = elem,
							ElementId = elem.Id,
							Id = colNum,
						});
					}
					else {
						string valueName = elem.GetPropertyValue<string>(TypeIDs.VALUE, string.Empty);
						string tagFullPath = null;
						for (int i = 0; i < Columns.Count; i++) {
							if (Columns[i].IsNameColumn || Columns[i].IsTotalColumn)
								continue;
							tagFullPath = string.Format("{0}.{1}", Columns[i].VtsTagName, valueName);
							IElement temp = null;
							string tagName = null;
							var uid = GetUniqueIdForTagPath(source, vtsServerElement, tagFullPath, out temp, out tagName);
							if (!uid.HasValue)
								uid = -1;
							if (TagElement == null && temp != null)
								TagElement = temp;
							if (TagElement != null) {
								WAComputer c = null;
								WAServer s = null;
								string u = null;
								project.GetComputerForTag(TagElement, out c, out s, out u);
								var tag = new VTSTag
								{
									Computer = c,
									Server = s,
									APIUrl = u,
									ElementId = elem.Id,
									Name = tagName,
									UniqueId = uid.Value,
									LocalId = Guid.NewGuid(),
									Owner = this
								};
								if (!Tags.ContainsKey(tagName))
									Tags.Add(tagName, tag);
								if (Tags[tagName].UniqueId == -1 && tag.UniqueId != -1)
									Tags[tagName].UniqueId = tag.UniqueId;
							}
							Columns[i].UniqueIds.Add(
								new WARow
								{
									Element = temp,
									ElementId = temp == null ? -1 : temp.Id,
									Id = 0,
									Sequence = 0,
									Name = temp == null ? valueName : temp.Name,
									Text = temp == null ? valueName : temp.Name,
									UniqueId = uid,
									VtsFolderName = tagFullPath
								});
						}
						if (TagElement == null) {
							var msg = string.Format("Cannot find a valid vts tag for Realtime table located at {0} on page {1}.", Location, Page.Name);
							Log.WriteRecord(msg);
							NonCriticalErrors.Add(msg);
						}
						valueNum++;
					}
				}
			}
			if (IsTablePivoted) {
				foreach (var col in cols) {
					if (col.Element == null) {
						Columns.Add(col);
						continue;
					}
					var colElem = col.Element;
					var colName = string.Empty;
					IElement colPeer = null;
					if (colElem.Peers != null && colElem.Peers.Any()) {
						colPeer = source.GetElemById(colElem.Peers.First().elemID);
						colName = colPeer.HasProperty(TypeIDs.NAME) ? colPeer.GetPropertyValue<string>(TypeIDs.NAME, string.Empty) : colPeer.Name;
					}
					var isNameColumn = colName.Equals("Name", StringComparison.OrdinalIgnoreCase);
					var isTotalColumn = colName.Equals("Total", StringComparison.OrdinalIgnoreCase);
					var width = colElem.GetPropertyValue<double>(TypeIDs.WIDTH, 100.0);
					var alignment = colElem.GetPropertyValue<string>(TypeIDs.ALIGNMENT, "Left");
					var vtsTagName = colElem.GetPropertyValue<string>(TypeIDs.LIST, string.Empty);
					int? uid = null;
					var text = !string.IsNullOrEmpty(colElem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty))
						? colElem.GetPropertyValue<string>(TypeIDs.TEXT, string.Empty)
						: colName;
					if (!isNameColumn && !isTotalColumn && colPeer != null) {
						if (vtsServerElement == null)
							vtsServerElement = VtsServerElement(colPeer);
						vtsTagName = colPeer.GetPropertyValue<string>(TypeIDs.VTSTAGFULLNAME, string.Empty);
						var parts = vtsTagName.Split('.');
						vtsTagName = parts[parts.Length - 1];
					}
					col.IsNameColumn = isNameColumn;
					col.IsTotalColumn = isTotalColumn;
					col.Text = text;
					col.Width = width;
					col.Alignment = alignment;
					col.VtsTagName = vtsTagName;
					foreach (var row in rows) {
						if (row.Element.Name.Equals("Name", StringComparison.OrdinalIgnoreCase))
							continue;
						var rowElem = row.Element;
						IElement rowPeer = null;
						if (rowElem.Peers != null && rowElem.Peers.Any()) {
							rowPeer = source.GetElemById(rowElem.Peers.First().elemID);
							row.Name = rowElem.GetPropertyValue<string>(TypeIDs.TEXT, rowElem.Name);
							row.Text = rowElem.GetPropertyValue<string>(TypeIDs.TEXT, rowElem.Name);
							row.VtsFolderName = string.Format("{0}.{1}", rowPeer.GetPropertyValue<string>(TypeIDs.VTSFOLDERFULLNAME, string.Empty), vtsTagName);
							IElement temp = null;
							string tagName = null;
							uid = GetUniqueIdForTagPath(source, vtsServerElement, row.VtsFolderName, out temp, out tagName);
							row.UniqueId = uid.HasValue ? uid.Value : -1;
							if (TagElement == null && temp != null)
								TagElement = temp;
							if (TagElement != null) {
								WAComputer c = null;
								WAServer s = null;
								string u = null;
								project.GetComputerForTag(TagElement, out c, out s, out u);
								var tag = new VTSTag
								{
									Computer = c,
									Server = s,
									APIUrl = u,
									ElementId = rowElem.Id,
									Name = tagName,
									UniqueId = row.UniqueId.Value,
									LocalId = Guid.NewGuid(),
									Owner = this
								};
								if (!Tags.ContainsKey(tagName))
									Tags.Add(tagName, tag);
								if (Tags[tagName].UniqueId == -1 && tag.UniqueId != -1)
									Tags[tagName].UniqueId = tag.UniqueId;
							}

						}
						else if (rowElem.Name.Equals("Total", StringComparison.OrdinalIgnoreCase)) {
							row.IsTotalRow = true;
						}
						col.UniqueIds.Add(new WARow
						{
							Name = row.Name,
							Text = row.Text,
							VtsFolderName = row.VtsFolderName,
							Element = row.Element,
							ElementId = row.ElementId,
							Id = row.Id,
							IsTotalRow = row.IsTotalRow,
							Sequence = row.Sequence,
							UniqueId = row.UniqueId
						});
					}
					Columns.Add(col);
					colNum++;
				}
			}
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// To the tags.
		/// </summary>
		/// <returns>List&lt;TagItem&gt;.</returns>
		public override List<TagItem> ToTags() {
			if (TagElement == null) {
				return null;
			}
			WAComputer c = null;
			WAServer s = null;
			string u = null;
			Project.GetComputerForTag(TagElement, out c, out s, out u);
			var grid = new KendoGrid(this, PageId, u);
			var control = grid.ToString();
			DocumentReadyScripts.Append(control);
			grid.NamedReplacements.Where(x => x.Key.StartsWith("global")).ToList().ForEach(x => AdditionalScriptLines.AppendLine(x.Value));
			RequestItemDefinition = grid.GridRequestItem;
			GridUpdateBody = grid.GridUpdateBody;
			PivotedTotalVariables = grid.PivotedTotalVariables;

			var result = new List<TagItem>();
			var styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<string>("position", "absolute"));
			styleSb.Append(GetStyleEntry<double>("left", Location.X, "0.0", true));
			styleSb.Append(GetStyleEntry<double>("top", Location.Y, "0.0", true));
			if (BackgroundBrush != null)
				styleSb.Append(GetStyleEntry<Color>("background-color", BackgroundBrush.Color1));
			if (Border > 0) {
				styleSb.Append(GetStyleEntry<string>("border", Border.ToString("0.0px") + " Solid #" + BorderBrush.Color1.ToHexString(false)));
				if (CornerRadius > 0)
					styleSb.Append(GetStyleEntry<double>("border-radius", CornerRadius, "0.0", true));
			}
			var outerDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"style", styleSb.ToString()},
					{"selectionoffset", "6"},
					{"oncontextmenu", "return false;"}
				}, false, false, true);
			styleSb = new StringBuilder();
			styleSb.Append(GetStyleEntry<double>("width", (Size.Width - 10), "0.0", true));
			styleSb.Append(GetStyleEntry<double>("height", (Size.Height - 10), "0.0", true));
			var tableDiv = TagItem.CreateTag(TagItem.TagTypes.div, null,
				new Dictionary<string, string>
				{
					{"id", PageId},
					{"style", styleSb.ToString()}
				});

			outerDiv.Tags.Add(tableDiv);
			result.Add(outerDiv);
			return result;
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the grid update body.
		/// </summary>
		/// <value>The grid update body.</value>
		public string GridUpdateBody { get; set; }
		/// <summary>
		/// Gets or sets the pivoted total variables.
		/// </summary>
		/// <value>The pivoted total variables.</value>
		public string PivotedTotalVariables { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is table pivoted.
		/// </summary>
		/// <value><c>true</c> if this instance is table pivoted; otherwise, <c>false</c>.</value>
		public bool IsTablePivoted { get; set; }
		/// <summary>
		/// Gets or sets the request item definition.
		/// </summary>
		/// <value>The request item definition.</value>
		public string RequestItemDefinition { get; set; }
		/// <summary>
		/// Gets or sets the tag element.
		/// </summary>
		/// <value>The tag element.</value>
		public IElement TagElement { get; set; }
		#endregion Public Properties
	}
}