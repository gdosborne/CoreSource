namespace GregOsborne.Dialogs {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Net;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;
	using System.Text;
	using System.Windows;
	using System.Windows.Interop;

	[DefaultProperty("MainInstruction"), DefaultEvent("UserNameChanged"), Description("Allows access to credential UI for generic credentials.")]
	public partial class CredentialDialog : Component {
		private string confirmTarget;
		private NetworkCredential credentials = new NetworkCredential();
		private bool isSaveChecked;
		private string target;
		private static readonly Dictionary<string, System.Net.NetworkCredential> applicationInstanceCredentialCache = new Dictionary<string, NetworkCredential>();
		private string caption;
		private string text;
		private string windowTitle;
		[Category("Property Changed"), Description("Event raised when the value of the UserName property changes.")]
		public event EventHandler UserNameChanged;
		[Category("Property Changed"), Description("Event raised when the value of the Password property changes.")]
		public event EventHandler PasswordChanged;
		public CredentialDialog() => this.InitializeComponent();
		public CredentialDialog(IContainer container) {
			if (container != null) {
				container.Add(this);
			}

			this.InitializeComponent();
		}
		[Category("Behavior"), Description("Indicates whether to use the application instance credential cache."), DefaultValue(false)]
		public bool UseApplicationInstanceCredentialCache { get; set; }
		[Category("Appearance"), Description("Indicates whether the \"Save password\" checkbox is checked."), DefaultValue(false)]
		public bool IsSaveChecked {
			get => this.isSaveChecked;
			set {
				this.confirmTarget = null;
				this.isSaveChecked = value;
			}
		}
		[Browsable(false)]
		public string Password {
			get => this.credentials.Password;
			private set {
				this.confirmTarget = null;
				this.credentials.Password = value;
				this.OnPasswordChanged(EventArgs.Empty);
			}
		}
		[Browsable(false)]
		public NetworkCredential Credentials => this.credentials;
		[Browsable(false)]
		public string UserName {
			get => this.credentials.UserName ?? string.Empty;
			private set {
				this.confirmTarget = null;
				this.credentials.UserName = value;
				this.OnUserNameChanged(EventArgs.Empty);
			}
		}
		[Category("Behavior"), Description("The target for the credentials, typically the server name prefixed by an application-specific identifier."), DefaultValue("")]
		public string Target {
			get => this.target ?? string.Empty;
			set {
				this.target = value;
				this.confirmTarget = null;
			}
		}
		[Localizable(true), Category("Appearance"), Description("The title of the credentials dialog."), DefaultValue("")]
		public string WindowTitle {
			get => this.windowTitle ?? string.Empty;
			set => this.windowTitle = value;
		}
		[Localizable(true), Category("Appearance"), Description("A brief message that will be displayed in the dialog box."), DefaultValue("")]
		public string MainInstruction {
			get => this.caption ?? string.Empty;
			set => this.caption = value;
		}
		[Localizable(true), Category("Appearance"), Description("Additional text to display in the dialog."), DefaultValue(""), Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Content {
			get => this.text ?? string.Empty;
			set => this.text = value;
		}
		[Localizable(true), Category("Appearance"), Description("Indicates how the text of the MainInstruction and Content properties is displayed on Windows XP."), DefaultValue(DownlevelTextMode.MainInstructionAndContent)]
		public DownlevelTextMode DownlevelTextMode { get; set; }
		[Category("Appearance"), Description("Indicates whether a check box is shown on the dialog that allows the user to choose whether to save the credentials or not."), DefaultValue(false)]
		public bool ShowSaveCheckBox { get; set; }
		[Category("Behavior"), Description("Indicates whether the dialog should be displayed even when saved credentials exist for the specified target."), DefaultValue(false)]
		public bool ShowUIForSavedCredentials { get; set; }
		public bool IsStoredCredential { get; private set; }
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public bool ShowDialog() => this.ShowDialog(null);
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public bool ShowDialog(Window owner) {
			if (string.IsNullOrEmpty(this.target)) {
				throw new InvalidOperationException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialEmptyTargetError);
			}

			this.UserName = "";
			this.Password = "";
			this.IsStoredCredential = false;
			if (this.RetrieveCredentialsFromApplicationInstanceCache()) {
				this.IsStoredCredential = true;
				this.confirmTarget = this.Target;
				return true;
			}
			var storedCredentials = false;
			if (this.ShowSaveCheckBox && this.RetrieveCredentials()) {
				this.IsSaveChecked = true;
				if (!this.ShowUIForSavedCredentials) {
					this.IsStoredCredential = true;
					this.confirmTarget = this.Target;
					return true;
				}
				storedCredentials = true;
			}
			var ownerHandle = owner == null ? NativeMethods.GetActiveWindow() : new WindowInteropHelper(owner).Handle;
			bool result;
			if (NativeMethods.IsWindowsVistaOrLater) {
				result = this.PromptForCredentialsCredUIWin(ownerHandle, storedCredentials);
			} else {
				result = this.PromptForCredentialsCredUI(ownerHandle, storedCredentials);
			}

			return result;
		}
		public void ConfirmCredentials(bool confirm) {
			if (this.confirmTarget == null || this.confirmTarget != this.Target) {
				throw new InvalidOperationException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialPromptNotCalled);
			}

			this.confirmTarget = null;
			if (this.IsSaveChecked && confirm) {
				if (this.UseApplicationInstanceCredentialCache) {
					lock (applicationInstanceCredentialCache) {
						applicationInstanceCredentialCache[this.Target] = new System.Net.NetworkCredential(this.UserName, this.Password);
					}
				}
				StoreCredential(this.Target, this.Credentials);
			}
		}
		public static void StoreCredential(string target, NetworkCredential credential) {
			if (target == null) {
				throw new ArgumentNullException("target");
			}

			if (target.Length == 0) {
				throw new ArgumentException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialEmptyTargetError, "target");
			}

			if (credential == null) {
				throw new ArgumentNullException("credential");
			}

			var c = new NativeMethods.CREDENTIAL {
				UserName = credential.UserName,
				TargetName = target,
				Persist = NativeMethods.CredPersist.Enterprise
			};
			var encryptedPassword = EncryptPassword(credential.Password);
			c.CredentialBlob = System.Runtime.InteropServices.Marshal.AllocHGlobal(encryptedPassword.Length);
			try {
				System.Runtime.InteropServices.Marshal.Copy(encryptedPassword, 0, c.CredentialBlob, encryptedPassword.Length);
				c.CredentialBlobSize = (uint)encryptedPassword.Length;
				c.Type = NativeMethods.CredTypes.CRED_TYPE_GENERIC;
				if (!NativeMethods.CredWrite(ref c, 0)) {
					throw new CredentialException(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}
			}
			finally {
				System.Runtime.InteropServices.Marshal.FreeCoTaskMem(c.CredentialBlob);
			}
		}
		public static NetworkCredential RetrieveCredential(string target) {
			if (target == null) {
				throw new ArgumentNullException("target");
			}

			if (target.Length == 0) {
				throw new ArgumentException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialEmptyTargetError, "target");
			}

			var cred = RetrieveCredentialFromApplicationInstanceCache(target);
			if (cred != null) {
				return cred;
			}

			var result = NativeMethods.CredRead(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0, out var credential);
			var error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
			if (result) {
				try {
					var c = (NativeMethods.CREDENTIAL)System.Runtime.InteropServices.Marshal.PtrToStructure(credential, typeof(NativeMethods.CREDENTIAL));
					var encryptedPassword = new byte[c.CredentialBlobSize];
					System.Runtime.InteropServices.Marshal.Copy(c.CredentialBlob, encryptedPassword, 0, encryptedPassword.Length);
					cred = new NetworkCredential(c.UserName, DecryptPassword(encryptedPassword));
				}
				finally {
					NativeMethods.CredFree(credential);
				}
				return cred;
			} else {
				if (error == (int)NativeMethods.CredUIReturnCodes.ERROR_NOT_FOUND) {
					return null;
				} else {
					throw new CredentialException(error);
				}
			}
		}
		public static NetworkCredential RetrieveCredentialFromApplicationInstanceCache(string target) {
			if (target == null) {
				throw new ArgumentNullException("target");
			}

			if (target.Length == 0) {
				throw new ArgumentException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialEmptyTargetError, "target");
			}

			lock (applicationInstanceCredentialCache) {
				if (applicationInstanceCredentialCache.TryGetValue(target, out var cred)) {
					return cred;
				}
			}
			return null;
		}
		public static bool DeleteCredential(string target) {
			if (target == null) {
				throw new ArgumentNullException("target");
			}

			if (target.Length == 0) {
				throw new ArgumentException(Ookii.Dialogs.Wpf.Properties.Resources.CredentialEmptyTargetError, "target");
			}

			var found = false;
			lock (applicationInstanceCredentialCache) {
				found = applicationInstanceCredentialCache.Remove(target);
			}
			if (NativeMethods.CredDelete(target, NativeMethods.CredTypes.CRED_TYPE_GENERIC, 0)) {
				found = true;
			} else {
				var error = Marshal.GetLastWin32Error();
				if (error != (int)NativeMethods.CredUIReturnCodes.ERROR_NOT_FOUND) {
					throw new CredentialException(error);
				}
			}
			return found;
		}
		protected virtual void OnUserNameChanged(EventArgs e) {
			if (UserNameChanged != null) {
				UserNameChanged(this, e);
			}
		}
		protected virtual void OnPasswordChanged(EventArgs e) {
			if (PasswordChanged != null) {
				PasswordChanged(this, e);
			}
		}
		private bool PromptForCredentialsCredUI(IntPtr owner, bool storedCredentials) {
			var info = this.CreateCredUIInfo(owner, true);
			var flags = NativeMethods.CREDUI_FLAGS.GENERIC_CREDENTIALS | NativeMethods.CREDUI_FLAGS.DO_NOT_PERSIST | NativeMethods.CREDUI_FLAGS.ALWAYS_SHOW_UI;
			if (this.ShowSaveCheckBox) {
				flags |= NativeMethods.CREDUI_FLAGS.SHOW_SAVE_CHECK_BOX;
			}

			var user = new StringBuilder(NativeMethods.CREDUI_MAX_USERNAME_LENGTH);
			user.Append(this.UserName);
			var pw = new StringBuilder(NativeMethods.CREDUI_MAX_PASSWORD_LENGTH);
			pw.Append(this.Password);
			var result = NativeMethods.CredUIPromptForCredentials(ref info, this.Target, IntPtr.Zero, 0, user, NativeMethods.CREDUI_MAX_USERNAME_LENGTH, pw, NativeMethods.CREDUI_MAX_PASSWORD_LENGTH, ref this.isSaveChecked, flags);
			switch (result) {
				case NativeMethods.CredUIReturnCodes.NO_ERROR:
					this.UserName = user.ToString();
					this.Password = pw.ToString();
					if (this.ShowSaveCheckBox) {
						this.confirmTarget = this.Target;
						if (storedCredentials && !this.IsSaveChecked) {
							DeleteCredential(this.Target);
						}
					}
					return true;
				case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
					return false;
				default:
					throw new CredentialException((int)result);
			}
		}
		private bool PromptForCredentialsCredUIWin(IntPtr owner, bool storedCredentials) {
			var info = this.CreateCredUIInfo(owner, false);
			var flags = NativeMethods.CredUIWinFlags.Generic;
			if (this.ShowSaveCheckBox) {
				flags |= NativeMethods.CredUIWinFlags.Checkbox;
			}

			var inBuffer = IntPtr.Zero;
			var outBuffer = IntPtr.Zero;
			try {
				uint inBufferSize = 0;
				if (this.UserName.Length > 0) {
					NativeMethods.CredPackAuthenticationBuffer(0, this.UserName, this.Password, IntPtr.Zero, ref inBufferSize);
					if (inBufferSize > 0) {
						inBuffer = Marshal.AllocCoTaskMem((int)inBufferSize);
						if (!NativeMethods.CredPackAuthenticationBuffer(0, this.UserName, this.Password, inBuffer, ref inBufferSize)) {
							throw new CredentialException(Marshal.GetLastWin32Error());
						}
					}
				}
				uint package = 0;
				var result = NativeMethods.CredUIPromptForWindowsCredentials(ref info, 0, ref package, inBuffer, inBufferSize, out outBuffer, out var outBufferSize, ref this.isSaveChecked, flags);
				switch (result) {
					case NativeMethods.CredUIReturnCodes.NO_ERROR:
						var userName = new StringBuilder(NativeMethods.CREDUI_MAX_USERNAME_LENGTH);
						var password = new StringBuilder(NativeMethods.CREDUI_MAX_PASSWORD_LENGTH);
						var userNameSize = (uint)userName.Capacity;
						var passwordSize = (uint)password.Capacity;
						uint domainSize = 0;
						if (!NativeMethods.CredUnPackAuthenticationBuffer(0, outBuffer, outBufferSize, userName, ref userNameSize, null, ref domainSize, password, ref passwordSize)) {
							throw new CredentialException(Marshal.GetLastWin32Error());
						}

						this.UserName = userName.ToString();
						this.Password = password.ToString();
						if (this.ShowSaveCheckBox) {
							this.confirmTarget = this.Target;
							if (storedCredentials && !this.IsSaveChecked) {
								DeleteCredential(this.Target);
							}
						}
						return true;
					case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
						return false;
					default:
						throw new CredentialException((int)result);
				}
			}
			finally {
				if (inBuffer != IntPtr.Zero) {
					Marshal.FreeCoTaskMem(inBuffer);
				}

				if (outBuffer != IntPtr.Zero) {
					Marshal.FreeCoTaskMem(outBuffer);
				}
			}
		}
		private NativeMethods.CREDUI_INFO CreateCredUIInfo(IntPtr owner, bool downlevelText) {
			var info = new NativeMethods.CREDUI_INFO();
			info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
			info.hwndParent = owner;
			if (downlevelText) {
				info.pszCaptionText = this.WindowTitle;
				switch (this.DownlevelTextMode) {
					case DownlevelTextMode.MainInstructionAndContent:
						if (this.MainInstruction.Length == 0) {
							info.pszMessageText = this.Content;
						} else if (this.Content.Length == 0) {
							info.pszMessageText = this.MainInstruction;
						} else {
							info.pszMessageText = this.MainInstruction + Environment.NewLine + Environment.NewLine + this.Content;
						}

						break;
					case DownlevelTextMode.MainInstructionOnly:
						info.pszMessageText = this.MainInstruction;
						break;
					case DownlevelTextMode.ContentOnly:
						info.pszMessageText = this.Content;
						break;
				}
			} else {
				info.pszMessageText = this.Content;
				info.pszCaptionText = this.MainInstruction;
			}
			return info;
		}
		private bool RetrieveCredentials() {
			var credential = RetrieveCredential(this.Target);
			if (credential != null) {
				this.UserName = credential.UserName;
				this.Password = credential.Password;
				return true;
			}
			return false;
		}
		private static byte[] EncryptPassword(string password) {
			var protectedData = System.Security.Cryptography.ProtectedData.Protect(System.Text.Encoding.UTF8.GetBytes(password), null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
			return protectedData;
		}
		private static string DecryptPassword(byte[] encrypted) {
			try {
				return System.Text.Encoding.UTF8.GetString(System.Security.Cryptography.ProtectedData.Unprotect(encrypted, null, System.Security.Cryptography.DataProtectionScope.CurrentUser));
			}
			catch (System.Security.Cryptography.CryptographicException) {
				return string.Empty;
			}
		}
		private bool RetrieveCredentialsFromApplicationInstanceCache() {
			if (this.UseApplicationInstanceCredentialCache) {
				var credential = RetrieveCredentialFromApplicationInstanceCache(this.Target);
				if (credential != null) {
					this.UserName = credential.UserName;
					this.Password = credential.Password;
					return true;
				}
			}
			return false;
		}
	}
}
