/* File="MailMessage"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OzFramework.Mail {
    public sealed class MailMessage {
        public MailMessage() {
            Body = new StringBuilder();
            Attachments = new List<FileInfo>();
        }

        public string Subject { get; set; }
        public List<string> Recipients { get; set; }
        public StringBuilder Body { get; set; }
        public List<FileInfo> Attachments { get; set; }
        public string SubmitterName { get; set; }

        public string PagePrefix { get; set; }
        public string PageSuffix { get; set; }
        public string PageHeader { get; set; }
        public string PageFooter { get; set; }
        
        public string AboutInfo { get; set; }


    }
}
