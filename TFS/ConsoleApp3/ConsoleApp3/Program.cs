using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;

namespace ConsoleApp3 {
	class Program {
		private static string url = "http://utiltfsap1.dot.util.lan:8080/tfs";
		static void Main(string[] args) {
			var tfsUri = new Uri(url);
			var teamProjectName = "Greg.Osborne";
			var myTfsTeamProjectCollection = new TfsTeamProjectCollection(tfsUri);
			var service = (ITestManagementService)myTfsTeamProjectCollection.GetService(typeof(ITestManagementService));
			var myTestManagementTeamProject = service.GetTeamProject(teamProjectName);			
		}
	}
}
