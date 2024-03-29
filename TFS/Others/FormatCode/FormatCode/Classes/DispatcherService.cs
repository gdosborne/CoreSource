// ***********************************************************************
// Assembly         : FormatCode
// Author           : Greg
// Created          : 05-19-2015
//
// Last Modified By : Greg
// Last Modified On : 05-19-2015
// ***********************************************************************
// <copyright file="DispatcherService.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
namespace FormatCode.Classes
{
	public static class DispatchService
	{
		public static void Invoke(Action action)
		{
			Dispatcher dispatchObject = System.Windows.Application.Current.Dispatcher;
			if (dispatchObject == null || dispatchObject.CheckAccess())
				action();
			else
				dispatchObject.Invoke(action);
		}
	}
}
