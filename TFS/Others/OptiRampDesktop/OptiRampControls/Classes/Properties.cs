using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyApplication.Primitives;

namespace OptiRampControls.Classes
{
	public delegate void ElementPropertyValueChangedHandler(object sender, ElementPropertyValueChangedEventArgs e);

	public class ElementPropertyValueChangedEventArgs : EventArgs
	{
		#region Public Constructors

		public ElementPropertyValueChangedEventArgs(string name, object newValue)
		{
			Name = name;
			NewValue = newValue;
		}

		#endregion

		#region Public Properties
		public string Name { get; private set; }
		public object NewValue { get; private set; }
		#endregion
	}

	public class Properties : IList<OptiRampProperty>
	{
		#region Private Fields
		private List<OptiRampProperty> _Internal = new List<OptiRampProperty>();
		private object _Locker = new object();
		#endregion

		#region Public Events
		public event ElementPropertyValueChangedHandler ElementPropertyValueChanged;
		#endregion

		#region Public Properties
		public int Count
		{
			get { return _Internal.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}
		#endregion

		#region Public Indexers
		public OptiRampProperty this[int index] { get { return _Internal[index]; } set { _Internal[index] = value; } }
		#endregion

		#region Public Methods

		public void Add(OptiRampProperty item)
		{
			item.PropertyChanged += item_PropertyChanged;
			_Internal.Add(item);
		}

		public void Clear()
		{
			_Internal.ForEach(x => x.PropertyChanged -= item_PropertyChanged);
			_Internal.Clear();
		}

		public bool Contains(OptiRampProperty item)
		{
			return _Internal.Contains(item);
		}

		public void CopyTo(OptiRampProperty[] array, int arrayIndex)
		{
			_Internal.CopyTo(array, arrayIndex);
		}

		public IEnumerator<OptiRampProperty> GetEnumerator()
		{
			return _Internal.GetEnumerator();
		}

		public T GetValue<T>(string name, T defaultValue)
		{
			if (_Internal.Any(x => x.Name.Equals(name)))
				return (T)(_Internal.First(x => x.Name.Equals(name)).Value == null ? defaultValue : _Internal.First(x => x.Name.Equals(name)).Value);
			return defaultValue;
		}

		public T GetValue<T>(string name)
		{
			if (_Internal.Any(x => x.Name.Equals(name)))
				return (T)_Internal.First(x => x.Name.Equals(name)).Value;
			return default(T);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _Internal.GetEnumerator();
		}

		public int IndexOf(OptiRampProperty item)
		{
			return _Internal.IndexOf(item);
		}

		public void Insert(int index, OptiRampProperty item)
		{
			item.PropertyChanged += item_PropertyChanged;
			_Internal.Insert(index, item);
		}

		public bool Remove(OptiRampProperty item)
		{
			item.PropertyChanged -= item_PropertyChanged;
			return _Internal.Remove(item);
		}

		public void RemoveAt(int index)
		{
			_Internal[index].PropertyChanged -= item_PropertyChanged;
			_Internal.RemoveAt(index);
		}

		public void SetValue<T>(string name, T value)
		{
			lock (_Locker)
			{
				if (_Internal.Any(x => x.Name.Equals(name)))
					_Internal.First(x => x.Name.Equals(name)).Value = value;
				else
				{
					var item = new OptiRampProperty { Name = name, Value = value, IsChangable = true };
					item.PropertyChanged += item_PropertyChanged;
					_Internal.Add(item);
				}
			}
		}

		#endregion

		#region Private Methods

		private void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var property = sender.As<OptiRampProperty>();
			RaiseValueChanged(property);
		}

		private void RaiseValueChanged(OptiRampProperty property)
		{
			if (ElementPropertyValueChanged != null)
				ElementPropertyValueChanged(this, new ElementPropertyValueChangedEventArgs(property.Name, property.Value));
		}

		#endregion
	}
}