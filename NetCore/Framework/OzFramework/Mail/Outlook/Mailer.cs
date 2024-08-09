/* File="Mailer"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Documents;

using Common.Dialogs;

using Microsoft.Office.Interop.Outlook;
using oLook = Microsoft.Office.Interop.Outlook;

using Ookii.Dialogs.Wpf;

namespace Common.Mail.Outlook {
    public class Mailer : IMailer {
        public void Initialize() {
            outlookApp = new oLook.Application();
            var accounts = outlookApp.Session.Accounts;
            if (accounts.Count > 1) {
                SelectAccount();
            } else {
                //these arrays start at index 1
                this.account = accounts[1];
            }
        }

        private void SelectAccount() {
            var accounts = outlookApp.Session.Accounts;
            var td = new TaskDialog {
                MainIcon = TaskDialogIcon.Shield,
                MainInstruction = "Select Account",
                Content = "You have multiple Outlook© accounts. Please select the account to use for sending enail.",
                AllowDialogCancellation = true,
                ButtonStyle = TaskDialogButtonStyle.Standard,
                WindowTitle = "Select Account",
                Width = 200,
                CenterParent = true
            };
            var accts = new Dictionary<oLook.Account, TaskDialogRadioButton>();
            foreach (oLook.Account account in accounts ) {
                var tdrb = new TaskDialogRadioButton() { Text = account.DisplayName, Checked = !accts.Any() };
                td.RadioButtons.Add(tdrb);
                accts.Add(account, tdrb);
            }
            td.Buttons.Add(new TaskDialogButton(ButtonType.Ok));
            td.Buttons.Add(new TaskDialogButton(ButtonType.Cancel));
            var result = td.ShowDialog(null);
            if(result.ButtonType == ButtonType.Ok) {
                foreach (var acct in accts) {
                    if(acct.Value.Checked) {
                        this.account = acct.Key;
                        break;
                    }
                }
            }
        }

        private oLook.Application outlookApp = default;
        private oLook.Account account = default;
        public bool SendMail(MailMessage msg, string templateFilename = default) {
            try {
                var bodyCreator = new HtmlBodyCreator();
                var mailMessage = (MailItem)outlookApp.CreateItem(OlItemType.olMailItem);
                mailMessage.BodyFormat = OlBodyFormat.olFormatHTML;
                mailMessage.Importance = OlImportance.olImportanceHigh;
                mailMessage.Categories = "PEAT";
                mailMessage.FlagDueBy = DateTime.Now.AddDays(1);
                mailMessage.FlagIcon = OlFlagIcon.olRedFlagIcon;                
                mailMessage.SendUsingAccount = account;
                mailMessage.Subject = msg.Subject;
                
                mailMessage.HTMLBody = bodyCreator.Generate(msg, account, templateFilename);
                foreach (var recip in msg.Recipients) {
                    mailMessage.Recipients.Add(recip);
                }
                foreach (var att in msg.Attachments) {
                    mailMessage.Attachments.Add(att.FullName,
                        oLook.OlAttachmentType.olByValue,
                        1, System.IO.Path.GetFileName(att.FullName));
                }  
                mailMessage.Send();
                return true;
            } catch (System.Exception ex) {
                return false;
                throw;
            }
        }
    }
}
