using Model.Commands;
using Unity.Mathematics;
using UnityEngine;

namespace Logic.Systems.CommandSystem {
	public class MoveCommandProcessor : CommandProcessor<MoveCommand> {

		private const float MinDistanceThreshold = 0.2f;

		public override CommandProcessingResult ProcessCommand(Model.Entities.WorldEntity entity, MoveCommand command) {
			var distanceToTarget = CalculateDistanceToTarget(entity, command.MoveTarget);
			if (distanceToTarget <= MinDistanceThreshold) {
				//TODO: notify the entity to stop
				//TODO 2: Refactor the logic beLow into MovableEntitySystem, joining it with WorldEntitySystem
				//and have it manage the queue of waypoints, slowing down or speeding up accordingly
				//In hre just receive a move command obj with a flag whether it should queue or overwrite and apply it to nav system
				return CommandProcessingResult.ClearCommand;
			}

			SetEntityRotationTowardsTarget(entity, command.MoveTarget);
			return CommandProcessingResult.ClearCommand;
		}

		private float CalculateDistanceToTarget(Model.Entities.WorldEntity entity, Vector2 targetWorldSpacePos) {
			return Vector3.Distance(entity.EntityPosition.WorldPosition, targetWorldSpacePos);
		}
		
		private void SetEntityRotationTowardsTarget(Model.Entities.WorldEntity entity, Vector2 target) {
			//TODO: add a check or enforce it some other way not to update if the rot was aligned once
			//and no changes happened to justify additional calculations 
			
			var toTargetDirection = (target - entity.EntityPosition.WorldPosition).normalized;
			var desiredRotWithYTowardsTarget = Quaternion.LookRotation(Vector3.forward, toTargetDirection);
			var desiredRot = desiredRotWithYTowardsTarget * Quaternion.Euler(Vector3.forward * 90);
			entity.EntityRotation.WorldRotation = desiredRot.eulerAngles;
			
			//TODO: Should the class calculate the rotation at all? Or should it delegate it to some other class, WorldEntitySystem probably?
		}
	}
}