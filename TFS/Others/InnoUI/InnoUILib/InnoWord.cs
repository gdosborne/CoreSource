namespace InnoUILib
{
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public sealed class InnoWord : INotifyPropertyChanged
	{
		#region Public Constructors
		static InnoWord()
		{
			InnoValues = new List<string>
			{
				"commonprograms",
				"app",
				"cm:CreateDesktopIcon",
				"cm:AdditionalIcons",
				"cm:LaunchProgram"
			};
			InnoKeywords = new List<string>
			{
				"AppId", 
				"AppName", 
				"AppVersion", 
				"AppPublisher",
				"AppPublisherURL", 
				"AppSupportURL", 
				"AppUpdatesURL",
				"DefaultDirName", 
				"DefaultGroupName", 
				"DisableDirPage",
				"DisableProgramGroupPage", 
				"DisableWelcomePage",
				"WizardImageFile", 
				"OutputDir", 
				"OutputBaseFilename",
				"Compression", 
				"SolidCompression", 
				"SetupIconFile",
				"CloseApplications", 
				"RestartApplications",
				"CloseApplicationsFilter", 
				"Name:", 
				"Source:",
				"Root:", 
				"Filename:",
				"Description:", 
				"MessagesFile:", 
				"GroupDescription:", 
				"Flags:", 
				"Tasks:", 
				"DestDir:", 
				"procedure",
				"function",
				"var",
				"begin",
				"end",
				"if",
				"else",
				"then"
			};
		}
		public InnoWord(string text, Brush defaultBrush, FontWeight weight, char splitChar, bool isLastWord = false)
		{
			Brush = defaultBrush;
			FontWeight = weight;
			if (InnoKeywords.Contains(text))
			{
				IsKeyWord = true;
				Brush = KeywordBrush;
			}
			DisplayMember = new TextBlock
			{
				Text = text + (isLastWord ? string.Empty : splitChar.ToString()),
				Foreground = Brush,
				FontWeight = FontWeight,
				Margin = new System.Windows.Thickness(0)
			};
		}
		#endregion Public Constructors

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion Public Events

		#region Public Fields
		public static List<string> InnoKeywords = null;
		public static List<string> InnoValues = null;
		#endregion Public Fields

		#region Private Fields
		private FontWeight _FontWeight;
		public FontWeight FontWeight
		{
			get { return _FontWeight; }
			set
			{
				_FontWeight = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private Brush _Brush;
		private TextBlock _DisplayMember;
		private bool _IsKeyWord;
		private string _Value;
		private Brush KeywordBrush = new SolidColorBrush(Colors.Blue);
		#endregion Private Fields

		#region Public Properties
		public Brush Brush
		{
			get
			{
				return _Brush;
			}
			set
			{
				_Brush = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public TextBlock DisplayMember
		{
			get
			{
				return _DisplayMember;
			}
			private set
			{
				_DisplayMember = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool IsKeyWord
		{
			get
			{
				return _IsKeyWord;
			}
			set
			{
				_IsKeyWord = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string Value
		{
			get
			{
				return _Value;
			}
			set
			{
				_Value = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion Public Properties
	}
}
