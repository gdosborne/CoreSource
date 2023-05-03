// ----------------------------------------------------------------------- 
// Copyright © 2016 Created by: Greg Osborne 
//
// 
// 
// -----------------------------------------------------------------------
namespace SNC.OptiRamp.Application.DeveloperEntities.Management {

	using MVVMFramework;
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls.Ribbon;

	public interface IExtender {

		#region Public Methods
		void AddRibbonItems(Ribbon ribbon);
		#endregion Public Methods

		#region Public Events
		event ShowUserControlHandler ShowUserControl;
		event ProjectChangedHandler ProjectChanged;
		#endregion Public Events

		#region Public Properties
		IList<DelegateCommand> ExportedCommands {
			get;
		}
		string Name {
			get;
		}
		Category OptionsCategory {
			get;
		}
		ProjectFile ProjectFile {
			get;
			set;
		}
		void UpdateInterface();
		#endregion Public Properties
	}
}
