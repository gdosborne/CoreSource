using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSoftFormatComponents.Interfaces
{
	public interface IRule
	{
		void ProcessStatement(string statement);
		IDictionary<string, object> Settings { get; }
	}
}
