using System.Collections.Generic;
using Model.Commands;
using Sirenix.OdinInspector;

namespace Model {
	public class CommandableEntity : WorldEntity, ICommandableEntity {
		
		[ShowInInspector] private List<Command> _currentCommands;

		public CommandableEntity() {
			_currentCommands = new List<Command>();
		}
		
		public void ReceiveCommand(Command command) {
			_currentCommands.Add(command);
		}

		public void ClearCommand(Command command) {
			_currentCommands.Remove(command);
		}

		public List<Command> GetAllCommands() {
			return _currentCommands;
		}
	}
}