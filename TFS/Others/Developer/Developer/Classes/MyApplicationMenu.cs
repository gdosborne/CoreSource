namespace SNC.OptiRamp.Application.Developer.Classes {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls.Primitives;
	using System.Windows.Controls.Ribbon;
	using System.Windows.Data;

	public class MyApplicationMenu : RibbonApplicationMenu {
		public bool ShowAuxilaryPanel {
			get {
				return (bool)GetValue(ShowAuxilaryPanelProperty);
			}
			set {
				SetValue(ShowAuxilaryPanelProperty, value);
			}
		}

		public static readonly DependencyProperty ShowAuxilaryPanelProperty = DependencyProperty.Register("ShowAuxilaryPanel", typeof(bool), typeof(MyApplicationMenu), new UIPropertyMetadata(true));

		#region MenuWidth
		public double MenuWidth {
			get {
				return (double)GetValue(MenuWidthProperty);
			}
			set {
				SetValue(MenuWidthProperty, value);
			}
		}

		public static readonly DependencyProperty MenuWidthProperty = DependencyProperty.Register("MenuWidth", typeof(double), typeof(MyApplicationMenu), new PropertyMetadata(100.0, onMenuWidthChanged));
		private static void onMenuWidthChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (MyApplicationMenu)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			//implementation code goes here
		}
		#endregion

		#region MenuHeight
		public double MenuHeight {
			get {
				return (double)GetValue(MenuHeightProperty);
			}
			set {
				SetValue(MenuHeightProperty, value);
			}
		}

		public static readonly DependencyProperty MenuHeightProperty = DependencyProperty.Register("MenuHeight", typeof(double), typeof(MyApplicationMenu), new PropertyMetadata(100.0, onMenuHeightChanged));
		private static void onMenuHeightChanged(DependencyObject source, DependencyPropertyChangedEventArgs e) {
			var src = (MyApplicationMenu)source;
			if (src == null)
				return;
			var value = (double)e.NewValue;
			//implementation code goes here
		}
		#endregion

		public override void OnApplyTemplate() {
			base.OnApplyTemplate();
			this.DropDownOpened += SlimRibbonApplicationMenu_DropDownOpened;
		}

		void SlimRibbonApplicationMenu_DropDownOpened(object sender, EventArgs e) {
			DependencyObject popupObj = base.GetTemplateChild("PART_Popup");
			Popup panel = (Popup)popupObj;
			//SetValue(MenuHeightProperty, panel.Height);
			var exp = panel.GetBindingExpression(Popup.WidthProperty);

			if (!this.ShowAuxilaryPanel && exp == null) {
				DependencyObject panelArea = base.GetTemplateChild("PART_SubMenuScrollViewer");

				var panelBinding = new Binding("ActualWidth") {
					Source = panelArea,
					Mode = BindingMode.OneWay
				};
				panel.SetBinding(Popup.WidthProperty, panelBinding);
			}
			else if (this.ShowAuxilaryPanel && exp != null) {
				BindingOperations.ClearBinding(panel, Popup.WidthProperty);
			}
			else {
				panel.Width = (double)GetValue(MenuWidthProperty);
			}
			panel.Height = (double)GetValue(MenuHeightProperty);
		}
		public void HidePopup() {
			DependencyObject popupObj = base.GetTemplateChild("PART_Popup");
			Popup panel = (Popup)popupObj;
			panel.IsOpen = false;
		}
	}
}
