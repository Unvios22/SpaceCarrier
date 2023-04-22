using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Entities.EntityData {
	[Serializable]
	public class EntityRotation {
		
		[ShowInInspector] private Vector3 _worldRotation;
		[ShowInInspector] private int _bearing;
		
		private int ParseWorldRotationToBearing(Vector3 rotation) {
			var entityForwardsDirection = Quaternion.Euler(rotation) * (Vector2.right) ;
			var signedAngleDifference = Vector2.SignedAngle(entityForwardsDirection, Vector2.up);
			var clockwiseAngleDifference =
				signedAngleDifference > 0 ? signedAngleDifference : 360 + signedAngleDifference;
			return (int)clockwiseAngleDifference;
		}
		
		//TODO: is doing this through properties the right/readable way? Constructor maybe?
		
		public Vector3 WorldRotation {
			get => _worldRotation;
			set {
				Bearing = ParseWorldRotationToBearing(value);
				_worldRotation = value;
			}
		}

		public int Bearing {
			get => _bearing;
			private set {
				if (value is < 0 or > 360) {
					throw new ArgumentException("Input bearing exceeds limit!");
				}
				_bearing = value;
			}
		}
	}
}