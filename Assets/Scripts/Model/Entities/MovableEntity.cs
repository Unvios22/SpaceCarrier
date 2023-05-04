using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Model.Entities {
	public abstract class MovableEntity : WorldEntity {
		[ShowInInspector] private Queue<Waypoint> _navigationPoints;

		public MovableEntity() {
			NavigationPoints = new Queue<Waypoint>();
		}

		public Queue<Waypoint> NavigationPoints {
			get => _navigationPoints;
			private set => _navigationPoints = value;
		}
	}
}