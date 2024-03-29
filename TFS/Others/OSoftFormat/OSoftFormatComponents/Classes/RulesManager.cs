using OSoftFormatComponents.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;

namespace OSoftFormatComponents.Classes
{
	public sealed class RulesManager : IManager
	{
		public RulesManager(string rulesFileName)
		{
			RulesFileName = rulesFileName;
			Rules = new List<IRule>();
		}

		public IEnumerable<IRule> Rules { get; set; }
		public string RulesFileName { get; private set; }
		public string CurrentlyProcessingFile { get; private set; }
		public bool IsProcessing { get; private set; }

		public void ProcessFile(string fileName)
		{
			if (!File.Exists(fileName))
				throw new FileNotFoundException("Cannot file source code file", fileName);

			CurrentlyProcessingFile = fileName;
			IsProcessing = true;

			MSBuildWorkspace workspace = MSBuildWorkspace.Create();
			
			//do stuff here

			IsProcessing = false;
			CurrentlyProcessingFile = null;
		}
	}
}
