using Installers.Factories;
using Logic.Systems;
using View;
using Zenject;

namespace Installers {
	public class SystemInstaller : MonoInstaller {
		public override void InstallBindings() {
			Container.BindInterfacesAndSelfTo<WorldEntitySystem<Model.WorldEntity>>().AsSingle();
			Container.Bind<ShipFactory>().To<TestShipFactory>().AsSingle();
		}
	}
}