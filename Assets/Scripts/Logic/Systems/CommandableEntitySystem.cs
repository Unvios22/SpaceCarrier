using System;
using System.Linq;
using Logic.Systems.CommandSystem;
using Model;
using Model.Commands;

namespace Logic.Systems {
	public class CommandableEntitySystem : System<CommandableEntity> {

		private CommandableEntity _currentEntity;
		
		protected override void TickEntity(CommandableEntity entity) {
			_currentEntity = entity;
			ProcessEntityCommands();
			//TODO: implement chain of command or some similar pattern
			//TODO: also implement strategy to divide each command handling into different classes
		}

		private void ProcessEntityCommands() {
			//TODO: decide whether current, actively carried out commands should be kept in the main list
			//or separated somehow from the rest
			
			//TODO: have this process automated, such that the list will be iterated upon and for each
			// of the commands, the type can be extracted and a suitable processor chosen

			var moveCommandProcessor = new MoveCommandProcessor();
			var entityCommands = _currentEntity.GetAllCommands();
			
			//TODO: research a way to remove the ToList to make it more performant
			foreach (var moveCommand in entityCommands.OfType<MoveCommand>().ToList()) {
				var result = moveCommandProcessor.ProcessCommand(_currentEntity, moveCommand);
				HandleProcessorResult(result, moveCommand);
			}
		}

		private void HandleProcessorResult(CommandProcessingResult result, Command command) {
			switch (result) {
				case CommandProcessingResult.KeepCommand:
					break;
				case CommandProcessingResult.ClearCommand:
					_currentEntity.ClearCommand(command);
					break;
				default:
					throw new ArgumentException("Unknown " + nameof(CommandProcessingResult)  + " type!");
			}
		}
	}
}