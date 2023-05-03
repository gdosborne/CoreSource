using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OzChat
{
	public partial class App : Application
	{
		public static string UserName { get; set; }
		public static string Uri = "http://localhost:5555/ChatService";
		protected override void OnExit(ExitEventArgs e)
		{
			using (var client = new ChatService.ChatServiceClient("ChatService", App.Uri))
			{
				client.UnRegister(UserName);
			}
		}
	}
}
