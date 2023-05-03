using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace OptiRampControls.Classes
{
	public interface IConnectable
	{
		#region Public Properties
		Point Element1ConnectionPoint { get; set; }
		Point Element2ConnectionPoint { get; set; }
		Brush Stroke { get; set; }
		double StrokeThickness { get; set; }
		#endregion
	}
}