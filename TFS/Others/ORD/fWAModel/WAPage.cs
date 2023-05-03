// ----------------------------------------------------------------------- Copyright (c) Statistics & Controls, Inc. All rights reserved. Created by: Greg -----------------------------------------------------------------------
//
// [your comment here]
namespace SNC.OptiRamp {

	using SNC.OptiRamp.PageObjects;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;

	/// <summary>
	/// Class WAPage.
	/// </summary>
	public sealed class WAPage : WAItem {

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAPage"/> class.
		/// </summary>
		/// <param name="id">      The identifier.</param>
		/// <param name="name">    The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAPage(int id, string name, int sequence)
			: base(id, name, sequence) {
			Pages = new List<WAPage>();
			Objects = new List<WAObject>();
			BackgroundBrush = Defaults.PageDefaults.Background;

			ScriptLines = new List<string>();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WAPage"/> class.
		/// </summary>
		/// <param name="id">      The identifier.</param>
		/// <param name="name">    The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="isPopup"> if set to <c>true</c> [is popup].</param>
		public WAPage(int id, string name, int sequence, bool isPopup)
			: this(id, name, sequence) {
			IsPopupPage = isPopup;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the background brush.
		/// </summary>
		/// <value>The background brush.</value>
		public WABrush BackgroundBrush {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the background picture.
		/// </summary>
		/// <value>The background picture.</value>
		public WAPicture BackgroundPicture {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is popup page.
		/// </summary>
		/// <value><c>true</c> if this instance is popup page; otherwise, <c>false</c>.</value>
		public bool IsPopupPage {
			get;
			set;
		}
		/// <summary>
		/// Gets the objects.
		/// </summary>
		/// <value>The objects.</value>
		public List<WAObject> Objects {
			get;
			private set;
		}
		/// <summary>
		/// Gets or sets the page folder.
		/// </summary>
		/// <value>The page folder.</value>
		public string PageFolder {
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
		/// Gets or sets the page URL folder.
		/// </summary>
		/// <value>The page URL folder.</value>
		public string PageUrlFolder {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>The path.</value>
		public string Path {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the script lines.
		/// </summary>
		/// <value>The script lines.</value>
		public List<string> ScriptLines {
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
		/// Gets or sets the name of the template.
		/// </summary>
		/// <value>The name of the template.</value>
		public string TemplateName {
			get;
			set;
		}
		#endregion Public Properties
	}
}