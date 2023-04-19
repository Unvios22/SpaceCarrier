using System.Collections.Generic;
using System.Linq;
using Model;
using Sirenix.Utilities;
using UnityEngine;

namespace Logic.Interface.EntitySelection {
	public class SelectableEntityPhysicsCaster : MonoBehaviour {
		[SerializeField] private PolygonCollider2D selectionCollider;

		public List<ISelectableEntity> CastPolygonForSelectableEntities(Vector2[] vertices, Vector3 castDirection) {
			var hitResults = CastPolygonToWorldSpace(vertices, castDirection);
			var selectableEntities = FilterHitResults(hitResults);
			return selectableEntities;
		}

		public List<ISelectableEntity> CastRayForSelectableEntities(Vector3 origin, Vector3 direction) {
			var hitResults = Physics2D.RaycastAll(origin, direction).ToList();
			var selectableEntities = FilterHitResults(hitResults);
			if (selectableEntities.Count > 0) {
				selectableEntities.RemoveRange(1, selectableEntities.Count - 1);
			}
			return selectableEntities;
		}

		private List<RaycastHit2D> CastPolygonToWorldSpace(Vector2[] vertices, Vector3 castDirection) {
			var hitResults = new List<RaycastHit2D>();
			selectionCollider.points = vertices;
			selectionCollider.Cast(castDirection, new ContactFilter2D().NoFilter(), hitResults);
			return hitResults;
		}
		
		private List<ISelectableEntity> FilterHitResults(List<RaycastHit2D> hitResults) {
			var selectedEntities = new List<ISelectableEntity>();
			
			if (hitResults.IsNullOrEmpty()) {
				return selectedEntities;
			}
			
			foreach (var result in hitResults) {
				var resultSelectableEntities = result.transform.GetComponentsInChildren<ISelectableEntity>();
				if (resultSelectableEntities.Length != 0) {
					resultSelectableEntities.ForEach(x => selectedEntities.Add(x));
				}
			}
			return selectedEntities;
		}
		
	}
}