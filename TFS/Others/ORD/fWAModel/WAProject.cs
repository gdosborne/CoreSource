// ----------------------------------------------------------------------- Copyright (c) Statistics & Controls, Inc.. All rights reserved. Created by: Greg -----------------------------------------------------------------------
//
// [your comment here]
namespace SNC.OptiRamp {

	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAProject.
	/// </summary>
	public sealed class WAProject : WAItem {

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAProject" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="number">The number.</param>
		/// <param name="imageKey">The image key.</param>
		/// <param name="source">The source.</param>
		public WAProject(int id, string name, int sequence, string number, string imageKey, IProject source)
			: base(id, name, sequence) {
			Source = source;
			Pages = new List<WAPage>();
			Fonts = new List<WAFont>();
			Brushes = new List<WABrush>();
			EngineeringUnits = new List<WAEngineeringUnit>();
			DataSources = new List<WADataSource>();
			Templates = new List<WATemplate>();
			Pictures = new List<WAPicture>();
			Revisions = new List<WARevision>();
			Computers = new List<WAComputer>();
			Number = number;
			ImageKey = imageKey;
		}
		#endregion Public Constructors
		public IProject Source {
			get;
			set;
		}
		#region Public Methods
		public IElement GetVTSTagElement(IElement folder, string name) {
			var tags = folder.Children.Select(x => Source.GetElemById(x)).ToList();
			return tags.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
		}
		public IElement GetVTSTagElement(int uniqueId, string name) {
			return GetVTSTagElement(GetVTSFolderElement(uniqueId), name);
		}
		public IElement GetVTSFolderElement(int uniqueId) {
			var folderType = Source.GetElemType(TypeIDs.VTSFOLDER);
			var folders = Source.GetElemsOfType(folderType).Where(x => x.Properties.ContainsKey(TypeIDs.UID)).ToList();
			var result = folders.FirstOrDefault(x => int.Parse((string)x.Properties[TypeIDs.UID].rowValue) == uniqueId);
			return result;
		}
		/// <summary>
		/// Gets the name of the brush by.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>WABrush.</returns>
		public WABrush GetBrushByName(string name) {
			WABrush brush = Brushes.FirstOrDefault(x => x.Name.Equals(name));
			if (brush == null)
				brush = SNC.OptiRamp.Defaults.BrushDefaults.Black;
			return brush;
		}
		/// <summary>
		/// Gets the computer for tag.
		/// </summary>
		/// <param name="elem">    The elem.</param>
		/// <param name="computer">The computer.</param>
		/// <param name="server">  The server.</param>
		/// <param name="url">     The URL.</param>
		public void GetComputerForTag(IElement elem, out WAComputer computer, out WAServer server, out string url) {
			computer = null;
			server = null;
			url = null;
			if (elem == null)
				return;
			while (!elem.Type.TypeID.Equals(TypeIDs.VTSSERVER, StringComparison.OrdinalIgnoreCase)) {
				elem = elem.Parent;
			}
			var compId = elem.Parent.Id;
			foreach (var comp in Computers) {
				if (comp.ElementId == compId) {
					computer = comp;
					server = comp.VTSServers.FirstOrDefault(x => x.ElementId == elem.Id);
					url = string.Format("http://{0}:{1}/api/RTData", computer.Address, server.Port);
					return;
				}
			}
		}
		/// <summary>
		/// Gets the name of the font by.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>WAFont.</returns>
		public WAFont GetFontByName(string name) {
			WAFont font = Fonts.FirstOrDefault(x => x.Name.Equals(name));
			if (font == null)
				font = SNC.OptiRamp.Defaults.FontDefaults.MSTahoma;
			return font;
		}
		#endregion Public Methods

		#region Public Properties
		/// <summary>
		/// Gets or sets the base URL.
		/// </summary>
		/// <value>The base URL.</value>
		public string BaseUrl {
			get;
			set;
		}
		/// <summary>
		/// Gets the brushes.
		/// </summary>
		/// <value>The brushes.</value>
		public List<WABrush> Brushes {
			get;
			private set;
		}
		/// <summary>
		/// Gets the computers.
		/// </summary>
		/// <value>The computers.</value>
		public List<WAComputer> Computers {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the content root folder.
		/// </summary>
		/// <value>The content root folder.</value>
		public string ContentRootFolder {
			get;
			set;
		}
		/// <summary>
		/// Gets the data sources.
		/// </summary>
		/// <value>The data sources.</value>
		public List<WADataSource> DataSources {
			get;
			private set;
		}
		/// <summary>
		/// Gets the engineering units.
		/// </summary>
		/// <value>The engineering units.</value>
		public List<WAEngineeringUnit> EngineeringUnits {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName {
			get;
			set;
		}
		/// <summary>
		/// Gets the fonts.
		/// </summary>
		/// <value>The fonts.</value>
		public List<WAFont> Fonts {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the image key.
		/// </summary>
		/// <value>The image key.</value>
		public string ImageKey {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the logo.
		/// </summary>
		/// <value>The logo.</value>
		public WAPicture Logo {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>The number.</value>
		public string Number {
			get;
			set;
		}
		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>The pages.</value>
		public List<WAPage> Pages {
			get;
			private set;
		}
		/// <summary>
		/// Gets the images.
		/// </summary>
		/// <value>The images.</value>
		public List<WAPicture> Pictures {
			get;
			private set;
		}
		/// <summary>
		/// Gets the revisions.
		/// </summary>
		/// <value>The revisions.</value>
		public List<WARevision> Revisions {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the root element.
		/// </summary>
		/// <value>The root element.</value>
		public IElement RootElement {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public long Size {
			get;
			set;
		}
		/// <summary>
		/// Gets the templates.
		/// </summary>
		/// <value>The templates.</value>
		public List<WATemplate> Templates {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the web analytics server.
		/// </summary>
		/// <value>The web analytics server.</value>
		public WAServer WebAnalyticsServer {
			get;
			set;
		}
		#endregion Public Properties
	}
}