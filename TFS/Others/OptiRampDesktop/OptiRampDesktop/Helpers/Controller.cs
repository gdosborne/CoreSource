using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OptiRampDesktop.Helpers
{
	public class Controller : IDisposable
	{
		#region Private Fields
		private System.Threading.Timer PeekTimer = null;
		#endregion

		#region Public Delegates

		public delegate void ControllerHandler(object sender, ControllerEventArgs e);

		#endregion

		#region Public Events
		public event ControllerHandler CanvasSizeChangeRequest;
		#endregion

		#region Public Properties
		public bool Cancelled { get; set; }
		public List<FrameworkElement> CanvasControls { get; set; }
		#endregion

		#region Public Methods

		public void Dispose()
		{
			Dispose(true);
		}

		public void Start(List<FrameworkElement> canvasControls)
		{
			CanvasControls = canvasControls;
			Start();
		}

		public void Start()
		{
			BackgroundWorker worker = new BackgroundWorker
			{
				WorkerReportsProgress = false,
				WorkerSupportsCancellation = true
			};
			worker.DoWork += worker_DoWork;
			worker.RunWorkerCompleted += worker_RunWorkerCompleted;
			worker.RunWorkerAsync();
		}

		#endregion

		#region Protected Methods

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				PeekTimer.Dispose();
				PeekTimer = null;
			}
		}

		#endregion

		#region Private Methods

		private void CheckSize()
		{
			double maxWidth = 0.0;
			double maxHeight = 0.0;
			CanvasControls.ForEach(x =>
			{
				var left = (double)x.GetValue(Canvas.LeftProperty);
				var top = (double)x.GetValue(Canvas.TopProperty);
				var right = left + x.ActualWidth;
				var bottom = top + x.ActualHeight;
				maxWidth = right > maxWidth ? right : maxWidth;
				maxHeight = bottom > maxHeight ? bottom : maxHeight;
			});
			if (CanvasSizeChangeRequest != null)
				CanvasSizeChangeRequest(this, new ControllerEventArgs(maxWidth, maxHeight));
		}

		private void CheckSize(object stateInfo)
		{
			if (CanvasControls == null || !CanvasControls.Any())
				return;
			DispatchService.Invoke(new Action(CheckSize));
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			PeekTimer = new System.Threading.Timer(CheckSize, null, 100, 100);
			while (!Cancelled)
			{
				System.Threading.Thread.Sleep(10);
			}
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		#endregion

		#region Public Classes

		public class ControllerEventArgs : EventArgs
		{
			#region Public Constructors

			public ControllerEventArgs(double width, double height)
			{
				Width = width;
				Height = height;
			}

			#endregion

			#region Public Properties
			public double Height { get; private set; }
			public double Width { get; private set; }
			#endregion
		}

		#endregion
	}
}