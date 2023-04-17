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

		private const float DragDistanceToStartBoxSelection = 2f;
		
		private Vector2 _mousePos;
		private Vector2 _selectionOrigin;
		
		private bool _isMouseDown;
		private bool _isSelecting;
		private bool _isDrawingBox;
		private bool _isSelectiveSelection;
		
		private Vector3[] _selectionBoxVertices;
		
		[ShowInInspector]
		private SelectableEntityList<ISelectableEntity> _currentlySelectedEntities;

		private void Start() {
			_currentlySelectedEntities = new SelectableEntityList<ISelectableEntity>();
		}

		private void Update() {
			ReadPlayerInput();
			
			//TODO: refactor this as a state machine or at least an enum
			
			if (_isMouseDown && !_isSelecting) {
				_selectionOrigin = _mousePos;
				_isSelecting = true;
			}
			
			if (_isSelecting) {
				var distanceDragged = Vector2.Distance(_selectionOrigin, _mousePos);
				if (distanceDragged > DragDistanceToStartBoxSelection) {
					_isDrawingBox = true;
				}
			}

			if (_isDrawingBox) {
				DrawSelectionBox();
			}
			
			if (_isSelecting && !_isMouseDown) {
				HandleEntitySelection();
				ClearSelectionBox();
				_isSelecting = false;
				_isDrawingBox = false;
			}
		}

		private void ReadPlayerInput() {
			//TODO: refactor using dedicated input class & the new Unity input system
			_isMouseDown = Input.GetKey(KeyCode.Mouse0);
			var mouseScreenPos = Input.mousePosition;
			_mousePos = playerCamera.ScreenToWorldPoint(mouseScreenPos);
			_isSelectiveSelection = Input.GetKey(KeyCode.LeftShift);
		}

		private void DrawSelectionBox() {
			var v1 = _selectionOrigin;
			var v3 = _mousePos;
			var v2 = new Vector2(v3.x, v1.y);
			var v4 = new Vector2(v1.x, v3.y);
			
			_selectionBoxVertices = new Vector3[] { v1, v2, v3, v4 };
			selectionBoxController.SetSelectionBoxVertices(_selectionBoxVertices);
		}

		private void HandleEntitySelection() {
			List<ISelectableEntity> entitiesInSelection;
			if (_isDrawingBox) {
				entitiesInSelection = DoBoxSelection();
			} else {
				entitiesInSelection = DoClickSelection();
			}
			
			if (_isSelectiveSelection) {
				HandleSelectiveSelection(entitiesInSelection);
			} else {
				HandleNonselectiveSelection(entitiesInSelection);
			}
		}

		private List<ISelectableEntity> DoBoxSelection() {
			var colliderVertices = _selectionBoxVertices.Select(x => (Vector2)x).ToArray();
			var castDirection = playerCamera.transform.forward;
			return selectableEntityPhysicsCaster.CastPolygonForSelectableEntities(colliderVertices, castDirection);
		}

		private List<ISelectableEntity> DoClickSelection() {
			var raycastOrigin = _mousePos;
			var raycastDirection = playerCamera.transform.forward;
			return selectableEntityPhysicsCaster.CastRayForSelectableEntities(raycastOrigin, raycastDirection);
		}
		
		private void HandleSelectiveSelection(List<ISelectableEntity> entitiesInSelection) {
			if (entitiesInSelection.IsNullOrEmpty()) {
				return;
			}
			InvertSelectionStatus(entitiesInSelection);
			
			//TODO: refactor to be more adaptable - if selective and there are both selected and unselected units in selection, only add more
			//if selective and only already selected units are in selection, then remove them
		}

		private void HandleNonselectiveSelection(List<ISelectableEntity> entitiesInSelection) {
			if (entitiesInSelection.IsNullOrEmpty()) {
				_currentlySelectedEntities.Clear();
			} else {
				_currentlySelectedEntities.Clear();
				_currentlySelectedEntities = new SelectableEntityList<ISelectableEntity>(entitiesInSelection);
			}
		}

		private void InvertSelectionStatus(List<ISelectableEntity> entities) {
			foreach (var entity in entities) {
				if (_currentlySelectedEntities.Contains(entity)) {
					_currentlySelectedEntities.Remove(entity);
				} else {
					_currentlySelectedEntities.Add(entity);
				}
			}
		}
		
		private void ClearSelectionBox() {
			selectionBoxController.ClearSelectionVertices();
		}
	}
}