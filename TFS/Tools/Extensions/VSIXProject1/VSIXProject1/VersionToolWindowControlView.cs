namespace VSIXProject1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using VSIXProject1.MVVM;

    public class VersionToolWindowControlView : ViewModelBase
    {
        private AsyncPackage _package = default(AsyncPackage);
        public AsyncPackage Package {
            get => _package;
            set {
                _package = value;
                SetSolution();
                InvokePropertyChanged(nameof(Package));
            }
        }

        private string _solutionName = default(string);
        public string SolutionName {
            get => _solutionName;
            set {
                _solutionName = value;
                InvokePropertyChanged(nameof(SolutionName));
            }
        }

        private async void SetSolution()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(Package.DisposalToken);
            var solution = await Package.GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            var solutionInfo = solution.GetSolutionInfo(out var solutionDir, out var solutionFile, out var solutionOptsFile);
            SolutionName = Path.GetFileNameWithoutExtension(solutionFile);
            //var xDoc = XDocument.Load(solutionFile);
        }
    }
}
