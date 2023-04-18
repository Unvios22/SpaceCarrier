using Installers.Factories;
using Model;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace View {
	public class ShipMB : SerializedMonoBehaviour, ISelectableEntity {
		
		[SerializeField] private GameObject selectionMarker;
		
		private Ship _ship;

		[Inject]
		private void Init(ShipFactory factory) {
			_ship = factory.Create();
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