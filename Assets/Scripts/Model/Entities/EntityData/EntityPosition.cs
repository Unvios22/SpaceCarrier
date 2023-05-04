using System;
using Sirenix.OdinInspector;
using UnityEngine;
using WorldEntity;

namespace Model.Entities.EntityData {
	[Serializable]
	public class EntityPosition {
		
		[ShowInInspector] private Vector2 _worldPosition;
		[ShowInInspector] private GridPosition _gridPosition;

		public EntityPosition(){}
		
		public EntityPosition(Vector2 worldPosition, GridPosition gridPosition) {
			_worldPosition = worldPosition;
			_gridPosition = gridPosition;
		}

		public EntityPosition(Vector2 worldPosition) {
			_worldPosition = worldPosition;
		}
		
		public EntityPosition(GridPosition gridPosition) {
			_gridPosition = gridPosition;
			throw new NotImplementedException();
			_worldPosition = GridToWorldPositon(gridPosition);
		}

		private GridPosition WorldToGridPositon(Vector2 worldPosition) {
			//TODO
			throw new NotImplementedException();
		}

		private Vector3 GridToWorldPositon(GridPosition gridPosition) {
			//TODO
			throw new NotImplementedException();
		}
		
		public Vector2 WorldPosition {
			get => _worldPosition;
			set => _worldPosition = value;
		}

		public GridPosition GridPosition {
			get => _gridPosition;
			set => _gridPosition = value;
		}
	}
}