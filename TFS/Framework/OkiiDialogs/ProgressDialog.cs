namespace GregOsborne.Dialogs {
	using System;
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Windows.Interop;

	[DefaultEvent("DoWork"), DefaultProperty("Text"), Description("Represents a dialog that can be used to report progress to the user.")]
	public partial class ProgressDialog : Component {
		private class ProgressChangedData {
			public string Text { get; set; }
			public string Description { get; set; }
			public object UserState { get; set; }
		}
		private string windowTitle;
		private string text;
		private string description;
		private Interop.IProgressDialog dialog;
		private string cancellationText;
		private bool useCompactPathsForText;
		private bool useCompactPathsForDescription;
		private SafeModuleHandle currentAnimationModuleHandle;
		private bool cancellationPending;
		public event DoWorkEventHandler DoWork;
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;
		public event ProgressChangedEventHandler ProgressChanged;
		public ProgressDialog()
			: this(null) {
		}
		public ProgressDialog(IContainer container) {
			if (container != null) {
				container.Add(this);
			}

			this.InitializeComponent();
			this.ProgressBarStyle = ProgressBarStyle.ProgressBar;
			this.ShowCancelButton = true;
			this.MinimizeBox = true;
			if (!NativeMethods.IsWindowsVistaOrLater) {
				this.Animation = AnimationResource.GetShellAnimation(GregOsborne.Dialogs.ShellAnimation.FlyingPapers);
			}
		}
		[Localizable(true), Category("Appearance"), Description("The text in the progress dialog's title bar."), DefaultValue("")]
		public string WindowTitle {
			get => this.windowTitle ?? string.Empty;
			set => this.windowTitle = value;
		}
		[Localizable(true), Category("Appearance"), Description("A short description of the operation being carried out.")]
		public string Text {
			get => this.text ?? string.Empty;
			set {
				this.text = value;
				if (this.dialog != null) {
					this.dialog.SetLine(1, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}
		[Category("Behavior"), Description("Indicates whether path strings in the Text property should be compacted if they are too large to fit on one line."), DefaultValue(false)]
		public bool UseCompactPathsForText {
			get => this.useCompactPathsForText;
			set {
				this.useCompactPathsForText = value;
				if (this.dialog != null) {
					this.dialog.SetLine(1, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
				}
			}
		}
		[Localizable(true), Category("Appearance"), Description("Additional details about the operation being carried out."), DefaultValue("")]
		public string Description {
			get => this.description ?? string.Empty;
			set {
				this.description = value;
				if (this.dialog != null) {
					this.dialog.SetLine(2, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}
		[Category("Behavior"), Description("Indicates whether path strings in the Description property should be compacted if they are too large to fit on one line."), DefaultValue(false)]
		public bool UseCompactPathsForDescription {
			get => this.useCompactPathsForDescription;
			set {
				this.useCompactPathsForDescription = value;
				if (this.dialog != null) {
					this.dialog.SetLine(2, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
				}
			}
		}
		[Localizable(true), Category("Appearance"), Description("The text that will be shown after the Cancel button is pressed."), DefaultValue("")]
		public string CancellationText {
			get => this.cancellationText ?? string.Empty;
			set => this.cancellationText = value;
		}
		[Category("Appearance"), Description("Indicates whether an estimate of the remaining time will be shown."), DefaultValue(false)]
		public bool ShowTimeRemaining { get; set; }
		[Category("Appearance"), Description("Indicates whether the dialog has a cancel button. Do not set to false unless absolutely necessary."), DefaultValue(true)]
		public bool ShowCancelButton { get; set; }
		[Category("Window Style"), Description("Indicates whether the progress dialog has a minimize button."), DefaultValue(true)]
		public bool MinimizeBox { get; set; }
		[Browsable(false)]
		public bool CancellationPending {
			get {
				this.backgroundWorker.ReportProgress(-1);
				return this.cancellationPending;
			}
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AnimationResource Animation { get; set; }
		[Category("Appearance"), Description("Indicates the style of the progress bar."), DefaultValue(ProgressBarStyle.ProgressBar)]
		public ProgressBarStyle ProgressBarStyle { get; set; }
		[Browsable(false)]
		public bool IsBusy => this.backgroundWorker.IsBusy;
		public void Show() => this.Show(null);
		public void Show(object argument) => this.RunProgressDialog(IntPtr.Zero, argument);
		public void ShowDialog() => this.ShowDialog(null, null);
		public void ShowDialog(Window owner) => this.ShowDialog(owner, null);
		public void ShowDialog(Window owner, object argument) => this.RunProgressDialog(owner == null ? NativeMethods.GetActiveWindow() : new WindowInteropHelper(owner).Handle, argument);
		public void ReportProgress(int percentProgress) => this.ReportProgress(percentProgress, null, null, null);
		public void ReportProgress(int percentProgress, string text, string description) => this.ReportProgress(percentProgress, text, description, null);
		public void ReportProgress(int percentProgress, string text, string description, object userState) {
			if (percentProgress < 0 || percentProgress > 100) {
				throw new ArgumentOutOfRangeException("percentProgress");
			}

			if (this.dialog == null) {
				throw new InvalidOperationException(Ookii.Dialogs.Wpf.Properties.Resources.ProgressDialogNotRunningError);
			}

			this.backgroundWorker.ReportProgress(percentProgress, new ProgressChangedData() { Text = text, Description = description, UserState = userState });
		}
		protected virtual void OnDoWork(DoWorkEventArgs e) {
			var handler = DoWork;
			if (handler != null) {
				handler(this, e);
			}
		}
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e) {
			var handler = RunWorkerCompleted;
			if (handler != null) {
				handler(this, e);
			}
		}
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e) {
			var handler = ProgressChanged;
			if (handler != null) {
				handler(this, e);
			}
		}
		private void RunProgressDialog(IntPtr owner, object argument) {
			if (this.backgroundWorker.IsBusy) {
				throw new InvalidOperationException(Ookii.Dialogs.Wpf.Properties.Resources.ProgressDialogRunning);
			}

			if (this.Animation != null) {
				try {
					this.currentAnimationModuleHandle = this.Animation.LoadLibrary();
				}
				catch (Win32Exception ex) {
					throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Ookii.Dialogs.Wpf.Properties.Resources.AnimationLoadErrorFormat, ex.Message), ex);
				}
				catch (System.IO.FileNotFoundException ex) {
					throw new InvalidOperationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Ookii.Dialogs.Wpf.Properties.Resources.AnimationLoadErrorFormat, ex.Message), ex);
				}
			}
			this.cancellationPending = false;
			this.dialog = new Interop.ProgressDialog();
			this.dialog.SetTitle(this.WindowTitle);
			if (this.Animation != null) {
				this.dialog.SetAnimation(this.currentAnimationModuleHandle, (ushort)this.Animation.ResourceId);
			}

			if (this.CancellationText.Length > 0) {
				this.dialog.SetCancelMsg(this.CancellationText, null);
			}

			this.dialog.SetLine(1, this.Text, this.UseCompactPathsForText, IntPtr.Zero);
			this.dialog.SetLine(2, this.Description, this.UseCompactPathsForDescription, IntPtr.Zero);
			var flags = GregOsborne.Dialogs.Interop.ProgressDialogFlags.Normal;
			if (owner != IntPtr.Zero) {
				flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.Modal;
			}

			switch (this.ProgressBarStyle) {
				case ProgressBarStyle.None:
					flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.NoProgressBar;
					break;
				case ProgressBarStyle.MarqueeProgressBar:
					if (NativeMethods.IsWindowsVistaOrLater) {
						flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.MarqueeProgress;
					} else {
						flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.NoProgressBar;
					}

					break;
			}
			if (this.ShowTimeRemaining) {
				flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.AutoTime;
			}

			if (!this.ShowCancelButton) {
				flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.NoCancel;
			}

			if (!this.MinimizeBox) {
				flags |= GregOsborne.Dialogs.Interop.ProgressDialogFlags.NoMinimize;
			}

			this.dialog.StartProgressDialog(owner, null, flags, IntPtr.Zero);
			this.backgroundWorker.RunWorkerAsync(argument);
		}
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) => this.OnDoWork(e);
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			this.dialog.StopProgressDialog();
			Marshal.ReleaseComObject(this.dialog);
			this.dialog = null;
			if (this.currentAnimationModuleHandle != null) {
				this.currentAnimationModuleHandle.Dispose();
				this.currentAnimationModuleHandle = null;
			}
			this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs((!e.Cancelled && e.Error == null) ? e.Result : null, e.Error, e.Cancelled));
		}
		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			this.cancellationPending = this.dialog.HasUserCancelled();
			if (e.ProgressPercentage >= 0 && e.ProgressPercentage <= 100) {
				this.dialog.SetProgress((uint)e.ProgressPercentage, 100);
				var data = e.UserState as ProgressChangedData;
				if (data != null) {
					if (data.Text != null) {
						this.Text = data.Text;
					}

					if (data.Description != null) {
						this.Description = data.Description;
					}

					this.OnProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, data.UserState));
				}
			}
		}
	}
}
