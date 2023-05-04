using Model.Commands;

namespace Logic.Systems.CommandSystem {
	public abstract class CommandProcessor<T> where T : Command {
		public abstract CommandProcessingResult ProcessCommand(Model.Entities.WorldEntity entity, T command);
	}
}