﻿namespace ORDModel
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Controls;

	public interface IPropertyItem
	{
		UserControl GetPropertiesControl();
	}
}