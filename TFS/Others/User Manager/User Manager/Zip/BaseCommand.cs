#pragma warning disable 0067
using System.Collections.Generic;

namespace SNC.OptiRamp.Services.fSncZip
{
	public abstract class BaseCommand : IOptiRampInstallerCommand
	{
		protected const string ExpandMarker = "$";
		public virtual event CommandCompleteHandler CommandComplete;
		public virtual event CommandStartedHandler CommandStarted;
		public virtual event CommandStatusChangedEventHandler CommandStatusChanged;
		public virtual event InitializeProgressHandler InitializeProgress;
		public virtual event UpdateProgressHandler UpdateProgress;
		public CommandResults Result { get; set; }
		public object Value { get; set; }
		public abstract void Execute(Dictionary<string, object> parameters);
		protected string ExpandParameter(string value, Dictionary<string, object> parameters)
		{
			if(!value.Contains(ExpandMarker)) return value;
			var result = value;
			var hasValue = true;
			while(hasValue)
			{
				hasValue = false;
				foreach(var name in parameters.Keys)
				{
					var key = name;
					if(!key.Contains(ExpandMarker))
						key = string.Format("{1}{0}{1}", name, ExpandMarker);
					if(!result.Contains(key)) continue;
					hasValue = true;
					if(parameters.ContainsKey(name))
						result = result.Replace(key, parameters[name].ToString());
				}
			}
			return result;
		}
		protected string GetParameterName(string value)
		{
			return string.Format("{0}{1}{0}", ExpandMarker, value);
		}
	}
}
