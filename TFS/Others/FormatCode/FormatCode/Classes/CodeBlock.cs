using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FormatCode.Classes
{
	public class CodeBlock
	{
		public int Level { get; set; }
		public int Sequence { get; set; }
		public string Code { get; set; }
		public CodeBlock Parent { get; set; }
	}
}
