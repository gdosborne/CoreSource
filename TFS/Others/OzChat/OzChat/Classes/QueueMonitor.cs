namespace OzChat.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal class QueueMonitor
	{
		#region Public Constructors
		public QueueMonitor(string userName)
		{
			UserName = userName;
		}
		#endregion Public Constructors
		#region Public Methods
		public void Start()
		{
			try
			{
				using (var client = new ChatService.ChatServiceClient("ChatService", App.Uri))
				{
					bool hasNewUsers = false;
					string requestingConversation = null;
					var msg = client.GetNextMessage(UserName, out hasNewUsers, out requestingConversation);
					if(!string.IsNullOrEmpty(requestingConversation))
					{
						client.AcknowledgeRequest(requestingConversation, UserName, true);
						msg = new OzChat.ChatService.ConversationItem 
						{ 
							Text = "Conversation requested", 
							ToPerson = UserName, 
							FromPerson = requestingConversation, 
							TimeSent = DateTime.Now, 
							TimeRecieved = DateTime.Now 
						};
					}
					if (hasNewUsers && UpdateUsers != null)
						UpdateUsers(this, EventArgs.Empty);
					if (msg != null && ConversationItemReceived != null)
						ConversationItemReceived(this, new ConversationItemReceivedEventArgs(msg));
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		#endregion Public Methods

		#region Public Events
		public event ConversationItemReceivedHandler ConversationItemReceived;
		public event EventHandler UpdateUsers;
		#endregion Public Events

		#region Public Properties
		public string UserName { get; private set; }
		#endregion Public Properties
	}
}
