using Sirenix.OdinInspector;
using UnityEngine;

namespace View {
	public abstract class WorldEntityMB <T> : SerializedMonoBehaviour where T: Model.WorldEntity {

		[ShowInInspector] protected T Entity;
		
		//TODO implement here a generic inject method that will get injected with factory and set the Entity
		//according to actual WorldEntity inheriting type in inheriting class
		
		public void Update() {
			UpdateEntityInWorldSpace(Entity);
		}

		protected void UpdateEntityInWorldSpace(T entity) {
			UpdateEntityPosition(entity);
			UpdateEntityRotation(entity);
			
		}

		private void UpdateEntityPosition(T entity) {
			transform.position = entity.Position.WorldPosition;
		}

		private void UpdateEntityRotation(T entity) {
			var currentRotation = transform.rotation;
			var desiredRotation = new Vector3(currentRotation.x, currentRotation.y, entity.Bearing);
			transform.rotation = Quaternion.Euler(desiredRotation);
		}
	}
}