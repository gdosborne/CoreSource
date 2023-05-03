// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace ORDModel
{
	using ORDControls.ItemProperties;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows.Controls;
	using System.Xml.Linq;

	/// <summary>
	/// Class Project. This class cannot be inherited.
	/// </summary>
	public sealed class Project : INotifyPropertyChanged, IPropertyItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Project"/> class.
		/// </summary>
		/// <param name="doc">The document.</param>
		/// <param name="projectImageFolder">The project image folder.</param>
		/// <param name="fileName">Name of the file.</param>
		public Project(XDocument doc, string projectImageFolder, string fileName) {
			Revisions = new ObservableCollection<Revision>();
			Computers = new ObservableCollection<Computer>();
			Brushes = new ObservableCollection<Brush>();
			Fonts = new ObservableCollection<Font>();

			FileName = fileName;
			ProjectImagesFolder = projectImageFolder;
			Source = doc;

			doc.Root.Elements().ToList().ForEach(element =>
			{
				if (element.Name.LocalName.StartsWith("N", StringComparison.Ordinal) && element.Attribute("Type").Value.Equals(TypeIDs.ROOT)) {
					Name = element.Attribute("Name").Value;
					Number = element.Attribute("ProjectNumber").Value;
					Description = element.Attribute("ProjectNotes").Value;
					element.Elements().ToList().ForEach(child =>
					{
						if (child.Name.LocalName.StartsWith("N", StringComparison.Ordinal)) {
							Computers.Add(Computer.FromXElement(child));
						}
						else if (child.Name.LocalName.Equals("RevisionList", StringComparison.Ordinal)) {
							child.Elements().ToList().ForEach(revision =>
							{
								Revisions.Add(Revision.FromXElement(revision));
							});
						}
					});
				}
				else if (element.Name.LocalName.Equals("brushes", StringComparison.Ordinal)) {
					element.Elements().ToList().ForEach(brush =>
					{
						Brushes.Add(Brush.FromXElement(brush));
					});
				}
				else if (element.Name.LocalName.Equals("fonts", StringComparison.Ordinal)) {
					element.Elements().ToList().ForEach(font =>
					{
						Fonts.Add(Font.FromXElement(font));
					});
				}
				else if (element.Name.LocalName.Equals("developer", StringComparison.Ordinal)) {
				}
				else if (element.Name.LocalName.Equals("Peers", StringComparison.Ordinal)) {
				}
			});

			Revisions.CollectionChanged += local_CollectionChanged;
			Computers.CollectionChanged += local_CollectionChanged;
			Brushes.CollectionChanged += local_CollectionChanged;
			Fonts.CollectionChanged += local_CollectionChanged;
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Gets the properties control.
		/// </summary>
		/// <returns>UserControl.</returns>
		public UserControl GetPropertiesControl() {
			var result = new ProjectProperties();
			if (!(result is ORDControls.ItemProperties.IItemProperties))
				return null;

			return result;
		}
		/// <summary>
		/// Saves this instance.
		/// </summary>
		public void Save() {
		}
		/// <summary>
		/// Saves as.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void SaveAs(string fileName) {
			FileName = fileName;
			Save();
		}
		#endregion Public Methods

		#region Private Methods
		private void GetOtherProjectProperties(string fileName) {
			//this is here because IProject cannot get revisions
			try {
				var root = XDocument.Load(fileName).Root;
				var element = root.Elements().First();
				var revElement = element.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("RevisionList", StringComparison.OrdinalIgnoreCase));
				if (revElement != null) {
					var itemId = 0;
					foreach (var rev in revElement.Elements()) {
						var r = Revision.FromXElement(rev);
						Revisions.Add(r);
						itemId++;
					}
				}
			}
			catch { }
		}
		private void local_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			HasChanges = true;
		}
		#endregion Private Methods

		#region Public Events
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private ObservableCollection<Brush> _Brushes;
		private ObservableCollection<Computer> _Computers;
		private string _Description;
		private ObservableCollection<Font> _Fonts;
		private bool _HasChanges;
		private string _Name;
		private string _Number;
		private ObservableCollection<Revision> _Revisions;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the brushes.
		/// </summary>
		/// <value>The brushes.</value>
		public ObservableCollection<Brush> Brushes {
			get {
				return _Brushes;
			}
			set {
				_Brushes = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Brushes"));
			}
		}
		/// <summary>
		/// Gets or sets the computers.
		/// </summary>
		/// <value>The computers.</value>
		public ObservableCollection<Computer> Computers {
			get {
				return _Computers;
			}
			set {
				_Computers = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Computers"));
			}
		}
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description {
			get {
				return _Description;
			}
			set {
				_Description = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Description"));
			}
		}
		/// <summary>
		/// Gets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { get; private set; }
		/// <summary>
		/// Gets or sets the fonts.
		/// </summary>
		/// <value>The fonts.</value>
		public ObservableCollection<Font> Fonts {
			get {
				return _Fonts;
			}
			set {
				_Fonts = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Fonts"));
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance has changes.
		/// </summary>
		/// <value><c>true</c> if this instance has changes; otherwise, <c>false</c>.</value>
		public bool HasChanges {
			get {
				return _HasChanges;
			}
			set {
				_HasChanges = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("HasChanges"));
			}
		}
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get {
				return _Name;
			}
			set {
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}
		/// <summary>
		/// Gets or sets the number.
		/// </summary>
		/// <value>The number.</value>
		public string Number {
			get {
				return _Number;
			}
			set {
				_Number = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Number"));
			}
		}
		/// <summary>
		/// Gets the project images folder.
		/// </summary>
		/// <value>The project images folder.</value>
		public string ProjectImagesFolder { get; private set; }
		/// <summary>
		/// Gets or sets the revisions.
		/// </summary>
		/// <value>The revisions.</value>
		public ObservableCollection<Revision> Revisions {
			get {
				return _Revisions;
			}
			set {
				_Revisions = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Revisions"));
			}
		}
		/// <summary>
		/// Gets the source.
		/// </summary>
		/// <value>The source.</value>
		public XDocument Source { get; private set; }
		#endregion Public Properties
	}
}