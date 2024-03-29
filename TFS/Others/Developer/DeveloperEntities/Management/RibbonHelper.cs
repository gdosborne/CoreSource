// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// Ribbon Helper
//
namespace SNC.OptiRamp.Application.DeveloperEntities.Management {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Controls.Ribbon;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;

	public static class RibbonHelper {

		#region Public Methods
		public static RibbonTextBox GetTextBox(double width, string value) {
			return new RibbonTextBox {
				Width = width,
				Text = value
			};
		}
		public static RibbonButton GetButton(Type type, ICommand command, string header, string tooltip, string imageFile) {
			return new RibbonButton {
				Command = command,
				Label = header,
				LargeImageSource = GetImageSource(type, 32, imageFile),
				SmallImageSource = GetImageSource(type, 24, imageFile),
				ToolTipImageSource = GetImageSource(type, 128, imageFile),
				ToolTipDescription = tooltip,
				ToolTipTitle = header
			};
		}
		public static RibbonMenuButton GetRibbonMenuButton(Type type, string header, string tooltip, string imageFile) {
			return new RibbonMenuButton {
				Label = header,
				LargeImageSource = GetImageSource(type, 32, imageFile),
				SmallImageSource = GetImageSource(type, 24, imageFile),
				ToolTipImageSource = GetImageSource(type, 128, imageFile),
				ToolTipDescription = tooltip,
				ToolTipTitle = header
			};
		}
		public static RibbonMenuItem GetButtonMenuItem(Type type, ICommand command, string header, string tooltip, string imageFile) {
			return new RibbonMenuItem {
				Command = command,
				Header = header,
				ImageSource = GetImageSource(type, 24, imageFile),
				ToolTipImageSource = GetImageSource(type, 128, imageFile),
				ToolTipDescription = tooltip,
				ToolTipTitle = header
			};
		}
		public static ImageSource GetImageSource(Type type, int size, string file) {
			return BitmapFrame.Create(GetImageUri(type, size, file));
		}
		public static Uri GetImageUri(Type type, int size, string file) {
			return new Uri(string.Format("pack://application:,,,/{0};component/Images/x{1}/{2}", type.Assembly.GetName().Name, size, file, UriKind.RelativeOrAbsolute));
		}
		#endregion Public Methods
	}
}
