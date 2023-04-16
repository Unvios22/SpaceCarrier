using System.Collections.Generic;
using Model;
using Ships;
using Sirenix.OdinInspector;
using UnityEngine;

namespace View {
	public class ShipMB : SerializedMonoBehaviour, ISelectableEntity {
		[SerializeField] private Ship _destroyer;

		private void Start() {
			_destroyer = new TestDestroyer("shipName", new List<Weapon>(), new List<Model.Personnel>(), 200f, 100);
		}
	}
}