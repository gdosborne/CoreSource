// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {

	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Xml.Serialization;

	[Serializable]
	[XmlInclude(typeof(DesignerObject))]
	[XmlInclude(typeof(DesignerRectangle))]
	public class Page : ObjectBase {

		#region Public Constructors
		public Page()
			: base() {
			Pages = new ObservableCollection<Page>();
			Objects = new ObservableCollection<DesignerObject>();
			Objects.CollectionChanged += Objects_CollectionChanged;
		}
		#endregion Public Constructors

		#region Private Methods
		private void Objects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			if (AddControl != null) {
				foreach (var item in e.NewItems) {
					AddControl(this, new AddControlEventArgs(item.As<DesignerObject>().Control));
				}
			}
		}
		#endregion Private Methods

		#region Public Events
		public event AddControlHandler AddControl;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private ObservableCollection<DesignerObject> _Objects;
		private ObservableCollection<Page> _Pages;
		private Size _Size;
		#endregion Private Fields

		#region Public Properties
		[XmlArray("Objects")]
		public ObservableCollection<DesignerObject> Objects {
			get {
				return _Objects;
			}
			set {
				_Objects = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Objects"));
			}
		}
		[XmlArray("Pages")]
		public ObservableCollection<Page> Pages {
			get {
				return _Pages;
			}
			set {
				_Pages = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Pages"));
			}
		}
		[XmlElementAttribute("Size")]
		public Size Size {
			get {
				return _Size;
			}
			set {
				_Size = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Size"));
			}
		}
		#endregion Public Properties
	}
}
