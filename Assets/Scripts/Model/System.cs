using System.Collections.Generic;
using Zenject;

namespace Model {
	public abstract class System<T> : ITickable {

		protected List<T> EntityList;

		public System() {
			EntityList = new List<T>();
		}
		
		public abstract void Tick();
	}
}