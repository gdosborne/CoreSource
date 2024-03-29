using OSoftFormatComponents.Classes.Rules;
using OSoftFormatComponents.Interfaces;
using System;
using System.Collections.Generic;

namespace OSoftFormatComponents.Classes
{
	public static class Configuration
	{
		public static Dictionary<string, Type> RuleTypeReference = new Dictionary<string, Type>
		{
			{ "usings", typeof(UsingRule) }
		};

		public static List<string> Log = new List<string>();

		public static IRule CreateInstance(string typeId)
		{
			IRule result = null;
			typeId = typeId.ToLower();
			if (!RuleTypeReference.ContainsKey(typeId))
				return result;
			try
			{
				result = (IRule)Activator.CreateInstance(RuleTypeReference[typeId]);
			}
			catch (Exception ex)
			{
				Log.Add(string.Format("{0} (1}", ex.Message, typeId));
			}
			return result;
		}
	}
}
