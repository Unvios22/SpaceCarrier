using UnityEngine;

namespace Model.Commands {
	public class MoveCommand : PlayerCommand {

		public Vector2 MoveTarget;

		public MoveCommand(Vector2 moveTarget) {
			MoveTarget = moveTarget;
		}
	}
}