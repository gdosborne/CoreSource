namespace OzChatServer
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;

	public class ConversationItem
	{
		public class NewUserReport
		{
			public bool IsReported { get; set; }
			public DateTime? WhenRefreshed { get; set; }
		}
		public class ConversationRequest
		{
			public string RequestingUser { get; set; }
			public string OtherUser { get; set; }
		}
		#region Public Constructors
		public ConversationItem()
		{
			TimeSent = DateTime.Now;
		}
		#endregion Public Constructors
		private static Dictionary<string, string> instanceUsers = null;
		private static Dictionary<string, Stack<ConversationItem>> instanceConversations = null;
		private static Dictionary<string, NewUserReport> instanceHasNewUsers = null;
		private static List<ConversationRequest> instanceRequests = null;
		private static List<ConversationRequest> instanceConversing = null;
		public static Dictionary<string, Stack<ConversationItem>> Conversations
		{
			get
			{
				if (instanceConversations == null)
					instanceConversations = new Dictionary<string, Stack<ConversationItem>>();
				return instanceConversations;
			}
		}
		public static Dictionary<string, string> Users
		{
			get
			{
				if (instanceUsers == null)
					instanceUsers = new Dictionary<string, string>();
				return instanceUsers;
			}
		}
		public static Dictionary<string, NewUserReport> HasNewUsers
		{
			get
			{
				if (instanceHasNewUsers == null)
					instanceHasNewUsers = new Dictionary<string, NewUserReport>();
				return instanceHasNewUsers;
			}
		}
		public static List<ConversationRequest> Requests
		{
			get
			{
				if (instanceRequests == null)
					instanceRequests = new List<ConversationRequest>();
				return instanceRequests;
			}
		}
		public static List<ConversationRequest> Conversing
		{
			get
			{
				if (instanceConversing == null)
					instanceConversing = new List<ConversationRequest>();
				return instanceConversing;
			}
		}

		#region Public Methods
		public static ConversationItem FromXml(string xml)
		{
			var element = XElement.Parse(xml);
			var result = new ConversationItem
			{
				Text = element.Value,
				TextBrush = element.Attribute("brush").Value,
				FromPerson = element.Attribute("fromname").Value,
				ToPerson = element.Attribute("toname").Value,
				TimeSent = DateTime.Parse(element.Attribute("sent").Value),
				TimeRecieved = DateTime.Now
			};
			return result;
		}
		public string ToXml()
		{
			return new XElement("conversationitem", Text,
				new XAttribute("toname", ToPerson),
				new XAttribute("fromname", FromPerson),
				new XAttribute("sent", TimeSent.ToString()),
				new XAttribute("brush", TextBrush)
				).ToString();
		}
		#endregion Public Methods

		#region Public Properties
		public string FromPerson { get; set; }
		public string Text { get; set; }
		public string TextBrush { get; set; }
		public DateTime TimeRecieved { get; set; }
		public DateTime TimeSent { get; set; }
		public string ToPerson { get; set; }
		#endregion Public Properties
	}
}
