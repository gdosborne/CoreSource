// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Brush
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	/// <summary>
	/// Enum BrushStyles
	/// </summary>
	public enum BrushStyles
	{
		/// <summary>
		/// The solid
		/// </summary>
		Solid,
		/// <summary>
		/// The gradient
		/// </summary>
		Gradient
	}

	/// <summary>
	/// Class WABrush.
	/// </summary>
	public sealed class WABrush : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WABrush" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WABrush(int id, string name, int sequence)
			: base(id, name, sequence)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WABrush" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="color">The color.</param>
		public WABrush(int id, string name, int sequence, Color color)
			: base(id, name, sequence)
		{
			BrushStyle = BrushStyles.Solid;
			Color1 = color;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WABrush" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="color1">The color1.</param>
		/// <param name="color2">The color2.</param>
		/// <param name="angle">The angle.</param>
		public WABrush(int id, string name, int sequence, Color color1, Color color2, double angle)
			: base(id, name, sequence)
		{
			BrushStyle = BrushStyles.Gradient;
			Color1 = color1;
			Color2 = color2;
			Angle = angle;
		}
		#endregion Public Constructors

		#region Internal Constructors
		internal WABrush(string name, Color color)
		{
			Name = name;
			Color1 = color;
		}
		#endregion Internal Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the angle.
		/// </summary>
		/// <value>The angle.</value>
		public double Angle { get; set; }
		/// <summary>
		/// Gets or sets the brush style.
		/// </summary>
		/// <value>The brush style.</value>
		public BrushStyles BrushStyle { get; set; }
		/// <summary>
		/// Gets or sets the color1.
		/// </summary>
		/// <value>The color1.</value>
		public Color Color1 { get; set; }
		/// <summary>
		/// Gets or sets the color2.
		/// </summary>
		/// <value>The color2.</value>
		public Color Color2 { get; set; }
		#endregion Public Properties
	}
}