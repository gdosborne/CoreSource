// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Enumerations
//
namespace fDefs.ProjectService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class Enumerations.
	/// </summary>
	public static class Enumerations
	{
		#region Public Enums
		/// <summary>
		/// Enum ProjectTypes
		/// </summary>
		public enum ProjectTypes
		{
			/// <summary>
			/// The runtime
			/// </summary>
			Runtime,
			/// <summary>
			/// The VTS
			/// </summary>
			Vts
		}
		/// <summary>
		/// Enum Statuses
		/// </summary>
		public enum Statuses
		{
			/// <summary>
			/// The none
			/// </summary>
			None,
			/// <summary>
			/// The unknown
			/// </summary>
			Unknown,
			/// <summary>
			/// The in progress
			/// </summary>
			InProgress,
			/// <summary>
			/// The completed
			/// </summary>
			Completed,
			/// <summary>
			/// The error
			/// </summary>
			Error,
			/// <summary>
			/// The queued
			/// </summary>
			Queued
		}
		#endregion Public Enums
	}
}