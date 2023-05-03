using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace OptiRampDesktop.Helpers
{
	public static class DispatchService
	{
		#region Public Methods

		public static void Invoke(Action action)
		{
			Dispatcher dispatchObject = System.Windows.Application.Current.Dispatcher;
			if (dispatchObject == null || dispatchObject.CheckAccess())
				action();
			else
				dispatchObject.Invoke(action);
		}

		public static void Invoke(TimerCallback callback, object[] parms)
		{
			Dispatcher dispatchObject = System.Windows.Application.Current.Dispatcher;
			dispatchObject.BeginInvoke(callback, parms);
		}

		#endregion
	}
}