namespace OSInstallerCommands
{
	using OSInstallerCommands.Classes.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class NullCommand : BaseCommand
	{
		public override event CommandStatusUpdateHandler CommandStatusUpdate;
		public override void Execute()
		{
#if SLEEP
			System.Threading.Thread.Sleep(1000);
#endif
			Status = CommandStatuses.Success;
		}
	}
}
