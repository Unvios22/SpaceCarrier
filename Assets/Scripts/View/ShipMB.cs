using System.Collections.Generic;
using Model;
using Ships;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View {
	public class ShipMB : SerializedMonoBehaviour, ISelectableEntity {
		[SerializeField] private Ship _destroyer;
		[SerializeField] private GameObject selectionMarker;

		private void Start() {
			selectionMarker.SetActive(false);
			_destroyer = new TestDestroyer("shipName", new List<Weapon>(), new List<Model.Personnel>(), 200f, 100);
		}

		public void DisplaySelectionMarker() {
			selectionMarker.SetActive(true);
		}

		public void HideSelectionMarker() {
			selectionMarker.SetActive(false);
		}
	}
}