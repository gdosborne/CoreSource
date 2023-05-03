using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;

namespace MyApplication.IO
{
	public static class Path
	{
		#region Public Methods

		public static System.IO.DriveInfo GetDriveInfo(string value)
		{
			if (value.StartsWith(@"\\", StringComparison.CurrentCulture))
				return null;
			var result = System.IO.DriveInfo.GetDrives()
				.Where(x => value.StartsWith(x.Name, StringComparison.CurrentCultureIgnoreCase))
				.FirstOrDefault();
			return result;
		}

		public static string GetDriveLetter(string value)
		{
			if (value.StartsWith(@"\\", StringComparison.CurrentCulture))
				return null;
			var di = MyApplication.IO.Path.GetDriveInfo(value);
			if (di != null)
				return di.RootDirectory.FullName.Replace(System.IO.Path.DirectorySeparatorChar.ToString(CultureInfo.CurrentCulture), string.Empty).ToUpper(CultureInfo.CurrentCulture);
			return null;
		}

		public static System.IO.DriveType GetDriveType(string value)
		{
			var result = GetDriveInfo(value);
			if (result == null)
				return System.IO.DriveType.Unknown;
			return result.DriveType;
		}

		public static bool IsNetworkDrive(string value)
		{
			return MyApplication.IO.Path.GetDriveType(value) == DriveType.Network;
		}

		public static string ResolveToRootUNC(string value)
		{
			if (value.StartsWith(@"\\"))
				return System.IO.Directory.GetDirectoryRoot(value);
			ManagementObject mo = new ManagementObject();
			string driveletter = GetDriveLetter(value);
			mo.Path = new ManagementPath(string.Format("Win32_LogicalDisk='{0}'", driveletter));
			uint DriveType = Convert.ToUInt32(mo["DriveType"]);
			string NetworkRoot = Convert.ToString(mo["ProviderName"]);
			mo = null;
			return DriveType == 4 ? NetworkRoot : driveletter + System.IO.Path.DirectorySeparatorChar;
		}

		public static string ResolveToUNC(string value)
		{
			if (value.StartsWith(@"\\"))
				return value;
			string root = ResolveToRootUNC(value);
			return value.StartsWith(root) ? value : value.Replace(GetDriveLetter(value), root);
		}

		#endregion
	}
}