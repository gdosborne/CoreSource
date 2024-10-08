// ***********************************************************************
// Assembly         : User Manager
// Author           : Greg
// Created          : 06-19-2015
//
// Last Modified By : Greg
// Last Modified On : 06-19-2015
// ***********************************************************************
// <copyright file="ClipboardElementsEventArgs.cs" company="Statistics and Controls, Inc.">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace User_Manager.Classes.Events
{
	public delegate void ClipboardElementHandler(object sender, ClipboardElementsEventArgs e);

	public class ClipboardElementsEventArgs : EventArgs
	{
		#region Public Constructors

		public ClipboardElementsEventArgs(XElement parent)
		{
			Parent = parent;
		}

		#endregion

		#region Public Properties
		public XElement Parent { get; private set; }
		#endregion
	}
}
