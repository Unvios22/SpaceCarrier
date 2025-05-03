using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Logic.Interface {
	// ReSharper disable once ClassNeverInstantiated.Global
	// Disabling because the class is in fact instantiated by zenject
	public class PlayerInputProcessor : PlayerInputActions.IGameplayActions, IDisposable {
		
		//It would be best to use the InputActions directly in other classes, but the new input system still
		//seems to be lacking without custom extensions. No inbuilt way to infer the expected input types reliably
		//(thus the classes would still know themselves what to expect, causing lots of boilerplate & breaking scope).
		//No continuous callbacks when input is being held, requiring custom Update() or bool polling that break the event workflow.
		//Modifier actions break when used with interactions, so no way to get predefined click&drag selection triggered on click hold, etc.
		
		//see:
		//https://forum.unity.com/threads/new-input-system-how-to-use-the-hold-interaction.605587/page-4
		//https://forum.unity.com/threads/using-buttonwithonemodifier-without-triggering-another-action-using-the-same-binding.775109/page-2
		//https://forum.unity.com/threads/implement-a-mouse-drag-composite.807906/
		
		//This is a dedicated input processor to provide one stable entrypoint for the rest of API
		
		// "Toggle" refers to events that change a bool value which may be used in multiple other events, e.g. enabling special selection
		// "Trigger" refers to events that internally require a different value type than the triggering input provides
		// (e.g. LMB tap *triggers* the EntityClickSelect that's then supplemented with the pointer screen pos Vector2;
		// so essentially how a composite modifier binding with button modifier with tap interaction would work, if it *did* work as of. 1.5.1)

		public event Action<Vector2> EntityBoxSelectionStarted;
		public event Action<Vector2> EntityBoxSelectionPerformed;
		public event Action<Vector2, bool> EntityBoxSelectionCancelled;
		public event Action<Vector2, bool> EntityClickSelectionPerformed;
		public event Action<Vector2, bool> EntityClickCommandPerformed;

		private bool _isSelectiveSelectionToggled;
		
		private PlayerInputActions _inputActions;

		private Vector2 _playerMouseScreenPos;
		
		private InputAction _pointerPosAction;
		
		//TODO:
		//info for selection controller (unit selection)
		//info for CameraController (map scroll)
		//info for CommandController (receiving clicks as unit commands)
		
		public PlayerInputProcessor(PlayerInputActions inputActions) {
			_inputActions = inputActions;
			_inputActions.gameplay.AddCallbacks(this);
			//todo also dispose/unsubscribe events properly
		}

		public void OnPointerPos(InputAction.CallbackContext context) {
			_pointerPosAction = context.action;
		}

		public void OnPointerScroll(InputAction.CallbackContext context) {

		}

		public void OnCameraPan(InputAction.CallbackContext context) {
			throw new NotImplementedException();
		}

		public void OnPointerClick(InputAction.CallbackContext context) {

		}

		public void OnPointerClickDrag(InputAction.CallbackContext context) {

		}

		public void OnNewaction(InputAction.CallbackContext context) {

		}

		public void OnCameraMove(InputAction.CallbackContext context) {

		}

		public void OnDrawEntitySelectionBox(InputAction.CallbackContext context) {
			var pointerScreenPos = context.action.ReadValue<Vector2>();

			switch (context.phase) {
				case InputActionPhase.Started:
					EntityBoxSelectionStarted?.Invoke(pointerScreenPos);
					break;
				case InputActionPhase.Performed:
					EntityBoxSelectionPerformed?.Invoke(pointerScreenPos);
					break;
				case InputActionPhase.Canceled:
					EntityBoxSelectionCancelled?.Invoke(pointerScreenPos, _isSelectiveSelectionToggled);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void OnToggleSelectiveSelection(InputAction.CallbackContext context) {
			_isSelectiveSelectionToggled = context.phase switch {
				InputActionPhase.Started => true,
				InputActionPhase.Canceled => false,
				_ => _isSelectiveSelectionToggled
			};
		}

		public void OnTriggerClickSelectEntity(InputAction.CallbackContext context) {
			if (context.phase != InputActionPhase.Performed) {
				return;
			}
			
			var pointerScreenPos = _pointerPosAction.ReadValue<Vector2>();
			EntityClickSelectionPerformed?.Invoke(pointerScreenPos, _isSelectiveSelectionToggled);
		}

		public void OnTriggerClickGiveCommand(InputAction.CallbackContext context) {
			if (context.phase != InputActionPhase.Performed) {
				return;
			}
			
			var pointerScreenPos = _pointerPosAction.ReadValue<Vector2>();
			EntityClickCommandPerformed?.Invoke(pointerScreenPos, _isSelectiveSelectionToggled);
		}

		public void OnCameraDrag(InputAction.CallbackContext context) {
			throw new NotImplementedException();
		}

		public void Dispose() {
			_inputActions.gameplay.RemoveCallbacks(this);
		}
	}
}