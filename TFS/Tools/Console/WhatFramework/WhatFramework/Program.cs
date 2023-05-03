using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WhatFramework
{
    internal static class Extensions
    {
        public static char Dash = '-';

        public static string Expand(char c, int count)
        {
            return new string(c, count);
        }

        public static string GetCopyright()
        {
            var result = string.Empty;
            var attribs = Assembly.GetExecutingAssembly().GetCustomAttributes(true);
            foreach (var att in attribs)
            {
                if (att is AssemblyCopyrightAttribute)
                {
                    result = (att as AssemblyCopyrightAttribute).Copyright;
                    break;
                }
            }
            return result;
        }
    }

    internal class Program
    {
        public static void Main()
        {
            Console.Title = "What Framework";

            //Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("What Framework");
            Console.WriteLine($"Version {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine($"{Extensions.GetCopyright()}{Environment.NewLine}{Environment.NewLine}{Extensions.Expand(Extensions.Dash, 40)}");
            GetPre45VersionFromRegistry();
            Console.WriteLine($"{Environment.NewLine}Latest version{Environment.NewLine}{Extensions.Expand(Extensions.Dash, 40)}");
            Get45PlusFromRegistry();
            Console.Write($"{Environment.NewLine}Press any key to continue...");
            Console.ReadKey();
        }

        // Checking the version using >= will enable forward compatibility.
        private static string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 461308)
                return "4.7.1 or later";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if ((releaseKey >= 379893))
                return "4.5.2";
            if ((releaseKey >= 378675))
                return "4.5.1";
            if ((releaseKey >= 378389))
                return "4.5";
            return ".NET Framework Version 4.5 or later is not detected.";
        }

        private static void Get45PlusFromRegistry()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            using (RegistryKey ndpKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty).OpenSubKey(subkey))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    Console.WriteLine($".NET Framework Version: {CheckFor45PlusVersion((int)ndpKey.GetValue("Release"))}");
                else
                    Console.WriteLine(".NET Framework Version 4.5 or later is not detected.");
            }
        }

        private static void GetPre45VersionFromRegistry()
        {
            // Opens the registry key for the .NET Framework entry.
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty).
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {
                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", string.Empty);
                        string sp = versionKey.GetValue("SP", string.Empty).ToString();
                        string install = versionKey.GetValue("Install", string.Empty).ToString();
                        if (string.IsNullOrEmpty(install)) //no install info, must be later.
                            WriteVersion(versionKeyName, name, null);
                        else
                        {
                            if (!string.IsNullOrEmpty(sp) && install.Equals("1"))
                                WriteVersion(versionKeyName, name, $"SP{sp}");
                        }
                        if (!string.IsNullOrEmpty(name))
                            continue;
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", string.Empty);
                            if (!string.IsNullOrEmpty(name))
                                sp = subKey.GetValue("SP", string.Empty).ToString();
                            install = subKey.GetValue("Install", string.Empty).ToString();
                            if (string.IsNullOrEmpty(install)) //no install info, must be later.
                                WriteVersion(versionKeyName, name, null);
                            else
                            {
                                if (!string.IsNullOrEmpty(sp) && install.Equals("1"))
                                    WriteVersion(subKeyName, name, $"SP{sp}");
                                else if (install == "1")
                                    WriteVersion(subKeyName, name, null);
                            }
                        }
                    }
                }
            }
        }

        private static void WriteVersion(string part1, string part2, string part3)
        {
            var triggers = new List<string> { "Client", "Full" };
            if (triggers.Contains(part1))
            {
                part1 = part1.PadLeft(10).PadRight(16);
                Console.Write(part1);
                Console.Write(part2);
            }
            else
            {
                part1 = part1.PadRight(10);
                Console.Write(part1);
                Console.Write(part2.PadLeft(20));
            }
            Console.WriteLine(string.IsNullOrEmpty(part3) ? string.Empty : part3.PadLeft(5));
        }
    }
}