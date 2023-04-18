using Installers.Factories;
using Model;
using UnityEngine;
using Zenject;

namespace View {
	public class ShipMB : WorldEntityMB<Ship>, ISelectableEntity {
		
		[SerializeField] private GameObject selectionMarker;

		[Inject]
		private void Init(ShipFactory factory) {
			Entity = factory.Create();
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
	}
}