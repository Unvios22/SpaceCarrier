using UnityEngine;

namespace Controllers {
	public class CameraController : MonoBehaviour {
		
		[SerializeField] private Camera camera;
		[SerializeField] private float dragSpeed = 1;
		[SerializeField] private float zoomSpeed = 1;
		
		[SerializeField] private float minZoom = 3f;
		[SerializeField] private float maxZoom = 35f;

		private Vector2 _mousePos;
		private Vector2 _mouseScroll;
		
		private Vector2 _dragOrigin;
		private bool _isMouseDown;
		private bool _isPanning;
		
		private void LateUpdate() {
			ReadPlayerInput();
			
			if (_isMouseDown && !_isPanning) {
				_dragOrigin = _mousePos;
				_isPanning = true;
			}
			
			if (_isPanning) {
				ApplyCameraDrag();
			}

			if (_isPanning && !_isMouseDown) {
				_isPanning = false;
			}

			ApplyCameraZoom();
		}

		private void ReadPlayerInput() {
			//TODO: refactor using dedicated input class & the new Unity input system
			_isMouseDown = Input.GetKey(KeyCode.Mouse0);
			var mouseScreenPos = Input.mousePosition;
			_mousePos = camera.ScreenToWorldPoint(mouseScreenPos);

			_mouseScroll = Input.mouseScrollDelta;
		}

		private void ApplyCameraDrag() {
			Vector2 moveVector = _dragOrigin - _mousePos;
			camera.transform.position += new Vector3(moveVector.x, moveVector.y, 0f) * dragSpeed;
		}

		private void ApplyCameraZoom() {
			var zoomChange = _mouseScroll.y * zoomSpeed * -1;
			var cameraSize = camera.orthographicSize;
			var desiredZoom = cameraSize + zoomChange;
			desiredZoom = Mathf.Clamp(desiredZoom, minZoom, maxZoom);

			camera.orthographicSize = desiredZoom;
		}
	}
}
