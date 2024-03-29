using System;
using System.Collections.Generic;

namespace MyApplication.IO
{
	public static class Directory
	{
		#region Public Methods

		public static System.IO.FileAttributes? Attributes(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).Attributes;
		}

		public static DateTime? CreationTime(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).CreationTime;
		}

		public static DateTime? CreationTimeUtc(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).CreationTimeUtc;
		}

		public static string Extension(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).Extension;
		}

		public static List<System.IO.FileInfo> GetFiles(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new List<System.IO.FileInfo>(new System.IO.DirectoryInfo(directoryName).GetFiles());
		}

		public static List<System.IO.FileInfo> GetFiles(string directoryName, string filter)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new List<System.IO.FileInfo>(new System.IO.DirectoryInfo(directoryName).GetFiles(filter));
		}

		public static DateTime? LastAccessTime(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).LastAccessTime;
		}

		public static DateTime? LastAccessTimeUtc(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).LastAccessTimeUtc;
		}

		public static DateTime? LastWriteTime(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).LastWriteTime;
		}

		public static DateTime? LastWriteTimeUtc(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			return new System.IO.DirectoryInfo(directoryName).LastWriteTimeUtc;
		}

		public static long? Size(string directoryName, string filter)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			long result = 0;
			GetFiles(directoryName, filter).ForEach(x => result += x.Length);
			return result;
		}

		public static long? Size(string directoryName)
		{
			if (!System.IO.Directory.Exists(directoryName))
				return null;
			long result = 0;
			GetFiles(directoryName).ForEach(x => result += x.Length);
			return result;
		}

		#endregion
	}
}