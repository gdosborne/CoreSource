namespace VSIXProject1
{
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using System;
    using System.ComponentModel.Design;
    using Task = System.Threading.Tasks.Task;

    internal sealed class Command1 : IVsSolutionEvents
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("ed629b2c-ffd2-44e5-ad15-32d5b6fafc2f");

        private readonly AsyncPackage package;

        private readonly OleMenuCommand menuItem;

        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            this.menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            this.menuItem.BeforeQueryStatus += this.OnBeforeQueryStatus;
            commandService.AddCommand(this.menuItem);
        }

        public static Command1 Instance {
            get;
            private set;
        }

        private static uint solutionCookie;

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            var solution = await package.GetServiceAsync((typeof(SVsSolution))) as IVsSolution;

            Instance = new Command1(package, commandService);
            Instance.menuItem.Enabled = Instance.IsSolutionLoaded;
            if (solution != null)
                solution.AdviseSolutionEvents(Instance, out var dwCookie);

            HideToolWindow(package);
        }

        private static async void ShowToolWindow(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            try
            {
                var window = package.FindToolWindow(typeof(VersionToolWindow), 0, true);
                if ((null == window) || (null == window.Frame))
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
                ((window as VersionToolWindow).Content as VersionToolWindowControl).View.Package = package;
                var windowFrame = (IVsWindowFrame)window.Frame;
                ErrorHandler.ThrowOnFailure(windowFrame.Show());
            }
            catch { }
        }

        private static async void HideToolWindow(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            try
            {
                var window = package.FindToolWindow(typeof(VersionToolWindow), 0, true);
                if ((null == window) || (null == window.Frame))
                {
                    throw new NotSupportedException("Cannot create tool window");
                }
                var windowFrame = (IVsWindowFrame)window.Frame;
                ErrorHandler.ThrowOnFailure(windowFrame.Hide());
            }
            catch { }
        }

        private async void Execute(object sender, EventArgs e) => ShowToolWindow(this.package);

        private bool IsSolutionLoaded => false;

        private void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            var theCommand = sender as OleMenuCommand;
            //theCommand.Enabled = IsSolutionLoaded;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded) => VSConstants.S_OK;
        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel) => VSConstants.S_OK;
        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved) => VSConstants.S_OK;
        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy) => VSConstants.S_OK;
        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel) => VSConstants.S_OK;
        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy) => VSConstants.S_OK;
        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            this.menuItem.Enabled = true;
            ShowToolWindow(this.package);
            return VSConstants.S_OK;
        }
        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            this.menuItem.Enabled = false;
            HideToolWindow(this.package);
            return VSConstants.S_OK;
        }
        public int OnBeforeCloseSolution(object pUnkReserved) => VSConstants.S_OK;
        public int OnAfterCloseSolution(object pUnkReserved) => VSConstants.S_OK;
    }
}
