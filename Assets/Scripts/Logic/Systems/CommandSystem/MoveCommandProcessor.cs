using Model.Commands;
using UnityEngine;

namespace Logic.Systems.CommandSystem {
	public class MoveCommandProcessor : CommandProcessor<MoveCommand> {

		public override CommandProcessingResult ProcessCommand(Model.WorldEntity entity, MoveCommand command) {
			Debug.Log("Move command processed");
			return CommandProcessingResult.ClearCommand;
			//TODO
		}
		
		private void AlignEntityToTarget() {
			//TODO
		}
	}
}