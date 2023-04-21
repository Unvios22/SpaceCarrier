using System.Diagnostics.CodeAnalysis;
using Model;
using UnityEngine;

namespace Logic.Systems {
	
	public class WorldEntitySystem : System<Model.WorldEntity> {
		
		private const float EntityMovementScale = 0.004f;
		//TODO: refactor as some settings obj, preferably also visible in editor
		
		protected override void TickEntity(Model.WorldEntity entity) {
			ApplyMovementLogic(entity);
		}
		
		private void ApplyMovementLogic(Model.WorldEntity entity) {
			var entityPos = entity.Position.WorldPosition;
			//TODO: also update Position.GridPosition when the grid is implemented
			
			var bearingVector = Quaternion.AngleAxis(entity.Bearing, Vector3.forward) * Vector3.right;
			var movementVector = bearingVector * entity.Speed * EntityMovementScale;

			entityPos = entityPos + movementVector;
			entity.Position.WorldPosition = entityPos;
		}
		
	}
}