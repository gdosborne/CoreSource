using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MyApplication.IO
{
	public static class File
	{
		#region Private Fields
		private static char[] IllegalCharacters = new char[] { '/', '?', '<', '>', '\\', ':', '*', '|' };
		#endregion

		#region Public Methods

		public static System.IO.FileAttributes? Attributes(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Attributes;
		}

		public static DateTime? CreationTime(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).CreationTime;
		}

		public static DateTime? CreationTimeUtc(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).CreationTimeUtc;
		}

		public static string Extension(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Extension;
		}

		public static byte[] GetBytes(this FileInfo value)
		{
			byte[] result = null;
			using (var br = new BinaryReader(value.OpenRead()))
			{
				result = new byte[Convert.ToInt32(br.BaseStream.Length)];
				br.Read(result, 0, result.Length);
			}
			return result;
		}

		public static string GetLegalFileName(string fileName, char illegalReplacement)
		{
			if (IsLegalFileName(fileName))
				return fileName;
			var path = System.IO.Path.GetDirectoryName(fileName);
			var fName = System.IO.Path.GetFileName(fileName);
			var temp = string.Empty;
			fName.ToCharArray().ToList().ForEach(x =>
			{
				if (IllegalCharacters.Contains(x))
					temp += illegalReplacement;
				else
					temp += x;
			});
			return System.IO.Path.Combine(path, temp);
		}

		public static Version GetVersion(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			try
			{
				var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(fileName);
				return new Version(fvi.FileVersion.Replace(',', '.').ToString(CultureInfo.CurrentCulture));
			}
			catch
			{
				try
				{
					var assy = System.Reflection.Assembly.LoadFile(fileName);
					return assy.GetName().Version;
				}
				catch
				{
					return null;
				}
			}
		}

		public static bool IsLegalFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return false;
			return !System.IO.Path.GetFileName(fileName).ToCharArray().Any(x => IllegalCharacters.Contains(x));
		}

		public static DateTime? LastAccessTime(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).LastAccessTime;
		}

		public static DateTime? LastAccessTimeUtc(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).LastAccessTimeUtc;
		}

		public static DateTime? LastWriteTime(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).LastWriteTime;
		}

		public static DateTime? LastWriteTimeUtc(string fileName)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			return new System.IO.FileInfo(fileName).LastWriteTimeUtc;
		}

		public static System.IO.FileStream OpenFile(string fileName, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share)
		{
			if (string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Open(mode, access, share);
		}

		public static System.IO.FileStream OpenFile(string fileName, System.IO.FileMode mode, System.IO.FileAccess access)
		{
			if (string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Open(mode, access);
		}

		public static System.IO.FileStream OpenFile(string fileName, System.IO.FileMode mode)
		{
			if (string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Open(mode);
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public static List<string> ReadAllLines(string fileName, System.IO.FileShare share, bool includeBlankLines)
		{
			if (!System.IO.File.Exists(fileName))
				return null;
			var result = new List<string>();
			using (var fs = OpenFile(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, share))
			using (var sr = new System.IO.StreamReader(fs))
			{
				while (sr.Peek() != -1)
				{
					var line = sr.ReadLine();
					if (!includeBlankLines && string.IsNullOrEmpty(line))
						continue;
					result.Add(line);
				}
			}
			return result;
		}

		public static List<string> ReadAllLines(string fileName, System.IO.FileShare share)
		{
			return ReadAllLines(fileName, share, true);
		}

		public static long? Size(string fileName)
		{
			if (string.IsNullOrEmpty(fileName) && IsLegalFileName(fileName))
				return null;
			return new System.IO.FileInfo(fileName).Length;
		}

		#endregion
	}
}