using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using static Common.Applicationn.MailTracker;
using static Common.Applicationn.Session;

namespace Common.Applicationn {
    public class EMailer : IDisposable {
        public EMailer(string smtpServer, TimeSpan cacheExpire, MatchingFlags flags) {
            addresses = new Dictionary<string, MailAddress>();
            SMTPServer = smtpServer;
            Tracker = new MailTracker(cacheExpire, flags);
        }

        public static string DefaultSMTPServer { get; set; } = "smtp.office365.com";

        public EMailer(string smtpServer, TimeSpan cacheExpire) : this(smtpServer, cacheExpire, MailTracker.MatchingFlags.Subject | MailTracker.MatchingFlags.Body | MailTracker.MatchingFlags.ToEmailAddresses) { }

        public EMailer(string smtpServer) : this(smtpServer, TimeSpan.FromMinutes(5)) { }

        public MailTracker Tracker { get; set; }

        public void Dispose() => Dispose(true);

        public EMailer AddAddress(string name, string address, string displayName = null) {
            addresses.Add(name, new MailAddress(address, displayName));
            return this;
        }

        public string SMTPServer { get; set; }

        public MailAddress[] GetMailAddresses(params string[] keys) {
            var addresses = this.addresses.Where(x => keys.Contains(x.Key)).Select(x => x.Value).Select(x => x.Address).ToList();
            var result = new List<MailAddress>();
            var temp = new List<string>();
            addresses.ForEach(x => temp.AddRange(x.Split(',')));
            temp.ForEach(x => result.Add(new MailAddress(x)));
            return result.ToArray();
        }

        public string[] GetMailAddressesRaw(params string[] keys) => addresses.Where(x => keys.Contains(x.Key)).Select(x => x.Value.Address).ToArray();

        public MailAddress GetMailAddress(string key) => addresses.ContainsKey(key) ? addresses[key] : null;

        public string GetMailAddressRaw(string key) => GetMailAddress(key).Address;

        private SmtpClient smtpClient = default;
        private readonly Dictionary<string, MailAddress> addresses = default;
        private bool isDisposed = false;

        private MailAddress GetAddress(string emailAddress) {
            if (!addresses.Any(x => x.Value.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))) {
                return new MailAddress(emailAddress);
            }

