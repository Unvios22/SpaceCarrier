using Sirenix.OdinInspector;
using UnityEngine;
using View.EntityWidgets;

namespace View {
	public abstract class WorldEntityMB <T> : SerializedMonoBehaviour where T: Model.WorldEntity {
		
		//TODO: move to IconDisplayWidget?
		[SerializeField] private Sprite entityIcon;
		[SerializeField] private SpriteRenderer entityIconSlot;
		
		[SerializeField] private NavigationStatsDisplayWidget navigationStatsDisplayWidget;
		
		[ShowInInspector] protected T Entity;

		//TODO implement here a generic inject method that will get injected with factory and set the Entity
		//according to actual WorldEntity inheriting type in inheriting class
		
		private void Start() {
			entityIconSlot.sprite = entityIcon;
		}

		public void Update() {
			UpdateEntityInWorldSpace(Entity);
			navigationStatsDisplayWidget.SetEntityNavigationValues(Entity.Bearing, Entity.Speed);
			AlignEntityIconToCamera();
		}

		protected virtual void UpdateEntityInWorldSpace(T entity) {
			UpdateEntityPosition(entity);
			UpdateEntityRotation(entity);
		}

		private void AlignEntityIconToCamera() {
			entityIconSlot.transform.rotation = Quaternion.identity;
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