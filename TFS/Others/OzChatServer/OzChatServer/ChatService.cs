namespace OzChatServer
{
	using System;
	using System.Collections.Generic;

	using System.Linq;

	public class ChatService : IChatService
	{
		#region Public Methods
		public Dictionary<string, string> GetAllUsers()
		{
			return ConversationItem.Users;
		}
		public ConversationItem GetNextMessage(string userName, out bool hasNewUsers, out string requestConversation)
		{
			hasNewUsers = ConversationItem.HasNewUsers[userName].IsReported;
			requestConversation = null;
			if (ConversationItem.Requests.Any(x => x.OtherUser == userName))
				requestConversation = ConversationItem.Requests.First(x => x.OtherUser == userName).RequestingUser;
			ConversationItem result = null;
			try
			{
				if (ConversationItem.Conversations.ContainsKey(userName))
				{
					var userConversations = ConversationItem.Conversations[userName];
					if (userConversations.Count > 0)
					{
						result = userConversations.Pop();
						Console.WriteLine(string.Format("{0} items for {1}", userConversations.Count, userName));
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return result;
		}
		public void AcknowledgeRequest(string requestingUser, string acknowledgingUser, bool isAccepted)
		{
			try
			{
				if (ConversationItem.Requests.Any(x => x.RequestingUser == requestingUser && x.OtherUser == acknowledgingUser) && isAccepted)
				{
					var request = ConversationItem.Requests.First(x => x.RequestingUser == requestingUser && x.OtherUser == acknowledgingUser);
					ConversationItem.Requests.Remove(request);
					ConversationItem.Conversing.Add(request);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return;
		}
		public void PostConversationItem(ConversationItem item)
		{
			try
			{
				var sender = item.ToPerson;
				if (!ConversationItem.Conversing.Any(x => (x.RequestingUser == item.ToPerson && x.OtherUser == item.FromPerson) || (x.OtherUser == item.ToPerson && x.RequestingUser == item.FromPerson)))
					ConversationItem.Requests.Add(new ConversationItem.ConversationRequest { RequestingUser = item.FromPerson, OtherUser = item.ToPerson });
				if (!ConversationItem.Conversations.ContainsKey(sender))
					ConversationItem.Conversations.Add(sender, new Stack<ConversationItem>());
				var senderConversations = ConversationItem.Conversations[sender];
				senderConversations.Push(item);
				Console.WriteLine(string.Format("{0} items for {1}", senderConversations.Count, item.ToPerson));
			}
			catch (Exception ex)
			{
				throw;
			}
			return;
		}
		public void RegisterUser(string name, string userName)
		{
			Console.WriteLine("Registering {0}", userName);
			try
			{
				if (!ConversationItem.Users.ContainsKey(userName))
				{
					ConversationItem.Users.Add(userName, name);
					ConversationItem.HasNewUsers.Add(userName, new ConversationItem.NewUserReport { IsReported = false, WhenRefreshed = null });
					foreach (var key in ConversationItem.HasNewUsers.Keys)
					{
						ConversationItem.HasNewUsers[key].IsReported = false;
						ConversationItem.HasNewUsers[key].WhenRefreshed = null;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public void UnRegister(string userName)
		{
			Console.WriteLine("Unregistering {0}", userName);
			try
			{
				if (!ConversationItem.Users.ContainsKey(userName))
				{
					ConversationItem.Users.Remove(userName);
					var conversations = ConversationItem.Conversing.Where(x => x.OtherUser == userName || x.RequestingUser == userName);
					var requests = ConversationItem.Requests.Where(x => x.OtherUser == userName || x.RequestingUser == userName);
					if(conversations.Any())
					{
						foreach (var item in conversations)
						{
							ConversationItem.Conversing.Remove(item);
						}
					}
					if (requests.Any())
					{
						foreach (var item in conversations)
						{
							ConversationItem.Requests.Remove(item);
						}
					}
					ConversationItem.HasNewUsers.Remove(userName);
					foreach (var key in ConversationItem.HasNewUsers.Keys)
					{
						ConversationItem.HasNewUsers[key].IsReported = false;
						ConversationItem.HasNewUsers[key].WhenRefreshed = null;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		#endregion Public Methods
	}
}
