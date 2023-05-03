namespace GregOsborne.Dialogs
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
                    if( currentAnimationModuleHandle != null )
                    {
                        currentAnimationModuleHandle.Dispose();
                        currentAnimationModuleHandle = null;
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
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
        }
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}
