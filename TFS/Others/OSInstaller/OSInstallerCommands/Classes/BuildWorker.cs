namespace OSInstallerCommands.Classes
{
	using OSInstallerCommands.Classes.Events;
	using OSInstallerCommands.Zip;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class BuildWorker
	{
		#region Public Constructors
		public BuildWorker(IDictionary<string, object> parameters)
		{
			Parameters = parameters;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Start()
		{

			var tempFileName = (string)Parameters["tempfilename"];
			var zipFileName = (string)Parameters["zipfile"];

			Parameters["zipfile"] = tempFileName;

			var items = Parameters["itemlist"] as Dictionary<string, string>;
			Parameters.Add("filename", null);
			Parameters.Add("fileid", null);
			_Value = 0;
			_Max = items.Count;
			items.Keys.ToList().ForEach(x =>
			{
				var zipCommand = new ZipFileCommand();
				zipCommand.CommandStatusUpdate += zipCommand_CommandStatusUpdate;
				Parameters["fileid"] = x;
				Parameters["filename"] = items[x];
				zipCommand.Execute(Parameters);
				_Value++;
			});
			if (File.Exists(zipFileName))
				File.Delete(zipFileName);
			File.Move(tempFileName, zipFileName);

			if (CommandStatusUpdate != null)
				CommandStatusUpdate(this, new CommandStatusUpdateEventArgs(true));

		}
		#endregion Public Methods

		#region Private Methods
		private void zipCommand_CommandStatusUpdate(object sender, CommandStatusUpdateEventArgs e)
		{
			if (CommandStatusUpdate != null)
				CommandStatusUpdate(this, new CommandStatusUpdateEventArgs(e.Message, _Max, _Value));
		}
		#endregion Private Methods

		#region Public Events
		public event CommandStatusUpdateHandler CommandStatusUpdate;
		#endregion Public Events

		#region Private Fields
		private double _Max;
		private double _Value;
		private IDictionary<string, object> Parameters;
		#endregion Private Fields
	}
}
