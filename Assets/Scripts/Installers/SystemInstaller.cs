using Installers.Factories;
using Logic.Systems;
using Zenject;

namespace Installers {
	public class SystemInstaller : MonoInstaller {
		public override void InstallBindings() {
			var worldEntitySystem = new WorldEntitySystem<Model.WorldEntity>();
			Container.BindInterfacesAndSelfTo<WorldEntitySystem<Model.WorldEntity>>().FromInstance(worldEntitySystem);
			Container.Bind<ShipFactory>().AsSingle();
		}
	}
}