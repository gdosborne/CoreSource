namespace OSInstallerBuilder.Controls
{
	using Microsoft.WindowsAPICodePack.Dialogs;
	using GregOsborne.MVVMFramework;
	using GregOsborne.Application.Primitives;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Shapes;

	public partial class InstallerUI : UserControl, IInstallerSettingsController
	{
		#region Public Constructors
		public InstallerUI()
		{
			InitializeComponent();
		}
		#endregion Public Constructors

		#region Public Methods
		public void Reset()
		{
			View.Manager = null;
			View.OverlayVisibility = Visibility.Collapsed;
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.None;
			View.UpdateInterface();
		}
		#endregion Public Methods

		#region Private Methods
		private static void onManagerChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			var src = (InstallerUI)source;
			if (src == null)
				return;
			var value = (IInstallerManager)e.NewValue;
			src.View.Manager = value;
		}
		private void InstallerUIView_ExecuteUIAction(object sender, ExecuteUiActionEventArgs e)
		{
			if (e.CommandToExecute.Equals("SelectImageFile"))
			{
				var dialog = new CommonOpenFileDialog
				{
					AddToMostRecentlyUsedList = false,
					AllowNonFileSystemItems = true,
					AllowPropertyEditing = true,
					DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
					EnsureFileExists = true,
					EnsurePathExists = true,
					InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
					Multiselect = false,
					NavigateToShortcut = true,
					Title = "Select installer image file..."
				};
				dialog.Filters.Add(new CommonFileDialogFilter("Images", "*.jpg;*.png;*.bmp;"));
				dialog.IsFolderPicker = false;
				CommonFileDialogResult result = dialog.ShowDialog();
				if (result == CommonFileDialogResult.Cancel)
					return;
				View.ImagePath = dialog.FileName;
			}
		}
		private void InstallerUIView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("WindowBackground")
				|| e.PropertyName.Equals("TitleBackgroundBrush")
				|| e.PropertyName.Equals("TitleForegroundBrush")
				|| e.PropertyName.Equals("AreaSeparator")
				|| e.PropertyName.Equals("WindowText")
				|| e.PropertyName.Equals("ImagePath"))
			{
				if (SettingsChanged != null)
					SettingsChanged(this, EventArgs.Empty);
			}
		}
		private void Line_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.Separator;
			View.CurrentColor = ((sender.As<Line>()).Stroke as SolidColorBrush).Color;
			View.OverlayLeft = 0;
			View.OverlayTop = PhoneyHeader.ActualHeight - 2;
			View.OverlayWidth = (sender.As<Line>()).ActualWidth;
			View.OverlayHeight = (sender.As<Line>()).ActualHeight + 4;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void Line1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.Separator;
			View.CurrentColor = ((sender.As<Line>()).Stroke as SolidColorBrush).Color;
			View.OverlayLeft = 0;
			View.OverlayTop = PhoneyHeader.ActualHeight + PhoneyStep.ActualHeight - 2;
			View.OverlayWidth = (sender.As<Line>()).ActualWidth;
			View.OverlayHeight = (sender.As<Line>()).ActualHeight + 4;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void PhoneyHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.HeaderBackground;
			View.CurrentColor = ((sender.As<Border>()).Background.As<SolidColorBrush>()).Color;
			View.OverlayLeft = 0;
			View.OverlayTop = 0;
			View.OverlayWidth = (sender.As<Border>()).ActualWidth;
			View.OverlayHeight = (sender.As<Border>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void PhoneyStep_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.Background;
			View.CurrentColor = ((sender.As<Border>()).Background.As<SolidColorBrush>()).Color;
			View.OverlayLeft = SetupImage.ActualWidth + (sender.As<Border>()).Margin.Left;
			View.OverlayTop = PhoneyHeader.ActualHeight + (sender.As<Border>()).Margin.Top;
			View.OverlayWidth = (sender.As<Border>()).ActualWidth;
			View.OverlayHeight = (sender.As<Border>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void PhoneyTitle_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.HeaderTextForeground;
			View.CurrentColor = ((sender.As<TextBlock>()).Foreground.As<SolidColorBrush>()).Color;
			View.OverlayLeft = (sender.As<TextBlock>()).Margin.Left;
			View.OverlayTop = (sender.As<TextBlock>()).Margin.Top;
			View.OverlayWidth = (sender.As<TextBlock>()).ActualWidth;
			View.OverlayHeight = (sender.As<TextBlock>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void SetupImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.Image;
			View.OverlayLeft = 0;
			View.OverlayTop = PhoneyHeader.ActualHeight + (sender.As<Image>()).Margin.Top;
			View.OverlayWidth = (sender.As<Image>()).ActualWidth;
			View.OverlayHeight = (sender.As<Image>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void StepText_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.WindowText;
			View.CurrentColor = ((sender.As<TextBlock>()).Foreground.As<SolidColorBrush>()).Color;
			View.OverlayLeft = SetupImage.ActualWidth + StepBorder.Padding.Left + (sender.As<TextBlock>()).Margin.Left;
			View.OverlayTop = PhoneyHeader.ActualHeight + StepBorder.Padding.Top + (sender.As<TextBlock>()).Margin.Top;
			View.OverlayWidth = (sender.As<TextBlock>()).ActualWidth;
			View.OverlayHeight = (sender.As<TextBlock>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void StepText1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.WindowText;
			View.CurrentColor = ((sender.As<TextBlock>()).Foreground.As<SolidColorBrush>()).Color;
			View.OverlayLeft = SetupImage.ActualWidth + StepBorder.Padding.Left + (sender.As<TextBlock>()).Margin.Left;
			View.OverlayTop = PhoneyHeader.ActualHeight + StepBorder.Padding.Top + PhoneyStepHeader.Margin.Top + PhoneyStepHeader.ActualHeight + PhoneyStepHeader.Margin.Bottom + (sender.As<TextBlock>()).Margin.Top;
			View.OverlayWidth = (sender.As<TextBlock>()).ActualWidth;
			View.OverlayHeight = (sender.As<TextBlock>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		private void StepText2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			View.SelectedItemType = InstallerUIView.SelectedItemTypes.WindowText;
			View.CurrentColor = ((sender.As<TextBlock>()).Foreground.As<SolidColorBrush>()).Color;
			View.OverlayLeft = SetupImage.ActualWidth + StepBorder.Padding.Left + (sender.As<TextBlock>()).Margin.Left;
			View.OverlayTop = PhoneyHeader.ActualHeight + StepBorder.Padding.Top + PhoneyStepHeader.Margin.Top + PhoneyStepHeader.ActualHeight + PhoneyStepHeader.Margin.Bottom + PhoneyStepText1.Margin.Top + PhoneyStepText1.ActualHeight + PhoneyStepText1.Margin.Bottom + (sender.As<TextBlock>()).Margin.Top;
			View.OverlayWidth = (sender.As<TextBlock>()).ActualWidth;
			View.OverlayHeight = (sender.As<TextBlock>()).ActualHeight;
			View.OverlayVisibility = Visibility.Visible;
			View.UpdateInterface();
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler SettingsChanged;
		#endregion Public Events

		#region Public Fields
		public static readonly DependencyProperty ManagerProperty = DependencyProperty.Register("Manager", typeof(IInstallerManager), typeof(InstallerUI), new PropertyMetadata(null, onManagerChanged));
		#endregion Public Fields

		#region Public Properties
		public IInstallerManager Manager
		{
			get { return (IInstallerManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public InstallerUIView View
		{
			get { return LayoutRoot.GetView<InstallerUIView>(); }
		}
		#endregion Public Properties
	}
}
