// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Engineeringunit
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAEngineeringUnit.
	/// </summary>
	public sealed class WAEngineeringUnit : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAEngineeringUnit" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAEngineeringUnit(int id, string name, int sequence)
			: base(id, name, sequence)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WAEngineeringUnit" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		public WAEngineeringUnit(int id, string name, int sequence, string value, string format)
			: base(id, name, sequence)
		{
			Value = value;
			Format = format;
		}
		#endregion Public Constructors

		#region Private Fields
		/// <summary>
		/// Gets or sets the digits.
		/// </summary>
		/// <value>The digits.</value>
		private int _Digits;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets or sets the ceu.
		/// </summary>
		/// <value>The ceu.</value>
		public string CEU { get; set; }
		/// <summary>
		/// Gets or sets the digits.
		/// </summary>
		/// <value>The digits.</value>
		public int Digits
		{
			get { return _Digits; }
			set 
			{ 
				_Digits = value;
				Format = string.Format("0,.{0}", new string('0', _Digits));
			}
		}

		/// <summary>
		/// Gets or sets the format.
		/// </summary>
		/// <value>The format.</value>
		public string Format { get; set; }
		/// <summary>
		/// Gets or sets the offset.
		/// </summary>
		/// <value>The offset.</value>
		public double Offset { get; set; }
		/// <summary>
		/// Gets or sets the span.
		/// </summary>
		/// <value>The span.</value>
		public double Span { get; set; }
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; set; }
		#endregion Public Properties
	}
}