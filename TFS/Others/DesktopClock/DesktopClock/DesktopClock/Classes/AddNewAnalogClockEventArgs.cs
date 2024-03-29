// ***********************************************************************
// Assembly         : DesktopClock
// Author           : Greg
// Created          : 09-11-2015
//
// Last Modified By : Greg
// Last Modified On : 09-11-2015
// ***********************************************************************
// <copyright file="AddNewAnalogClockEventArgs.cs" company="OSoft">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using OSControls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopClock.Classes
{
	public delegate void AddNewAnalogClockHandler(object sender, AddNewAnalogClockEventArgs e);

	public class AddNewAnalogClockEventArgs : EventArgs
	{
		#region Public Constructors

		public AddNewAnalogClockEventArgs(AnalogClock clock)
		{
			Clock = clock;
		}

		#endregion Public Constructors

		#region Public Properties

		public AnalogClock Clock { get; private set; }

		#endregion Public Properties
	}
}
