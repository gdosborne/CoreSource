namespace MVVMFramework {
	using System;
	using System.Windows.Input;
	using Windows.UI.Core;

	public class DelegateCommand : ICommand {
		private readonly Predicate<object> canExecute;
		private readonly Action<object> execute;
		public DelegateCommand(Action<object> execute)
			: this(execute, null) {
		}

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute) {
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;
		public event CheckAccessEventHandler CheckAccess;
		public void BlankHandler() {
		}

		public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute(parameter);

		public void Execute(object parameter) => this.execute(parameter);

		public async void RaiseCanExecuteChanged() {
			if (CheckAccess != null) {
				var e = new CheckAccessEventArgs();
				CheckAccess(this, e);
				if (e.HasAccess) {
					CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				} else {
					await e.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(this.BlankHandler));
				}
			} else {
				try {
					CanExecuteChanged?.Invoke(this, EventArgs.Empty);
				}
				catch {
					// ignored
				}
			}
		}
	}
}
