using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSoftFormatComponents.Interfaces
{
	public interface IManager
	{
		IEnumerable<IRule> Rules { get; set; }
		string RulesFileName { get; }
		string CurrentlyProcessingFile { get; }
		bool IsProcessing { get; }
		void ProcessFile(string fileName);
	}
}
