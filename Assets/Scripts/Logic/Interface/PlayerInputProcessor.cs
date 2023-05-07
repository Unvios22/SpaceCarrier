using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Interface {
	// ReSharper disable once ClassNeverInstantiated.Global
	// Disabling because the class is in fact instantiated by zenject
	public class PlayerInputProcessor : PlayerInputActions.IGameplayActions {
		
		//It would be best to use the InputActions directly in other classes, but Unity doesn't provide any
		//way to infer the expected input types reliably (thus the classes would still know themselves what
		//to expect, causing lots of boilerplate & breaking scope). Also the actions behave unreliably sometimes
		//(eg. no way to keep receiving input every frame an input is still being provided, only on value change).
		//
		//A dedicated input processor it is, then, to provide one stable entrypoint for the rest of API.

		public event Action<Vector2> EntitySelectionStarted;
		public event Action<Vector2> EntitySelectionPerformed;
		public event Action<Vector2, bool> EntitySelectionCancelled;

		private bool _isSpecialSelection;
		
		private PlayerInputActions _inputActions;

		private Vector2 _playerMouseScreenPos;
		
		//TODO:
		//info for selection controller (unit selection)
		//info for CameraController (map scroll)
		//info for CommandController (receiving clicks as unit commands)
		
		
		public PlayerInputProcessor(PlayerInputActions inputActions) {
			_inputActions = inputActions;
			_inputActions.gameplay.AddCallbacks(this);
		}

		public void OnPointerPos(InputAction.CallbackContext context) {

		}

		public void OnPointerScroll(InputAction.CallbackContext context) {

		}

		public void OnPointerClick(InputAction.CallbackContext context) {

		}

		public void OnPointerClickDrag(InputAction.CallbackContext context) {

		}

		public void OnNewaction(InputAction.CallbackContext context) {

		}

		public void OnCameraMove(InputAction.CallbackContext context) {

		}

		public void OnDrawSelectionBox(InputAction.CallbackContext context) {
			Debug.Log("OnDrawSelectionBox!");
			Debug.Log("Status: " + context.phase);

			var pointerScreenPos = context.action.ReadValue<Vector2>();

			switch (context.phase) {
				case InputActionPhase.Started:
					EntitySelectionStarted?.Invoke(pointerScreenPos);
					break;
				case InputActionPhase.Performed:
					EntitySelectionPerformed?.Invoke(pointerScreenPos);
					break;
				case InputActionPhase.Canceled:
					EntitySelectionCancelled?.Invoke(pointerScreenPos, _isSpecialSelection);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void OnSpecialSelection(InputAction.CallbackContext context) {
			_isSpecialSelection = context.phase switch {
				InputActionPhase.Started => true,
				InputActionPhase.Canceled => false,
				_ => _isSpecialSelection
			};
		}
	}
}