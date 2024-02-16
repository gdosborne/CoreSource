namespace GregOsborne.Application.Net.SMS {
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Xml.Linq;
	using GregOsborne.Application.Primitives;
	using GregOsborne.Application.Text;
	using GregOsborne.Application.Xml.Linq;

	public class Messenger {
		private static List<(string carrier, string name, string emailAddress)> carrierInfo = default;
		private static string carrierFileName = "Carriers.xml";
		public static string[] GetCarrierNames() {
			LoadCarriers();
			return carrierInfo.Select(x => x.name).OrderBy(x => x).ToArray();
		}

		private static void LoadCarriers() {
			if (carrierInfo != null) {
				return;
			}

			carrierInfo = new List<(string carrier, string name, string emailAddress)>();
			var dir = Path.GetDirectoryName(typeof(Messenger).Assembly.Location);
			var fname = Path.Combine(dir, "Net", "SMS", carrierFileName);
			if (!File.Exists(fname)) {
				return;
			}
			try {
				var doc = XDocument.Load(fname);
				doc.Root.Elements().ToList().ForEach(element => {
					if (!element.LocalName().Equals("carrier")
						|| element.Attribute("id") == null || string.IsNullOrEmpty(element.Attribute("id").Value)
						|| element.Attribute("name") == null || string.IsNullOrEmpty(element.Attribute("name").Value)
						|| element.Attribute("email") == null || string.IsNullOrEmpty(element.Attribute("email").Value)) {
						return;
					}
					var id = element.Attribute("id").Value;
					var name = element.Attribute("name").Value;
					var email = element.Attribute("email").Value;
					var tempEmail = $"temp@{email}";
					if (!tempEmail.IsValidEmailAddress()) {
						return;
					}
					carrierInfo.Add((carrier: id, name, emailAddress: email));
				});
			}
			catch (System.Exception) {
				throw;
			}
		}

		public Messenger(string recipientCarrier, string recipientPhoneNumber, string smtpServer, string senderEmail, int breakMessageAt = int.MaxValue) {
			this.Carrier = recipientCarrier;
			this.PhoneNumber = recipientPhoneNumber;
			this.SMTPServer = smtpServer;
			this.SenderEmail = senderEmail;
		}

		public int BreakMessageAt { get; set; } = int.MaxValue;

		public string SMTPServer { get; set; } = default;

		public string Carrier { get; set; } = default;

		public string PhoneNumber { get; set; } = default;

		public string SenderEmail { get; set; } = default;

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject, string phoneNumber, string carrier) {
			this.Carrier = !string.IsNullOrEmpty(carrier) ? carrier : this.Carrier;
			this.PhoneNumber = !string.IsNullOrEmpty(phoneNumber) ? phoneNumber.RemoveNonNumbers(false) : this.PhoneNumber;
			this.SMTPServer = !string.IsNullOrEmpty(smtpServer) ? smtpServer : this.SMTPServer;
			this.SenderEmail = !string.IsNullOrEmpty(senderEmail) ? senderEmail : this.SenderEmail;

			if (string.IsNullOrEmpty(this.Carrier)) {
				return;
			}

			if (string.IsNullOrEmpty(this.PhoneNumber) || !this.PhoneNumber.Length.IsBetween(10, 11, true)) {
				return;
			}
			if (!carrierInfo.Any()) {
				LoadCarriers();
				if (!carrierInfo.Any()) {
					return;
				}
			}

			try {
				var info = carrierInfo.FirstOrDefault(x => x.carrier == carrier);
				if (info != default) {
					var email = $"{this.PhoneNumber}@{info.emailAddress}";
					var emailer = new EMailer(this.SMTPServer);
					if (this.BreakMessageAt < int.MaxValue) {
						var index = 1;
						var count = System.Math.Truncate(Convert.ToDouble(message.Length) / Convert.ToDouble(this.BreakMessageAt)) + (message.Length % this.BreakMessageAt == 0 ? 0 : 1);
						while (message.Length > this.BreakMessageAt + 1) {
							var msg = message.Substring(0, this.BreakMessageAt);
							var remainder = message.Length - this.BreakMessageAt;
							message = message.Substring(140, remainder);
							emailer.SendEmail(email, this.SenderEmail, msg, subject, Session.EmailTypes.Unspecified);
							index++;
						}
						emailer.SendEmail(email, this.SenderEmail, message, subject, Session.EmailTypes.Unspecified);

					} else {
						emailer.SendEmail(email, this.SenderEmail, message, subject, Session.EmailTypes.Unspecified);
					}
				}
			}
			catch (Exception) {
				throw;
			}
		}

		public void SendMessage(string message, string subject) => this.SendMessage(default, default, message, subject);

		public void SendMessage(string senderEmail, string message, string subject) => this.SendMessage(default, senderEmail, message, subject, this.PhoneNumber);

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject) => this.SendMessage(smtpServer, senderEmail, message, subject, this.PhoneNumber);

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject, string phoneNumber) => this.SendMessage(smtpServer, senderEmail, message, subject, phoneNumber, this.Carrier);

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject, string carrier, params string[] recipientsPhoneNumbers) => recipientsPhoneNumbers.ToList().ForEach(p => {
			this.SendMessage(smtpServer, senderEmail, message, subject, p, carrier);
		});

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject,params string[] recipientsPhoneNumbers) => recipientsPhoneNumbers.ToList().ForEach(p => {
			this.SendMessage(smtpServer, senderEmail, message, subject, p, this.Carrier);
		});

		public void SendMessage(string smtpServer, string senderEmail, string message, string subject, params (string carrier, string phone)[] recipients) => recipients.ToList().ForEach(t => {
			this.SendMessage(smtpServer, senderEmail, message, subject, t.phone, t.carrier);
		});

	}
}
