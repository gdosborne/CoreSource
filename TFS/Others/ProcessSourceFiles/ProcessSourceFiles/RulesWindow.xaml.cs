using MVVMFramework;
using GregOsborne.Application.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace ProcessSourceFiles
{
	public partial class RulesWindow : Window
	{
		public RulesWindow()
		{
			InitializeComponent();
		}
		protected override void OnSourceInitialized(EventArgs e)
		{
			this.HideMinimizeAndMaximizeButtons();
			this.HideControlBox();
			base.OnSourceInitialized(e);
		}
		public RulesWindowView View
		{
			get { return LayoutRoot.GetView<RulesWindowView>(); }
		}
	}
}
