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

	public enum Sections
	{
		None,
		Define,
		Setup,
		Types,
		Components,
		Tasks,
		Dirs,
		Files,
		Icons,
		INI,
		InstallDate,
		Languages,
		Messages,
		CustomMessages,
		LangOptions,
		Registry,
		Run,
		UninstallDelete,
		UninstallRun,
		Code
	}

	public abstract class InnoLine : INotifyPropertyChanged
	{
		//private List<string> Sections = null;

		#region Public Constructors
		public InnoLine(int number, Sections section, string text)
		{
			Text = text;
			Section = section;
			Number = number;

			var lineWeight = GetLineWeight(Text);
			var brush = GetLineBrush(Text);

			Words = new List<InnoWord>();
			var words = text.Split(' ');
			if (Text.StartsWith("#define") || Text.StartsWith(";") || Text.StartsWith("//"))
			{
				foreach (var w in words)
				{
					Words.Add(new InnoWord(w, brush, lineWeight, ' '));
				}
			}
			else
			{
				foreach (var w in words)
				{
					brush = GetWordBrush(w);
					lineWeight = GetWordWeight(w);
					var parts = w.Split('=');
					if (parts.Length > 1)
					{
						foreach (var x in parts)
						{
							brush = GetWordBrush(x);
							lineWeight = GetWordWeight(x);
							Words.Add(new InnoWord(x, brush, lineWeight, '=', x == parts[parts.Length - 1]));
						}
					}
					else
					{
						brush = GetWordBrush(Text);
						lineWeight = GetWordWeight(Text);
						Words.Add(new InnoWord(w, brush, lineWeight, ' '));
					}
				}
			}
			DisplayMember = new StackPanel { Orientation = Orientation.Horizontal };
			Words.ForEach(x => DisplayMember.Children.Add(x.DisplayMember));
		}
		#endregion

		#region Private Methods
		private Brush GetLineBrush(string text)
		{
			if (text.StartsWith("#define"))
				return DefineBrush;
			else if (text.StartsWith(";") || text.StartsWith("//"))
				return CommentBrush;
			else
				return DefaultBrush;
		}
		private FontWeight GetLineWeight(string text)
		{
			if (text.StartsWith("#define"))
				return DefineFontWeight;
			else if (text.StartsWith(";") || text.StartsWith("//"))
				return CommentFontWeight;
			else
				return DefaultFontWeight;
		}
		private Brush GetWordBrush(string word)
		{
			if ((word.StartsWith("{#") && word.EndsWith("}")))
				return DefineBrush;
			else if (InnoWord.InnoKeywords.Contains(word.TrimEnd(';')))
				return KeywordBrush;
			else if (InnoWord.InnoValues.Contains(word.Trim('{','}')))
				return InternalValueBrush;
			else
				return DefaultBrush;
		}
		private FontWeight GetWordWeight(string word)
		{
			if ((word.StartsWith("{#") && word.EndsWith("}")))
				return DefineFontWeight;
			else if (InnoWord.InnoKeywords.Contains(word.TrimEnd(';')))
				return KeywordFontWeight;
			else if (InnoWord.InnoValues.Contains(word.Trim('{', '}')))
				return InternalValueFontWeight;
			else
				return DefaultFontWeight;
		}
		#endregion

		#region Public Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		#region Private Fields
		private StackPanel _DisplayMember;
		private int _Number;
		private Sections _Section;
		private string _Text;
		private List<InnoWord> _Words;
		private Brush CommentBrush = new SolidColorBrush(Colors.Green);
		private FontWeight CommentFontWeight = FontWeights.Regular;
		private Brush DefaultBrush = new SolidColorBrush(Colors.Black);
		private FontWeight DefaultFontWeight = FontWeights.Regular;
		private Brush DefineBrush = new SolidColorBrush(Colors.Red);
		private FontWeight DefineFontWeight = FontWeights.Regular;
		private Brush KeywordBrush = new SolidColorBrush(Colors.Blue);
		private FontWeight KeywordFontWeight = FontWeights.Regular;
		private Brush SectionBrush = new SolidColorBrush(Colors.Black);
		private FontWeight SectionFontWeight = FontWeights.Bold;
		private Brush InternalValueBrush = new SolidColorBrush(Colors.Purple);
		private FontWeight InternalValueFontWeight = FontWeights.Regular;
		#endregion

		#region Public Properties
		public StackPanel DisplayMember
		{
			get
			{
				return _DisplayMember;
			}
			set
			{
				_DisplayMember = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public int Number
		{
			get
			{
				return _Number;
			}
			set
			{
				_Number = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Sections Section
		{
			get
			{
				return _Section;
			}
			set
			{
				_Section = value;
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
		public List<InnoWord> Words
		{
			get
			{
				return _Words;
			}
			set
			{
				_Words = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		#endregion
	}
}
