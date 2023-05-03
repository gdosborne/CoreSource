using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DomainationClient.Service
{
	internal static class References
	{
		public static DomainationService.DomainationServiceClient DomainationClient()
		{
			return new DomainationService.DomainationServiceClient();
		}
		public static DomainationService.DomainationServiceClient DomainationClient(string clientUrl)
		{
			return new DomainationService.DomainationServiceClient("BasicHttpBinding_IDomainationService", clientUrl);
		}
	}
}
