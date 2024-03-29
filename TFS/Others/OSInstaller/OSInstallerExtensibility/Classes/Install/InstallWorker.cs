namespace OSInstallerExtensibility.Classes.Install
{
	using OSInstallerCommands;
	using OSInstallerCommands.IO;
	using OSInstallerExtensibility.Classes.Data;
	using OSInstallerExtensibility.Events;
	using OSInstallerExtensibility.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	public class InstallWorker
	{
		#region Public Constructors
		public InstallWorker(IInstallerWizardStep step)
		{
			Step = step;
		}
		#endregion Public Constructors

		#region Public Methods
		public void Start()
		{
			var manager = Step.Manager;
			if (InstallationStarted != null)
				InstallationStarted(this, EventArgs.Empty);

			Status = ExecuteCommands(manager.Commands, manager);
			BaseCommand.TempFiles.ToList().ForEach(x =>
			{
				new DeleteFileCommand(new Dictionary<string, object> { { "source", x } }).Execute();
			});

			if (InstallComplete != null)
				InstallComplete(this, new InstallCompleteEventArgs(Status));
		}
		#endregion Public Methods

		#region Private Methods
		private CommandStatuses ExecuteCommands(IList<BaseCommand> commands, IInstallerManager manager)
		{
			CommandStatuses result = CommandStatuses.Success;
			foreach (var command in commands)
			{
				if (InstallerCommandExecuting != null)
					InstallerCommandExecuting(this, new InstallerCommandExecutingEventArgs(command, command.Message, commands.Count, command.Sequence));

				IList<IInstallerData> temp = new List<IInstallerData>();
				manager.Datum.ToList().ForEach(y => temp.Add(y));
				command.Parameters.Keys.ToList().ForEach(y => temp.Add(new InstallerData(y) { Value = command.Parameters[y].ToString() }));
				command.Parameters.Keys.ToList().ForEach(x =>
				{
					if (command.Parameters[x].GetType() == typeof(string))
					{
						var newValue = ((string)command.Parameters[x]).ExpandString(temp, manager.VariableTrigger);
						temp.First(y => y.Name.Equals(x)).Value = newValue;
						command.Parameters[x] = newValue;
					}
				});
				var t = Task.Factory.StartNew(() => command.Execute());
				t.Wait();
				Status = command.Status;
				manager.WriteToLog(string.Format("Command status: {0}", Status.ToString()));
				if (command.Commands.Any())
					result = ExecuteCommands(command.Commands, manager);
				if (Status == CommandStatuses.Failure)
				{
					manager.WriteToLog(string.Format("Error: {0}", command.Result));
					break;
				}
			}
			return result;
		}
		#endregion Private Methods

		#region Public Events
		public event EventHandler InstallationStarted;
		public event InstallCompleteHandler InstallComplete;
		public event InstallerCommandExecutingHandler InstallerCommandExecuting;
		#endregion Public Events

		#region Public Properties
		public CommandStatuses Status { get; private set; }
		#endregion Public Properties

		#region Private Properties
		private IInstallerWizardStep Step { get; set; }
		#endregion Private Properties
	}
}
