namespace InnoUILib
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public sealed class DefineLine : InnoLine
	{
		public DefineLine(int number, string text)
			: base(number, Sections.Define, text)
		{

		}
	}
}
