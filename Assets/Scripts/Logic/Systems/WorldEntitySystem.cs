using Model;
using UnityEngine;

namespace Logic.Systems {
	public class WorldEntitySystem<T> : System<T> where T : Model.WorldEntity {
		
		public void RegisterEntity(T entity) {
			Debug.Log("Entity registered");
			EntityList.Add(entity);
		}

		public override void Tick() {
			Debug.Log("Tick");
			Debug.Log("RegisteredEntities: " + EntityList.Count);
		}
	}
}