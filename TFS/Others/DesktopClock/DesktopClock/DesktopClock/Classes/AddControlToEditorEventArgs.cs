// ***********************************************************************
// Assembly         : DesktopClock
// Author           : Greg
// Created          : 09-09-2015
//
// Last Modified By : Greg
// Last Modified On : 09-09-2015
// ***********************************************************************
// <copyright file="AddControlToEditorEventArgs.cs" company="OSoft">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DesktopClock.Classes
{
	public delegate void AddControlToEditorHandler(object sender, AddControlToEditorEventArgs e);

	public class AddControlToEditorEventArgs : EventArgs
	{
		#region Public Constructors

		public AddControlToEditorEventArgs(FrameworkElement element)
		{
			Element = element;
		}

		#endregion Public Constructors

		#region Public Properties

		public FrameworkElement Element { get; private set; }

		#endregion Public Properties
	}
}
