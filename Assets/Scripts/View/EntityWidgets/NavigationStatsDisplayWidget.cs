using TMPro;
using UnityEngine;

namespace View.EntityWidgets {
	public class NavigationStatsDisplayWidget : MonoBehaviour {
		
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private TextMeshProUGUI textDisplay;
		
		[SerializeField] private Color bearingLineColor;
		[SerializeField] private float lineStartOffset;
		[SerializeField] private float lineLength;
		[SerializeField] private float bearingTextOffset;

		private const string EntitySpeedUnit = "kt";
		
		private Transform _transform;
		private int _entityBearing;
		private float _entitySpeed;

		public void SetEntityNavigationValues(int bearing, float speed) {
			_entityBearing = bearing;
			_entitySpeed = speed;
		}
		
		private void Start() {
			_transform = transform;
			SetupLineRenderer();
		}

		private void SetupLineRenderer() {
			lineRenderer.positionCount = 2;
			lineRenderer.startColor = bearingLineColor;
			lineRenderer.endColor = bearingLineColor;
		}
		
		private void Update() {
			UpdateLineRenderer();
			UpdateTextDisplay();
			//TODO: add possibility for bearing display to be disabled
		}

		private void UpdateLineRenderer() {
			var lineVertices = ConstructLineVertices();
			lineRenderer.SetPositions(lineVertices);
		}

		private Vector3[] ConstructLineVertices() {
			var towardsBearingMoveVector = _transform.right;
			var v1 = _transform.position + (towardsBearingMoveVector * lineStartOffset);
			var v2 = v1 + (towardsBearingMoveVector * lineLength);
			
			return new [] { v1, v2 };
		}

		private void UpdateTextDisplay() {
			//TODO: this would ideally be implemented as a reference field to main entity MB that'd provide a property
			//to get the bearing, but there was trouble serializing WorldEntityMB<Model.WorldEntity> type field

			textDisplay.text = ConstructNavigationDataText();
			var totalTextOffset = lineStartOffset + lineLength + bearingTextOffset;
			textDisplay.transform.position = _transform.position + (_transform.right * totalTextOffset);

			textDisplay.transform.rotation = Quaternion.identity;
		}

		private string ConstructNavigationDataText() {
			return _entityBearing + "\n" + _entitySpeed.ToString("0.#") + " " + EntitySpeedUnit;
		}
	}
}