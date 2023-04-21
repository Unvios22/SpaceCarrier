using System.Collections.Generic;
using Model.Commands;

namespace Model {
	public interface ICommandableEntity {
		public void ReceiveCommand(Command command);
		public void ClearCommand(Command command);
		public List<Command> GetAllCommands();
	}
}