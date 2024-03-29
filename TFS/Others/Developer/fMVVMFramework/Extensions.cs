// -----------------------------------------------------------------------
// Copyright© 2016 Statistics & Controls, Inc.
// Created by: Greg Osborne
// Created: 
// Updated: 06/08/2016 16:19:58
// Updated by: Greg Osborne
// -----------------------------------------------------------------------
// 
// Extensions.cs
//
namespace MVVMFramework
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Windows;

	public static class Extensions
	{
		#region Public Methods
		public static T GetView<T>(this FrameworkElement root)
		{
			if (DesignerProperties.GetIsInDesignMode(root))
				return default(T);
			return (T)root.DataContext;
		}
		#endregion Public Methods
	}
}
