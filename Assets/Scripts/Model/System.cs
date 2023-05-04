using System.Collections.Generic;
using Zenject;

namespace Model {
	public abstract class System<T> : ITickable where T : class {

		protected List<T> EntityList;

		public System() {
			EntityList = new List<T>();
		}
		
		public virtual void RegisterEntity(T entity) {
			EntityList.Add(entity);
		}

		public void Tick() {
			foreach (var entity in EntityList) {
				TickEntity(entity);
			}
		}

		protected abstract void TickEntity(T entity);
	}
}