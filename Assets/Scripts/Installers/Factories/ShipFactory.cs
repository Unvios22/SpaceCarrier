using Logic.Systems;
using Model;
using Zenject;

namespace Installers.Factories {
	public abstract class ShipFactory : IFactory<Ship> {
		
		//TODO: refactor as dedicated zenject abstract factory

		protected WorldEntitySystem WorldEntitySystem;
		protected CommandableEntitySystem CommandableEntitySystem;

		public ShipFactory(WorldEntitySystem worldEntitySystem, CommandableEntitySystem commandableEntitySystem) {
			WorldEntitySystem = worldEntitySystem;
			CommandableEntitySystem = commandableEntitySystem;
		}

		public virtual Ship Create() {
			//TODO: add factory for commandables
			var ship = new Ship();
			RegisterShipToSystems(ship);
			return ship;
		}

		private void RegisterShipToSystems(Ship ship) {
			WorldEntitySystem.RegisterEntity(ship);
			CommandableEntitySystem.RegisterEntity(ship);
		}
	}
}