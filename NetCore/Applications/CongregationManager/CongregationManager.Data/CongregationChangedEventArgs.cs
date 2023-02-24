using System;

namespace CongregationManager.Data {
    public delegate void CongregationChangedHandler(object sender, CongregationChangedEventArgs e);
    public class CongregationChangedEventArgs : EventArgs {
        public CongregationChangedEventArgs(Congregation cong) => Congregation = cong;

        public Congregation Congregation { get; private set; } = null;

    }
}
