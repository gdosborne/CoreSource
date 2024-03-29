namespace OSInstallerExtensibility.Classes.Data
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	public class InstallerItem : IInstallerItem
	{
		#region Public Constructors
		public InstallerItem(string name)
		{
			Name = name;
		}
		#endregion Public Constructors

		#region Public Properties
		public bool IncludeSubFolders { get; set; }
		public ItemTypes ItemType { get; set; }
		public string Name { get; private set; }
		public string Path { get; set; }
		public ImageSource TypeSource { get; set; }
		#endregion Public Properties
	}
}
