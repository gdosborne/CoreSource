namespace GregOsborne.Dialogs {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.Design;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Windows;
	using System.Windows.Interop;
	using Ookii.Dialogs.Wpf.Properties;

	[DefaultProperty("MainInstruction")]
	[DefaultEvent("ButtonClicked")]
	[Description("Displays a task dialog.")]
	[Designer(typeof(TaskDialogDesigner))]
	public partial class TaskDialog : Component, IWin32Window {
		private TaskDialogItemCollection<TaskDialogButton> buttons;
		private Dictionary<int, TaskDialogButton> buttonsById;
		private NativeMethods.TASKDIALOGCONFIG config;
		private Icon customFooterIcon;
		private Icon customMainIcon;
		private TaskDialogIcon footerIcon;
		private IntPtr handle;
		private int inEventHandler;
		private TaskDialogIcon mainIcon;
		private int progressBarMarqueeAnimationSpeed = 100;
		private int progressBarMaximum = 100;
		private int progressBarMinimimum;
		private ProgressBarState progressBarState = ProgressBarState.Normal;
		private int progressBarValue;
		private TaskDialogItemCollection<TaskDialogRadioButton> radioButtons;
		private Dictionary<int, TaskDialogRadioButton> radioButtonsById;
		private bool updatePending;
		private Icon windowIcon;

		public TaskDialog() {
			this.InitializeComponent();
			this.config.cbSize = (uint)Marshal.SizeOf(this.config);
			this.config.pfCallback = this.TaskDialogCallback;
		}

		public TaskDialog(IContainer container) {
			container?.Add(this);
			this.InitializeComponent();
			this.config.cbSize = (uint)Marshal.SizeOf(this.config);
			this.config.pfCallback = this.TaskDialogCallback;
		}

		public static bool OsSupportsTaskDialogs => NativeMethods.IsWindowsVistaOrLater;

		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("A list of the buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogButton> Buttons => this.buttons ?? (this.buttons = new TaskDialogItemCollection<TaskDialogButton>(this));

		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Appearance")]
		[Description("A list of the radio buttons on the Task Dialog.")]
		public TaskDialogItemCollection<TaskDialogRadioButton> RadioButtons => this.radioButtons ?? (this.radioButtons = new TaskDialogItemCollection<TaskDialogRadioButton>(this));

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The window title of the task dialog.")]
		[DefaultValue("")]
		public string WindowTitle {
			get => this.config.pszWindowTitle ?? string.Empty;
			set {
				this.config.pszWindowTitle = string.IsNullOrEmpty(value) ? null : value;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's main instruction.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string MainInstruction {
			get => this.config.pszMainInstruction ?? string.Empty;
			set {
				this.config.pszMainInstruction = string.IsNullOrEmpty(value) ? null : value;
				this.SetElementText(NativeMethods.TaskDialogElements.MainInstruction, this.MainInstruction);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The dialog's primary content.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Content {
			get => this.config.pszContent ?? string.Empty;
			set {
				this.config.pszContent = string.IsNullOrEmpty(value) ? null : value;
				this.SetElementText(NativeMethods.TaskDialogElements.Content, this.Content);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to be used in the title bar of the dialog. Used only when the dialog is shown as a modeless dialog.")]
		[DefaultValue(null)]
		public Icon WindowIcon {
			get {
				if (!this.IsDialogRunning) {
					return this.windowIcon;
				}

				var icon = NativeMethods.SendMessage(this.Handle, NativeMethods.WM_GETICON, new IntPtr(NativeMethods.ICON_SMALL), IntPtr.Zero);
				return Icon.FromHandle(icon);
			}
			set => this.windowIcon = value;
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon MainIcon {
			get => this.mainIcon;
			set {
				if (this.mainIcon == value) {
					return;
				}

				this.mainIcon = value;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the dialog.")]
		[DefaultValue(null)]
		public Icon CustomMainIcon {
			get => this.customMainIcon;
			set {
				if (this.customMainIcon == value) {
					return;
				}

				this.customMainIcon = value;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The icon to display in the footer area of the task dialog.")]
		[DefaultValue(TaskDialogIcon.Custom)]
		public TaskDialogIcon FooterIcon {
			get => this.footerIcon;
			set {
				if (this.footerIcon == value) {
					return;
				}

				this.footerIcon = value;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("A custom icon to display in the footer area of the task dialog.")]
		[DefaultValue(null)]
		public Icon CustomFooterIcon {
			get => this.customFooterIcon;
			set {
				if (this.customFooterIcon == value) {
					return;
				}

				this.customFooterIcon = value;
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether custom buttons should be displayed as normal buttons or command links.")]
		[DefaultValue(TaskDialogButtonStyle.Standard)]
		public TaskDialogButtonStyle ButtonStyle {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon) ? TaskDialogButtonStyle.CommandLinksNoIcon : this.GetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks) ? TaskDialogButtonStyle.CommandLinks : TaskDialogButtonStyle.Standard;
			set {
				this.SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinks, value == TaskDialogButtonStyle.CommandLinks);
				this.SetFlag(NativeMethods.TaskDialogFlags.UseCommandLinksNoIcon, value == TaskDialogButtonStyle.CommandLinksNoIcon);
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The label for the verification checkbox.")]
		[DefaultValue("")]
		public string VerificationText {
			get => this.config.pszVerificationText ?? string.Empty;
			set {
				var realValue = string.IsNullOrEmpty(value) ? null : value;
				if (this.config.pszVerificationText == realValue) {
					return;
				}

				this.config.pszVerificationText = realValue;
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the verification checkbox is checked ot not.")]
		[DefaultValue(false)]
		public bool IsVerificationChecked {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked);
			set {
				if (value == this.IsVerificationChecked) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.VerificationFlagChecked, value);
				if (this.IsDialogRunning) {
					this.ClickVerification(value, false);
				}
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Additional information to be displayed on the dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string ExpandedInformation {
			get => this.config.pszExpandedInformation ?? string.Empty;
			set {
				this.config.pszExpandedInformation = string.IsNullOrEmpty(value) ? null : value;
				this.SetElementText(NativeMethods.TaskDialogElements.ExpandedInformation, this.ExpandedInformation);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for collapsing the expandable information.")]
		[DefaultValue("")]
		public string ExpandedControlText {
			get => this.config.pszExpandedControlText ?? string.Empty;
			set {
				var realValue = string.IsNullOrEmpty(value) ? null : value;
				if (this.config.pszExpandedControlText == realValue) {
					return;
				}

				this.config.pszExpandedControlText = realValue;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to use for the control for expanding the expandable information.")]
		[DefaultValue("")]
		public string CollapsedControlText {
			get => this.config.pszCollapsedControlText ?? string.Empty;
			set {
				var realValue = string.IsNullOrEmpty(value) ? null : value;
				if (this.config.pszCollapsedControlText == realValue) {
					return;
				}

				this.config.pszCollapsedControlText = string.IsNullOrEmpty(value) ? null : value;
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("The text to be used in the footer area of the task dialog.")]
		[DefaultValue("")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string Footer {
			get => this.config.pszFooterText ?? string.Empty;
			set {
				this.config.pszFooterText = string.IsNullOrEmpty(value) ? null : value;
				this.SetElementText(NativeMethods.TaskDialogElements.Footer, this.Footer);
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("the width of the task dialog's client area in DLU's. If 0, task dialog will calculate the ideal width.")]
		[DefaultValue(0)]
		public int Width {
			get => (int)this.config.cxWidth;
			set {
				if (this.config.cxWidth == (uint)value) {
					return;
				}

				this.config.cxWidth = (uint)value;
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether hyperlinks are allowed for the Content, ExpandedInformation and Footer properties.")]
		[DefaultValue(false)]
		public bool EnableHyperlinks {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks);
			set {
				if (this.EnableHyperlinks == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.EnableHyperLinks, value);
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the dialog should be able to be closed using Alt-F4, Escape and the title bar's close button even if no cancel button is specified.")]
		[DefaultValue(false)]
		public bool AllowDialogCancellation {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation);
			set {
				if (this.AllowDialogCancellation == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.AllowDialogCancellation, value);
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed at the bottom of the dialog's footer area instead of immediately after the dialog's content.")]
		[DefaultValue(false)]
		public bool ExpandFooterArea {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea);
			set {
				if (this.ExpandFooterArea == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.ExpandFooterArea, value);
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates that the string specified by the ExpandedInformation property should be displayed by default.")]
		[DefaultValue(false)]
		public bool ExpandedByDefault {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault);
			set {
				if (this.ExpandedByDefault == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.ExpandedByDefault, value);
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("Indicates whether the Timer event is raised periodically while the dialog is visible.")]
		[DefaultValue(false)]
		public bool RaiseTimerEvent {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.CallbackTimer);
			set {
				if (this.RaiseTimerEvent == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.CallbackTimer, value);
				this.UpdateDialog();
			}
		}

		[Category("Layout")]
		[Description("Indicates whether the dialog is centered in the parent window instead of the screen.")]
		[DefaultValue(false)]
		public bool CenterParent {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow);
			set {
				if (this.CenterParent == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.PositionRelativeToWindow, value);
				this.UpdateDialog();
			}
		}

		[Localizable(true)]
		[Category("Appearance")]
		[Description("Indicates whether text is displayed right to left.")]
		[DefaultValue(false)]
		public bool RightToLeft {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.RtlLayout);
			set {
				if (this.RightToLeft == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.RtlLayout, value);
				this.UpdateDialog();
			}
		}

		[Category("Window Style")]
		[Description("Indicates whether the dialog has a minimize box on its caption bar.")]
		[DefaultValue(false)]
		public bool MinimizeBox {
			get => this.GetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized);
			set {
				if (this.MinimizeBox == value) {
					return;
				}

				this.SetFlag(NativeMethods.TaskDialogFlags.CanBeMinimized, value);
				this.UpdateDialog();
			}
		}

		[Category("Behavior")]
		[Description("The type of progress bar displayed on the dialog.")]
		[DefaultValue(ProgressBarStyle.None)]
		public ProgressBarStyle ProgressBarStyle {
			get {
				if (this.GetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar)) {
					return ProgressBarStyle.MarqueeProgressBar;
				}

				return this.GetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar) ? ProgressBarStyle.ProgressBar : ProgressBarStyle.None;
			}
			set {
				this.SetFlag(NativeMethods.TaskDialogFlags.ShowMarqueeProgressBar, value == ProgressBarStyle.MarqueeProgressBar);
				this.SetFlag(NativeMethods.TaskDialogFlags.ShowProgressBar, value == ProgressBarStyle.ProgressBar);
				this.UpdateProgressBarStyle();
			}
		}

		[Category("Behavior")]
		[Description("The marquee animation speed of the progress bar in milliseconds.")]
		[DefaultValue(100)]
		public int ProgressBarMarqueeAnimationSpeed {
			get => this.progressBarMarqueeAnimationSpeed;
			set {
				this.progressBarMarqueeAnimationSpeed = value;
				this.UpdateProgressBarMarqueeSpeed();
			}
		}

		[Category("Behavior")]
		[Description("The lower bound of the range of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarMinimum {
			get => this.progressBarMinimimum;
			set {
				if (this.progressBarMaximum <= value) {
					throw new ArgumentOutOfRangeException("value");
				}

				this.progressBarMinimimum = value;
				this.UpdateProgressBarRange();
			}
		}

		[Category("Behavior")]
		[Description("The upper bound of the range of the task dialog's progress bar.")]
		[DefaultValue(100)]
		public int ProgressBarMaximum {
			get => this.progressBarMaximum;
			set {
				if (value <= this.progressBarMinimimum) {
					throw new ArgumentOutOfRangeException("value");
				}

				this.progressBarMaximum = value;
				this.UpdateProgressBarRange();
			}
		}

		[Category("Behavior")]
		[Description("The current value of the task dialog's progress bar.")]
		[DefaultValue(0)]
		public int ProgressBarValue {
			get => this.progressBarValue;
			set {
				if (value < this.ProgressBarMinimum || value > this.ProgressBarMaximum) {
					throw new ArgumentOutOfRangeException("value");
				}

				this.progressBarValue = value;
				this.UpdateProgressBarValue();
			}
		}

		[Category("Behavior")]
		[Description("The state of the task dialog's progress bar.")]
		[DefaultValue(ProgressBarState.Normal)]
		public ProgressBarState ProgressBarState {
			get => this.progressBarState;
			set {
				this.progressBarState = value;
				this.UpdateProgressBarState();
			}
		}

		[Category("Data")]
		[Description("User-defined data about the component.")]
		[DefaultValue(null)]
		public object Tag { get; set; }

		private bool IsDialogRunning => this.handle != IntPtr.Zero;

		[Browsable(false)]
		public IntPtr Handle {
			get {
				this.CheckCrossThreadCall();
				return this.handle;
			}
		}

		public static TaskDialog Create(string content, string mainInstruction, TaskDialogIcon mainIcon, string windowTitle, string expandedInformation = null) => new TaskDialog {
			Content = content,
			ButtonStyle = TaskDialogButtonStyle.Standard,
			CenterParent = true,
			MainInstruction = mainInstruction,
			MainIcon = mainIcon,
			WindowTitle = windowTitle,
			ExpandedInformation = expandedInformation
		};

		[Category("Behavior")]
		[Description("Event raised when the task dialog has been created.")]
		public event EventHandler Created;

		[Category("Behavior")]
		[Description("Event raised when the task dialog has been destroyed.")]
		public event EventHandler Destroyed;

		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		public event EventHandler<TaskDialogItemClickedEventArgs> ButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks a button.")]
		public event EventHandler<TaskDialogItemClickedEventArgs> RadioButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks a hyperlink.")]
		public event EventHandler<HyperlinkClickedEventArgs> HyperlinkClicked;

		[Category("Action")]
		[Description("Event raised when the user clicks the verification check box.")]
		public event EventHandler VerificationClicked;

		[Category("Behavior")]
		[Description("Event raised periodically while the dialog is displayed.")]
		public event EventHandler<TimerEventArgs> Timer;

		[Category("Action")]
		[Description("Event raised when the user clicks the expand button on the task dialog.")]
		public event EventHandler<ExpandButtonClickedEventArgs> ExpandButtonClicked;

		[Category("Action")]
		[Description("Event raised when the user presses F1 while the dialog has focus.")]
		public event EventHandler HelpRequested;

		public TaskDialogButton Show() => this.ShowDialog(IntPtr.Zero);

		public TaskDialogButton ShowDialog() => this.ShowDialog(null);

		public TaskDialogButton ShowDialog(Window owner) {
			IntPtr ownerHandle;
			if (owner == null) {
				ownerHandle = NativeMethods.GetActiveWindow();
			} else {
				ownerHandle = new WindowInteropHelper(owner).Handle;
			}

			return this.ShowDialog(ownerHandle);
		}

		public void ClickVerification(bool checkState, bool setFocus) {
			if (!this.IsDialogRunning) {
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}

			NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.ClickVerification, new IntPtr(checkState ? 1 : 0), new IntPtr(setFocus ? 1 : 0));
		}

		protected virtual void OnHyperlinkClicked(HyperlinkClickedEventArgs e) => HyperlinkClicked?.Invoke(this, e);

		protected virtual void OnButtonClicked(TaskDialogItemClickedEventArgs e) => ButtonClicked?.Invoke(this, e);

		protected virtual void OnRadioButtonClicked(TaskDialogItemClickedEventArgs e) => RadioButtonClicked?.Invoke(this, e);

		protected virtual void OnVerificationClicked(EventArgs e) => VerificationClicked?.Invoke(this, e);

		protected virtual void OnCreated(EventArgs e) => Created?.Invoke(this, e);

		protected virtual void OnTimer(TimerEventArgs e) => Timer?.Invoke(this, e);

		protected virtual void OnDestroyed(EventArgs e) => Destroyed?.Invoke(this, e);

		protected virtual void OnExpandButtonClicked(ExpandButtonClickedEventArgs e) => ExpandButtonClicked?.Invoke(this, e);

		protected virtual void OnHelpRequested(EventArgs e) => HelpRequested?.Invoke(this, e);

		internal void SetItemEnabled(TaskDialogItem item) {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)(item is TaskDialogButton ? NativeMethods.TaskDialogMessages.EnableButton : NativeMethods.TaskDialogMessages.EnableRadioButton), new IntPtr(item.Id), new IntPtr(item.Enabled ? 1 : 0));
			}
		}

		internal void SetButtonElevationRequired(TaskDialogButton button) {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetButtonElevationRequiredState, new IntPtr(button.Id), new IntPtr(button.ElevationRequired ? 1 : 0));
			}
		}

		internal void ClickItem(TaskDialogItem item) {
			if (!this.IsDialogRunning) {
				throw new InvalidOperationException(Resources.TaskDialogNotRunningError);
			}

			NativeMethods.SendMessage(this.Handle, (int)(item is TaskDialogButton ? NativeMethods.TaskDialogMessages.ClickButton : NativeMethods.TaskDialogMessages.ClickRadioButton), new IntPtr(item.Id), IntPtr.Zero);
		}

		private TaskDialogButton ShowDialog(IntPtr owner) {
			if (!OsSupportsTaskDialogs) {
				throw new NotSupportedException(Resources.TaskDialogsNotSupportedError);
			}

			if (this.IsDialogRunning) {
				throw new InvalidOperationException(Resources.TaskDialogRunningError);
			}

			if (this.buttons == null || this.buttons.Count == 0) {
				throw new InvalidOperationException(Resources.TaskDialogNoButtonsError);
			}

			this.config.hwndParent = owner;
			this.config.dwCommonButtons = 0;
			this.config.pButtons = IntPtr.Zero;
			this.config.cButtons = 0;
			var buttons = this.SetupButtons();
			var radioButtons = this.SetupRadioButtons();
			this.SetupIcon();
			try {
				MarshalButtons(buttons, out this.config.pButtons, out this.config.cButtons);
				MarshalButtons(radioButtons, out this.config.pRadioButtons, out this.config.cRadioButtons);
				int buttonId;
				int radioButton;
				bool verificationFlagChecked;
				using (new ComCtlv6ActivationContext(true)) {
					NativeMethods.TaskDialogIndirect(ref this.config, out buttonId, out radioButton, out verificationFlagChecked);
				}
				this.IsVerificationChecked = verificationFlagChecked;
#pragma warning disable IDE0018 // Inline variable declaration
				TaskDialogRadioButton selectedRadioButton;
#pragma warning restore IDE0018 // Inline variable declaration
				if (this.radioButtonsById.TryGetValue(radioButton, out selectedRadioButton)) {
					selectedRadioButton.Checked = true;
				}

				return this.buttonsById.TryGetValue(buttonId, out var selectedButton) ? selectedButton : null;
			}
			finally {
				CleanUpButtons(ref this.config.pButtons, ref this.config.cButtons);
				CleanUpButtons(ref this.config.pRadioButtons, ref this.config.cRadioButtons);
			}
		}

		internal void UpdateDialog() {
			if (!this.IsDialogRunning) {
				return;
			}

			if (this.inEventHandler > 0) {
				this.updatePending = true;
			} else {
				this.updatePending = false;
				CleanUpButtons(ref this.config.pButtons, ref this.config.cButtons);
				CleanUpButtons(ref this.config.pRadioButtons, ref this.config.cRadioButtons);
				this.config.dwCommonButtons = 0;
				var buttons = this.SetupButtons();
				var radioButtons = this.SetupRadioButtons();
				this.SetupIcon();
				MarshalButtons(buttons, out this.config.pButtons, out this.config.cButtons);
				MarshalButtons(radioButtons, out this.config.pRadioButtons, out this.config.cRadioButtons);
				var size = Marshal.SizeOf(this.config);
				var memory = Marshal.AllocHGlobal(size);
				try {
					Marshal.StructureToPtr(this.config, memory, false);
					NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.NavigatePage, IntPtr.Zero, memory);
				}
				finally {
					Marshal.DestroyStructure(memory, typeof(NativeMethods.TASKDIALOGCONFIG));
					Marshal.FreeHGlobal(memory);
				}
			}
		}

		private void SetElementText(NativeMethods.TaskDialogElements element, string text) {
			if (!this.IsDialogRunning) {
				return;
			}

			var newTextPtr = Marshal.StringToHGlobalUni(text);
			try {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetElementText, new IntPtr((int)element), newTextPtr);
			}
			finally {
				if (newTextPtr != IntPtr.Zero) {
					Marshal.FreeHGlobal(newTextPtr);
				}
			}
		}

		private void SetupIcon() {
			this.SetupIcon(this.MainIcon, this.CustomMainIcon, NativeMethods.TaskDialogFlags.UseHIconMain);
			this.SetupIcon(this.FooterIcon, this.CustomFooterIcon, NativeMethods.TaskDialogFlags.UseHIconFooter);
		}

		private void SetupIcon(TaskDialogIcon icon, Icon customIcon, NativeMethods.TaskDialogFlags flag) {
			this.SetFlag(flag, false);
			if (icon == TaskDialogIcon.Custom) {
				if (customIcon == null) {
					return;
				}

				this.SetFlag(flag, true);
				if (flag == NativeMethods.TaskDialogFlags.UseHIconMain) {
					this.config.hMainIcon = customIcon.Handle;
				} else {
					this.config.hFooterIcon = customIcon.Handle;
				}
			} else {
				if (flag == NativeMethods.TaskDialogFlags.UseHIconMain) {
					this.config.hMainIcon = new IntPtr((int)icon);
				} else {
					this.config.hFooterIcon = new IntPtr((int)icon);
				}
			}
		}

		private static void CleanUpButtons(ref IntPtr buttons, ref uint count) {
			if (buttons == IntPtr.Zero) {
				return;
			}

			var elementSize = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
			for (var x = 0; x < count; ++x) {
				var offset = new IntPtr(buttons.ToInt64() + x * elementSize);
				Marshal.DestroyStructure(offset, typeof(NativeMethods.TASKDIALOG_BUTTON));
			}
			Marshal.FreeHGlobal(buttons);
			buttons = IntPtr.Zero;
			count = 0;
		}

		private static void MarshalButtons(List<NativeMethods.TASKDIALOG_BUTTON> buttons, out IntPtr buttonsPtr, out uint count) {
			buttonsPtr = IntPtr.Zero;
			count = 0;
			if (buttons.Count <= 0) {
				return;
			}

			var elementSize = Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON));
			buttonsPtr = Marshal.AllocHGlobal(elementSize * buttons.Count);
			for (var x = 0; x < buttons.Count; ++x) {
				var offset = new IntPtr(buttonsPtr.ToInt64() + x * elementSize);
				Marshal.StructureToPtr(buttons[x], offset, false);
			}
			count = (uint)buttons.Count;
		}

		private List<NativeMethods.TASKDIALOG_BUTTON> SetupButtons() {
			this.buttonsById = new Dictionary<int, TaskDialogButton>();
			var buttons = new List<NativeMethods.TASKDIALOG_BUTTON>();
			this.config.nDefaultButton = 0;
			foreach (var button in this.Buttons) {
				if (button.Id < 1) {
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}

				this.buttonsById.Add(button.Id, button);
				if (button.Default) {
					this.config.nDefaultButton = button.Id;
				}

				if (button.ButtonType == ButtonType.Custom) {
					if (string.IsNullOrEmpty(button.Text)) {
						throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
					}

					var taskDialogButton = new NativeMethods.TASKDIALOG_BUTTON {
						nButtonID = button.Id,
						pszButtonText = button.Text
					};
					if (this.ButtonStyle == TaskDialogButtonStyle.CommandLinks || this.ButtonStyle == TaskDialogButtonStyle.CommandLinksNoIcon && !string.IsNullOrEmpty(button.CommandLinkNote)) {
						taskDialogButton.pszButtonText += "\n" + button.CommandLinkNote;
					}

					buttons.Add(taskDialogButton);
				} else {
					this.config.dwCommonButtons |= button.ButtonFlag;
				}
			}
			return buttons;
		}

		private List<NativeMethods.TASKDIALOG_BUTTON> SetupRadioButtons() {
			this.radioButtonsById = new Dictionary<int, TaskDialogRadioButton>();
			var radioButtons = new List<NativeMethods.TASKDIALOG_BUTTON>();
			this.config.nDefaultRadioButton = 0;
			foreach (var radioButton in this.RadioButtons) {
				if (string.IsNullOrEmpty(radioButton.Text)) {
					throw new InvalidOperationException(Resources.TaskDialogEmptyButtonLabelError);
				}

				if (radioButton.Id < 1) {
					throw new InvalidOperationException(Resources.InvalidTaskDialogItemIdError);
				}

				this.radioButtonsById.Add(radioButton.Id, radioButton);
				if (radioButton.Checked) {
					this.config.nDefaultRadioButton = radioButton.Id;
				}

				var taskDialogButton = new NativeMethods.TASKDIALOG_BUTTON {
					nButtonID = radioButton.Id,
					pszButtonText = radioButton.Text
				};
				radioButtons.Add(taskDialogButton);
			}
			this.SetFlag(NativeMethods.TaskDialogFlags.NoDefaultRadioButton, this.config.nDefaultRadioButton == 0);
			return radioButtons;
		}

		private void SetFlag(NativeMethods.TaskDialogFlags flag, bool value) {
			if (value) {
				this.config.dwFlags |= flag;
			} else {
				this.config.dwFlags &= ~flag;
			}
		}

		private bool GetFlag(NativeMethods.TaskDialogFlags flag) => (this.config.dwFlags & flag) != 0;

		private uint TaskDialogCallback(IntPtr hwnd, uint uNotification, IntPtr wParam, IntPtr lParam, IntPtr dwRefData) {
			Interlocked.Increment(ref this.inEventHandler);
			try {
				// ReSharper disable once SwitchStatementMissingSomeCases
				switch ((NativeMethods.TaskDialogNotifications)uNotification) {
					case NativeMethods.TaskDialogNotifications.Created:
						this.handle = hwnd;
						this.DialogCreated();
						this.OnCreated(EventArgs.Empty);
						break;
					case NativeMethods.TaskDialogNotifications.Destroyed:
						this.handle = IntPtr.Zero;
						this.OnDestroyed(EventArgs.Empty);
						break;
					case NativeMethods.TaskDialogNotifications.Navigated:
						this.DialogCreated();
						break;
					case NativeMethods.TaskDialogNotifications.HyperlinkClicked:
						var url = Marshal.PtrToStringUni(lParam);
						this.OnHyperlinkClicked(new HyperlinkClickedEventArgs(url));
						break;
					case NativeMethods.TaskDialogNotifications.ButtonClicked:
						TaskDialogButton button;
						if (this.buttonsById.TryGetValue((int)wParam, out button)) {
							var e = new TaskDialogItemClickedEventArgs(button);
							this.OnButtonClicked(e);
							if (e.Cancel) {
								return 1;
							}
						}
						break;
					case NativeMethods.TaskDialogNotifications.VerificationClicked:
						this.IsVerificationChecked = (int)wParam == 1;
						this.OnVerificationClicked(EventArgs.Empty);
						break;
					case NativeMethods.TaskDialogNotifications.RadioButtonClicked:
						if (this.radioButtonsById.TryGetValue((int)wParam, out var radioButton)) {
							radioButton.Checked = true;
							var e = new TaskDialogItemClickedEventArgs(radioButton);
							this.OnRadioButtonClicked(e);
						}
						break;
					case NativeMethods.TaskDialogNotifications.Timer:
						var timerEventArgs = new TimerEventArgs(wParam.ToInt32());
						this.OnTimer(timerEventArgs);
						return (uint)(timerEventArgs.ResetTickCount ? 1 : 0);
					case NativeMethods.TaskDialogNotifications.ExpandoButtonClicked:
						this.OnExpandButtonClicked(new ExpandButtonClickedEventArgs(wParam.ToInt32() != 0));
						break;
					case NativeMethods.TaskDialogNotifications.Help:
						this.OnHelpRequested(EventArgs.Empty);
						break;
				}
				return 0;
			}
			finally {
				Interlocked.Decrement(ref this.inEventHandler);
				if (this.updatePending) {
					this.UpdateDialog();
				}
			}
		}

		private void DialogCreated() {
			if (this.config.hwndParent == IntPtr.Zero && this.windowIcon != null) {
				NativeMethods.SendMessage(this.Handle, NativeMethods.WM_SETICON, new IntPtr(NativeMethods.ICON_SMALL), this.windowIcon.Handle);
			}

			foreach (var button in this.Buttons) {
				if (!button.Enabled) {
					this.SetItemEnabled(button);
				}

				if (button.ElevationRequired) {
					this.SetButtonElevationRequired(button);
				}
			}
			this.UpdateProgressBarStyle();
			this.UpdateProgressBarMarqueeSpeed();
			this.UpdateProgressBarRange();
			this.UpdateProgressBarValue();
			this.UpdateProgressBarState();
		}

		private void UpdateProgressBarStyle() {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetMarqueeProgressBar, new IntPtr(this.ProgressBarStyle == ProgressBarStyle.MarqueeProgressBar ? 1 : 0), IntPtr.Zero);
			}
		}

		private void UpdateProgressBarMarqueeSpeed() {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetProgressBarMarquee, new IntPtr(this.ProgressBarMarqueeAnimationSpeed > 0 ? 1 : 0), new IntPtr(this.ProgressBarMarqueeAnimationSpeed));
			}
		}

		private void UpdateProgressBarRange() {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetProgressBarRange, IntPtr.Zero, new IntPtr((this.ProgressBarMaximum << 16) | this.ProgressBarMinimum));
			}

			if (this.ProgressBarValue < this.ProgressBarMinimum) {
				this.ProgressBarValue = this.ProgressBarMinimum;
			}

			if (this.ProgressBarValue > this.ProgressBarMaximum) {
				this.ProgressBarValue = this.ProgressBarMaximum;
			}
		}

		private void UpdateProgressBarValue() {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetProgressBarPos, new IntPtr(this.ProgressBarValue), IntPtr.Zero);
			}
		}

		private void UpdateProgressBarState() {
			if (this.IsDialogRunning) {
				NativeMethods.SendMessage(this.Handle, (int)NativeMethods.TaskDialogMessages.SetProgressBarState, new IntPtr((int)this.ProgressBarState + 1), IntPtr.Zero);
			}
		}

		private void CheckCrossThreadCall() {
			var handle = this.handle;
			if (handle == IntPtr.Zero) {
				return;
			}

			var windowThreadId = NativeMethods.GetWindowThreadProcessId(handle, out var processId);
			var threadId = NativeMethods.GetCurrentThreadId();
			if (windowThreadId != threadId) {
				throw new InvalidOperationException(Resources.TaskDialogIllegalCrossThreadCallError);
			}
		}

	}
}