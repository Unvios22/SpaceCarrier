using System.Collections.Generic;
using Model;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Logic.Interface.EntitySelection {
	public class EntitySelectionController : MonoBehaviour, ISelectionController {

		[SerializeField] private Camera playerCamera;
		[SerializeField] private SelectionBoxController selectionBoxController;
		[SerializeField] private SelectableEntityPhysicsCaster selectableEntityPhysicsCaster;

		private const float BoxSelectionActivationDiagonalLength = 0.7f;
		
		private Vector2 _boxSelectionOriginPos;
		private Vector2[] _selectionBoxVertices;

		private PlayerInputProcessor _playerInputProcessor;

		private bool _isBoxSelectionActivated;
		
		[ShowInInspector]
		private SelectableEntityList<ISelectableEntityMB> _currentlySelectedEntities;

		[Inject]
		private void Init(PlayerInputProcessor playerInputProcessor) {
			//TODO: provide some interface instead of direct class reference?
			//Either on the SelectionController (XYZ consumer) or InputProcessor (XYZ provider) and provide
			//auto subscribe and unsubscribe events accordingly
			
			_playerInputProcessor = playerInputProcessor;
			SubscribeToInputProcessorEvents();
		}

		private void SubscribeToInputProcessorEvents() {
			_playerInputProcessor.EntityClickSelectionPerformed += OnEntityClickSelectionPerformed;
			_playerInputProcessor.EntityBoxSelectionStarted += OnEntityBoxSelectionStarted;
			_playerInputProcessor.EntityBoxSelectionPerformed += OnEntityBoxSelectionPerformed;
			_playerInputProcessor.EntityBoxSelectionCancelled += OnEntityBoxSelectionCancelled;
		}

		private void Start() {
			_currentlySelectedEntities = new SelectableEntityList<ISelectableEntityMB>();
		}

		private void OnEntityClickSelectionPerformed(Vector2 pointerScreenPos, bool isSelectiveSelection) {
			if (_isBoxSelectionActivated) {
				return;
			}
			var pointerWorldPos = ParsePointerScreenToWorldPos(pointerScreenPos);
			Debug.DrawLine(playerCamera.transform.position, pointerWorldPos);
			var entitiesInSelection = CastForClickSelection(pointerWorldPos);
			HandleEntitySelection(entitiesInSelection, isSelectiveSelection);
		}
		
		private void OnEntityBoxSelectionStarted(Vector2 pointerScreenPos) {
			_boxSelectionOriginPos = ParsePointerScreenToWorldPos(pointerScreenPos);
		}
		
		private void OnEntityBoxSelectionPerformed(Vector2 pointerScreenPos) {
			var currentPointerWorldPos = ParsePointerScreenToWorldPos(pointerScreenPos);

			if (!_isBoxSelectionActivated) {
				if (IsPointerPastBoxSelectionThreshold(currentPointerWorldPos)) {
					_isBoxSelectionActivated = true;
				}
			} else {
				UpdateSelectionBoxVertices(currentPointerWorldPos);
				DrawSelectionBox();
			}
		}

		private bool IsPointerPastBoxSelectionThreshold(Vector2 pointerPos) {
			return Vector3.Distance(_boxSelectionOriginPos, pointerPos) >
			       BoxSelectionActivationDiagonalLength;
		}
		
		private void OnEntityBoxSelectionCancelled(Vector2 pointerScreenPos, bool isSelectiveSelection) {
			if (!_isBoxSelectionActivated) {
				return;
			}
			var entitiesInSelection = CastForBoxSelection();
			HandleEntitySelection(entitiesInSelection, isSelectiveSelection);
			HideSelectionBox();
			_isBoxSelectionActivated = false;
		}

		private void UpdateSelectionBoxVertices(Vector2 currentPointerPos) {
			var v1 = _boxSelectionOriginPos;
			var v3 = currentPointerPos;
			var v2 = new Vector2(v3.x, v1.y);
			var v4 = new Vector2(v1.x, v3.y);
			
			_selectionBoxVertices = new[] { v1, v2, v3, v4 };
		}
		
		private void DrawSelectionBox() {
			selectionBoxController.SetSelectionBoxVertices(_selectionBoxVertices);
		}
		
		private void HandleEntitySelection(List<ISelectableEntityMB> entitiesInSelection, bool isSelectiveSelection) {
			if (isSelectiveSelection) {
				HandleSelectiveSelection(entitiesInSelection);
			} else {
				HandleNonselectiveSelection(entitiesInSelection);
			}
		}

		private List<ISelectableEntityMB> CastForBoxSelection() {
			var colliderVertices = _selectionBoxVertices;
			var castDirection = playerCamera.transform.forward;
			return selectableEntityPhysicsCaster.CastPolygonForSelectableEntities(colliderVertices, castDirection);
		}

		private List<ISelectableEntityMB> CastForClickSelection(Vector2 raycastOrigin) {
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
		
		private void HideSelectionBox() {
			selectionBoxController.ClearSelectionVertices();
		}
		
		private Vector2 ParsePointerScreenToWorldPos(Vector2 screenPos) {
			return playerCamera.ScreenToWorldPoint(screenPos);
		}

		public SelectableEntityList<ISelectableEntityMB> CurrentlySelectedEntities => _currentlySelectedEntities;
	}
}