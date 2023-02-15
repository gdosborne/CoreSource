﻿using Common.Applicationn.Primitives;
using System.Windows;
using System;
using System.Windows.Controls;
using TerritoryManager.Extension.ViewModels;

namespace TerritoryManager.Extension {
    public partial class ExtensionControl : UserControl {
        public ExtensionControl() {
            InitializeComponent();

            View.Initialize(App.AppSettings, App.DataManager);
        }

        public ExtensionControlViewModel View => MainGrid.DataContext.As<ExtensionControlViewModel>();

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateColumnsWidth(sender as ListView);
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e) {
            UpdateColumnsWidth(sender as ListView);
        }

        private void UpdateColumnsWidth(ListView listView) {
            int autoFillColumnIndex = (listView.View as GridView).Columns.Count - 1;
            if (listView.ActualWidth == Double.NaN)
                listView.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            double remainingSpace = listView.ActualWidth;
            for (int i = 0; i < (listView.View as GridView).Columns.Count; i++)
                if (i != autoFillColumnIndex)
                    remainingSpace -= (listView.View as GridView).Columns[i].ActualWidth;
            (listView.View as GridView).Columns[autoFillColumnIndex].Width = remainingSpace >= 0 ? remainingSpace : 0;
        }
    }
}
