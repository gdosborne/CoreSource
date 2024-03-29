// ***********************************************************************
// Assembly         : DesktopClock
// Author           : Greg
// Created          : 09-09-2015
//
// Last Modified By : Greg
// Last Modified On : 09-09-2015
// ***********************************************************************
// <copyright file="AddTopLevelSettingEventArgs.cs" company="OSoft">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DesktopClock.Classes
{
	public delegate void AddTopLevelSettingHandler(object sender, AddTopLevelSettingEventArgs e);

	public class AddTopLevelSettingEventArgs : EventArgs
	{
		#region Public Constructors

		public AddTopLevelSettingEventArgs(TreeViewItem item)
		{
			Item = item;
		}

		#endregion Public Constructors

		#region Public Properties

		public TreeViewItem Item { get; private set; }

		#endregion Public Properties
	}
}
