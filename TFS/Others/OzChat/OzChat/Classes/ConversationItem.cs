namespace OzChat.Classes
{
	using GregOsborne.Application.Media;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;
	using System.Xml.Linq;

	public class ConversationItem
	{
		#region Public Constructors
		public ConversationItem()
		{
			TimeSent = DateTime.Now;
		}
		#endregion Public Constructors

		#region Public Methods
		public static ConversationItem FromXml(string xml)
		{
			var element = XElement.Parse(xml);
			var result = new ConversationItem
			{
				Text = element.Value,
				TextBrush = new SolidColorBrush(element.Attribute("brush").Value.ToColor()),
				FromPerson = new Person { Name = element.Attribute("fromname").Value, UserName = element.Attribute("fromqueue").Value },
				ToPerson = new Person { Name = element.Attribute("toname").Value, UserName = element.Attribute("toqueue").Value },
				TimeSent = DateTime.Parse(element.Attribute("sent").Value),
				TimeRecieved = DateTime.Now
			};
			result.FromText = "(" + result.FromPerson.Name + ")";
			return result;
		}
		public string ToXml()
		{
			return new XElement("conversationitem", Text,
				new XAttribute("toname", ToPerson.Name),
				new XAttribute("toqueue", ToPerson.UserName),
				new XAttribute("fromname", FromPerson.Name),
				new XAttribute("fromqueue", FromPerson.UserName),
				new XAttribute("sent", TimeSent.ToString()),
				new XAttribute("brush", TextBrush.As<SolidColorBrush>().Color.ToHexValue())
				).ToString();
		}
		#endregion Public Methods

		#region Public Properties
		public string FromText { get; set; }
		public Person FromPerson { get; set; }
		public string Text { get; set; }
		public Brush TextBrush { get; set; }
		public DateTime TimeRecieved { get; set; }
		public DateTime TimeSent { get; set; }
		public Person ToPerson { get; set; }
		#endregion Public Properties
	}
}
