// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// [your comment here]
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// Enum ImageTypes
	/// </summary>
	public enum ImageTypes
	{
		/// <summary>
		/// The rastor
		/// </summary>
		Rastor,
		/// <summary>
		/// The SVG
		/// </summary>
		SVG
	}

	/// <summary>
	/// Class WAImage.
	/// </summary>
	public sealed class WAPicture : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAPicture" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="imageType">Type of the image.</param>
		/// <param name="bitmap">The bitmap.</param>
		/// <param name="svgCommands">The SVG commands.</param>
		/// <param name="projectName">Name of the project.</param>
		/// <param name="appRootFolder">The application root folder.</param>
		/// <param name="appRootUrl">The application root URL.</param>
		public WAPicture(int id, string name, int sequence, ImageTypes imageType, Bitmap bitmap, string svgCommands, string projectName, string appRootFolder, string appRootUrl)
			: base(id, name, sequence) {
			ImageType = imageType;
			Bitmap = bitmap;
			SVGCommands = svgCommands;
			var imgFolder = Path.Combine(appRootFolder, "ProjectImages");
			if (!Directory.Exists(imgFolder))
				Directory.CreateDirectory(imgFolder);
			var url = string.Format("{0}{1}", appRootUrl, "ProjectImages");

			imgFolder = Path.Combine(imgFolder, projectName);
			if (!Directory.Exists(imgFolder))
				Directory.CreateDirectory(imgFolder);
			url = string.Format("{0}/{1}", url, projectName);
			string fileName = Bitmap != null ? string.Format("{0}.png", name) : string.Format("{0}.svg", name);
			ImageFileName = Path.Combine(imgFolder, fileName);
			if (File.Exists(ImageFileName))
				File.Delete(ImageFileName);
			ImageUrl = string.Format("{0}/{1}", url, fileName).Replace(" ", "%20");

			if (Bitmap != null) {
				MemoryStream msImage = new MemoryStream();
				Bitmap.Save(msImage, ImageFormat.Png);
				using (var fs = new FileStream(ImageFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
				using (var bw = new BinaryWriter(fs)) {
					bw.Write(msImage.ToArray());
				}
			}
			else {
				using (var fs = new FileStream(ImageFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
				using (var sw = new StreamWriter(fs)) {
					sw.WriteLine(this.SVGCommands);
				}
			}
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the bitmap.
		/// </summary>
		/// <value>The bitmap.</value>
		public Bitmap Bitmap { get; set; }
		/// <summary>
		/// Gets or sets the name of the image file.
		/// </summary>
		/// <value>The name of the image file.</value>
		public string ImageFileName { get; set; }
		/// <summary>
		/// Gets or sets the type of the image.
		/// </summary>
		/// <value>The type of the image.</value>
		public ImageTypes ImageType { get; set; }
		/// <summary>
		/// Gets or sets the image URL.
		/// </summary>
		/// <value>The image URL.</value>
		public string ImageUrl { get; set; }
		/// <summary>
		/// Gets or sets the picture data.
		/// </summary>
		/// <value>The picture data.</value>
		public string PictureData { get; set; }
		/// <summary>
		/// Gets or sets the SVG commands.
		/// </summary>
		/// <value>The SVG commands.</value>
		public string SVGCommands { get; set; }
		#endregion Public Properties
	}
}