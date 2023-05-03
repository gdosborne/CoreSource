// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Math extensions
//
namespace SNC.OptiRamp.Application.Extensions.Primitives
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class Math.
	/// </summary>
	public static class Math
	{
		#region Public Methods
		/// <summary>
		/// Maximums the of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="values">The values.</param>
		/// <returns>T.</returns>
		public static T MaxOf<T>(T[] values)
		{
			return values.ToList().Max();
		}
		/// <summary>
		/// Maximums the of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value1">The value1.</param>
		/// <param name="value2">The value2.</param>
		/// <returns>T.</returns>
		public static T MaxOf<T>(T value1, T value2)
		{
			return MaxOf<T>(new T[] { value1, value2 });
		}
		#endregion Public Methods
	}
}