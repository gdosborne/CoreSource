// ***********************************************************************
// Assembly         : MVVMFramework
// Author           : Greg
// Created          : 05-28-2015
//
// Last Modified By : Greg
// Last Modified On : 07-16-2015
// ***********************************************************************
// <copyright file="Extensions.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace MVVMFramework
{
	public static class Extensions
	{
		#region Public Methods

		public static T GetView<T>(this FrameworkElement root)
		{
			if (DesignerProperties.GetIsInDesignMode(root))
				return default(T);
			return (T)root.DataContext;
		}

		#endregion
	}
}