            return addresses.First(x => x.Value.Address.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)).Value;
        }

        private MailAddress[] GetAddresses(string emailAddress) {
            emailAddress = emailAddress.Replace(";", ",");
            return GetAddresses(emailAddress.Split(','));
        }

        private MailAddress[] GetAddresses(string[] emailAddress) => emailAddress.Select(x => GetAddress(x)).ToArray();

        //public bool SafeSendEmail(MatchingFlags flags, params KeyValuePair<MatchingFlags, object>[] data) {
        //    var result = false;
        //    if (Tracker.IsMailOkToSend(flags, data)) {
        //        var hasMailTo = data.Any(x => x.Key == MatchingFlags.ToEmailAddresses);
        //        var hasMailFrom = data.Any(x => x.Key == MatchingFlags.FromEmailAddress);
        //        var hasMessage = data.Any(x => x.Key == MatchingFlags.Body);
        //        var hasSubject = data.Any(x => x.Key == MatchingFlags.Subject);
        //        var hasMailType = data.Any(x => x.Key == MatchingFlags.MailType);
        //        var hasBCC = data.Any(x => x.Key == MatchingFlags.SendToBCC);
        //        var hasExceptionNumber = data.Any(x => x.Key == MatchingFlags.ExceptionNumber);
        //        var hasPriority = data.Any(x => x.Key == MatchingFlags.Priority);
        //        var hasAttachments = data.Any(x => x.Key == MatchingFlags.Attachments);

        //        if (!hasMailTo || !hasMailFrom || !hasSubject || !hasMessage || !hasMailType) {
        //            return result;
        //        }

        //        var mailTo = default(string);
        //        var mailFrom = default(string);
        //        var mailBody = default(string);
        //        var mailSubject = default(string);
        //        var mailType = default(EmailTypes);
        //        var mailBcc = default(string);
        //        var mailPriority = default(MailPriority);
        //        var mailAttachments = default(string[]);
        //        var item = new TrackedItem {
        //            Sent = DateTime.Now
        //        };
        //        if (hasExceptionNumber) {
        //            item.ExceptionNumber = (int)data.First(x => x.Key == MatchingFlags.ExceptionNumber).Value;
        //        }
        //        if (hasMailTo) {
        //            mailTo = (string)data.First(x => x.Key == MatchingFlags.ToEmailAddresses).Value;
        //            var addresses = mailTo.Contains(";")
        //                ? mailTo.Split(';')
        //                : mailTo.Contains(",")
        //                    ? mailTo.Split(',')
        //                    : new string[] { mailTo };
        //            item.ToAddresses = addresses;
        //        }
        //        if (hasMailFrom) {
        //            mailFrom = (string)data.First(x => x.Key == MatchingFlags.FromEmailAddress).Value;
        //            item.FromAddress = mailFrom;
        //        }
        //        if (hasMessage) {
        //            mailBody = (string)data.First(x => x.Key == MatchingFlags.Body).Value;
        //            item.Body = mailBody;
        //        }
        //        if (hasSubject) {
        //            mailSubject = (string)data.First(x => x.Key == MatchingFlags.Subject).Value;
        //            item.Subject = mailSubject;
        //        }
        //        if (hasMailType) {
        //            mailType = (EmailTypes)data.First(x => x.Key == MatchingFlags.MailType).Value;
        //            item.MailType = mailType;
        //        }
        //        if (hasBCC) {
        //            mailBcc = (string)data.First(x => x.Key == MatchingFlags.SendToBCC).Value;
        //            item.BCC = mailBcc;
        //        }
        //        if (hasPriority) {
        //            mailPriority = (MailPriority)data.First(x => x.Key == MatchingFlags.Priority).Value;
        //            item.Priority = mailPriority;
        //        }
        //        if (hasAttachments) {
        //            mailAttachments = (string[])data.First(x => x.Key == MatchingFlags.Attachments).Value;
        //            item.Attachments = mailAttachments;
        //        }

        //        SendEmail(mailTo, mailFrom, mailBody, mailSubject, hasBCC, mailBcc, mailPriority, mailAttachments, mailType);
        //        result = true;
        //        Tracker.TrackedData.Add(item);
        //    }
        //    return result;
        //}

        //public EMailer SendEmail(string emailto, string fromEmail, string message, string subject, EmailTypes mailType = EmailTypes.Unspecified) {
        //    var to = GetAddresses(emailto);
        //    var fr = GetAddress(fromEmail);
        //    SendEmail(to, fr, message, subject, false, default, MailPriority.Normal, null, mailType);
        //    return this;
        //}

        //public EMailer SendEmail(string emailto, string fromEmail, string message, string subject, bool sendBccEmail, string bccEmail, EmailTypes mailType = EmailTypes.Unspecified) {
        //    SendEmail(emailto, fromEmail, message, subject, sendBccEmail, bccEmail, MailPriority.Normal, null, mailType);
        //    return this;
        //}

        //public EMailer SendEmail(string emailto, string fromEmail, string message, string subject, bool sendBccEmail, string bccEmail, MailPriority priority, string[] attachmentPaths, EmailTypes mailType = EmailTypes.Unspecified) {
        //    var to = GetAddresses(emailto);
        //    var fr = GetAddress(fromEmail);
        //    var bc = sendBccEmail && !string.IsNullOrEmpty(bccEmail) ? new MailAddress(bccEmail) : default;
        //    SendEmail(to, fr, message, subject, sendBccEmail, bc, priority, attachmentPaths, mailType);
        //    return this;
        //}

        //public EMailer SendEmail(MailAddress[] emailto, MailAddress fromEmail, string message, string subject, bool sendBccEmail, MailAddress bccEmail, MailPriority priority, string[] attachmentPaths, EmailTypes mailType = EmailTypes.Unspecified) {
        //    try {
        //        using (var emailMessage = new MailMessage {
        //            From = fromEmail,
        //            Subject = subject,
        //            IsBodyHtml = true,
        //            Body = message,
        //            Priority = priority
        //        }) {
        //            foreach (var email in emailto.Distinct()) {
        //                emailMessage.To.Add(email);
        //            }

        //            if (attachmentPaths != null && attachmentPaths.Length > 0) {
        //                attachmentPaths.ToList().ForEach(path => {
        //                    if (File.Exists(path)) {
        //                        emailMessage.Attachments.Add(new Attachment(path));
        //                    }
        //                });
        //            }

        //            if (sendBccEmail && bccEmail != null) {
        //                emailMessage.Bcc.Add(bccEmail);
        //            }

        //            var isAvailable = Common.Applicationn.Net.Utilities.IsServerPortAvailable(SMTPServer, 25);
        //            if (isAvailable) {
        //                using (var client = new SmtpClient(SMTPServer, 25)) {
        //                    client.Send(emailMessage);
        //                }
        //            }
        //            else {
        //                throw new ApplicationException($"Port 25 not available on {SMTPServer}");
        //            }
        //            //this.Tracker.TrackedData.Add(new TrackedItem {
        //            //	MailType = mailType,
        //            //	FromAddress = fromEmail.Address,
        //            //	ToAddresses = emailto.Select(x => x.Address).ToArray(),
        //            //	Subject = subject,
        //            //	Priority = priority,
        //            //	Sent = DateTime.Now
        //            //});
        //        }
        //    }
        //    catch {
        //        throw;
        //    }
        //    return this;
        //}

        protected virtual void Dispose(bool isDisposing) {
            if (!isDisposed) {
                if (isDisposing) {
                    if (smtpClient != null) {
                        smtpClient.Dispose();
                        smtpClient = null;
                    }
                }
                isDisposed = true;
            }
        }
    }
}
