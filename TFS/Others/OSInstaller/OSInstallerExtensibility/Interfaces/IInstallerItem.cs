namespace OSInstallerExtensibility.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	public enum ItemTypes
	{
		Folder,
		File
	}

	public interface IInstallerItem : IInstallerPropertyBase
	{
		#region Public Properties
		bool IncludeSubFolders { get; set; }
		ItemTypes ItemType { get; set; }
		string Path { get; set; }
		ImageSource TypeSource { get; set; }
		#endregion Public Properties
	}
}
