using OSoftFormatComponents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSoftFormatComponents.Classes.Rules
{
	public abstract class RuleBase : IRule
	{
		public RuleBase()
		{
			Settings = new Dictionary<string, object>();
		}
		public abstract void ProcessStatement(string statement);
		public IDictionary<string, object> Settings { get; private set; }
	}
}
