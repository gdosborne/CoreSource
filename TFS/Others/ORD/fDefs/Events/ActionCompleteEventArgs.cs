//-------------------------------------------------------------------
// (©) 2015 Statistics & Control Inc. All rights reserved.
// Created by:	Greg Osborne
//-------------------------------------------------------------------
//
// Collection of events that can be used by custom or framework components
//
namespace SNC.OptiRamp.Services.Events
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Delegate GenerationCompleteHandler
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="ActionCompleteEventArgs" /> instance containing the event data.</param>
	public delegate void ActionCompleteHandler(object sender, ActionCompleteEventArgs e);

	/// <summary>
	/// Class GenerationCompleteEventArgs.
	/// </summary>
	public class ActionCompleteEventArgs : EventArgs
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCompleteEventArgs" /> class.
		/// </summary>
		/// <param name="elapsedTime">The elapsed time.</param>
		/// <param name="success">if set to <c>true</c> [success].</param>
		public ActionCompleteEventArgs(TimeSpan elapsedTime, bool success)
		{
			ElapsedTime = elapsedTime;
			Success = success;
		}
		public ActionCompleteEventArgs(TimeSpan elapsedTime, string errorText)
		{
			ElapsedTime = elapsedTime;
			Success = false;
			ErrorText = errorText;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets the elapsed time.
		/// </summary>
		/// <value>The elapsed time.</value>
		public TimeSpan ElapsedTime { get; private set; }
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionCompleteEventArgs" /> is success.
		/// </summary>
		/// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
		public bool Success { get; private set; }
		/// <summary>
		/// Gets or sets the error text.
		/// </summary>
		/// <value>The error text.</value>
		public string ErrorText { get; set; }
		#endregion Public Properties
	}
}