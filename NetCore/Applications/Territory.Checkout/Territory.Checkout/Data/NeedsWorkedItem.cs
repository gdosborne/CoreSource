namespace Territory.Checkout.Data {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;

	internal class NeedsWorkedItem : INotifyPropertyChanged {
		#region PropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		protected void InvokePropertyChanged([CallerMemberName] string propertyName = null) =>
			OnPropertyChanged(propertyName);
		#endregion

		#region Territory Property
		private TerritoryItem _Territory = default;
		public TerritoryItem Territory {
			get => _Territory;
			set {
				_Territory = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region LastWorked Property
		private DateTime? _LastWorked = default;
		public DateTime? LastWorked {
			get => _LastWorked;
			set {
				_LastWorked = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		#region NumberOfDays Property
		private string _NumberOfDays = default;
		public string NumberOfDays {
			get => _NumberOfDays;
			set {
				_NumberOfDays = value;
				InvokePropertyChanged();
			}
		}
		#endregion

		internal static NeedsWorkedItem Create(TerritoryItem territory, List<CheckoutItem> checkOuts) {
			var lastCheckout = checkOuts.OrderByDescending(x => x.CheckedOut).FirstOrDefault();
			if (!checkOuts.Any() || (lastCheckout != null && lastCheckout.CheckedIn.HasValue)) {
				var days = !checkOuts.Any() ? int.MaxValue : DateTime.Now.Subtract(lastCheckout.CheckedIn.Value).Days;
				days = days < 1 ? 0 : days;
				if (days > App.NumberOfDaysNeedsWorked) {
					return new NeedsWorkedItem {
						Territory = territory,
						LastWorked = lastCheckout == null ? null : lastCheckout.CheckedIn.Value,
						NumberOfDays = days == int.MaxValue ? "Never" : days.ToString()
					};
				}
			}
			return null;
		}
	}
}
