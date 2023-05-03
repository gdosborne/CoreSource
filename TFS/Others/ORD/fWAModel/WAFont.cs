// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Font
//
namespace SNC.OptiRamp
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class WAFont.
	/// </summary>
	public sealed class WAFont : WAItem
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="WAFont" /> class.
		/// </summary>
		public WAFont() { }
		/// <summary>
		/// Initializes a new instance of the <see cref="WAFont" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		public WAFont(int id, string name, int sequence)
			: base(id, name, sequence)
		{
			FontFamilies = new List<string>();
			FontStyle = Enumerations.FontStyles.Normal;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WAFont" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="sequence">The sequence.</param>
		/// <param name="fontFamilies">The font family.</param>
		/// <param name="fontStyle">The font style.</param>
		/// <param name="fontSize">Size of the font.</param>
		/// <param name="unit">The unit.</param>
		public WAFont(int id, string name, int sequence, List<string> fontFamilies, SNC.OptiRamp.Enumerations.FontStyles fontStyle, double fontSize, SNC.OptiRamp.Enumerations.MeasureUnits unit)
			: base(id, name, sequence)
		{
			FontFamilies = fontFamilies;
			FontStyle = fontStyle;
			FontSize = fontSize;
			Unit = unit;
		}
		#endregion Public Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the font families.
		/// </summary>
		/// <value>The font families.</value>
		public List<string> FontFamilies { get; set; }
		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public double FontSize { get; set; }
		/// <summary>
		/// Gets or sets the font style.
		/// </summary>
		/// <value>The font style.</value>
		public SNC.OptiRamp.Enumerations.FontStyles FontStyle { get; set; }
		/// <summary>
		/// Gets or sets the unit.
		/// </summary>
		/// <value>The unit.</value>
		public SNC.OptiRamp.Enumerations.MeasureUnits Unit { get; set; }
		#endregion Public Properties
	}
}