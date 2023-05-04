using Installers.Factories;
using Model;
using UnityEngine;
using View.EntityWidgets;
using Zenject;

namespace View {
	public class ShipMB : WorldEntityMB<Ship>, ISelectableEntityMB, ICommandableEntityMB {
		
		//TODO: refactor the naming convention as: (EntityType)ViewMB & I(EntityInterfaceType)View ?
		//TODO: refactor the MB interfaces as data on the Entity object
		
		//TODO: refactor selection marker as widget
		[SerializeField] private GameObject selectionMarker;
		[SerializeField] private NameDisplayWidget nameDisplayWidget;

		[Inject]
		private void Init(ShipFactory factory) {
			Entity = factory.Create();
			nameDisplayWidget.SetEntityName(Entity.Name);
		}

		private void Start() {
			selectionMarker.SetActive(false);
		}

		public void DisplaySelectionMarker() {
			selectionMarker.SetActive(true);
		}

		public void HideSelectionMarker() {
			selectionMarker.SetActive(false);
		}

		public ICommandableEntity GetEntityAsCommandableEntity() {
			return Entity;
		}
	}
}