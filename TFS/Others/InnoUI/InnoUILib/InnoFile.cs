namespace InnoUILib
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using AppIO = GregOsborne.Application.IO;
	using SysIO = System.IO;

	public class InnoFile : INotifyPropertyChanged, IDisposable
	{
		#region Public Constructors
		public InnoFile(string path, double controlWidth)
		{
			ControlWidth = controlWidth;
			FullPath = path;
			Name = System.IO.Path.GetFileName(path);
			Lines = new ObservableCollection<InnoLine>();
			ScriptLines = new ObservableCollection<InnoLine>();
			Open();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Close()
		{
			Dispose();
		}
		public void Dispose()
		{
			if (isDisposing)
				return;
			isDisposing = true;
			FullPath = null;
			Name = null;
			Text = null;
			ControlWidth = 0.0;
			Lines.Clear();
			isDisposing = false;
		}
		public bool Save()
		{
			return true;
		}
		#endregion Public Methods

		#region Private Methods
		private void Open()
		{
			using (var sr = new SysIO.StreamReader(AppIO.File.OpenFile(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)))
			{
				var lineNumber = 0;
				while (sr.Peek() > -1)
				{
					lineNumber++;
					var line = sr.ReadLine();

					if (line.StartsWith("#define"))
					{
						Lines.Add(new DefineLine(lineNumber, line));
						ScriptLines.Add(new DefineLine(lineNumber, line));
					}
					else
					{
						Lines.Add(new OtherLine(lineNumber, line));
						ScriptLines.Add(new OtherLine(lineNumber, line));
					}

				}
			}
			IsDirty = false;
		}
		#endregion Private Methods

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Private Fields
		private double _ControlWidth;
		private string _FullPath;
		private bool _IsDirty;
		private ObservableCollection<InnoLine> _Lines;
		private ObservableCollection<InnoLine> _ScriptLines;
		private string _Name;
		private string _Text;

		private bool isDisposing = false;
		#endregion Private Fields

		#region Public Properties
		public double ControlWidth
		{
			get
			{
				return _ControlWidth;
			}
			set
			{
				_ControlWidth = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string FullPath
		{
			get
			{
				return _FullPath;
			}
			set
			{
				_FullPath = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsDirty
		{
			get
			{
				return _IsDirty;
			}
			set
			{
				_IsDirty = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<InnoLine> Lines
		{
			get
			{
				return _Lines;
			}
			set
			{
				_Lines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<InnoLine> ScriptLines
		{
			get
			{
				return _ScriptLines;
			}
			set
			{
				_ScriptLines = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string Text
		{
			get
			{
				return _Text;
			}
			set
			{
				_Text = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties
	}
}
