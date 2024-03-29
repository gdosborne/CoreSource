namespace OSInstallerExtensibility.Classes
{
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public static class Extensions
	{
		#region Public Methods
		public static string ExpandString(this string value, IList<IInstallerData> datum, char trigger)
		{
			if (value == null)
				return string.Empty;
			var result = string.Empty;
			bool isInVariable = false;
			var variableName = string.Empty;
			foreach (var character in value.ToCharArray())
			{
				if (character.Equals(trigger))
					if (isInVariable)
					{
						var data = datum.FirstOrDefault(x => x.Name.Equals(variableName));
						if (data == null)
							result += (trigger + variableName + trigger);
						else
							result += data.Value.ExpandString(datum, trigger);
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

	public static class Helpers
	{
		#region Public Methods
		public static ImageSource GetImageSourceFromResource(string assemblyName, string resourceName)
		{
			Uri oUri = new Uri("pack://application:,,,/" + assemblyName + ";component/" + resourceName, UriKind.RelativeOrAbsolute);
			return BitmapFrame.Create(oUri);
		}
		#endregion Public Methods
	}
}
