using System.Collections.Generic;
using System.Linq;
using Model;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Logic.Interface {
	public class EntitySelectionController : MonoBehaviour {

		[SerializeField] private Camera playerCamera;
		[SerializeField] private SelectionBoxController selectionBoxController;
		[SerializeField] private SelectableEntityPhysicsCaster selectableEntityPhysicsCaster;
		
		private Vector2 _mousePos;
		private Vector2 _selectionOrigin;
		
		private bool _isMouseDown;
		private bool _isSelecting;
		
		private Vector3[] _selectionBoxVertices;
		
		[ShowInInspector]
		private List<ISelectableEntity> _currentlySelectedEntities;

		private void Start() {
			_currentlySelectedEntities = new List<ISelectableEntity>();
		}

		private void Update() {
			ReadPlayerInput();
			
			if (_isMouseDown && !_isSelecting) {
				_selectionOrigin = _mousePos;
				_isSelecting = true;
			}
			if (_isSelecting) {
				DrawSelectionBox();
			}
			if (_isSelecting && !_isMouseDown) {
				_isSelecting = false;
				DetermineSelectedObjects();
				ClearSelectionBox();
			}
		}

		private void ReadPlayerInput() {
			//TODO: refactor using dedicated input class & the new Unity input system
			_isMouseDown = Input.GetKey(KeyCode.Mouse0);
			var mouseScreenPos = Input.mousePosition;
			_mousePos = playerCamera.ScreenToWorldPoint(mouseScreenPos);
		}

		private void DrawSelectionBox() {
			var v1 = _selectionOrigin;
			var v3 = _mousePos;
			var v2 = new Vector2(v3.x, v1.y);
			var v4 = new Vector2(v1.x, v3.y);
			
			_selectionBoxVertices = new Vector3[] { v1, v2, v3, v4 };
			selectionBoxController.SetSelectionBoxVertices(_selectionBoxVertices);
		}

		private void DetermineSelectedObjects() {
			var hitResults = CastSelectionToWorldSpace();
			if (hitResults.IsNullOrEmpty()) {
				return;
			}
			var selectedEntities= FilterHitResults(hitResults);
			_currentlySelectedEntities = selectedEntities;
		}
		
		private List<RaycastHit2D> CastSelectionToWorldSpace() {
			var colliderVertices = _selectionBoxVertices.Select(x => (Vector2)x).ToArray();
			var castDirection = playerCamera.transform.forward;
			
			return selectableEntityPhysicsCaster.CastPolygonToWorldSpace(colliderVertices, castDirection);
		}

		private List<ISelectableEntity> FilterHitResults(List<RaycastHit2D> hitResults) {
			var selectedEntities = new List<ISelectableEntity>();
			
			foreach (var result in hitResults) {
				var resultSelectableEntities = result.transform.GetComponentsInChildren<ISelectableEntity>();
				if (resultSelectableEntities.Length != 0) {
					resultSelectableEntities.ForEach(x => selectedEntities.Add(x));
				}
			}
			return selectedEntities;
		}

		private void ClearSelectionBox() {
			selectionBoxController.ClearSelectionVertices();
		}
	}
}