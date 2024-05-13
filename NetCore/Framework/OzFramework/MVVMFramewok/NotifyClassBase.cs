/* File="NotifyClassBase"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="12/5/2023" */

namespace HP.MVVMFramework {
	using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NotifyClassBase : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		protected void InvokePropertyChanged([CallerMemberName]string propertyName = default) => 
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
