using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApplication.Primitives
{
	public static class Math
	{
		#region Public Methods

		public static T MaxOf<T>(T[] values)
		{
			return values.ToList().Max();
		}

		public static T MaxOf<T>(T value1, T value2)
		{
			return MaxOf<T>(new T[] { value1, value2 });
		}

		#endregion
	}
}