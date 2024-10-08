namespace OzChat.Classes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Media;

	public class RowColor
	{
		#region Public Constructors
		public RowColor(SolidColorBrush background, string name)
		{
			Background = background;
			Name = name;
		}
		#endregion Public Constructors

		#region Public Methods
		public static List<RowColor> GetColors()
		{
			var result = new List<RowColor>();
			var props = (typeof(Colors)).GetProperties(BindingFlags.Public | BindingFlags.Static);
			foreach (var prop in props)
			{
				var name = prop.Name;
				var value = prop.GetValue(null, null);
				result.Add(new RowColor(new SolidColorBrush((Color)value), name));
			}
			return result;
		}
		#endregion Public Methods

		#region Public Properties
		public SolidColorBrush Background { get; set; }

		public String Name { get; set; }
		#endregion Public Properties
	}
}
