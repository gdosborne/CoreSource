namespace OzChatServer
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.ServiceModel;
	using System.Text;
	using System.Threading.Tasks;

	[ServiceContract]
	public interface IChatService
	{
		[OperationContract]
		void PostConversationItem(ConversationItem item);

		[OperationContract]
		ConversationItem GetNextMessage(string userName, out bool hasNewUsers, out string requestConversation);

		[OperationContract]
		Dictionary<string, string> GetAllUsers();

		[OperationContract]
		void RegisterUser(string name, string userName);

		[OperationContract]
		void UnRegister(string userName);

		[OperationContract]
		void AcknowledgeRequest(string requestingUser, string acknowledgingUser, bool isAccepted);
	}
}
