using Data;
using Logic.Systems;
using Model;

namespace Installers.Factories {
	public class TestShipFactory : ShipFactory {
		
		public TestShipFactory(WorldEntitySystem worldEntitySystem, CommandableEntitySystem commandableEntitySystem)
			: base(worldEntitySystem, commandableEntitySystem) { }
		
		public override Ship Create() {
			var ship = base.Create();
			ship.Name = NameList.GetRandomTestShipName();
			ship.SetRandomBearing();
			ship.SetRandomSpeed(0f, 12f);
			return ship;
		}
	}
}