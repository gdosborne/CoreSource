// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// NonCriticalErrors
//
namespace SNC.OptiRamp.Services.fGenerator {
	using System;
	using System.Collections;
	using System.Collections.Generic;

	using System.Linq;
	/// <summary>
	/// Class CritalErrors.
	/// </summary>
	public class CriticalErrors : NonCriticalErrors {

	}
	/// <summary>
	/// Class NonCriticalErrors.
	/// </summary>
	public class NonCriticalErrors : IList<string> {
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NonCriticalErrors"/> class.
		/// </summary>
		/// <param name="separator">The separator.</param>
		/// <param name="value">The value.</param>
		public NonCriticalErrors(char separator, string value) {
			_Internal = new List<string>();
			_Internal.AddRange(value.Split(separator));
			_IsReady = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NonCriticalErrors"/> class.
		/// </summary>
		/// <param name="initial">The initial.</param>
		public NonCriticalErrors(string[] initial) {
			_Internal = new List<string>();
			_Internal.AddRange(initial);
			_IsReady = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NonCriticalErrors"/> class.
		/// </summary>
		/// <param name="initial">The initial.</param>
		public NonCriticalErrors(IList<string> initial) {
			_Internal = new List<string>();
			_Internal.AddRange(initial);
			_IsReady = true;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="NonCriticalErrors"/> class.
		/// </summary>
		public NonCriticalErrors() {
			_Internal = new List<string>();
			_IsReady = true;
		}
		#endregion Public Constructors

		#region Public Methods
		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		public void Add(string item) {
			_Internal.Add(item);
		}
		/// <summary>
		/// Adds the specified format.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The arguments.</param>
		public void Add(string format, params object[] args) {
			_Internal.Add(string.Format(format, args));
		}
		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <param name="value">The value.</param>
		public void AddRange(IList<string> value) {
			_Internal.AddRange(value);
		}
		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <param name="value">The value.</param>
		public void AddRange(string[] value) {
			_Internal.AddRange(value);
		}
		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <param name="value">The value.</param>
		public void AddRange(NonCriticalErrors value) {
			_Internal.AddRange(value);
		}
		/// <summary>
		/// Adds the range.
		/// </summary>
		/// <param name="separator">The separator.</param>
		/// <param name="value">The value.</param>
		public void AddRange(char separator, string value) {
			_Internal.AddRange(value.Split(separator));
		}
		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public void Clear() {
			_Internal.Clear();
		}
		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
		public bool Contains(string item) {
			return _Internal.Contains(item);
		}
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		public void CopyTo(string[] array, int arrayIndex) {
			_Internal.CopyTo(array, arrayIndex);
		}
		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		public IEnumerator<string> GetEnumerator() {
			return _Internal.GetEnumerator();
		}
		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator() {
			return _Internal.GetEnumerator();
		}
		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <returns>The index of <paramref name="item" /> if found in the list; otherwise, -1.</returns>
		public int IndexOf(string item) {
			return _Internal.IndexOf(item);
		}
		/// <summary>
		/// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		public void Insert(int index, string item) {
			_Internal.Insert(index, item);
		}
		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
		public bool Remove(string item) {
			return _Internal.Remove(item);
		}
		/// <summary>
		/// Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		public void RemoveAt(int index) {
			_Internal.RemoveAt(index);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString() {
			return ToString(Environment.NewLine);
		}
		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <param name="separator">The separator.</param>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public string ToString(string separator) {
			return string.Join(separator, _Internal);
		}
		#endregion Public Methods

		#region Private Fields
		private List<string> _Internal = null;
		private bool _IsReady = false;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get {
				return _Internal.Count;
			}
		}
		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly {
			get {
				return _IsReady;
			}
		}
		#endregion Public Properties

		#region Public Indexers
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>System.String.</returns>
		public string this[int index] {
			get {
				return _Internal[index];
			}
			set {
				_Internal[index] = value;
			}
		}
		#endregion Public Indexers
	}
}