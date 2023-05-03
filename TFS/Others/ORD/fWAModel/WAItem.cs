// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
//
// [your comment here]
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAItem.
	/// </summary>
	public abstract class WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAItem" /> class.
		/// </summary>
		public WAItem() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="WAItem" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAItem(int id, string name, int sequence)
		{
			Id = id;
			Name = name;
			Sequence = sequence;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the element identifier.
		/// </summary>
		/// <value>The element identifier.</value>
		public int ElementId { get; set; }
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the sequence.
		/// </summary>
		/// <value>The sequence.</value>
		public int Sequence { get; set; }
		#endregion Public Properties
	}
}