namespace GregOsborne.Suite.Extender.AppSettings {
	using System.Windows;
	using System.Windows.Controls;
	using GregOsborne.Application.Primitives;

	/// <summary>Setting Value for folders</summary>
	public class FolderSettingValue : StringSettingValue {

		/// <summary>Initializes a new instance of the <see cref="FolderSettingValue" /> class.</summary>
		/// <param name="name">The name.</param>
		public FolderSettingValue(string name, string propertyName)
			: base(name, propertyName) { }

	}
}
