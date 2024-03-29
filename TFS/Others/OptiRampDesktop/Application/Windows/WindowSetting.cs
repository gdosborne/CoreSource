namespace MyApplication.Windows
{
	public class WindowSetting
	{
		#region Public Constructors

		public WindowSetting()
		{
		}

		public WindowSetting(double left, double top, double width, double height)
			: this()
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}

		public WindowSetting(double left, double top, double width, double height, System.Windows.WindowState windowState)
			: this()
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
			WindowState = windowState;
		}

		#endregion

		#region Public Properties
		public double Height { get; set; }
		public double Left { get; set; }
		public double Top { get; set; }
		public double Width { get; set; }
		public System.Windows.WindowState WindowState { get; set; }
		#endregion

		#region Public Methods

		public static WindowSetting FromWindow(System.Windows.Window win)
		{
			return new WindowSetting
			{
				Left = win.RestoreBounds.Left,
				Top = win.RestoreBounds.Top,
				Width = win.RestoreBounds.Width,
				Height = win.RestoreBounds.Height,
				WindowState = win.WindowState
			};
		}

		public static WindowSetting Parse(string value)
		{
			var val = value.Replace("{", string.Empty).Replace("}", string.Empty);
			var parts = val.Split(',');
			var result = new WindowSetting
			{
				Left = double.Parse(parts[0]),
				Top = double.Parse(parts[1]),
				Width = double.Parse(parts[2]),
				Height = double.Parse(parts[3])
			};
			if (parts.Length > 4)
				result.WindowState = (System.Windows.WindowState)System.Enum.Parse(typeof(System.Windows.WindowState), parts[4]);
			return result;
		}

		public static bool TryParse(string value, out WindowSetting test)
		{
			test = null;
			try
			{
				test = Parse(value);
				return true;
			}
			catch { return false; }
		}

		public void SetWindow(System.Windows.Window win)
		{
			try
			{
				win.Left = Left < 0 ? 0 : Left;
				win.Top = Top < 0 ? 0 : Top;
				win.Width = Width;
				win.Height = Height;
				win.WindowState = WindowState;
			}
			catch { }
		}

		public override string ToString()
		{
			return string.Format("{{{0},{1},{2},{3},{4}}}", new object[] { Left, Top, Width, Height, WindowState.ToString() });
		}

		#endregion
	}
}