// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// 
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Configuration {

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;

	public sealed class Category : ConfigBase {

		#region Public Constructors
		public Category() {
			Pages = new ObservableCollection<Page>();
		}
		#endregion Public Constructors

		#region Public Properties
		private ObservableCollection<Page> _Pages;
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
		public override event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Properties
	}
}
