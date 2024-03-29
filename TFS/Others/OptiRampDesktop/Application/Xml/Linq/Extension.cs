using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Application.Xml.Linq
{
	public static class Extension
	{
		#region Public Methods

		public static XDocument GetXDocument(this string fileName)
		{
			if (!File.Exists(fileName))
				return null;
			return XDocument.Load(fileName);
		}

		#endregion
	}
}