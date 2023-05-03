using System;
using System.Collections.Generic;

namespace MyApplication.Registry
{
	public class RegistrySection
	{
		#region Public Constructors

		public RegistrySection()
		{
			Values = new Dictionary<string, object>();
			Sections = new List<RegistrySection>();
		}

		#endregion

		#region Public Properties
		public string Name { get; set; }
		public List<RegistrySection> Sections { get; internal set; }
		public Dictionary<string, object> Values { get; internal set; }
		#endregion
	}
}