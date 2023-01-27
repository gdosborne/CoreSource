﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using static Common.Applicationn.Session;

namespace Common.Applicationn {
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
            CacheTime = cacheTime;
            TrackedData = new List<TrackedItem>();
            Flags = flags;
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
                var amount = TrackedData.Count(y => y.MailType.ToString().Equals(x));
                if (amount > 0) {
                    result.Add($"{x.PadRight(longest)}:= {amount,-10:N0}");
                }
            });
            return result.ToArray();
        }

        public MatchingFlags Flags { get; set; }

        public TimeSpan CacheTime { get; set; }

        public List<TrackedItem> TrackedData { get; private set; }

        public void CleanTrackedMailItems() => TrackedData.RemoveAll(x => x.Sent < DateTime.Now.Subtract(CacheTime));

        public void RemoveById(Guid id) => TrackedData.Remove(TrackedData.FirstOrDefault(x => x.Id == id));

        public bool IsMailOkToSend(params KeyValuePair<MatchingFlags, object>[] data) => IsMailOkToSend(Flags, data);

        public bool IsMailOkToSend(MatchingFlags testFor, params KeyValuePair<MatchingFlags, object>[] data) {
            CleanTrackedMailItems();

            var temp = testFor;
            var doNotSendEmail = false;
            var value = default(object);
            var current = MatchingFlags.Subject;
            while (temp != MatchingFlags.None) {
                if (temp.HasFlag(current)) {
                    if (data.Any(x => x.Key == current)) {
                        value = data.First(x => x.Key == current).Value;
                        if (current == MatchingFlags.Subject) {
                            doNotSendEmail |= TrackedData.Any(x => x.Subject.Equals(value));
                        }
                        else if (current == MatchingFlags.Body) {
                            doNotSendEmail |= TrackedData.Any(x => x.Body.Equals(value));
                        }
                        else if (current == MatchingFlags.ToEmailAddresses) {
                            doNotSendEmail |= TrackedData.Any(x => x.ToAddresses.Contains(value));
                        }
                        else if (current == MatchingFlags.FromEmailAddress) {
                            doNotSendEmail |= TrackedData.Any(x => x.FromAddress.Equals(value));
                        }
                        else if (current == MatchingFlags.Priority) {
                            doNotSendEmail |= TrackedData.Any(x => x.Priority.Equals(value));
                        }
                        else if (current == MatchingFlags.Attachments) {
                            doNotSendEmail |= TrackedData.Any(x => x.Attachments.Contains(value));
                        }
                        else if (current == MatchingFlags.ExceptionNumber) {
                            doNotSendEmail |= TrackedData.Any(x => x.ExceptionNumber.Equals(value));
                        }
                        else if (current == MatchingFlags.MailType) {
                            doNotSendEmail |= TrackedData.Any(x => x.MailType.Equals(value));
                        }
                        else if (current == MatchingFlags.SendToBCC) {
                            doNotSendEmail |= TrackedData.Any(x => x.BCC.Equals(value));
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
            if (!disposedValue) {
                if (disposing) {
                    TrackedData.Clear();
                }
                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
    }
}
