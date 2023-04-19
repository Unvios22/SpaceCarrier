using TMPro;
using UnityEngine;

namespace View.EntityWidgets {
	public class NameDisplayWidget : MonoBehaviour{
		
		[SerializeField] private TextMeshProUGUI entityNameDisplay;

		[SerializeField] private float nameDisplayOffset;

		//TODO: temp
		private string _entityName;
		
		private Transform _transform;

		//TODO: temp
		public void SetEntityName(string entityName) {
			_entityName = entityName;
			entityNameDisplay.text = _entityName;
		}
		
		private void Start() {
			_transform = transform;
		}

		private void Update() {
			UpdateNameDisplayPosition();
			AlignNameDisplayToCamera();
		}

		private void UpdateNameDisplayPosition() {
			var desiredPosition = _transform.position + (-Vector3.up * nameDisplayOffset);
			entityNameDisplay.transform.position = desiredPosition;
		}

		private void AlignNameDisplayToCamera() {
			entityNameDisplay.transform.rotation = Quaternion.identity;
		}
	}
}