using Logic.Systems;
using Model;
using Zenject;

namespace Installers.Factories {
	public abstract class ShipFactory : IFactory<Ship> {

		protected WorldEntitySystem<Model.WorldEntity> WorldEntitySystem;

		public ShipFactory(WorldEntitySystem<Model.WorldEntity> worldEntitySystem) {
			WorldEntitySystem = worldEntitySystem;
		}

		public virtual Ship Create() {
			var ship = new Ship();
			RegisterShipToSystem(ship);
			return ship;
		}

		private void RegisterShipToSystem(Ship ship) {
			WorldEntitySystem.RegisterEntity(ship);
		}
	}
}