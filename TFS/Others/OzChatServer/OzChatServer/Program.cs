namespace OzChatServer
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.ServiceModel;
	using System.ServiceModel.Description;
	using System.Security.Principal;

	class Program
	{
		private static int ERROR_ELEVATION_REQUIRED = 740;
		private static Uri baseAddress = new Uri("http://localhost:5555/ChatService");
		static void Main(string[] args)
		{
			if (!HasAdministratorPrivileges())
			{
				Console.Error.WriteLine("Access Denied. Administrator permissions are " +
					"needed to use the selected options. Use an administrator command " +
					"prompt to complete these tasks.");
				Console.ReadKey();
				Environment.Exit(ERROR_ELEVATION_REQUIRED);
			}

			using (ServiceHost host = new ServiceHost(typeof(ChatService), baseAddress))
			{
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior() {
                    HttpGetEnabled = true
                };
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
				host.Description.Behaviors.Add(smb);
				host.Open();
				Console.WriteLine("The chat service is ready at {0}", baseAddress);
				Console.WriteLine("Press any key to stop the service...");
				Console.ReadKey();
				host.Close();
			}
		}
		private static bool HasAdministratorPrivileges()
		{
			WindowsIdentity id = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(id);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
