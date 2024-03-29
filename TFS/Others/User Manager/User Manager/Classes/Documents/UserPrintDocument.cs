using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using SNC.Authorization.Management;
using MyApplication.Primatives;

namespace User_Manager.Classes.Documents
{
	public class UserPrintPaginator<T> : DocumentPaginator
	{
		public UserPrintPaginator(ISNCPermissionManager manager)
			: this()
		{
			Manager = manager;
		}
		public ISNCPermissionManager Manager { get; private set; }
		protected int _Rows;
		protected int _RowsPerPage;
		private const double _Dpi = 96;
		private const double _PageHeight = 11 * _Dpi;
		private const double _PageWidth = 8.5 * _Dpi;
		private bool _IsLandscape = false;
		private Size _PageSize;

		private UserPrintPaginator()
		{
			BaseFontSize = 14;
			HeaderFontSize = BaseFontSize * 1.2;
			TitleFontSize = BaseFontSize * 1.5;
			FontFamily = new FontFamily("Calibri");
			PageMargin = 50;
			HeaderHeight = 50;
			ColumnSpacing = 10;
			IsLandscape = false;
			_RowsPerPage = 20;
		}

		public bool IsLandscape
		{
			get { return _IsLandscape; }
			set
			{
				_IsLandscape = value;
				if (_IsLandscape)
					PageSize = new Size(_PageHeight, _PageWidth);
				else
					PageSize = new Size(_PageWidth, _PageHeight);
			}
		}
		public override bool IsPageCountValid 
		{ 
			get 
			{ 
				return Manager.Permissions.Count > 0; 
			} 
		}
		public override int PageCount
		{
			get 
			{ 
				return (int)System.Math.Ceiling(_Rows / (double)_RowsPerPage); 
			}
		}
		public override Size PageSize
		{
			get { return _PageSize; }
			set
			{
				_PageSize = value;
				_RowsPerPage = (int)System.Math.Floor((_PageSize.Height - (2 * PageMargin) - HeaderHeight) / LineHeight);
			}
		}
		public override IDocumentPaginatorSource Source { get { return null; } }
		public string Title { get; set; }
		protected double BaseFontSize { get; set; }
		protected double ColumnSpacing { get; set; }
		protected FontFamily FontFamily { get; set; }
		protected double HeaderFontSize { get; set; }
		protected double HeaderHeight { get; set; }
		protected double LineHeight 
		{ 
			get 
			{ 
				return TextHeight("Xy", FontFamily, BaseFontSize, FontWeights.Normal); 
			} 
		}
		protected double PageMargin { get; set; }
		protected double TitleFontSize { get; set; }

		public override DocumentPage GetPage(int pageNumber)
		{
			int currentRow = _RowsPerPage * pageNumber;
			int maxRows = currentRow + _RowsPerPage > Manager.Permissions.Count - 1 ? Manager.Permissions.Count : currentRow + _RowsPerPage;

			var g = GetWrapperGrid();

			//AddColumn(g, new GridLength(1, GridUnitType.Star));
			//AddColumn(g, new GridLength(150, GridUnitType.Pixel));
			//AddColumn(g, new GridLength(150, GridUnitType.Pixel));
			//AddRow(g, new GridLength(0, GridUnitType.Auto));

			//var hdrHeight = TextHeight(Title, FontFamily, TitleFontSize, FontWeights.Bold);
			//AddTextBlock(g, Title, 0, 0, g.ColumnDefinitions.Count, FontFamily, TitleFontSize, FontWeights.Bold, new Thickness(0, 0, 0, HeaderHeight - hdrHeight), TextAlignment.Center);

			//AddRow(g, new GridLength(0, GridUnitType.Auto));
			//AddTextBlock(g, "Name", g.RowDefinitions.Count - 1, 0, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//AddTextBlock(g, "Created", g.RowDefinitions.Count - 1, 1, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//AddTextBlock(g, "Active", g.RowDefinitions.Count - 1, 2, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0), TextAlignment.Center);

			//for (int i = currentRow; i < maxRows; i++)
			//{
			//	var acct = Data[i];
			//	AddRow(g, new GridLength(0, GridUnitType.Auto));
			//	AddTextBlock(g, acct.Name, g.RowDefinitions.Count - 1, 0, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	AddTextBlock(g, acct.Created.ToString("yyyy-MM-dd"), g.RowDefinitions.Count - 1, 1, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	AddTextBlock(g, acct.Active ? "X" : string.Empty, g.RowDefinitions.Count - 1, 2, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0), TextAlignment.Center);
			//}

			g.Measure(PageSize);
			g.Arrange(new Rect(new Point(0, 0), PageSize));
			return new DocumentPage(g);
		}

		protected void AddColumn(Grid g, GridLength gl)
		{
			g.ColumnDefinitions.Add(new ColumnDefinition { Width = gl });
		}

		protected void AddRow(Grid g, GridLength gl)
		{
			g.RowDefinitions.Add(new RowDefinition { Height = gl });
		}

		protected void AddTextBlock(Grid g, string text, int row, int col, int colSpan, FontFamily fontFamily, double fontSize, FontWeight fontWeight, Thickness margin, TextAlignment textAlignment)
		{
			var tb = new TextBlock
			{
				Text = text,
				Margin = margin,
				FontSize = fontSize,
				FontWeight = fontWeight,
				TextAlignment = textAlignment
			};
			if (fontFamily != null)
				tb.FontFamily = fontFamily;
			tb.SetValue(Grid.RowProperty, row);
			tb.SetValue(Grid.ColumnProperty, col);
			tb.SetValue(Grid.ColumnSpanProperty, colSpan);
			g.Children.Add(tb);
		}

