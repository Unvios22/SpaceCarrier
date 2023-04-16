using UnityEngine;

namespace Logic.Interface {
	public class SelectionBoxController : MonoBehaviour {

		[SerializeField] private Color boxOutlineColor = Color.white;
		[SerializeField] private LineRenderer boxLineRenderer;

		private const int VertexCount = 4;

		private void Start() {
			SetupLineRendererSettings();
		}

		private void SetupLineRendererSettings() {
			boxLineRenderer.startColor = boxOutlineColor;
			boxLineRenderer.endColor = boxOutlineColor;
		}

		public void SetSelectionBoxVertices(Vector3[] vertices) {
			boxLineRenderer.positionCount = VertexCount;
			boxLineRenderer.SetPositions(vertices);
		}

		public void ClearSelectionVertices() {
			boxLineRenderer.positionCount = 0;
		}
	}
}