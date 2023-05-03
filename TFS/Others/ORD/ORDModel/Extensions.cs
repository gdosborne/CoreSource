// -----------------------------------------------------------------------
// Copyright ©  2016
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
//
//
namespace ORDModel
{
	using SNC.OptiRamp.Services.fDefs;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class Extensions.
	/// </summary>
	public static class Extensions
	{
		#region Public Methods
		/// <summary>
		/// Gets the name of the property by.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="name">The name.</param>
		/// <returns>IProperty.</returns>
		public static IProperty GetPropertyByName(this IElement element, string name) {
			IProperty result = null;
			if (element == null)
				return null;
			//I don't use the key sent because I want to make the
			// key case ignorable
			element.Properties.Keys.ToList().ForEach(key =>
			{
				if (name.Equals(key, StringComparison.OrdinalIgnoreCase)) {
					result = element.Properties[key];
					return;
				}
			});
			return result;
		}
		/// <summary>
		/// Gets the property value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="element">The element.</param>
		/// <param name="name">The name.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>T.</returns>
		public static T GetPropertyValue<T>(this IElement element, string name, T defaultValue) {
			IProperty prop = element.GetPropertyByName(name);
			return element.GetPropertyValue<T>(prop, defaultValue);
		}
		/// <summary>
		/// Gets the property value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="element">The element.</param>
		/// <param name="prop">The property.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns>T.</returns>
		public static T GetPropertyValue<T>(this IElement element, IProperty prop, T defaultValue) {
			if (prop == null || prop.rowValue == null)
				return (T)(object)defaultValue;
			if (prop.rowValue.GetType() == typeof(string))
				return (T)(object)Convert.ChangeType(prop.rowValue, typeof(T));
			return (T)prop.rowValue;
		}
		/// <summary>
		/// Determines whether the specified name has property.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="name">The name.</param>
		/// <returns><c>true</c> if the specified name has property; otherwise, <c>false</c>.</returns>
		public static bool HasProperty(this IElement element, string name) {
			return element.GetPropertyByName(name) != null;
		}
		#endregion Public Methods
	}
}