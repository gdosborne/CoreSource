namespace SDFManager
{
	using GregOsborne.Application.Primitives;
	using GregOsborne.Dialog;
	using Microsoft.Win32;
	using MVVMFramework;
	using SDFManagerSupport;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Ribbon;
	using System.Windows.Input;
	using System.Windows.Shapes;

	public partial class SDFMainWindow : RibbonWindow
	{
		#region Public Constructors
		public SDFMainWindow()
		{
			InitializeComponent();
			if (View != null)
			{
				View.ExecuteAction += View_ExecuteAction;
				View.AddControl += View_AddControl;
				View.ClearCanvas += View_ClearCanvas;
			}
		}
		#endregion Public Constructors

		#region Private Methods
		private void DiagramCanvas_DeselectItem(object sender, EventArgs e)
		{
			View.SelectedTableDefinition = null;
		}
		private void DiagramCanvas_MoveComplete(object sender, MoveCompleteEventArgs e)
		{
			e.TableDef.LeftPosition = e.Location.X;
			e.TableDef.TopPosition = e.Location.Y;
			View.Definition.IsChanged = true;
			SetCanvasSize();
		}
		private void DiagramCanvas_TableSelected(object sender, TableSelectedEventArgs e)
		{
			View.SelectedTableDefinition = e.Table;
		}
		private void RibbonWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (View.Definition != null && View.Definition.IsChanged)
			{
				switch (View.AskSave())
				{
					case true:
						View.Save(null);
						break;
					case null:
						e.Cancel = true;
						return;
				}
			}
			View.Persist(this);
		}
		private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
		{
			View.InitView();
			View.Initialize(this);
		}
		private void ScaleContent(double value)
		{
			DiagramCanvas.UpdateLayout();
			View.CurrentScale = value;
			SetCanvasSize();
		}
		private void Scroller_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
			if (!handle)
				return;

			double zoom = e.Delta;
			if (View.CurrentScale <= .1 && zoom < 0)
				return;
			var value = zoom > 0 ? .05 : -.05;
			ScaleContent(View.CurrentScale + value);
			e.Handled = true;
		}
		private void Scroller_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			SetCanvasSize();
		}
		private void SetCanvasSize()
		{
			DiagramCanvas.Width = DiagramCanvas.MaxChildPositionX();
			DiagramCanvas.Height = DiagramCanvas.MaxChildPositionY();
		}
		private void View_AddControl(object sender, AddControlEventArgs e)
		{
			e.Element.SetValue(Canvas.LeftProperty, e.Location.X);
			e.Element.SetValue(Canvas.TopProperty, e.Location.Y);
			DiagramCanvas.Children.Add(e.Element);
			if (e.Element.Is<Polyline>())
				e.Element.SetValue(Canvas.ZIndexProperty, -99);
			SetCanvasSize();
		}
		private void View_ClearCanvas(object sender, ClearCanvasEventArgs e)
		{
			DiagramCanvas.Clear();
		}
		private void View_ExecuteAction(object sender, ExecuteUIActionEventArgs e)
		{
			switch (e.CommandToExecute)
			{
				case "ShowOptionsDialog":
					var optionsDialog = new SettingsWindow
					{
						Owner = this
					};
					optionsDialog.ShowDialog();
					break;
				case "SaveAsDefinitionFile":
					var saveAsFileDialog = new SaveFileDialog
					{
						CreatePrompt = false,
						OverwritePrompt = true,
						CheckPathExists = true,
						CheckFileExists = false,
						DefaultExt = ".def",
						Filter = "Definition files|*.def",
						InitialDirectory = (string)e.Parameters["initialdirectory"],
						Title = "Save definition file..."
					};
					e.Parameters["result"] = saveAsFileDialog.ShowDialog(this);
					e.Parameters["filename"] = saveAsFileDialog.FileName;
					break;
				case "ClearCanvas":
					DiagramCanvas.Clear();
					break;
				case "ShowAddTable":
					var tableDialog = new TableWindow
					{
						Owner = this
					};
					tableDialog.View.Definition = e.Parameters["definition"].As<TableDefinition>();
					var tableResult = tableDialog.ShowDialog();
					if (!tableResult.GetValueOrDefault())
						return;
					if (tableDialog.View.IsNewTable)
					{
						View.Definition.Tables.Add(tableDialog.View.Definition);
						View.Definition.IsChanged = true;
					}
					DiagramCanvas.ClearSelection();
					View.RefreshDiagram();
					break;
				case "ShowMessage":
					var messageDialog = new TaskDialog
					{
						AdditionalInformation = (string)e.Parameters["additionalinfo"],
						AllowClose = false,
						Width = 400,
						Image = (ImagesTypes)e.Parameters["imagetype"],
						MessageText = (string)e.Parameters["text"],
						Title = (string)e.Parameters["title"]
					};
					messageDialog.AddButtons((ButtonTypes[])e.Parameters["buttons"]);
					var messageResult = messageDialog.ShowDialog(this);
					e.Parameters["result"] = messageResult == (int)ButtonTypes.Yes ? (bool?)true : messageResult == (int)ButtonTypes.No ? (bool?)false : (bool?)null;
					break;
				case "OpenDefinitionFile":
					var openFileDialog = new OpenFileDialog
					{
						CheckPathExists = true,
						CheckFileExists = true,
						DefaultExt = ".def",
						Filter = "Definition files|*.def",
						InitialDirectory = (string)e.Parameters["initialdirectory"],
						Multiselect = false,
						Title = "Open definition file..."
					};
					e.Parameters["result"] = openFileDialog.ShowDialog(this);
					e.Parameters["filename"] = openFileDialog.FileName;
					break;
			}
		}
		#endregion Private Methods

		#region Public Properties
		public SDFMainWindowView View { get { return LayoutRoot.GetView<SDFMainWindowView>(); } }
		#endregion Public Properties
	}
}
