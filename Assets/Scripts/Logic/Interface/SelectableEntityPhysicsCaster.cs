using System.Collections.Generic;
using UnityEngine;

namespace Logic.Interface {
	public class SelectableEntityPhysicsCaster : MonoBehaviour {
		[SerializeField] private PolygonCollider2D selectionCollider;

		public List<RaycastHit2D> CastPolygonToWorldSpace(Vector2[] vertices, Vector3 castDirection) {
			Debug.Log("Casting!");
			var hitResults = new List<RaycastHit2D>();
			selectionCollider.points = vertices;
			selectionCollider.Cast(castDirection, new ContactFilter2D().NoFilter(), hitResults);
			Debug.Log("Cast results count: " + hitResults.Count);
			return hitResults;
		}
	}
}