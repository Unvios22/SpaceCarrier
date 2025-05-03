using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Logic.Interface {
	public class CameraController : MonoBehaviour {
		
		[SerializeField] private Camera playerCamera;
		[SerializeField] private float dragSpeed = 1;
		[SerializeField] private float zoomSpeed = 1;
		
		[SerializeField] private float minZoom = 3f;
		[SerializeField] private float maxZoom = 35f;

		[SerializeField] private float zoomCursorConvergenceStrength = 0.1f;

		private Vector2 _mousePos;
		private Vector2 _mouseScroll;
		
		private Vector2 _dragOrigin;
		private bool _isMouseDown;
		private bool _isPanning;

		private PlayerInputProcessor _inputProcessor;
		
		[Inject]
		private void Init(PlayerInputProcessor inputProcessor) {
			_inputProcessor = inputProcessor;
			//TODO: subscribe to events or otherwise prepare to receive callbacks/info
		}

		private void OnMousePos(InputAction.CallbackContext ctx) {
		}
		
		private void OnCameraMove(InputAction.CallbackContext ctx) {
			Debug.Log("Received CameraMove!");
			var result = ctx.action.ReadValue<Vector2>();
			ApplyCameraMove(result);
			Debug.Log(result);
		}

		private void ApplyCameraMove(Vector2 moveVector) {
			playerCamera.transform.position += new Vector3(moveVector.x, moveVector.y, 0f) * dragSpeed;
		}
		
		private void LateUpdate() {
			// ReadPlayerInput();
			//
			// if (_isMouseDown && !_isPanning) {
			// 	_dragOrigin = _mousePos;
			// 	_isPanning = true;
			// }
			//
			// if (_isPanning) {
			// 	ApplyCameraDrag();
			// }
			//
			// if (_isPanning && !_isMouseDown) {
			// 	_isPanning = false;
			// }
			//
			// ApplyCameraZoom();
			//
			// if (_mouseScroll.y > 0f) {
			// 	ApplyCameraZoomMove();
			// }
		}

		private void ReadPlayerInput() {
			//TODO: refactor using dedicated input class & the new Unity input system
			_isMouseDown = Input.GetKey(KeyCode.Mouse2);
			var mouseScreenPos = Input.mousePosition;
			_mousePos = playerCamera.ScreenToWorldPoint(mouseScreenPos);

			_mouseScroll = Input.mouseScrollDelta;
		}

		private void ApplyCameraDrag() {
			Vector2 moveVector = _dragOrigin - _mousePos;
			playerCamera.transform.position += new Vector3(moveVector.x, moveVector.y, 0f) * dragSpeed;
		}

		private void ApplyCameraZoom() {
			var zoomChange = _mouseScroll.y * zoomSpeed * -1;
			var cameraSize = playerCamera.orthographicSize;
			
			var desiredZoom = cameraSize + zoomChange;
			desiredZoom = Mathf.Clamp(desiredZoom, minZoom, maxZoom);
			
			playerCamera.orthographicSize = desiredZoom;
		}

		private void ApplyCameraZoomMove() {

			var cameraPos = playerCamera.transform.position;
			var desiredPosition = Vector2.Lerp(cameraPos, _mousePos, zoomCursorConvergenceStrength);
			
			playerCamera.transform.position = new Vector3(desiredPosition.x, desiredPosition.y, cameraPos.z);
		}
	}
}
