using System;
using UnityEngine;
using WorldEntity;

namespace Model.Entities.EntityData {
	public class Position {
		
		private Vector3 _worldPosition;
		private GridPosition _gridPosition;

		public Position(){}
		
		public Position(Vector3 worldPosition, GridPosition gridPosition) {
			_worldPosition = worldPosition;
			_gridPosition = gridPosition;
		}

		public Position(Vector3 worldPosition) {
			_worldPosition = worldPosition;
		}
		
		public Position(GridPosition gridPosition) {
			_gridPosition = gridPosition;
			throw new NotImplementedException();
			_worldPosition = GridToWorldPositon(gridPosition);
		}

		private GridPosition WorldToGridPositon(Vector3 worldPosition) {
			//TODO
			throw new NotImplementedException();
		}

		private Vector3 GridToWorldPositon(GridPosition gridPosition) {
			//TODO
			throw new NotImplementedException();
		}
		
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