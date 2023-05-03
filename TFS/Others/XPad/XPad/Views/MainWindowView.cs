namespace XPad.Views
{
	using GregOsborne.Application.Primitives;
	using MVVMFramework;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Threading;
	using System.Xml.Linq;
	using XPad.Controls;
	using XPadLib;

	public partial class MainWindowView : INotifyPropertyChanged
	{
		internal enum TreeItemTypes
		{
			Xml,
			Element
		}

		private ObservableCollection<XPadAttribute> _Attributes;
		private DispatcherTimer _ViewReadyTimer = null;
		public event PropertyChangedEventHandler PropertyChanged;
		public event ExecuteUIActionHandler ExecuteUIAction;
		private Visibility _TextVisibility;
		private bool _Initializing;
		private Visibility _ImageVisibility;
		private string _ApplicationTitle;
		private double _TreeWidth;
		private string _FileName;
		private bool _DocumentHasChanges;
		private XAttribute _SelectedAttribute;
		private Visibility _ErrorVisibility;
		private string _ErrorText;
		private Visibility _SpinnerVisibility;
		private XPadElement _SelectedElement;
		private XDocument _Document;

		public event EventHandler StartRead;
		public event EventHandler EndRead;
		public MainWindowView()
		{
			Initializing = true;
			ApplicationTitle = "Xpad (Xml Notepad)";
			TextVisibility = Visibility.Collapsed;
			ImageVisibility = Visibility.Collapsed;
			ErrorText = "This is an error";
			ErrorVisibility = Visibility.Collapsed;
			SpinnerVisibility = Visibility.Collapsed;

			_ViewReadyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
			_ViewReadyTimer.Tick += _ViewReadyTimer_Tick;
			_ViewReadyTimer.Start();
		}
		public void DisplayError(string message)
		{
			ErrorText = message;
			ErrorVisibility = Visibility.Visible;
		}
		public Visibility SpinnerVisibility
		{
			get { return _SpinnerVisibility; }
			set
			{
				_SpinnerVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility ErrorVisibility
		{
			get { return _ErrorVisibility; }
			set
			{
				_ErrorVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string ErrorText
		{
			get { return _ErrorText; }
			set
			{
				_ErrorText = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		private void RefreshTree()
		{
			if (ExecuteUIAction != null)
				ExecuteUIAction(this, new ExecuteUIActionEventArgs("ClearTree", null));
			if (Document.Root != null)
				Task.Factory.StartNew(() => RefreshElement(Document.Root, null), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
			DocumentHasChanges = false;
		}
		private async Task<TreeViewItem> RefreshElement(XElement element, TreeViewItem parent)
		{
			var ti = await GetTreeViewItem(element.Name.LocalName, TreeItemTypes.Element, element);
			if (parent != null)
				parent.Items.Add(ti);
			else
			{
				if (StartRead != null && SpinnerVisibility == Visibility.Collapsed)
					StartRead(this, EventArgs.Empty); 
				if (ExecuteUIAction != null)
				{
					var p = new Dictionary<string, object>
					{
						{ "parent", null },
						{ "treeviewitem", ti }
					};
					ExecuteUIAction(this, new ExecuteUIActionEventArgs("AddTreeItem", p));
				}
			}
			element.Elements().ToList().ForEach(async x => await RefreshElement(x, ti));
			if (parent == null && EndRead != null)
				EndRead(this, EventArgs.Empty);
			return ti;
		}
		private async Task<TreeViewItem> GetTreeViewItem(string text, TreeItemTypes type, XElement element)
		{
			var theElement = new XPadElement(element);
			theElement.PropertyChanged += theElement_PropertyChanged;
			TreeViewItem ti = null;
			try
			{
				var tvh = new TreeViewItemHeader();
				tvh.Icon = type == TreeItemTypes.Xml ? 0xEC51 : 0xEC50;
				tvh.Title = text;
				tvh.Element = theElement;
				tvh.RenameComplete += tvh_RenameComplete;
				ti = new TreeViewItem
				{
					Header = tvh,
					Tag = theElement
				};
			}
			catch (Exception)
			{

				throw;
			}
			return ti;
		}
		void tvh_RenameComplete(object sender, TreeViewItemHeader.RenameCompleteEventArgs e)
		{
			DocumentHasChanges = true;
		}
		void theElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			DocumentHasChanges = true;
		}
		void _ViewReadyTimer_Tick(object sender, EventArgs e)
		{
			sender.As<DispatcherTimer>().Stop();
			Initializing = false;
			TreeWidth = XPad.App.GetSetting<double>("XPad", "MainWindow", "TreeWidth", 150.0);
		}
		public void InitView()
		{
		}
		public void Initialize(Window window)
		{
			window.Left = XPad.App.GetSetting<double>("XPad", "MainWindow", "Left", window.Left);
			window.Top = XPad.App.GetSetting<double>("XPad", "MainWindow", "Top", window.Top);
			window.Width = XPad.App.GetSetting<double>("XPad", "MainWindow", "Width", window.Width);
			window.Height = XPad.App.GetSetting<double>("XPad", "MainWindow", "Height", window.Height);
			window.WindowState = XPad.App.GetSetting<WindowState>("XPad", "MainWindow", "WindowState", window.WindowState);
		}
		public void Persist(Window window)
		{
			XPad.App.SetSetting<double>("XPad", "MainWindow", "Left", window.RestoreBounds.Left);
			XPad.App.SetSetting<double>("XPad", "MainWindow", "Top", window.RestoreBounds.Top);
			XPad.App.SetSetting<double>("XPad", "MainWindow", "Width", window.RestoreBounds.Width);
			XPad.App.SetSetting<double>("XPad", "MainWindow", "Height", window.RestoreBounds.Height);
			XPad.App.SetSetting<WindowState>("XPad", "MainWindow", "WindowState", window.WindowState);
		}
		public bool Initializing
		{
			get { return _Initializing; }
			set
			{
				_Initializing = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string ApplicationTitle
		{
			get { return _ApplicationTitle; }
			set
			{
				_ApplicationTitle = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public double TreeWidth
		{
			get { return _TreeWidth; }
			set
			{
				_TreeWidth = value;
				UpdateInterface();
				if (!Initializing)
					XPad.App.SetSetting<double>("XPad", "MainWindow", "TreeWidth", TreeWidth);
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public string FileName
		{
			get { return _FileName; }
			set
			{
				_FileName = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public XDocument Document
		{
			get { return _Document; }
			set
			{
				_Document = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public bool DocumentHasChanges
		{
			get { return _DocumentHasChanges; }
			set
			{
				_DocumentHasChanges = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public XPadElement SelectedElement
		{
			get { return _SelectedElement; }
			set
			{
				_SelectedElement = value;
				if (value == null)
					Attributes = new ObservableCollection<XPadAttribute>();
				else
					Attributes = value.Attributes;

				if (SelectedElement.Image == null && !string.IsNullOrEmpty(SelectedElement.Value))
					TextVisibility = Visibility.Visible;
				else
					ImageVisibility = Visibility.Visible;

				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public XAttribute SelectedAttribute
		{
			get { return _SelectedAttribute; }
			set
			{
				_SelectedAttribute = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public ObservableCollection<XPadAttribute> Attributes
		{
			get { return _Attributes; }
			set
			{
				_Attributes = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility ImageVisibility
		{
			get { return _ImageVisibility; }
			set
			{
				_ImageVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
		public Visibility TextVisibility
		{
			get { return _TextVisibility; }
			set
			{
				_TextVisibility = value;
				UpdateInterface();
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(System.Reflection.MethodBase.GetCurrentMethod().GetPropertyName()));
			}
		}
	}
}
