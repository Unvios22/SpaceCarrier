using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Interface.EntitySelection {
	public class SelectionBoxController : MonoBehaviour {

		[SerializeField] private Color boxOutlineColor = Color.white;
		[SerializeField] private LineRenderer boxLineRenderer;
		[SerializeField] private float zPosition;

		private const int VertexCount = 4;

		private void Start() {
			SetupLineRendererSettings();
		}

		private void SetupLineRendererSettings() {
			boxLineRenderer.startColor = boxOutlineColor;
			boxLineRenderer.endColor = boxOutlineColor;
		}

		public void SetSelectionBoxVertices(IEnumerable<Vector2> vertices) {
			var verticesWithZOffset = vertices.Select(v => new Vector3(v.x, v.y, zPosition)).ToArray();
			boxLineRenderer.positionCount = VertexCount;
			boxLineRenderer.SetPositions(verticesWithZOffset);
		}

		public void ClearSelectionVertices() {
			boxLineRenderer.positionCount = 0;
		}
	}
}