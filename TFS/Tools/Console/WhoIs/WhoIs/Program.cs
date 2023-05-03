using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WhoIs
{
    internal class Program
    {
        private static XDocument GetXDocFromUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/xml";
            HttpWebResponse response = null;
            using (var dataStream = request.GetResponse())
            {
                response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    var responseStream = response.GetResponseStream();
                    using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        var doc = XDocument.Load(reader);
                        Console.WriteLine($"  received {doc.ToString().Length} more bytes of data");
                        return doc;
                    }
                }
            }
            return null;
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"WhoIs Version {Assembly.GetExecutingAssembly().GetName().Version}{Environment.NewLine}");
            var nm = args.Length > 0 ? args[0] : string.Empty;
            if (string.IsNullOrEmpty(nm))
            {
                Console.Write("Missing name or ip address...");
                Console.ReadKey();
                return;
            }
            var urlData = $"showDetails=true&showARIN=false&showNonArinTopLevelNet=false&ext=netref2";
            var resultData = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create($"https://whois.arin.net/rest/nets;q={nm}");
            request.Method = "GET";
            request.ContentType = "application/xml";

            var docs = new List<XDocument>();

            var sb = new StringBuilder(1024 * 5);
            request.Timeout = 100000;
            if (!string.IsNullOrEmpty(urlData))
            {
                Console.WriteLine($"Querying {request.Address}");
                var byteArray = Encoding.UTF8.GetBytes(urlData);
                HttpWebResponse response = null;
                try
                {
                    using (var dataStream = request.GetResponse())
                    {
                        response = (HttpWebResponse)request.GetResponse();
                        Console.WriteLine($"  received response");
                        if (response != null)
                        {
                            var responseStream = response.GetResponseStream();
                            using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                var doc = XDocument.Load(reader);
                                Console.WriteLine($"  received {doc.ToString().Length} bytes of data");
                                docs.Add(doc);
                                var root = doc.Root;
                                if (root.Name.LocalName.Equals("nets"))
                                {
                                    foreach (var item in root.Elements())
                                    {
                                        if (item.Name.LocalName == "limitExceeded")
                                            continue;
                                        var subDoc = GetXDocFromUrl(item.Value);
                                        if (doc != null)
                                            docs.Add(subDoc);
                                    }
                                }
                            }
                        }
                        dataStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                if (docs.Any())
                {
                    Console.WriteLine($"Found {docs.Count} records to display.");
                    Console.Write("Display to (");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("S");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")creen or (");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("C");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(")opy to clipboard? ");
                    var key = Console.ReadKey();
                    var data = string.Join(Environment.NewLine, docs.Select(x => x.ToString()));
                    if (key.Key == ConsoleKey.C)
                    {
                        System.Windows.Forms.Clipboard.SetText(data);
                    }
                    else
                    {
                        Console.WriteLine(data);
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine($"Cannot find any information for \"{nm}\"");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}