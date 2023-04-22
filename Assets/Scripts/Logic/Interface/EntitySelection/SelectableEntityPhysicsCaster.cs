using System.Collections.Generic;
using System.Linq;
using Model;
using Sirenix.Utilities;
using UnityEngine;

namespace Logic.Interface.EntitySelection {
	public class SelectableEntityPhysicsCaster : MonoBehaviour {
		[SerializeField] private PolygonCollider2D selectionCollider;

		public List<ISelectableEntityMB> CastPolygonForSelectableEntities(Vector2[] vertices, Vector3 castDirection) {
			var hitResults = CastPolygonToWorldSpace(vertices, castDirection);
			var selectableEntities = FilterHitResults(hitResults);
			return selectableEntities;
		}

		public List<ISelectableEntityMB> CastRayForSelectableEntities(Vector3 origin, Vector3 direction) {
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
		
		private List<ISelectableEntityMB> FilterHitResults(List<RaycastHit2D> hitResults) {
			var selectedEntities = new List<ISelectableEntityMB>();
			
			if (hitResults.IsNullOrEmpty()) {
				return selectedEntities;
			}
			
			foreach (var result in hitResults) {
				var resultSelectableEntities = result.transform.GetComponentsInChildren<ISelectableEntityMB>();
				if (resultSelectableEntities.Length != 0) {
					resultSelectableEntities.ForEach(x => selectedEntities.Add(x));
				}
			}
			return selectedEntities;
		}
	}
}