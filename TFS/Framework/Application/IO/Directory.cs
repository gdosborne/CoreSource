using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GregOsborne.Application.Primitives;

namespace GregOsborne.Application.IO {
    public static class Directory {
        public static FileAttributes? Attributes(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (FileAttributes?) null : new DirectoryInfo(directoryName).Attributes;
        public static DateTime? CreationTime(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).CreationTime;
        public static DateTime? CreationTimeUtc(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).CreationTimeUtc;
        public static string Extension(string directoryName) => !System.IO.Directory.Exists(directoryName) ? null : new DirectoryInfo(directoryName).Extension;
        public static List<FileInfo> GetFiles(string directoryName) => !System.IO.Directory.Exists(directoryName) ? null : new List<FileInfo>(new DirectoryInfo(directoryName).GetFiles());
        public static List<FileInfo> GetFiles(string directoryName, string filter) => !System.IO.Directory.Exists(directoryName) ? null : new List<FileInfo>(new DirectoryInfo(directoryName).GetFiles(filter));
        public static DateTime? LastWriteTime(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).LastWriteTime;
        public static DateTime? LastWriteTimeUtc(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?) null : new DirectoryInfo(directoryName).LastWriteTimeUtc;
        public static long? Size(string directoryName) => Size(directoryName, "*.*");
        public static long? Size(string directoryName, string filter) => !System.IO.Directory.Exists(directoryName) ? (long?)null : GetFiles(directoryName, filter).ToList().Sum(x => x.Length);
        public static DateTime? LastAccessTimeUtc(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?)null : new DirectoryInfo(directoryName).LastAccessTimeUtc;
        public static DateTime? LastAccessTime(string directoryName) => !System.IO.Directory.Exists(directoryName) ? (DateTime?)null : new DirectoryInfo(directoryName).LastAccessTime;

        public static DirectoryInfo CreateIfNotExist(this DirectoryInfo value) {
            if (value == null)
                return value;
            if (!value.Exists)
                value.Create();
            return value;
        }

		public static void CleanupDirectory(this DirectoryInfo dirInfo, params string[] excludeExtensions) {
			if (dirInfo.Exists) {
				var files = dirInfo.GetFiles().Where(x => !excludeExtensions.ContainsIgnoreCase(x.Extension)).ToList();
				files.ForEach(x => x.Delete());
			}
		}

		public static long Size(this DirectoryInfo value, string filter = "*.*", bool isRecursive = true) {
            if (value == null || !value.Exists)
                return 0;
            var result = value.GetFiles(filter).ToList().Sum(x => x.Length);
            if (!isRecursive || value.GetDirectories().Length == 0)
                return result;
            result += value.GetDirectories().Select(x => x.Size(filter, isRecursive)).Sum();
            return result;
        }
        
        public static string GetTempFile(this DirectoryInfo dirInfo) {
            var fileName = new Random().Next().ToString("X") + ".tmp";
            while (System.IO.File.Exists(System.IO.Path.Combine(dirInfo.FullName, fileName)))
                fileName = new Random().Next().ToString("X") + ".tmp";
            return System.IO.Path.Combine(dirInfo.FullName, fileName);
        }

		public static DirectoryInfo[] FindAllByName(this DirectoryInfo parent, string directoryName, bool isRecursive = true) {
			var result = default(DirectoryInfo[]);

			result = SearchDirectory(parent, directoryName, isRecursive);

			if (result == null) {
				result = new DirectoryInfo[0];
			}
			return result;
		}

		private static DirectoryInfo[] SearchDirectory(DirectoryInfo dir, string directoryName, bool isRecursive) {
			var result = new List<DirectoryInfo>();

			if (dir.Name.Equals(directoryName, StringComparison.OrdinalIgnoreCase)) {
				result.Add(dir);
			}
			//this is necessary for unc (network) paths because they can timeout or not be available
			var dirs = default(DirectoryInfo[]);
			var st = Task.Factory.StartNew(() => {
				try {
					dirs = dir.GetDirectories();
				}
				catch { }
			});
			st.Wait(TimeSpan.FromSeconds(5));
			if (dirs == null) {
				return result.ToArray();
			}

			if (isRecursive) {
				foreach (var sub in dirs) {
					result.AddRange(SearchDirectory(sub, directoryName, isRecursive));
				}
			}
			return result.ToArray();
		}
	}
}