// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Revision
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WARevision.
	/// </summary>
	public sealed class WARevision : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WARevision" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WARevision(int id, string name, int sequence)
			: base(id, name, sequence)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WARevision" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="major">The major.</param>
		/// <param name="minor">The minor.</param>
		/// <param name="date">The date.</param>
		/// <param name="user">The user.</param>
		/// <param name="description">The description.</param>
		public WARevision(int id, string name, int sequence, DateTime date, string user, string description)
			: base(id, name, sequence)
		{
			Date = date;
			User = user;
			Description = description;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		public string User { get; set; }
		#endregion Public Properties
	}
}