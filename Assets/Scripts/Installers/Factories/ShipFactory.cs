using Logic.Systems;
using Model;
using Zenject;

namespace Installers.Factories {
	public class ShipFactory : IFactory<Ship> {

		private WorldEntitySystem<Model.WorldEntity> _worldEntitySystem;

		public ShipFactory(WorldEntitySystem<Model.WorldEntity> worldEntitySystem) {
			_worldEntitySystem = worldEntitySystem;
		}

		public Ship Create() {
			var ship = new Ship();
			RegisterShipToSystem(ship);
			return new Ship();
		}

		private void RegisterShipToSystem(Ship ship) {
			_worldEntitySystem.RegisterEntity(ship);
		}
	}
}