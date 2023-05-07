using Installers.Factories;
using Logic.Interface;
using Logic.Interface.EntitySelection;
using Logic.Systems;
using Zenject;

namespace Installers {
    public class MainInstaller : MonoInstaller {
        public override void InstallBindings() {
            InstallInputSystem();
            InstallSystems();
            InstallFactories();
            InstallControllers();
        }

        private void InstallInputSystem() {
            var playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
            Container.Bind<PlayerInputActions>().FromInstance(playerInputActions);
            Container.Bind<PlayerInputProcessor>().AsSingle();
        }
        
        private void InstallSystems() {     
            Container.BindInterfacesAndSelfTo<WorldEntitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandableEntitySystem>().AsSingle();
        }

        private void InstallFactories() {
            Container.Bind<ShipFactory>().To<TestShipFactory>().AsSingle();
        }

        private void InstallControllers() {
            Container.Bind<EntitySelectionController>().FromComponentInHierarchy().AsSingle();
        }
    }
}