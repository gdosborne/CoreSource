// -----------------------------------------------------------------------
// Copyright© 2016 Statistcs & Controls, Inc.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Extender.cs
//
namespace SNC.OptiRamp.Application.Developer.Extensions.DesignerExtension {

	using GregOsborne.MVVMFramework;
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.Controls;
	using SNC.OptiRamp.Application.DeveloperEntities.Designer;
	using SNC.OptiRamp.Application.DeveloperEntities.IO;
	using SNC.OptiRamp.Application.DeveloperEntities.Management;
	using GregOsborne.Application.Primitives;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Ribbon;

	[Export(typeof(IExtender))]
	public class Extender : IExtender {

		#region Public Constructors
		public Extender() {
			Name = "Designer Extension";
			_Control = new ExtensionControl();
			_Control.View.Extender = this;
			OptionsCategory = new DeveloperEntities.Configuration.Category {
				Title = "Designer Extension"
			};
			var genPage = new DeveloperEntities.Configuration.Page {
				Title = "General"
			};
			genPage.Settings.Add(new DeveloperEntities.Configuration.Setting {
				Name = "Default Page Size",
				SettingType = typeof(Size),
				Value = new Size(800, 600)
			});
			OptionsCategory.Pages.Add(genPage);
			ExportedCommands = new List<DelegateCommand> {
				AddControlCommand
			};
		}
		#endregion Public Constructors

		#region Public Methods
		public void AddRibbonItems(System.Windows.Controls.Ribbon.Ribbon ribbon) {
			designerTab = new RibbonTab {
				Header = "Designer"
			};
			ribbon.SelectionChanged += ribbon_SelectionChanged;
			var objectsGroup = new RibbonGroup {
				Name = "ObjectsGroup",
				Header = "Objects"
			};
			objectsMenuButton = RibbonHelper.GetRibbonMenuButton(this.GetType(), "Add", "Adds an object to the currently selected project page", "gear.png");
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddRectangleCommand, "Rectangle", "Adds a rectangle to the page", "rectangle.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddEllipseCommand, "Ellipse", "Adds an ellipse to the page", "ellipse.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddStaticLineCommand, "Static Line", "Adds a static line to the page", "staticLine.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddPinCommand, "Pin", "Adds a pin to the page", "pin.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddStaticTextCommand, "Static Text", "Adds static text to the page", "staticText.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddDynamicTextCommand, "Dynamic Text", "Adds dynamic text to the page", "dynamicText.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddUpdateableTextCommand, "Updateable Text", "Adds updateable text to the page", "updatableText.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddStaticImageCommand, "Static Image", "Adds a static image to the page", "staticImage.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddDynamicImageCommand, "Dynamic Image", "Adds a dynamic image to the page", "dynamicImage.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddRuntimeImageCommand, "Runtime Image", "Adds a runtime image to the page", "runtimeImage.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddBarChartCommand, "Bar Chart", "Adds a bar chart to the page", "barChart.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddTrendBarChartCommand, "Trend Bar Chart", "Adds a trending bar chart to the page", "trendbarChart.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddPieChartCommand, "Pie Chart", "Adds a pie chart to the page", "pieChart.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddTrendChartCommand, "Trend Chart", "Adds a trend chart to the page", "trendChart.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddSpiderPlotCommand, "Spider Plot", "Adds a spider plot to the page", "spiderPlot.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddCircularProgressCommand, "Circular Progress Bar", "Adds a circular progress bar to the page", "circularProgress.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddSpeedometerCommand, "Speedometer", "Adds a speedomoter to the page", "speedometer.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddLinearMeterCommand, "Linear Meter", "Adds a linear meter to the page", "meter.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddSnapshotTableCommand, "Snapshot Table", "Adds a snapshot table to the page", "tableObject.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddRealtimeTableCommand, "Realtime Table", "Adds a realtime table to the page", "realtimeTableObject.png"));
			objectsMenuButton.Items.Add(new RibbonSeparator());
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddNormalDistributionPlotCommand, "Normal Distribution Plot", "Adds a normal distribution plot to the page", "normalPlot.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddNormalDistributionCompliancePlotCommand, "Normal Distribution Compliance Plot", "Adds a normal distribution compliance plot to the page", "normalCompliancePlot.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddBoxAndWhiskerPlotCommand, "Box and Whisker Plot", "Adds a box and whisker plot to the page", "boxAndWhiskerPlot.png"));
			objectsMenuButton.Items.Add(RibbonHelper.GetButtonMenuItem(this.GetType(), AddCompliancePlotCommand, "Target Compliance Plot", "Adds a target compliance to the page", "compliancePlot.png"));
			objectsGroup.Items.Add(objectsMenuButton);

