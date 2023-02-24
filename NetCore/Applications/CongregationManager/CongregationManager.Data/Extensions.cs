using Common.Application;
using Common.Application.Media;
using Common.Application.Primitives;
using Common.Application.Windows.Expressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace CongregationManager.Data {
    public static class Extensions {
        public static string ShortenedName(this FileInfo file) {
            var result = file.Name;
            if (result.Contains(".congregation", StringComparison.OrdinalIgnoreCase)) {
                result = result.Replace(".congregation", string.Empty);
            }
            return result;
        }

        public static Dictionary<T, string> GetDescriptions<T>() where T : Enum {
            var result = new Dictionary<T, string>();
            var values = Enum.GetValues(typeof(T));
            foreach ( var value in values ) {
                result.Add((T)value, value.DescriptionValue());
            }
            return result;
        }

        public static string DescriptionValue<T>(this T source) {
            var fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static void SetupColors(this Settings appSettings, List<string> names, ResourceDictionary resources) {            
            //foreach (var name in names) {
            //    var resValue = ((SolidColorBrush)resources[name]).Color;
            //    var setValue = appSettings.GetValue("AlternateColors", name, resValue.ToHexValue());
            //    resources[name] = new SolidColorBrush(setValue.ToColor());
            //}
        }

        public static List<string> GetBrushNames(this ResourceDictionary resources) {
            var result = new List<string>();
            foreach (var dict in resources.MergedDictionaries) {
                foreach (var name in dict.Keys) {
                    if (name.Is<string>()) {
                        if (dict[name].Is<SolidColorBrush>()) {
                            result.Add((string)name);
                        }
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, string> BrushAliasNames(this ResourceDictionary resources) {
            var result = new Dictionary<string, string>();
            foreach (var dict in resources.MergedDictionaries) {
                foreach (var name in dict.Keys) {
                    if (name.Is<string>()) {
                        if (name.As<string>().StartsWith("AliasKey-")) {
                            result.Add(name.As<string>().Replace("AliasKey-", string.Empty), dict[name].As<string>());
                        }
                    }
                }
            }
            return result;
        }
    }
}
