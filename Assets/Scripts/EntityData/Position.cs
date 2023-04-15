using UnityEngine;
using WorldEntity;

namespace EntityData {
	public class Position {
		
		private Vector3 _worldPosition;
		private GridPosition _gridPosition;

		public Vector3 WorldPosition {
			get => _worldPosition;
			set => _worldPosition = value;
		}

		public GridPosition GridPosition {
			get => _gridPosition;
			set => _gridPosition = value;
		}
	}
}