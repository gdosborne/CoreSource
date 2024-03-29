using Microsoft.Win32;
// ***********************************************************************
// Assembly         : DesktopClock
// Author           : Greg
// Created          : 08-26-2015
//
// Last Modified By : Greg
// Last Modified On : 08-26-2015
// ***********************************************************************
// <copyright file="Extensions.cs" company="OSoft">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopClock.Classes
{
	public static class Extensions
	{
		public static ImageSource GetImageSource(this Uri uri)
		{
			return new BitmapImage(uri);
		}
		public static ImageSource GetImageSource(this string uriString)
		{
			return new Uri(uriString, UriKind.Absolute).GetImageSource();
		}
		public static string GetImagePack(this string fileName)
		{
			return @"pack://application:,,,/DesktopClock;component/Images/" + fileName;
		}		
		public static T Choose<T>(this bool compareResult, T choice1, T choice2)
		{
			return compareResult ? choice1 : choice2;
		}

		public static Binding GetBinding(this string propertyName, object source)
		{
			return new Binding(propertyName)
			{
				Source = source,
				Path = new PropertyPath(propertyName),
				Mode = propertyName.Equals("SelectedTimeZone") ? BindingMode.OneWay : BindingMode.TwoWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

	}
}
