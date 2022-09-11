using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.Services.ScoreService;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine,ServiceLocator serviceLocator, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            RegisterServices();

        }

        public void Enter()
        {
            _stateMachine.Enter<LoadNetworkState>();
        }

        private void RegisterServices()
        {
            var services = new ServiceLocator();
            services.RegisterService<ISceneLoaderService>(new SceneLoaderService(_coroutineRunner));
            services.RegisterService<IAssetProvider>(new AssetProvider());
            services.RegisterService<IInputService>(new DesktopInput());
            services.RegisterService<IDataService>(new DataService(ServiceLocator.GetService<IAssetProvider>()));
            services.RegisterService<IScoreService>(new ScoreService(ServiceLocator.GetService<IDataService>()));
            services.RegisterService<IGameFactory>(new GameFactory(ServiceLocator.GetService<IAssetProvider>(), ServiceLocator.GetService<IInputService>(),ServiceLocator.GetService<IDataService>(),ServiceLocator.GetService<IScoreService>()));
        }

        public void Exit()
        {
            
        }
    }
}