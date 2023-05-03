namespace GregOsborne.Application {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Mail;
	using static GregOsborne.Application.Session;

	public class MailTracker : IDisposable {

		[Flags]
		public enum MatchingFlags {
			None = 0,
			Subject = 1,
			Body = 2,
			ToEmailAddresses = 4,
			FromEmailAddress = 8,
			Priority = 16,
			Attachments = 32,
			ExceptionNumber = 64,
			MailType = 128,
			SendToBCC = 256
		}

		public MailTracker(TimeSpan cacheTime, MatchingFlags flags) {
			this.CacheTime = cacheTime;
			this.TrackedData = new List<TrackedItem>();
			this.Flags = flags;
		}

		public struct TrackedItem {
			public Guid Id;
			public DateTime Sent;
			public string Subject;
			public string Body;
			public MailPriority Priority;
			public string[] ToAddresses;
			public string FromAddress;
			public string[] Attachments;
			public EmailTypes MailType;
			public int? ExceptionNumber;
			public string BCC;
		}

		public string[] Report() {
			var result = new List<string> {
				"Email types sent"
			};
			var typeNames = Enum.GetNames(typeof(EmailTypes));
			var longest = typeNames.Max(x => x.Length) + 1;
			typeNames.ToList().ForEach(x => {
				var amount = this.TrackedData.Count(y => y.MailType.ToString().Equals(x));
				if (amount > 0) {
					result.Add($"{x.PadRight(longest)}:= {amount,-10:N0}");
				}
			});
			return result.ToArray();
		}

		public MatchingFlags Flags { get; set; }

		public TimeSpan CacheTime { get; set; }

		public List<TrackedItem> TrackedData { get; private set; }

		public void CleanTrackedMailItems() => this.TrackedData.RemoveAll(x => x.Sent < DateTime.Now.Subtract(this.CacheTime));

		public void RemoveById(Guid id) => this.TrackedData.Remove(this.TrackedData.FirstOrDefault(x => x.Id == id));

		public bool IsMailOkToSend(params KeyValuePair<MatchingFlags, object>[] data) => this.IsMailOkToSend(this.Flags, data);

		public bool IsMailOkToSend(MatchingFlags testFor, params KeyValuePair<MatchingFlags, object>[] data) {
			this.CleanTrackedMailItems();

			var temp = testFor;
			var doNotSendEmail = false;
			var value = default(object);
			var current = MatchingFlags.Subject;
			while (temp != MatchingFlags.None) {
				if (temp.HasFlag(current)) {
					if (data.Any(x => x.Key == current)) {
						value = data.First(x => x.Key == current).Value;
						if (current == MatchingFlags.Subject) {
							doNotSendEmail |= this.TrackedData.Any(x => x.Subject.Equals(value));
						} else if (current == MatchingFlags.Body) {
							doNotSendEmail |= this.TrackedData.Any(x => x.Body.Equals(value));
						} else if (current == MatchingFlags.ToEmailAddresses) {
							doNotSendEmail |= this.TrackedData.Any(x => x.ToAddresses.Contains(value));
						} else if (current == MatchingFlags.FromEmailAddress) {
							doNotSendEmail |= this.TrackedData.Any(x => x.FromAddress.Equals(value));
						} else if (current == MatchingFlags.Priority) {
							doNotSendEmail |= this.TrackedData.Any(x => x.Priority.Equals(value));
						} else if (current == MatchingFlags.Attachments) {
							doNotSendEmail |= this.TrackedData.Any(x => x.Attachments.Contains(value));
						} else if (current == MatchingFlags.ExceptionNumber) {
							doNotSendEmail |= this.TrackedData.Any(x => x.ExceptionNumber.Equals(value));
						} else if (current == MatchingFlags.MailType) {
							doNotSendEmail |= this.TrackedData.Any(x => x.MailType.Equals(value));
						} else if (current == MatchingFlags.SendToBCC) {
							doNotSendEmail |= this.TrackedData.Any(x => x.BCC.Equals(value));
						}
					}
					temp = temp & ~current;
				}
				current = (MatchingFlags)(((int)current) * 2);
			}
			return !doNotSendEmail;
		}

		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing) {
			if (!this.disposedValue) {
				if (disposing) {
					this.TrackedData.Clear();
				}
				this.disposedValue = true;
			}
		}

		public void Dispose() => this.Dispose(true);
	}
}
