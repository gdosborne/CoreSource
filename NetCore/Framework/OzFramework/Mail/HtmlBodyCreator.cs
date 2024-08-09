/* File="HtmlBodyCreator"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Globalization;
using System.IO;
using System.Text;
using Common.Primitives;
using Microsoft.Office.Interop.Outlook;

using Universal.Common;

namespace Common.Mail {
    public class HtmlBodyCreator : IBodyCreator {
        public string PagePrefix { get; set; }
        public string PageSuffix { get; set; }
        public string PageHeader { get; set; }
        public string PageFooter { get; set; }
        public string AboutInfo { get; set; }
        public string Generate(MailMessage msg, Account acct, string templateFilename) {
            PagePrefix = msg.PagePrefix;
            PageSuffix = msg.PageSuffix;
            PageHeader = msg.PageHeader;
            PageFooter = msg.PageFooter;
            AboutInfo = msg.AboutInfo.Replace("\r\n", "<br />");

            if (templateFilename.IsNull() || !File.Exists(templateFilename)) {
                return Generate(msg, acct);
            }
            var result = new FileInfo(templateFilename).OpenRead().ReadToEnd();
            var dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt");

            var body = new StringBuilder();
            using var reader = new StringReader(msg.Body.ToString());
            while (reader.Peek() != -1) {
                var line = reader.ReadLine();
                if (line.IsNull()) continue;
                body.Append(line);
                body.AppendLine(HtmlTags.Break).AppendLine(HtmlTags.Break);
            }

            result = result
                .Replace("{PageHeader}", PageHeader)
                .Replace("{PagePrefix}", PagePrefix)
                .Replace("{SubmitterName}", acct.CurrentUser.Name)
                .Replace("{SubmittalDateTime}", dateTime)
                .Replace("{AboutInfo}", AboutInfo)
                .Replace("{Body}", body.ToString())
                .Replace("{PageSuffix}", PageSuffix)
                .Replace("{PageFooter}", PageFooter);
            return result;
        }

        public string Generate(MailMessage msg, Account acct) {
            PagePrefix = msg.PagePrefix;
            PageSuffix = msg.PageSuffix;
            PageHeader = msg.PageHeader;
            PageFooter = msg.PageFooter;

            var result = new StringBuilder();
            result.AppendLine(HtmlTags.DocType);
            result.AppendLine(HtmlTags.HtmlStart);
            result.AppendLine(HtmlTags.BodyStart);
            if (!PageHeader.IsNull()) {
                result.AppendLine(HtmlTags.PageHeaderStart);
                result.AppendLine(PageHeader);
                result.AppendLine(HtmlTags.ParaEnd);
            }
            if (!PagePrefix.IsNull()) {
                result.Append(HtmlTags.PagePrefixStart);
                result.AppendLine(PagePrefix);
                result.Append(HtmlTags.ParaEnd);
            }

            result.AppendLine(HtmlTags.ParaStart);
            result.Append(HtmlTags.BoldStart);
            result.Append("Submitter: ");
            result.Append(HtmlTags.BoldEnd);
            result.Append(acct.CurrentUser.Name);
            result.Append(HtmlTags.Break);
            result.Append(HtmlTags.BoldStart);
            result.Append("Submitted: ");
            result.Append(HtmlTags.BoldEnd);
            result.Append(DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat));
            result.AppendLine(HtmlTags.ParaEnd);

            result.AppendLine(HtmlTags.Hline);

            using var reader = new StringReader(msg.Body.ToString());
            while (reader.Peek() != -1) {
                var line = reader.ReadLine();
                if (line.IsNull()) continue;
                result.AppendLine(HtmlTags.ParaStart);
                result.AppendLine(line);
                result.AppendLine(HtmlTags.ParaEnd);
            }
            using var reader1 = new StringReader(msg.AboutInfo);
            while (reader1.Peek() != -1) {
                var line = reader1.ReadLine();
                //if (line.IsNull()) continue;
                result.AppendLine(line);
            }
            result.AppendLine(HtmlTags.Hline);
            if (!PageSuffix.IsNull()) {
                result.Append(HtmlTags.PageSuffixStart);
                result.AppendLine(PageSuffix);
                result.Append(HtmlTags.ParaEnd);
            }
            if (!PageFooter.IsNull()) {
                result.Append(HtmlTags.PageFooterStart);
                result.AppendLine(PageFooter);
                result.Append(HtmlTags.ParaEnd);
            }
            result.AppendLine(HtmlTags.BodyEnd);
            result.AppendLine(HtmlTags.HtmlEnd);
            return result.ToString();
        }
    }
}