		protected Grid GetWrapperGrid()
		{
			var g = new Grid
			{
				Width = PageSize.Width - (2 * PageMargin),
				Height = PageSize.Height - (2 * PageMargin)
			};
			return g;
		}

		protected double TextHeight(string text, FontFamily fontFamily, double fontSize, FontWeight fontWeight)
		{
			var tb = new TextBlock
			{
				Text = text,
				FontSize = fontSize,
				FontWeight = fontWeight
			};
			if (fontFamily != null)
				tb.FontFamily = fontFamily;
			tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
			return tb.DesiredSize.Height;
		}

		protected DocumentPage GetTasksPage(int pageNumber, bool includePercentComplete, bool includeCompleted, bool includeOverdueDays, out string actualTitle)
		{
			int currentRow = _RowsPerPage * pageNumber;
			int maxRows = currentRow + _RowsPerPage > Manager.Permissions.Count - 1 ? Manager.Permissions.Count : currentRow + _RowsPerPage;

			var g = GetWrapperGrid();

			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(1, GridUnitType.Star));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddColumn(g, new GridLength(0, GridUnitType.Auto));
			AddRow(g, new GridLength(0, GridUnitType.Auto));

			actualTitle = "User Manager";

			var hdrHeight = TextHeight(actualTitle, FontFamily, TitleFontSize, FontWeights.Bold);
			AddTextBlock(g, actualTitle, 0, 0, g.ColumnDefinitions.Count, FontFamily, TitleFontSize, FontWeights.Bold, new Thickness(0, 0, 0, HeaderHeight - hdrHeight), TextAlignment.Center);
			AddTextBlock(g, string.Format("Print date: {0}", DateTime.Now.ToString("yyyy-MM-dd")), 0, 0, g.ColumnDefinitions.Count, FontFamily, BaseFontSize, FontWeights.Bold, new Thickness(0, hdrHeight, 0, 0), TextAlignment.Center);

			//var col = 0;
			//AddRow(g, new GridLength(0, GridUnitType.Auto));
			//AddTextBlock(g, "Workgroup", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//AddTextBlock(g, "Name", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//AddTextBlock(g, "Assigned To", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//AddTextBlock(g, "Start Date", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//AddTextBlock(g, "End Date", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//if(includeCompleted)
			//{
			//	AddTextBlock(g, "Completed", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//}
			//if(includePercentComplete)
			//{
			//	AddTextBlock(g, "%", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Center);
			//	col++;
			//}
			//if(includeOverdueDays)
			//{
			//	AddTextBlock(g, "Days", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Center);
			//	col++;
			//}
			//AddTextBlock(g, "Task Type", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//col++;
			//AddTextBlock(g, "Sub Type", g.RowDefinitions.Count - 1, col, 1, FontFamily, HeaderFontSize, FontWeights.Bold, new Thickness(0), TextAlignment.Left);

			//for(int i = currentRow; i < maxRows; i++)
			//{
			//	var task = Data[i].As<Task>();				
			//	var subType = string.Empty;
			//	var completedDate = string.Empty;
			//	var pctComplete = string.Empty;
			//	var overdueDays = string.Empty;
			//	if(task.TaskSubSubTypeCode != null)
			//		subType = task.TaskSubSubTypeCode.Name;
			//	if(task.TaskNotes.Any(x => x.Created == task.TaskNotes.Max(y => y.Created) && x.PercentComplete == 100))
			//		completedDate = task.TaskNotes.First(x => x.Created == task.TaskNotes.Max(y => y.Created) && x.PercentComplete == 100).Created.ToString("yyyy-MM-dd");
			//	if(task.PercentComplete < 100)
			//		pctComplete = task.PercentComplete.ToString();
			//	if(task.ProjectedEndDate < DateTime.Now.Date && task.PercentComplete < 100)
			//		overdueDays = DateTime.Now.Date.Subtract(task.ActualEndDate).Days.ToString();
			//	AddRow(g, new GridLength(0, GridUnitType.Auto));
			//	col = 0;
			//	AddTextBlock(g, task.WorkgroupName, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	AddTextBlock(g, task.Name, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	AddTextBlock(g, task.Member.UserFullName, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	AddTextBlock(g, task.StartText, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	AddTextBlock(g, task.ActualEndDate.ToString("yyyy-MM-dd"), g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	if(includeCompleted)
			//	{
			//		AddTextBlock(g, completedDate, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//		col++;
			//	}
			//	if(includePercentComplete)
			//	{
			//		AddTextBlock(g, pctComplete, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Right);
			//		col++;
			//	}
			//	if(includeOverdueDays)
			//	{
			//		AddTextBlock(g, overdueDays, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Right);
			//		col++;
			//	}
			//	AddTextBlock(g, task.TaskTypeCode.Name, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0, 0, ColumnSpacing, 0), TextAlignment.Left);
			//	col++;
			//	AddTextBlock(g, subType, g.RowDefinitions.Count - 1, col, 1, FontFamily, BaseFontSize, FontWeights.Normal, new Thickness(0), TextAlignment.Left);
			//}
			g.Measure(PageSize);
			g.Arrange(new Rect(new Point(0, 0), PageSize));
			return new DocumentPage(g, PageSize, new Rect(PageSize), new Rect(PageSize));
		}
	}
	public class PaginatorSource : IDocumentPaginatorSource
	{
		public PaginatorSource(DocumentPaginator paginator)
		{
			DocumentPaginator = paginator;
		}
		public DocumentPaginator DocumentPaginator { get; private set; }
	}
}
