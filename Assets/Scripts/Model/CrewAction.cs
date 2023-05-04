using System.Collections.Generic;

namespace Model {
	public abstract class CrewAction<T, U>
		where T : ICrewActionable
		where U: Personnel {
		
		private T _actionTarget;
		private List<U> _engagedPersonnel;

		public abstract void ApplyActionEffect();
	}
}