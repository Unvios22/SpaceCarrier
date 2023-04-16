using UnityEngine;

namespace Logic.Interface {
	public class EntitySelectionController : MonoBehaviour {

		[SerializeField] private Camera playerCamera;
		[SerializeField] private SelectionBoxController selectionBoxController;
		
		private Vector2 _mousePos;
		private Vector2 _selectionOrigin;
		
		private bool _isMouseDown;
		private bool _isSelecting;
		
		private Vector3[] _selectionBoxVertices;
		
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
				RetrieveSelectedObjects();
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

		private void RetrieveSelectedObjects() {
			ProjectSelectionToWorldSpace();
		}

		private void ClearSelectionBox() {
			selectionBoxController.ClearSelectionVertices();
		}

		private void ProjectSelectionToWorldSpace() {
			
		}
	}
}