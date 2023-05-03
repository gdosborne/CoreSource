// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Defines interface that returns tag
//
namespace SNC.OptiRamp.ObjectInterfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Interface IWATag
	/// </summary>
	public interface IWATag
	{
		#region Public Methods
		/// <summary>
		/// To the tag.
		/// </summary>
		/// <returns>Tag.</returns>
		List<TagItem> ToTags();
		#endregion Public Methods
	}
}