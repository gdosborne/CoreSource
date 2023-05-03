using System;
using System.Collections.Generic;

namespace OptiRampDesktop.Helpers
{
	public class ApplicationSettings
	{
		#region Public Constructors

		static ApplicationSettings()
		{
			ApplicationMode = ApplicationModes.Normal;
		}

		#endregion

		#region Public Properties
		public static ApplicationModes ApplicationMode { get; set; }
		#endregion
	}
}