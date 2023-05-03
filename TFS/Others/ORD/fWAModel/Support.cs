// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Support classes and defaults
//
namespace SNC.OptiRamp
{
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Media;

	/// <summary>
	/// Class PropertyGroups.
	/// </summary>
	public static class PropertyGroups
	{
		/// <summary>
		/// The background
		/// </summary>
		public static List<string> Background = new List<string>
		{
			TypeIDs.FILLBRUSH,
			TypeIDs.BACKGROUND,
			TypeIDs.BACKGROUNDBRUSH,
			TypeIDs.BACKGROUNDCOLOR,
			TypeIDs.BRUSH,
		};
		/// <summary>
		/// The border
		/// </summary>
		public static List<string> Border = new List<string>
		{
			TypeIDs.BORDER,
			TypeIDs.BORDERBRUSH
		};
		/// <summary>
		/// The foreground
		/// </summary>
		public static List<string> Foreground = new List<string>
		{
			TypeIDs.FOREGROUND,
			TypeIDs.FOREGROUNDBRUSH
		};
		/// <summary>
		/// The border size
		/// </summary>
		public static List<string> BorderSize = new List<string>
		{
			TypeIDs.BORDER,
			TypeIDs.BORDERSIZE,
			TypeIDs.BORDERTHICKNESS,
			TypeIDs.THICKNESS
		};
	}
	/// <summary>
	/// Class Defaults.
	/// </summary>
	public static class Defaults
	{
		#region Public Classes

		/// <summary>
		/// Class ObjectDefaults.
		/// </summary>
		public static class ObjectDefaults
		{
			#region Public Fields
			/// <summary>
			/// The background
			/// </summary>
			public static WABrush Background = PageDefaults.Background;
			/// <summary>
			/// The foreground
			/// </summary>
			public static WABrush Foreground = PageDefaults.Foreground;
			/// <summary>
			/// The border
			/// </summary>
			public static int Border = 0;
			/// <summary>
			/// The border brush
			/// </summary>
			public static WABrush BorderBrush = PageDefaults.Foreground;
			/// <summary>
			/// The corner radius
			/// </summary>
			public static int CornerRadius = 0;
			#endregion Public Fields
		}

		/// <summary>
		/// Class PageDefaults.
		/// </summary>
		public static class PageDefaults
		{
			#region Public Fields
			/// <summary>
			/// The background
			/// </summary>
			public static WABrush Background = new WABrush("Transparent", Colors.Transparent);
			/// <summary>
			/// The foreground
			/// </summary>
			public static WABrush Foreground = new WABrush("Black", Colors.Black);
			#endregion Public Fields
		}

		/// <summary>
		/// Class TextDefaults.
		/// </summary>
		public static class TextDefaults
		{
			#region Public Fields
			/// <summary>
			/// The alignment
			/// </summary>
			public static Enumerations.DynamicAlignments Alignment = Enumerations.DynamicAlignments.Left;
			/// <summary>
			/// The foreground
			/// </summary>
			public static WABrush Foreground = PageDefaults.Foreground;
			/// <summary>
			/// The show engineering unit when value is na n
			/// </summary>
			public static bool ShowEngineeringUnitWhenValueIsNaN = false;
			/// <summary>
			/// The text
			/// </summary>
			public static string Text = string.Empty;
			#endregion Public Fields
		}

        /// <summary>
        /// Class FontDefaults.
        /// </summary>
        public static class FontDefaults
        {
            #region Public Fields
            public static WAFont MSTahoma = new WAFont(0, "Tahoma", 0, new List<string>{"Tahoma"}, SNC.OptiRamp.Enumerations.FontStyles.Normal, 8, SNC.OptiRamp.Enumerations.MeasureUnits.Pt);
            #endregion Public Fields
        }

        /// <summary>
        /// Class BrushDefaults.
        /// </summary>
        public static class BrushDefaults
        {
            #region Public Fields
            public static WABrush Black = new WABrush(0, "black", 0, Colors.Black);
            public static WABrush White = new WABrush(0, "white", 0, Colors.White);
            #endregion Public Fields
        }

		#endregion Public Classes
	}
}