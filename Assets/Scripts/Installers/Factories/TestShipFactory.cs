using Logic.Systems;
using Model;

namespace Installers.Factories {
	public class TestShipFactory : ShipFactory {
		
		public TestShipFactory(WorldEntitySystem<Model.WorldEntity> worldEntitySystem) : base(worldEntitySystem) { }
		
		public override Ship Create() {
			var ship = base.Create();
			ship.SetRandomBearing();
			ship.SetRandomSpeed(0f, 12f);
			return ship;
		}
	}
}