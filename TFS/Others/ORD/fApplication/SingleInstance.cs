// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc.. All rights reserved.
// Created by: Greg
// -----------------------------------------------------------------------
// 
// Functionality for single-instance application using mutex
//
namespace Microsoft.Shell
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Runtime.Remoting;
	using System.Runtime.Remoting.Channels;
	using System.Runtime.Remoting.Channels.Ipc;
	using System.Runtime.Serialization.Formatters;
	using System.Security;
	using System.Threading;
	using System.Windows;
	using System.Windows.Threading;

	/// <summary>
	/// Enum WM
	/// </summary>
	internal enum WM
	{
		/// <summary>
		/// The null
		/// </summary>
		NULL = 0x0000,
		/// <summary>
		/// The create
		/// </summary>
		CREATE = 0x0001,
		/// <summary>
		/// The destroy
		/// </summary>
		DESTROY = 0x0002,
		/// <summary>
		/// The move
		/// </summary>
		MOVE = 0x0003,
		/// <summary>
		/// The size
		/// </summary>
		SIZE = 0x0005,
		/// <summary>
		/// The activate
		/// </summary>
		ACTIVATE = 0x0006,
		/// <summary>
		/// The setfocus
		/// </summary>
		SETFOCUS = 0x0007,
		/// <summary>
		/// The killfocus
		/// </summary>
		KILLFOCUS = 0x0008,
		/// <summary>
		/// The enable
		/// </summary>
		ENABLE = 0x000A,
		/// <summary>
		/// The setredraw
		/// </summary>
		SETREDRAW = 0x000B,
		/// <summary>
		/// The settext
		/// </summary>
		SETTEXT = 0x000C,
		/// <summary>
		/// The gettext
		/// </summary>
		GETTEXT = 0x000D,
		/// <summary>
		/// The gettextlength
		/// </summary>
		GETTEXTLENGTH = 0x000E,
		/// <summary>
		/// The paint
		/// </summary>
		PAINT = 0x000F,
		/// <summary>
		/// The close
		/// </summary>
		CLOSE = 0x0010,
		/// <summary>
		/// The queryendsession
		/// </summary>
		QUERYENDSESSION = 0x0011,
		/// <summary>
		/// The quit
		/// </summary>
		QUIT = 0x0012,
		/// <summary>
		/// The queryopen
		/// </summary>
		QUERYOPEN = 0x0013,
		/// <summary>
		/// The erasebkgnd
		/// </summary>
		ERASEBKGND = 0x0014,
		/// <summary>
		/// The syscolorchange
		/// </summary>
		SYSCOLORCHANGE = 0x0015,
		/// <summary>
		/// The showwindow
		/// </summary>
		SHOWWINDOW = 0x0018,
		/// <summary>
		/// The activateapp
		/// </summary>
		ACTIVATEAPP = 0x001C,
		/// <summary>
		/// The setcursor
		/// </summary>
		SETCURSOR = 0x0020,
		/// <summary>
		/// The mouseactivate
		/// </summary>
		MOUSEACTIVATE = 0x0021,
		/// <summary>
		/// The childactivate
		/// </summary>
		CHILDACTIVATE = 0x0022,
		/// <summary>
		/// The queuesync
		/// </summary>
		QUEUESYNC = 0x0023,
		/// <summary>
		/// The getminmaxinfo
		/// </summary>
		GETMINMAXINFO = 0x0024,
		/// <summary>
		/// The windowposchanging
		/// </summary>
		WINDOWPOSCHANGING = 0x0046,
		/// <summary>
		/// The windowposchanged
		/// </summary>
		WINDOWPOSCHANGED = 0x0047,
		/// <summary>
		/// The contextmenu
		/// </summary>
		CONTEXTMENU = 0x007B,
		/// <summary>
		/// The stylechanging
		/// </summary>
		STYLECHANGING = 0x007C,
		/// <summary>
		/// The stylechanged
		/// </summary>
		STYLECHANGED = 0x007D,
		/// <summary>
		/// The displaychange
		/// </summary>
		DISPLAYCHANGE = 0x007E,
		/// <summary>
		/// The geticon
		/// </summary>
		GETICON = 0x007F,
		/// <summary>
		/// The seticon
		/// </summary>
		SETICON = 0x0080,
		/// <summary>
		/// The nccreate
		/// </summary>
		NCCREATE = 0x0081,
		/// <summary>
		/// The ncdestroy
		/// </summary>
		NCDESTROY = 0x0082,
		/// <summary>
		/// The nccalcsize
		/// </summary>
		NCCALCSIZE = 0x0083,
		/// <summary>
		/// The nchittest
		/// </summary>
		NCHITTEST = 0x0084,
		/// <summary>
		/// The ncpaint
		/// </summary>
		NCPAINT = 0x0085,
		/// <summary>
		/// The ncactivate
		/// </summary>
		NCACTIVATE = 0x0086,
		/// <summary>
		/// The getdlgcode
		/// </summary>
		GETDLGCODE = 0x0087,
		/// <summary>
		/// The syncpaint
		/// </summary>
		SYNCPAINT = 0x0088,
		/// <summary>
		/// The ncmousemove
		/// </summary>
		NCMOUSEMOVE = 0x00A0,
		/// <summary>
		/// The nclbuttondown
		/// </summary>
		NCLBUTTONDOWN = 0x00A1,
		/// <summary>
		/// The nclbuttonup
		/// </summary>
		NCLBUTTONUP = 0x00A2,
		/// <summary>
		/// The nclbuttondblclk
		/// </summary>
		NCLBUTTONDBLCLK = 0x00A3,
		/// <summary>
		/// The ncrbuttondown
		/// </summary>
		NCRBUTTONDOWN = 0x00A4,
		/// <summary>
		/// The ncrbuttonup
		/// </summary>
		NCRBUTTONUP = 0x00A5,
		/// <summary>
		/// The ncrbuttondblclk
		/// </summary>
		NCRBUTTONDBLCLK = 0x00A6,
		/// <summary>
		/// The ncmbuttondown
		/// </summary>
		NCMBUTTONDOWN = 0x00A7,
		/// <summary>
		/// The ncmbuttonup
		/// </summary>
		NCMBUTTONUP = 0x00A8,
		/// <summary>
		/// The ncmbuttondblclk
		/// </summary>
		NCMBUTTONDBLCLK = 0x00A9,
		/// <summary>
		/// The syskeydown
		/// </summary>
		SYSKEYDOWN = 0x0104,
		/// <summary>
		/// The syskeyup
		/// </summary>
		SYSKEYUP = 0x0105,
		/// <summary>
		/// The syschar
		/// </summary>
		SYSCHAR = 0x0106,
		/// <summary>
		/// The sysdeadchar
		/// </summary>
		SYSDEADCHAR = 0x0107,
		/// <summary>
		/// The command
		/// </summary>
		COMMAND = 0x0111,
		/// <summary>
		/// The syscommand
		/// </summary>
		SYSCOMMAND = 0x0112,
		/// <summary>
		/// The mousemove
		/// </summary>
		MOUSEMOVE = 0x0200,
		/// <summary>
		/// The lbuttondown
		/// </summary>
		LBUTTONDOWN = 0x0201,
		/// <summary>
		/// The lbuttonup
		/// </summary>
		LBUTTONUP = 0x0202,
		/// <summary>
		/// The lbuttondblclk
		/// </summary>
		LBUTTONDBLCLK = 0x0203,
		/// <summary>
		/// The rbuttondown
		/// </summary>
		RBUTTONDOWN = 0x0204,
		/// <summary>
		/// The rbuttonup
		/// </summary>
		RBUTTONUP = 0x0205,
		/// <summary>
		/// The rbuttondblclk
		/// </summary>
		RBUTTONDBLCLK = 0x0206,
		/// <summary>
		/// The mbuttondown
		/// </summary>
		MBUTTONDOWN = 0x0207,
		/// <summary>
		/// The mbuttonup
		/// </summary>
		MBUTTONUP = 0x0208,
		/// <summary>
		/// The mbuttondblclk
		/// </summary>
		MBUTTONDBLCLK = 0x0209,
		/// <summary>
		/// The mousewheel
		/// </summary>
		MOUSEWHEEL = 0x020A,
		/// <summary>
		/// The xbuttondown
		/// </summary>
		XBUTTONDOWN = 0x020B,
		/// <summary>
		/// The xbuttonup
		/// </summary>
		XBUTTONUP = 0x020C,
		/// <summary>
		/// The xbuttondblclk
		/// </summary>
		XBUTTONDBLCLK = 0x020D,
		/// <summary>
		/// The mousehwheel
		/// </summary>
		MOUSEHWHEEL = 0x020E,
		/// <summary>
		/// The capturechanged
		/// </summary>
		CAPTURECHANGED = 0x0215,
		/// <summary>
		/// The entersizemove
		/// </summary>
		ENTERSIZEMOVE = 0x0231,
		/// <summary>
		/// The exitsizemove
		/// </summary>
		EXITSIZEMOVE = 0x0232,
		/// <summary>
		/// The im e_ setcontext
		/// </summary>
		IME_SETCONTEXT = 0x0281,
		/// <summary>
		/// The im e_ notify
		/// </summary>
		IME_NOTIFY = 0x0282,
		/// <summary>
		/// The im e_ control
		/// </summary>
		IME_CONTROL = 0x0283,
		/// <summary>
		/// The im e_ compositionfull
		/// </summary>
		IME_COMPOSITIONFULL = 0x0284,
		/// <summary>
		/// The im e_ select
		/// </summary>
		IME_SELECT = 0x0285,
		/// <summary>
		/// The im e_ character
		/// </summary>
		IME_CHAR = 0x0286,
		/// <summary>
		/// The im e_ request
		/// </summary>
		IME_REQUEST = 0x0288,
		/// <summary>
		/// The im e_ keydown
		/// </summary>
		IME_KEYDOWN = 0x0290,
		/// <summary>
		/// The im e_ keyup
		/// </summary>
		IME_KEYUP = 0x0291,
		/// <summary>
		/// The ncmouseleave
		/// </summary>
		NCMOUSELEAVE = 0x02A2,
		/// <summary>
		/// The dwmcompositionchanged
		/// </summary>
		DWMCOMPOSITIONCHANGED = 0x031E,
		/// <summary>
		/// The dwmncrenderingchanged
		/// </summary>
		DWMNCRENDERINGCHANGED = 0x031F,
		/// <summary>
		/// The dwmcolorizationcolorchanged
		/// </summary>
		DWMCOLORIZATIONCOLORCHANGED = 0x0320,
		/// <summary>
		/// The dwmwindowmaximizedchange
		/// </summary>
		DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
		/// <summary>
		/// The dwmsendiconicthumbnail
		/// </summary>
		DWMSENDICONICTHUMBNAIL = 0x0323,
		/// <summary>
		/// The dwmsendiconiclivepreviewbitmap
		/// </summary>
		DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,
		/// <summary>
		/// The user
		/// </summary>
		USER = 0x0400,
		/// <summary>
		/// The traymousemessage
		/// </summary>
		TRAYMOUSEMESSAGE = 0x800, //WM_USER + 1024
		/// <summary>
		/// The application
		/// </summary>
		APP = 0x8000,
	}

	/// <summary>
	/// Interface ISingleInstanceApp
	/// </summary>
	public interface ISingleInstanceApp
	{
		#region Public Methods
		/// <summary>
		/// Signals the external command line arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		bool SignalExternalCommandLineArgs(IList<string> args);
		#endregion Public Methods
	}

	/// <summary>
	/// Class SingleInstance.
	/// </summary>
	/// <typeparam name="TApplication">The type of the t application.</typeparam>
	public static class SingleInstance<TApplication> where TApplication : Application, ISingleInstanceApp
	{
		#region Public Methods
		/// <summary>
		/// Cleanups this instance.
		/// </summary>
		public static void Cleanup()
		{
			if (singleInstanceMutex != null)
			{
				singleInstanceMutex.Close();
				singleInstanceMutex = null;
			}
			if (channel != null)
			{
				ChannelServices.UnregisterChannel(channel);
				channel = null;
			}
		}
		/// <summary>
		/// Initializes as first instance.
		/// </summary>
		/// <param name="uniqueName">Name of the unique.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool InitializeAsFirstInstance(string uniqueName)
		{
			commandLineArgs = GetCommandLineArgs(uniqueName);
			string applicationIdentifier = uniqueName + Environment.UserName;
			string channelName = String.Concat(applicationIdentifier, Delimiter, ChannelNameSuffix);
			bool firstInstance;
			singleInstanceMutex = new Mutex(true, applicationIdentifier, out firstInstance);
			if (firstInstance)
			{
				CreateRemoteService(channelName);
			}
			else
			{
				SignalFirstInstance(channelName, commandLineArgs);
			}
			return firstInstance;
		}
		#endregion Public Methods

		#region Private Methods
		private static void ActivateFirstInstance(IList<string> args)
		{
			if (Application.Current == null)
			{
				return;
			}
			((TApplication)Application.Current).SignalExternalCommandLineArgs(args);
		}
		private static object ActivateFirstInstanceCallback(object arg)
		{
			IList<string> args = arg as IList<string>;
			ActivateFirstInstance(args);
			return null;
		}
		private static void CreateRemoteService(string channelName)
		{
			BinaryServerFormatterSinkProvider serverProvider = new BinaryServerFormatterSinkProvider();
			serverProvider.TypeFilterLevel = TypeFilterLevel.Full;
			IDictionary props = new Dictionary<string, string>();
			props["name"] = channelName;
			props["portName"] = channelName;
			props["exclusiveAddressUse"] = "false";
			channel = new IpcServerChannel(props, serverProvider);
			ChannelServices.RegisterChannel(channel, true);
			IPCRemoteService remoteService = new IPCRemoteService();
			RemotingServices.Marshal(remoteService, RemoteServiceName);
		}
		private static IList<string> GetCommandLineArgs(string uniqueApplicationName)
		{
			string[] args = null;
			if (AppDomain.CurrentDomain.ActivationContext == null)
			{
				args = Environment.GetCommandLineArgs();
			}
			else
			{
				string appFolderPath = Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), uniqueApplicationName);
				string cmdLinePath = Path.Combine(appFolderPath, "cmdline.txt");
				if (File.Exists(cmdLinePath))
				{
					try
					{
						using (TextReader reader = new StreamReader(cmdLinePath, System.Text.Encoding.Unicode))
						{
							args = NativeMethods.CommandLineToArgvW(reader.ReadToEnd());
						}
						File.Delete(cmdLinePath);
					}
					catch (IOException)
					{
					}
				}
			}
			if (args == null)
			{
				args = new string[] { };
			}
			return new List<string>(args);
		}
		private static void SignalFirstInstance(string channelName, IList<string> args)
		{
			IpcClientChannel secondInstanceChannel = new IpcClientChannel();
			ChannelServices.RegisterChannel(secondInstanceChannel, true);
			string remotingServiceUrl = IpcProtocol + channelName + "/" + RemoteServiceName;
			IPCRemoteService firstInstanceRemoteServiceReference = (IPCRemoteService)RemotingServices.Connect(typeof(IPCRemoteService), remotingServiceUrl);
			if (firstInstanceRemoteServiceReference != null)
			{
				firstInstanceRemoteServiceReference.InvokeFirstInstance(args);
			}
		}
		#endregion Private Methods

		#region Private Fields
		private const string ChannelNameSuffix = "SingeInstanceIPCChannel";
		private const string Delimiter = ":";
		private const string IpcProtocol = "ipc://";
		private const string RemoteServiceName = "SingleInstanceApplicationService";
		private static IpcServerChannel channel;
		private static IList<string> commandLineArgs;
		private static Mutex singleInstanceMutex;
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Gets the command line arguments.
		/// </summary>
		/// <value>The command line arguments.</value>
		public static IList<string> CommandLineArgs
		{
			get { return commandLineArgs; }
		}
		#endregion Public Properties

		#region Private Classes

		/// <summary>
		/// Class IPCRemoteService.
		/// </summary>
		private class IPCRemoteService : MarshalByRefObject
		{
			#region Public Methods
			/// <summary>
			/// Obtains a lifetime service object to control the lifetime policy for this instance.
			/// </summary>
			/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.</returns>
			/// <PermissionSet>
			///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
			/// </PermissionSet>
			public override object InitializeLifetimeService()
			{
				return null;
			}
			/// <summary>
			/// Invokes the first instance.
			/// </summary>
			/// <param name="args">The arguments.</param>
			public void InvokeFirstInstance(IList<string> args)
			{
				if (Application.Current != null)
				{
					Application.Current.Dispatcher.BeginInvoke(
						DispatcherPriority.Normal, new DispatcherOperationCallback(SingleInstance<TApplication>.ActivateFirstInstanceCallback), args);
				}
			}
			#endregion Public Methods
		}

		#endregion Private Classes
	}

	/// <summary>
	/// Class NativeMethods.
	/// </summary>
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		#region Public Delegates
		/// <summary>
		/// Delegate MessageHandler
		/// </summary>
		/// <param name="uMsg">The u MSG.</param>
		/// <param name="wParam">The w parameter.</param>
		/// <param name="lParam">The l parameter.</param>
		/// <param name="handled">if set to <c>true</c> [handled].</param>
		/// <returns>IntPtr.</returns>
		public delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
		#endregion Public Delegates

		#region Public Methods
		/// <summary>
		/// Commands the line to argv w.
		/// </summary>
		/// <param name="cmdLine">The command line.</param>
		/// <returns>System.String[].</returns>
		/// <exception cref="System.ComponentModel.Win32Exception"></exception>
		/// <exception cref="Win32Exception"></exception>
		public static string[] CommandLineToArgvW(string cmdLine)
		{
			IntPtr argv = IntPtr.Zero;
			try
			{
				int numArgs = 0;
				argv = _CommandLineToArgvW(cmdLine, out numArgs);
				if (argv == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				var result = new string[numArgs];
				for (int i = 0; i < numArgs; i++)
				{
					IntPtr currArg = Marshal.ReadIntPtr(argv, i * Marshal.SizeOf(typeof(IntPtr)));
					result[i] = Marshal.PtrToStringUni(currArg);
				}
				return result;
			}
			finally
			{
				IntPtr p = _LocalFree(argv);
			}
		}
		#endregion Public Methods

		#region Private Methods
		[DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
		private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);
		[DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
		private static extern IntPtr _LocalFree(IntPtr hMem);
		#endregion Private Methods
	}
}