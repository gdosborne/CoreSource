// ----------------------------------------------------------------------- Copyright (c) Statistics & Controls, Inc.. All rights reserved. Created by: Greg -----------------------------------------------------------------------
//
// Factory to generically create WA objects
namespace SNC.OptiRamp {

	using SNC.OptiRamp.Application.Extensions.Media;
	using SNC.OptiRamp.Application.Extensions.Primitives;
	using SNC.OptiRamp.PageObjects;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fDiagnostics;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;

	/// <summary>
	/// Class WAObjectFactory.
	/// </summary>
	public static class WAObjectFactory {

		#region Public Methods
		/// <summary>
		/// Creates the specified element.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="element">   The element.</param>
		/// <param name="id">        The identifier.</param>
		/// <param name="sequence">  The sequence.</param>
		/// <param name="project">   The project.</param>
		/// <param name="page">      The page.</param>
		/// <param name="rootFolder">The root folder.</param>
		/// <param name="rootUrl">   The root URL.</param>
		/// <param name="source">    The source.</param>
		/// <returns>T.</returns>
		public static T Create<T>(IElement element, int id, int sequence, WAProject project, WAPage page, string rootFolder, string rootUrl, IProject source, IOptiRampLog log) where T : WAItem {
			WAItem result = null;
			var name = element.GetPropertyValue<string>(TypeIDs.NAME, string.Empty);
			if (typeof(T) == typeof(WABrush)) {
				result = new WABrush(id, name, sequence);
				result.As<WABrush>().Color1 = element.GetPropertyValue<string>(TypeIDs.COLOR1, "#000000").ToColor();
			}
			else if (typeof(T) == typeof(WADataSource)) {
				result = new WADataSource(id, name, sequence);
				result.As<WADataSource>().Type = element.GetPropertyValue<string>(TypeIDs.TYPE, string.Empty) != null ? element.GetPropertyValue<string>(TypeIDs.TYPE, string.Empty).ToLower() : element.Type.Name.ToLower();
				result.As<WADataSource>().Address = element.GetPropertyValue<string>(TypeIDs.ADDRESS, string.Empty);
				result.As<WADataSource>().Database = element.GetPropertyValue<string>(TypeIDs.DB, string.Empty);
				result.As<WADataSource>().ChannelsTable = element.GetPropertyValue<string>(TypeIDs.TABLECHANNELS, string.Empty);
				result.As<WADataSource>().ValuesTable = element.GetPropertyValue<string>(TypeIDs.TABLEVALUES, string.Empty);
			}
			else if (typeof(T) == typeof(WAEngineeringUnit)) {
				result = new WAEngineeringUnit(id, name, sequence);
				result.As<WAEngineeringUnit>().CEU = element.GetPropertyValue<string>(TypeIDs.CEU, string.Empty);
				result.As<WAEngineeringUnit>().Digits = element.GetPropertyValue<int>(TypeIDs.DIGITS, 2);
				result.As<WAEngineeringUnit>().Span = element.GetPropertyValue<double>(TypeIDs.SPAN, 0.0);
				result.As<WAEngineeringUnit>().Offset = element.GetPropertyValue<double>(TypeIDs.OFFSET, 0.0);
			}
			else if (typeof(T) == typeof(WAFont)) {
				result = new WAFont(id, name, sequence);
				result.As<WAFont>().FontFamilies = new List<string> { element.GetPropertyValue<string>(TypeIDs.FONT, string.Empty) };
				if (element.GetPropertyValue<string>(TypeIDs.FONTSTYLE, "Normal") == SNC.OptiRamp.Enumerations.FontStyles.Bold.ToString())
					result.As<WAFont>().FontStyle = SNC.OptiRamp.Enumerations.FontStyles.Bold;
				result.As<WAFont>().FontSize = element.GetPropertyValue<double>(TypeIDs.FONTSIZE, 10.0);
				result.As<WAFont>().Unit = Enumerations.MeasureUnits.Pt;
			}
			else if (typeof(T) == typeof(WAPage)) {
				result = new WAPage(id, name, sequence);

				//TODO: Remove when developer supports popup pages
				result.As<WAPage>().IsPopupPage = result.Name.Equals("PopupTester", StringComparison.OrdinalIgnoreCase);

				double width = 800;
				double height = 600;
				if (!string.IsNullOrEmpty(element.GetPropertyValue<string>(TypeIDs.WIDTH, "0.0")) && !string.IsNullOrEmpty(element.GetPropertyValue<string>(TypeIDs.HEIGHT, "0.0"))) {
					width = double.Parse(element.GetPropertyValue<string>(TypeIDs.WIDTH, "0.0"));
					height = double.Parse(element.GetPropertyValue<string>(TypeIDs.HEIGHT, "0.0"));
				}
				result.As<WAPage>().Size = new System.Windows.Size(width, height);
				if (element.Peers.Any() && project.Pictures.Any()) {
					foreach (var peer in element.Peers) {
						foreach (var pic in project.Pictures) {
							if (peer.elemID == pic.ElementId) {
								result.As<WAPage>().BackgroundPicture = pic;
								break;
							}
						}
						if (result.As<WAPage>().BackgroundPicture != null)
							break;
					}
				}
			}
			else if (typeof(T) == typeof(WAComputer)) {
				result = new WAComputer(id, name, sequence, element.GetPropertyValue<string>(TypeIDs.NETWORKADDRESS, string.Empty));
			}
			else if (typeof(T) == typeof(WAPicture)) {
				Bitmap bmp = element.GetPropertyValue<Bitmap>("bitmap", null);
				string svgCommands = element.GetPropertyValue<string>("svg", null);
				var isLogo = element.HasProperty("LOGO");
				if (isLogo) {
					try {
						bmp = element.GetPropertyValue<Bitmap>("LOGO", null);
					}
					catch {
						svgCommands = element.GetPropertyValue<string>("svg", null);
					}
				}
				if (isLogo) {
					name = "LOGO";
				}
				if (bmp != null || !string.IsNullOrEmpty(svgCommands)) {
					result = new WAPicture(id, name, sequence, !string.IsNullOrEmpty(svgCommands) ? ImageTypes.SVG : ImageTypes.Rastor, bmp, svgCommands, System.IO.Path.GetFileNameWithoutExtension(project.FileName), rootFolder, rootUrl);
				}
			}
			else if (typeof(T) == typeof(WAServer)) {
				var type = ServerTypes.OPC;
				var typeName = (element.GetPropertyValue<string>(TypeIDs.TYPE, string.Empty) != null ? element.GetPropertyValue<string>(TypeIDs.TYPE, string.Empty).ToLower() : element.Type.Name).ToUpper();
				switch (typeName) {
					case TypeIDs.WEBSERVER:
						type = ServerTypes.WA;
						break;
					//case "OPC_SERVER":
					//	type = ServerTypes.OPC;
					//	break;
					//case "VTS_SERVER":
					//	type = ServerTypes.VTS;
					//	break;
					//case "WIKI":
					//	type = ServerTypes.WIKI;
					//	break;
				}
				result = new WAServer(id, name, sequence, type);
			}
			else if (typeof(T).IsSubclassOf(typeof(WAObject))) {
				if (typeof(T) == typeof(WAStaticText))
					result = new WAStaticText(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WADynamicText))
					result = new WADynamicText(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAPopupLink))
					result = new WAPopupLink(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAUpdatableText))
					result = new WAUpdatableText(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAStaticImage))
					result = new WAStaticImage(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WADynamicImage))
					result = new WADynamicImage(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WARectangle))
					result = new WARectangle(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAEllipse))
					result = new WAEllipse(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAStaticLine))
					result = new WAStaticLine(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAStaticConnector))
					result = new WAStaticConnector(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WADynamicConnector))
					result = new WADynamicConnector(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WALinearMeter))
					result = new WALinearMeter(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WARadialMeter))
					result = new WARadialMeter(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAVerticalBarChart))
					result = new WAVerticalBarChart(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WAPieChart))
					result = new WAPieChart(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WARealtimeTable))
					result = new WARealtimeTable(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WASnapshotTable))
					result = new WASnapshotTable(id, name, sequence, element, project, page, source, log);
				else if (typeof(T) == typeof(WATrendChart))
					result = new WATrendChart(id, name, sequence, element, project, page, source, log);
			}
			if (result != null)
				result.ElementId = element.Id;
			return (T)(object)result;
		}
		#endregion Public Methods

	}
}