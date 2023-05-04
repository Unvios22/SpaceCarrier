using UnityEngine;

namespace Model {
	public class Waypoint {
		private Vector2 _worldPosition;

		public Waypoint(Vector2 position) {
			WorldPosition = position;
		}
		
		public Vector2 WorldPosition {
			get => _worldPosition;
			private set => _worldPosition = value;
		}
	}
}