			var pagesGroup = new RibbonGroup {
				Name = "PagesGroup",
				Header = "Pages"
			};
			pagesGroup.Items.Add(RibbonHelper.GetButton(this.GetType(), AddWAPageCommand, "Add Page", "Adds a new web analytics page to the project at the same level as the current page.", "waPage.png"));
			pagesGroup.Items.Add(RibbonHelper.GetButton(this.GetType(), AddWASubPageCommand, "Add Sub-Page", "Adds a new web analytics page to the project as a sub-page of the current page.", "waSubPage.png"));
			pagesGroup.Items.Add(RibbonHelper.GetButton(this.GetType(), AddWAPopupPageCommand, "Add Popup-Page", "Adds a new web analytics popup page to the project as a sub-page of the current page.", "waPopupPage.png"));

			var pagePropertiesGroup = new RibbonGroup {
				Name = "PagePropertiesGroup",
				Header = "Properties"
			};
			var cc = new ContentControl();
			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition {
				Height = new GridLength(0, GridUnitType.Auto)
			});
			grid.RowDefinitions.Add(new RowDefinition {
				Height = new GridLength(0, GridUnitType.Auto)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition {
				Width = new GridLength(0, GridUnitType.Auto)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition {
				Width = new GridLength(0, GridUnitType.Auto)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition {
				Width = new GridLength(0, GridUnitType.Auto)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition {
				Width = new GridLength(0, GridUnitType.Auto)
			});
			var l = new Label {
				Content = "Width"
			};
			l.SetValue(Grid.ColumnProperty, 0);
			l.SetValue(Grid.RowProperty, 0);
			grid.Children.Add(l);
			var t = new RibbonTextBox {
				Text = "800"
			};
			t.SetValue(Grid.ColumnProperty, 1);
			t.SetValue(Grid.RowProperty, 0);
			grid.Children.Add(t);
			l = new Label {
				Content = "Height"
			};
			l.SetValue(Grid.ColumnProperty, 2);
			l.SetValue(Grid.RowProperty, 0);
			grid.Children.Add(l);
			t = new RibbonTextBox {
				Text = "600"
			};
			t.SetValue(Grid.ColumnProperty, 3);
			t.SetValue(Grid.RowProperty, 0);
			grid.Children.Add(t);
			l = new Label {
				Content = "Background"
			};
			l.SetValue(Grid.ColumnProperty, 0);
			l.SetValue(Grid.RowProperty, 1);
			grid.Children.Add(l);
			cc.Content = grid;
			pagePropertiesGroup.Items.Add(cc);

			designerTab.Items.Add(objectsGroup);
			designerTab.Items.Add(pagesGroup);
			designerTab.Items.Add(pagePropertiesGroup);

			ribbon.Items.Add(designerTab);
		}
		public void AddUserControl(UserControl control) {
			_Control.As<ExtensionControl>().MyCanvas.Children.Add(control);
		}
		public void UpdateInterface() {
			AddRectangleCommand.RaiseCanExecuteChanged();
			AddEllipseCommand.RaiseCanExecuteChanged();
			AddStaticLineCommand.RaiseCanExecuteChanged();
			AddPinCommand.RaiseCanExecuteChanged();
			AddStaticTextCommand.RaiseCanExecuteChanged();
			AddDynamicTextCommand.RaiseCanExecuteChanged();
			AddUpdateableTextCommand.RaiseCanExecuteChanged();
			AddStaticImageCommand.RaiseCanExecuteChanged();
			AddDynamicImageCommand.RaiseCanExecuteChanged();
			AddRuntimeImageCommand.RaiseCanExecuteChanged();
			AddBarChartCommand.RaiseCanExecuteChanged();
			AddTrendBarChartCommand.RaiseCanExecuteChanged();
			AddPieChartCommand.RaiseCanExecuteChanged();
			AddTrendChartCommand.RaiseCanExecuteChanged();
			AddSpiderPlotCommand.RaiseCanExecuteChanged();
			AddCircularProgressCommand.RaiseCanExecuteChanged();
			AddSpeedometerCommand.RaiseCanExecuteChanged();
			AddLinearMeterCommand.RaiseCanExecuteChanged();
			AddSnapshotTableCommand.RaiseCanExecuteChanged();
			AddRealtimeTableCommand.RaiseCanExecuteChanged();
			AddNormalDistributionPlotCommand.RaiseCanExecuteChanged();
			AddNormalDistributionCompliancePlotCommand.RaiseCanExecuteChanged();
			AddBoxAndWhiskerPlotCommand.RaiseCanExecuteChanged();
			AddCompliancePlotCommand.RaiseCanExecuteChanged();
			AddWAPageCommand.RaiseCanExecuteChanged();
			AddWASubPageCommand.RaiseCanExecuteChanged();
			AddWAPopupPageCommand.RaiseCanExecuteChanged();
		}
		#endregion Public Methods

		#region Private Methods
		private void _Project_AddControl(object sender, DeveloperEntities.Controls.AddControlEventArgs e) {
			_Control.As<ExtensionControl>().MyCanvas.Children.Add(e.Control);
		}
		private void AddBarChart(object state) {
			UpdateObjectMenuButton("Bar Chart", "Adds a bar chart to the page", "barChart.png");
		}
		private void AddBoxAndWhiskerPlot(object state) {
			UpdateObjectMenuButton("Box and Whisker Plot", "Adds a box and whisker plot to the page", "boxAndWhiskerPlot.png");
		}
		private void AddCircularProgress(object state) {
			UpdateObjectMenuButton("Circular Progress Bar", "Adds a circular progress bar to the page", "circularProgress.png");
		}
		private void AddCompliancePlot(object state) {
			UpdateObjectMenuButton("Target Compliance Plot", "Adds a target compliance to the page", "compliancePlot.png");
		}
		private void AddControl(object state) {
			if (!AddControlCommand.CanExecute(state) || state == null)
				return;
			DesignerObject obj = state.As<DesignerObject>();
			obj.Control.SetValue(Canvas.LeftProperty, obj.Location.X);
			obj.Control.SetValue(Canvas.TopProperty, obj.Location.Y);
			if (obj is DesignerRectangle) {
				var ctrl = obj.Control.As<uRectangle>();
				var def = obj.As<DesignerRectangle>();
				ctrl.CornerRadius = def.CornerRadius;
			}
			else if (obj is DesignerEllipse) {
				var ctrl = obj.Control.As<uEllipse>();
				var def = obj.As<DesignerEllipse>();
			}
			AddUserControl(obj.Control);
		}
		private void AddDynamicImage(object state) {
			UpdateObjectMenuButton("Dynamic Image", "Adds a dynamic image to the page", "dynamicImage.png");
		}
		private void AddDynamicText(object state) {
			UpdateObjectMenuButton("Dynamic Text", "Adds dynamic text to the page", "dynamicText.png");
		}
		private void AddEllipse(object state) {
			UpdateObjectMenuButton("Ellipse", "Adds an ellipse to the page", "ellipse.png");
			ProjectFile.Project.CurrentPage.Objects.Add(new DesignerEllipse {
				Location = _Control.View.LastPoint,
				Size = new Size(100, 100)
			});
			ProjectFile.IsChanged = true;
			UpdateInterface();
			if (ProjectChanged != null)
				ProjectChanged(this, EventArgs.Empty);
		}
		private void AddLinearMeter(object state) {
			UpdateObjectMenuButton("Linear Meter", "Adds a linear meter to the page", "meter.png");
		}
		private void AddNormalDistributionCompliancePlot(object state) {
			UpdateObjectMenuButton("Normal Distribution Compliance Plot", "Adds a normal distribution compliance plot to the page", "normalCompliancePlot.png");
		}
		private void AddNormalDistributionPlot(object state) {
			UpdateObjectMenuButton("Normal Distribution Plot", "Adds a normal distribution plot to the page", "normalPlot.png");
		}
		private void AddPieChart(object state) {
			UpdateObjectMenuButton("Pie Chart", "Adds a pie chart to the page", "pieChart.png");
		}
		private void AddPin(object state) {
			UpdateObjectMenuButton("Pin", "Adds a pin to the page", "pin.png");
		}
		private void AddRealtimeTable(object state) {
			UpdateObjectMenuButton("Realtime Table", "Adds a realtime table to the page", "realtimeTableObject.png");
		}
		private void AddRectangle(object state) {
			UpdateObjectMenuButton("Rectangle", "Adds a rectangle to the page", "rectangle.png");
			ProjectFile.Project.CurrentPage.Objects.Add(new DesignerRectangle {
				Location = _Control.View.LastPoint,
				Size = new Size(100, 100)
			});
			ProjectFile.IsChanged = true;
			UpdateInterface();
			if (ProjectChanged != null)
				ProjectChanged(this, EventArgs.Empty);
		}
		private void AddRuntimeImage(object state) {
			UpdateObjectMenuButton("Runtime Image", "Adds a runtime image to the page", "runtimeImage.png");
		}
		private void AddSnapshotTable(object state) {
			UpdateObjectMenuButton("Snapshot Table", "Adds a snapshot table to the page", "tableObject.png");
		}
		private void AddSpeedometer(object state) {
			UpdateObjectMenuButton("Speedometer", "Adds a speedomoter to the page", "speedometer.png");
		}
		private void AddSpiderPlot(object state) {
			UpdateObjectMenuButton("Spider Plot", "Adds a spider plot to the page", "spiderPlot.png");
		}
		private void AddStaticImage(object state) {
			UpdateObjectMenuButton("Static Image", "Adds a static image to the page", "staticImage.png");
		}
		private void AddStaticLine(object state) {
			UpdateObjectMenuButton("Static Line", "Adds a static line to the page", "staticLine.png");
		}
		private void AddStaticText(object state) {
			UpdateObjectMenuButton("Static Text", "Adds static text to the page", "staticText.png");
		}
		private void AddTrendBarChart(object state) {
			UpdateObjectMenuButton("Trend Bar Chart", "Adds a trending bar chart to the page", "trendbarChart.png");
		}
		private void AddTrendChart(object state) {
			UpdateObjectMenuButton("Trend Chart", "Adds a trend chart to the page", "trendChart.png");
		}
		private void AddUpdateableText(object state) {
			UpdateObjectMenuButton("Updateable Text", "Adds updateable text to the page", "updatableText.png");
		}
		private void AddWAPage(object state) {
		}
		private void AddWAPopupPage(object state) {
		}
		private void AddWASubPage(object state) {
		}
		private void ribbon_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
			if ((e.AddedItems[0]) == designerTab) {
				if (_Control != null && ShowUserControl != null)
					ShowUserControl(this, new ShowUserControlEventArgs(_Control));
			}
		}
		private void ShowDesigner(object state) {
			if (_Control != null && ShowUserControl != null)
				ShowUserControl(this, new ShowUserControlEventArgs(_Control));
		}
		private void UpdateObjectMenuButton(string title, string tooltip, string imageFile) {
			objectsMenuButton.LargeImageSource = RibbonHelper.GetImageSource(this.GetType(), 32, imageFile);
			objectsMenuButton.ToolTipImageSource = RibbonHelper.GetImageSource(this.GetType(), 128, imageFile);
			objectsMenuButton.ToolTipDescription = tooltip;
			objectsMenuButton.ToolTipTitle = title;
		}
		private bool ValidateAddBarChartState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddBoxAndWhiskerPlotState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddCircularProgressState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddCompliancePlotState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddControlState(object state) {
			return true;
		}
		private bool ValidateAddDynamicImageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddDynamicTextState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddEllipseState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddLinearMeterState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddNormalDistributionCompliancePlotState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddNormalDistributionPlotState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddPieChartState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddPinState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddRealtimeTableState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddRectangleState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddRuntimeImageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddSnapshotTableState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddSpeedometerState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddSpiderPlotState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddStaticImageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddStaticLineState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddStaticTextState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddTrendBarChartState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddTrendChartState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddUpdateableTextState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddWAPageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddWAPopupPageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateAddWASubPageState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		private bool ValidateShowDesignerState(object state) {
			return ProjectFile != null && ProjectFile.Project.CurrentPage != null;
		}
		#endregion Private Methods

		#region Public Events
		public event ProjectChangedHandler ProjectChanged;
		public event ShowUserControlHandler ShowUserControl;
		#endregion Public Events

		#region Private Fields
		private DelegateCommand _AddBarChartCommand = null;
		private DelegateCommand _AddBoxAndWhiskerPlotCommand = null;
		private DelegateCommand _AddCircularProgressCommand = null;
		private DelegateCommand _AddCompliancePlotCommand = null;
		private DelegateCommand _AddControlCommand = null;
		private DelegateCommand _AddDynamicImageCommand = null;
		private DelegateCommand _AddDynamicTextCommand = null;
		private DelegateCommand _AddEllipseCommand = null;
		private DelegateCommand _AddLinearMeterCommand = null;
		private DelegateCommand _AddNormalDistributionCompliancePlotCommand = null;
		private DelegateCommand _AddNormalDistributionPlotCommand = null;
		private DelegateCommand _AddPieChartCommand = null;
		private DelegateCommand _AddPinCommand = null;
		private DelegateCommand _AddRealtimeTableCommand = null;
		private DelegateCommand _AddRectangleCommand = null;
		private DelegateCommand _AddRuntimeImageCommand = null;
		private DelegateCommand _AddSnapshotTableCommand = null;
		private DelegateCommand _AddSpeedometerCommand = null;
		private DelegateCommand _AddSpiderPlotCommand = null;
		private DelegateCommand _AddStaticImageCommand = null;
		private DelegateCommand _AddStaticLineCommand = null;
		private DelegateCommand _AddStaticTextCommand = null;
		private DelegateCommand _AddTrendBarChartCommand = null;
		private DelegateCommand _AddTrendChartCommand = null;
		private DelegateCommand _AddUpdateableTextCommand = null;
		private DelegateCommand _AddWAPageCommand = null;
		private DelegateCommand _AddWAPopupPageCommand = null;
		private DelegateCommand _AddWASubPageCommand = null;
		private ExtensionControl _Control = null;
		private IList<DelegateCommand> _ExportedCommands = null;
		private string _Name = string.Empty;
		private Category _OptionsCategory = null;
		private ProjectFile _Project = null;
		private DelegateCommand _ShowDesignerCommand = null;
		private RibbonTab designerTab = null;
		private RibbonMenuButton objectsMenuButton = null;
		#endregion Private Fields

		#region Public Properties
		public DelegateCommand AddBarChartCommand {
			get {
				if (_AddBarChartCommand == null)
					_AddBarChartCommand = new DelegateCommand(AddBarChart, ValidateAddBarChartState);
				return _AddBarChartCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddBoxAndWhiskerPlotCommand {
			get {
				if (_AddBoxAndWhiskerPlotCommand == null)
					_AddBoxAndWhiskerPlotCommand = new DelegateCommand(AddBoxAndWhiskerPlot, ValidateAddBoxAndWhiskerPlotState);
				return _AddBoxAndWhiskerPlotCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddCircularProgressCommand {
			get {
				if (_AddCircularProgressCommand == null)
					_AddCircularProgressCommand = new DelegateCommand(AddCircularProgress, ValidateAddCircularProgressState);
				return _AddCircularProgressCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddCompliancePlotCommand {
			get {
				if (_AddCompliancePlotCommand == null)
					_AddCompliancePlotCommand = new DelegateCommand(AddCompliancePlot, ValidateAddCompliancePlotState);
				return _AddCompliancePlotCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddControlCommand {
			get {
                if (_AddControlCommand == null)
					_AddControlCommand = new DelegateCommand(AddControl, ValidateAddControlState) {
						//Name = "AddControl",
						//Description = "Adds a control to the designer canvas"
					};
				return _AddControlCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddDynamicImageCommand {
			get {
				if (_AddDynamicImageCommand == null)
					_AddDynamicImageCommand = new DelegateCommand(AddDynamicImage, ValidateAddDynamicImageState);
				return _AddDynamicImageCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddDynamicTextCommand {
			get {
				if (_AddDynamicTextCommand == null)
					_AddDynamicTextCommand = new DelegateCommand(AddDynamicText, ValidateAddDynamicTextState);
				return _AddDynamicTextCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddEllipseCommand {
			get {
				if (_AddEllipseCommand == null)
					_AddEllipseCommand = new DelegateCommand(AddEllipse, ValidateAddEllipseState);
				return _AddEllipseCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddLinearMeterCommand {
			get {
				if (_AddLinearMeterCommand == null)
					_AddLinearMeterCommand = new DelegateCommand(AddLinearMeter, ValidateAddLinearMeterState);
				return _AddLinearMeterCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddNormalDistributionCompliancePlotCommand {
			get {
				if (_AddNormalDistributionCompliancePlotCommand == null)
					_AddNormalDistributionCompliancePlotCommand = new DelegateCommand(AddNormalDistributionCompliancePlot, ValidateAddNormalDistributionCompliancePlotState);
				return _AddNormalDistributionCompliancePlotCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddNormalDistributionPlotCommand {
			get {
				if (_AddNormalDistributionPlotCommand == null)
					_AddNormalDistributionPlotCommand = new DelegateCommand(AddNormalDistributionPlot, ValidateAddNormalDistributionPlotState);
				return _AddNormalDistributionPlotCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddPieChartCommand {
			get {
				if (_AddPieChartCommand == null)
					_AddPieChartCommand = new DelegateCommand(AddPieChart, ValidateAddPieChartState);
				return _AddPieChartCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddPinCommand {
			get {
				if (_AddPinCommand == null)
					_AddPinCommand = new DelegateCommand(AddPin, ValidateAddPinState);
				return _AddPinCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddRealtimeTableCommand {
			get {
				if (_AddRealtimeTableCommand == null)
					_AddRealtimeTableCommand = new DelegateCommand(AddRealtimeTable, ValidateAddRealtimeTableState);
				return _AddRealtimeTableCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddRectangleCommand {
			get {
				if (_AddRectangleCommand == null)
					_AddRectangleCommand = new DelegateCommand(AddRectangle, ValidateAddRectangleState);
				return _AddRectangleCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddRuntimeImageCommand {
			get {
				if (_AddRuntimeImageCommand == null)
					_AddRuntimeImageCommand = new DelegateCommand(AddRuntimeImage, ValidateAddRuntimeImageState);
				return _AddRuntimeImageCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddSnapshotTableCommand {
			get {
				if (_AddSnapshotTableCommand == null)
					_AddSnapshotTableCommand = new DelegateCommand(AddSnapshotTable, ValidateAddSnapshotTableState);
				return _AddSnapshotTableCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddSpeedometerCommand {
			get {
				if (_AddSpeedometerCommand == null)
					_AddSpeedometerCommand = new DelegateCommand(AddSpeedometer, ValidateAddSpeedometerState);
				return _AddSpeedometerCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddSpiderPlotCommand {
			get {
				if (_AddSpiderPlotCommand == null)
					_AddSpiderPlotCommand = new DelegateCommand(AddSpiderPlot, ValidateAddSpiderPlotState);
				return _AddSpiderPlotCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddStaticImageCommand {
			get {
				if (_AddStaticImageCommand == null)
					_AddStaticImageCommand = new DelegateCommand(AddStaticImage, ValidateAddStaticImageState);
				return _AddStaticImageCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddStaticLineCommand {
			get {
				if (_AddStaticLineCommand == null)
					_AddStaticLineCommand = new DelegateCommand(AddStaticLine, ValidateAddStaticLineState);
				return _AddStaticLineCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddStaticTextCommand {
			get {
				if (_AddStaticTextCommand == null)
					_AddStaticTextCommand = new DelegateCommand(AddStaticText, ValidateAddStaticTextState);
				return _AddStaticTextCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddTrendBarChartCommand {
			get {
				if (_AddTrendBarChartCommand == null)
					_AddTrendBarChartCommand = new DelegateCommand(AddTrendBarChart, ValidateAddTrendBarChartState);
				return _AddTrendBarChartCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddTrendChartCommand {
			get {
				if (_AddTrendChartCommand == null)
					_AddTrendChartCommand = new DelegateCommand(AddTrendChart, ValidateAddTrendChartState);
				return _AddTrendChartCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddUpdateableTextCommand {
			get {
				if (_AddUpdateableTextCommand == null)
					_AddUpdateableTextCommand = new DelegateCommand(AddUpdateableText, ValidateAddUpdateableTextState);
				return _AddUpdateableTextCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddWAPageCommand {
			get {
				if (_AddWAPageCommand == null)
					_AddWAPageCommand = new DelegateCommand(AddWAPage, ValidateAddWAPageState);
				return _AddWAPageCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddWAPopupPageCommand {
			get {
				if (_AddWAPopupPageCommand == null)
					_AddWAPopupPageCommand = new DelegateCommand(AddWAPopupPage, ValidateAddWAPopupPageState);
				return _AddWAPopupPageCommand as DelegateCommand;
			}
		}
		public DelegateCommand AddWASubPageCommand {
			get {
				if (_AddWASubPageCommand == null)
					_AddWASubPageCommand = new DelegateCommand(AddWASubPage, ValidateAddWASubPageState);
				return _AddWASubPageCommand as DelegateCommand;
			}
		}
		public IList<DelegateCommand> ExportedCommands {
			get {
				return _ExportedCommands;
			}
			private set {
				_ExportedCommands = value;
			}
		}
		public string Name {
			get {
				return _Name;
			}
			private set {
				_Name = value;
			}
		}
		public Category OptionsCategory {
			get {
				return _OptionsCategory;
			}
			set {
				_OptionsCategory = value;
			}
		}
		public ProjectFile ProjectFile {
			get {
				return _Project;
			}
			set {
				_Project = value;
				if (_Project != null) {
					_Project.AddControl += _Project_AddControl;
				}
			}
		}
		#endregion Public Properties

		#region Private Properties
		private DelegateCommand ShowDesignerCommand {
			get {
				if (_ShowDesignerCommand == null)
					_ShowDesignerCommand = new DelegateCommand(ShowDesigner, ValidateShowDesignerState);
				return _ShowDesignerCommand as DelegateCommand;
			}
		}
		#endregion Private Properties
	}
}
