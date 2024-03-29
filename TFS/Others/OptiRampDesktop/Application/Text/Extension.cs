namespace MyApplication.Text
{
	public static class Extension
	{
		#region Public Methods

		public static void AppendLineFormat(this System.Text.StringBuilder value, string format, object[] args)
		{
			value.AppendFormat(format, args);
			value.AppendLine();
		}

		public static void AppendLineFormat(this System.Text.StringBuilder value, string format, object arg1)
		{
			value.AppendLineFormat(format, new object[] { arg1 });
		}

		public static void AppendLineFormat(this System.Text.StringBuilder value, string format, object arg1, object arg2)
		{
			value.AppendLineFormat(format, new object[] { arg1, arg2 });
		}

		public static void AppendLineFormat(this System.Text.StringBuilder value, string format, object arg1, object arg2, object arg3)
		{
			value.AppendLineFormat(format, new object[] { arg1, arg2, arg3 });
		}

		public static void Return(this System.Text.StringBuilder value)
		{
			value.AppendLine();
		}

		#endregion
	}
}