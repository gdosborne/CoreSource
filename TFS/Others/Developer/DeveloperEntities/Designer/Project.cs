// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {

	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Serialization;

	[Serializable]
	public class Project : ObjectBase {

		#region Public Constructors
		public Project()
			: base() {
			Pages = new List<Page>();
			if (CurrentPage != null)
				CurrentPage.AddControl += CurrentPage_AddControl;
		}
		#endregion Public Constructors

		#region Private Methods
		private void CurrentPage_AddControl(object sender, Controls.AddControlEventArgs e) {
			if (AddControl != null)
				AddControl(this, e);
		}
		#endregion Private Methods

		#region Public Events
		public event AddControlHandler AddControl;
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		[XmlElementAttribute("CurrentPage")]
		private Page _CurrentPage;
		[XmlElementAttribute("Pages")]
		private List<Page> _Pages;
		#endregion Private Fields

		#region Public Properties
		[XmlIgnore]
		public Page CurrentPage {
			get {
				return _CurrentPage;
			}
			set {
				_CurrentPage = value;
				if (CurrentPage != null)
					CurrentPage.AddControl += CurrentPage_AddControl;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
			}
		}
		[XmlArray("Pages")]
		public List<Page> Pages {
			get {
				return _Pages;
			}
			set {
				_Pages = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Pages"));
			}
		}
		#endregion Public Properties
	}
}
