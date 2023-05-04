using System.Collections.Generic;
using Model;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Logic.Interface.EntitySelection {
	public class EntitySelectionController : MonoBehaviour, ISelectionController {

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
		
		private Vector2[] _selectionBoxVertices;
		
		[ShowInInspector]
		private SelectableEntityList<ISelectableEntityMB> _currentlySelectedEntities;

		private void Start() {
			_currentlySelectedEntities = new SelectableEntityList<ISelectableEntityMB>();
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
				UpdateSelectionBoxVertices();
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

		private void UpdateSelectionBoxVertices() {
			var v1 = _selectionOrigin;
			var v3 = _mousePos;
			var v2 = new Vector2(v3.x, v1.y);
			var v4 = new Vector2(v1.x, v3.y);
			
			_selectionBoxVertices = new[] { v1, v2, v3, v4 };
		}
		
		private void DrawSelectionBox() {
			selectionBoxController.SetSelectionBoxVertices(_selectionBoxVertices);
		}

		private void HandleEntitySelection() {
			List<ISelectableEntityMB> entitiesInSelection;
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

		private List<ISelectableEntityMB> DoBoxSelection() {
			var colliderVertices = _selectionBoxVertices;
			var castDirection = playerCamera.transform.forward;
			return selectableEntityPhysicsCaster.CastPolygonForSelectableEntities(colliderVertices, castDirection);
		}

		private List<ISelectableEntityMB> DoClickSelection() {
			var raycastOrigin = _mousePos;
			var raycastDirection = playerCamera.transform.forward;
			return selectableEntityPhysicsCaster.CastRayForSelectableEntities(raycastOrigin, raycastDirection);
		}
		
		private void HandleSelectiveSelection(List<ISelectableEntityMB> entitiesInSelection) {
			if (entitiesInSelection.IsNullOrEmpty()) {
				return;
			}
			var unselectedEntitiesInSelection = entitiesInSelection.FindAll(x => !_currentlySelectedEntities.Contains(x));
				
			if (unselectedEntitiesInSelection.Count > 0) {
				unselectedEntitiesInSelection.ForEach(x => _currentlySelectedEntities.Add(x));
			} else {
				entitiesInSelection.ForEach(x => _currentlySelectedEntities.Remove(x));
			}
		}

		private void HandleNonselectiveSelection(List<ISelectableEntityMB> entitiesInSelection) {
			if (entitiesInSelection.IsNullOrEmpty()) {
				_currentlySelectedEntities.Clear();
			} else {
				_currentlySelectedEntities.Clear();
				_currentlySelectedEntities = new SelectableEntityList<ISelectableEntityMB>(entitiesInSelection);
			}
		}
		
		private void ClearSelectionBox() {
			selectionBoxController.ClearSelectionVertices();
		}

		public SelectableEntityList<ISelectableEntityMB> CurrentlySelectedEntities => _currentlySelectedEntities;
	}
}