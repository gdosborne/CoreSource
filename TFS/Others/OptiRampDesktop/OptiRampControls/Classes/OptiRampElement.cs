using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using OptiRampControls.DesignerObjects;

namespace OptiRampControls.Classes
{
	public delegate void ElementSelectedHandler(object sender, ElementEventArgs e);

	public class ElementEventArgs : EventArgs
	{
		#region Public Constructors

		public ElementEventArgs(OptiRampElement element)
		{
			Element = element;
		}

		#endregion

		#region Public Properties
		public OptiRampElement Element { get; private set; }
		#endregion
	}

	public abstract class OptiRampElement
	{
		#region Public Constructors

		public OptiRampElement()
		{
			Id = Guid.NewGuid();
			Properties = new Classes.Properties();
			Properties.SetValue<Point>("Location", Defaults.Location);
			Properties.SetValue<Size>("Size", Defaults.Size);
			Properties.SetValue<Size>("MinimumSize", Defaults.Size);
			Properties.SetValue<bool>("HasDropShadow", Defaults.HasDropShadow);
			Properties.SetValue<double>("Rotation", Defaults.Rotation);
			Properties.ElementPropertyValueChanged += Properties_ValueChanged;
		}

		#endregion

		#region Public Events
		public event ElementPropertyValueChangedHandler ElementPropertyValueChanged;
		public event ElementSelectedHandler ElementSelected;
		#endregion

		#region Public Properties
		public FrameworkElement Element { get; set; }

		public bool HasDropShadow
		{
			get { return Properties.GetValue<bool>("HasDropShadow"); }
			set { Properties.SetValue<bool>("HasDropShadow", value); }
		}

		public Guid Id { get; set; }

		public abstract bool IsMovable { get; }

		public bool IsSelected { get; set; }

		public Point Location
		{
			get { return Properties.GetValue<Point>("Location"); }
			set { Properties.SetValue<Point>("Location", value); }
		}

		public Size MinimiumSize
		{
			get { return Properties.GetValue<Size>("MinimiumSize"); }
			set { Properties.SetValue<Size>("MinimiumSize", value); }
		}

		public abstract ImageSource ObjectImageSource { get; }

		public Classes.Properties Properties { get; private set; }

		public double Rotation
		{
			get { return Properties.GetValue<double>("Rotation"); }
			set { Properties.SetValue<double>("Rotation", value); }
		}

		public Size Size
		{
			get { return Properties.GetValue<Size>("Size"); }
			set { Properties.SetValue<Size>("Size", value); }
		}

		public abstract string TypeName { get; }
		#endregion

		#region Private Methods

		private void Properties_ValueChanged(object sender, ElementPropertyValueChangedEventArgs e)
		{
			if (ElementPropertyValueChanged != null)
				ElementPropertyValueChanged(this, e);
		}

		#endregion
	}
}