namespace OzChat.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal delegate void ConversationItemReceivedHandler(object sender, ConversationItemReceivedEventArgs e);
	internal class ConversationItemReceivedEventArgs : EventArgs
	{
		public ConversationItemReceivedEventArgs(ChatService.ConversationItem item)
		{
			Item = item;
		}
		public ChatService.ConversationItem Item { get; private set; }
	}
}
