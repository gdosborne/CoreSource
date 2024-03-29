namespace SNC.OptiRamp.Application.DeveloperEntities.Controls {
	using System;
	using System.Windows.Controls;
	using System.Linq;
	using System.Windows;

	public delegate void LocationChangedHandler(object sender, LocationChangedEventArgs e);
	public class LocationChangedEventArgs : EventArgs {
		public LocationChangedEventArgs(Point newLocation) {
			NewLocation = newLocation;
		}
		public Point NewLocation {
			get;
			private set;
		}
	}
}
