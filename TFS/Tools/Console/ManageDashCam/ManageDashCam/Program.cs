using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageDashCam {
    class Program {
        static void Main(string[] args) {
            var folder = args.FirstOrDefault(x => x.StartsWith("/f", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Dash Cam Video Manager");
            var dir = folder == null ? @"J:\Dash Cam" : folder.Substring(3).Replace("\"", string.Empty);

            if (!Directory.Exists(dir)) {
                Console.WriteLine("Specified directory does not exist");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
            }
            var dInfo = new DirectoryInfo(dir);
            var fileCount = dInfo.GetFiles().Length;
            var clearLine = new string(' ', 79);
            var line = default(string);
            var fileName = default(string);

            Console.WriteLine($"Source folder: {dir}");
            Console.WriteLine($"There are {fileCount} videos in the source folder");
            var files = dInfo.GetFiles();

            for (int i = 0; i < fileCount; i++) {
                fileName = files[i].Name;
                line = fileName.PadRight(79);

                var year = fileName.Substring(0, 4);
                var month = fileName.Substring(5, 2);
                var day = fileName.Substring(7, 2);

                var datePath = Path.Combine(dir, year, month, day);

                if (!Directory.Exists(datePath))
                    Directory.CreateDirectory(datePath);

                Console.CursorLeft = 0;
                Console.CursorTop = 4;
                Console.Write(clearLine);
                Console.CursorLeft = 0;
                Console.CursorTop = 4;
                Console.Write(line);

                var destFileName = Path.Combine(datePath, fileName);
                if (File.Exists(destFileName))
                    File.Delete(destFileName);
                try {
                    files[i].MoveTo(destFileName);
                }
                catch { }

            }
            Console.CursorLeft = 0;
            Console.CursorTop = 4;
            Console.WriteLine(clearLine);
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
