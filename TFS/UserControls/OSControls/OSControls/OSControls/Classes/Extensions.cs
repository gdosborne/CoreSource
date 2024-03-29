namespace OSControls.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class Extensions
	{
		#region Public Methods
		public static string ExpandString(this string value, IList<string> names, IList<string> values, char trigger)
		{
			if (value == null || names == null || values == null)
				return value;
			var result = string.Empty;
			bool isInVariable = false;
			var variableName = string.Empty;
			foreach (var character in value.ToCharArray())
			{
				if (character.Equals(trigger))
					if (isInVariable)
					{
						var nameIndex = names.IndexOf(variableName);
						if (nameIndex < 0)
							result += (trigger + variableName + trigger);
						else
							result += values[nameIndex].ExpandString(names, values, trigger);
						isInVariable = false;
						variableName = string.Empty;
					}
					else
						isInVariable = true;
				else if (isInVariable)
					variableName += character;
				else
					result += character;
			}
			return result;
		}
		#endregion Public Methods
	}
}
