namespace Ookii.Dialogs.Wpf
{
    partial class ProgressDialog
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            try
            {
                if( disposing )
                {
                    if( components != null )
                        components.Dispose();
                    if( _currentAnimationModuleHandle != null )
                    {
                        _currentAnimationModuleHandle.Dispose();
                        _currentAnimationModuleHandle = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        private void InitializeComponent()
        {
            this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this._backgroundWorker.WorkerReportsProgress = true;
            this._backgroundWorker.WorkerSupportsCancellation = true;
            this._backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this._backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            this._backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._backgroundWorker_ProgressChanged);
        }
        private System.ComponentModel.BackgroundWorker _backgroundWorker;
    }
}
