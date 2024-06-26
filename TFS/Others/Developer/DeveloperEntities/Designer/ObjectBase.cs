// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Designer {

	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Xml.Serialization;

	[Serializable]
	public abstract class ObjectBase : INotifyPropertyChanged {

		#region Internal Constructors
		internal ObjectBase() {
			Id = Guid.NewGuid();
		}
		#endregion Internal Constructors

		#region Public Events
		public virtual event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private Guid _Id;
		private string _Name;
		#endregion Private Fields

		#region Public Properties
		[XmlElementAttribute("Id")]
		public Guid Id {
			get {
				return _Id;
			}
			set {
				_Id = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Id"));
			}
		}
		[XmlElementAttribute("Name")]
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
		#endregion Public Properties
	}
}
