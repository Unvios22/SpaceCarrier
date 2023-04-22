using System.Diagnostics.CodeAnalysis;
using Model;
using UnityEngine;

namespace Logic.Systems {
	
	public class WorldEntitySystem : System<Model.Entities.WorldEntity> {
		
		private const float EntityMovementScale = 0.004f;
		//TODO: refactor as some settings obj, preferably also visible in editor
		
		protected override void TickEntity(Model.Entities.WorldEntity entity) {
			ApplyMovementLogic(entity);
		}
		
		private void ApplyMovementLogic(Model.Entities.WorldEntity entity) {
			var entityPos = entity.EntityPosition.WorldPosition;
			var entityWorldRot = entity.EntityRotation.WorldRotation;
			//TODO: also update EntityPosition.GridPosition when the grid is implemented

			var entityForwardDirection = Quaternion.Euler(entityWorldRot) * Vector2.right;
			var movementVector = entityForwardDirection * entity.Speed * EntityMovementScale;
			
			entityPos = entityPos + (Vector2) movementVector;
			entity.EntityPosition.WorldPosition = entityPos;
		}
	}
}