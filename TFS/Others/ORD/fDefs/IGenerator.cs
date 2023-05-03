// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
// 
// IGenerator Interface
//
namespace SNC.OptiRamp.Services.fGenerator
{
	using SNC.OptiRamp.Services.Events;
	using SNC.OptiRamp.Services.fCommunication;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Interface IGenerator
	/// </summary>
	public interface IGenerator
	{
		#region Public Methods
		/// <summary>
		/// Starts this instance.
		/// </summary>
		void Start();
		/// <summary>
		/// Stops this instance.
		/// </summary>
		void Stop();
		#endregion Public Methods

		#region Public Events
		/// <summary>
		/// Occurs when [generation complete].
		/// </summary>
		event ActionCompleteHandler GenerationComplete;
		#endregion Public Events

		#region Public Properties
		/// <summary>
		/// Gets the critacal errors.
		/// </summary>
		/// <value>The critacal error.</value>
		CriticalErrors CriticalErrors { get; }
		/// <summary>
		/// Gets the non critical errors.
		/// </summary>
		/// <value>The non critical errors.</value>
		NonCriticalErrors NonCriticalErrors { get; }
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		string FileName { get; set; }
		/// <summary>
		/// Gets or sets the communicator.
		/// </summary>
		/// <value>The communicator.</value>
		ICommunicator Communicator { get; set; }
		/// <summary>
		/// Gets or sets the elapse time.
		/// </summary>
		/// <value>The elapse time.</value>
		TimeSpan ElapseTime { get; set; }
		/// <summary>
		/// Gets or sets the exception persons.
		/// </summary>
		/// <value>The exception persons.</value>
		IPerson[] ExceptionPersons { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether [send e mail on exception].
		/// </summary>
		/// <value><c>true</c> if [send e mail on exception]; otherwise, <c>false</c>.</value>
		bool SendEMailOnException { get; set; }
		/// <summary>
		/// Gets or sets the source folder.
		/// </summary>
		/// <value>The source folder.</value>
		string SourceFolder { get; set; }
		/// <summary>
		/// Gets or sets the name of the template.
		/// </summary>
		/// <value>The name of the template.</value>
		string TemplateName { get; set; }
		/// <summary>
		/// Gets or sets the web root folder.
		/// </summary>
		/// <value>The web root folder.</value>
		string WebRootFolder { get; set; }
		/// <summary>
		/// Gets or sets the web root URI.
		/// </summary>
		/// <value>The web root URI.</value>
		string WebRootUrl { get; set; }
		/// <summary>
		/// Gets the error.
		/// </summary>
		/// <value>The error.</value>
		string Error { get; }
		int CurrentId {
			get;
			set;
		}
		#endregion Public Properties
	}
}