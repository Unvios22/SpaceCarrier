using System.Linq;
using Logic.Interface.EntitySelection;
using Model;
using Model.Commands;
using UnityEngine;
using Zenject;

namespace Logic.Interface.EntityCommands {
	public class EntityCommandController : MonoBehaviour {
		//TODO: refactor Controllers as "InputProcessors" or something similar
		
		[SerializeField] private Camera playerCamera;

		private Vector2 _mousePos;
		private bool _isMouseDown;

		private EntitySelectionController _selectionController;

		[Inject]
		private void Init(EntitySelectionController selectionController) {
			_selectionController = selectionController;
		}

		private void Update() {
			ReadPlayerInput();
			if (_isMouseDown) {
				//TODO: add delay between clicks
				HandleMoveCommand();
			}
		}

		private void ReadPlayerInput() {
			//TODO: refactor using dedicated input class & the new Unity input system
			_isMouseDown = Input.GetKey(KeyCode.Mouse1);
			var mouseScreenPos = Input.mousePosition;
			_mousePos = playerCamera.ScreenToWorldPoint(mouseScreenPos);
		}

		private void HandleMoveCommand() {
			var currentlySelectedEntities = _selectionController.CurrentlySelectedEntities;
			var selectedCommandableEntities = currentlySelectedEntities.OfType<ICommandableEntityMB>();
			var commandableEntities = selectedCommandableEntities.Select(x => x.GetEntityAsCommandableEntity());

			foreach (var commandableEntity in commandableEntities) {
				var moveCommand = new MoveCommand(_mousePos);
				commandableEntity.ReceiveCommand(moveCommand);
			}
		}
	}
}