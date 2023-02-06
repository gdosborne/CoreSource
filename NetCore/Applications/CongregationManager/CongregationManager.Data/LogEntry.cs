using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CongregationManager.Data {
    public class LogEntry {
        public string Time { get; set; }
        public string LogItemType { get; set; }
        public string Message { get; set; }

        public static List<LogEntry> GetLogEntriesForLog(string logDir, string logDate) {
            var dir = Path.Combine(logDir, DateTime.Parse(logDate).ToString("yyyy-MM-dd"));
            var filename = Path.Combine(dir, "application.xml");

            var result = new List<LogEntry>();
            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
                return result;
            var data = File.ReadAllText(filename);
            var doc = XDocument.Parse(data);
            foreach (var item in doc.Root.Element("entries").Elements()) {
                result.Add(FromXElement(item));
            }
            return result.OrderByDescending(x => x.Time).ToList();
        }

        public static LogEntry? FromXElement(XElement? element) {
            if (element == null)
                return null;
            var tod = DateTime.Parse(element.Attribute("timestamp").Value).TimeOfDay;
            var tm = tod.ToString(@"hh\:mm\:ss\.fff");
            var ty = element.Attribute("type").Value;
            var mg = element.Value;
            var result = new LogEntry {
                Time = tm,
                LogItemType = ty,
                Message = mg
            };
            return result;
        }
    }
}
