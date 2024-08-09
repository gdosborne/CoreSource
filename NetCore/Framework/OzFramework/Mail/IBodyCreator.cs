/* File="IBodyCreator"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Outlook;

namespace Common.Mail {
    public interface IBodyCreator {
        string Generate(MailMessage msg, Account acct);
        string Generate(MailMessage msg, Account acct, string templateFilename);
        string PagePrefix { get; set; }
        string PageSuffix { get; set; }
        string PageHeader { get; set; }
        string PageFooter { get; set; }
    }
}
