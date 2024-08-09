/* File="HtmlTags"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mail {
    internal static class HtmlTags {
        public static readonly string DocType = "<!DOCTYPE html>";
        public static readonly string HtmlStart = "<html>";
        public static readonly string HtmlEnd = "</html>";
        public static readonly string PagePrefixStart = "<p style=\"font-family: verdana;font-size:1.1em;font-weight: bold;\">";
        public static readonly string PageSuffixStart = "<p style=\"font-family: verdana;font-size:1.1em;\">";
        public static readonly string PageHeaderStart = "<p style=\"font-family: verdana;font-size:1.3em;font-weight: bold;\">";
        public static readonly string PageFooterStart = "<p style=\"font-family: verdana;font-size:0.8em;font-weight: bold;\">";
        public static readonly string BodyStart = "<body>";
        public static readonly string BodyEnd = "</body>";
        public static readonly string ParaStart = $"<p style=\"font-family: verdana;font-size:1.0em\">";
        public static readonly string ParaEnd = "</p>";
        public static readonly string Hline = "<hr />";
        public static readonly string Break = "<br />";
        public static readonly string BoldStart = "<strong>";
        public static readonly string BoldEnd = "</strong>";
    }
}
