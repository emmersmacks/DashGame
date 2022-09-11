using System;
using System.Collections.Generic;
using Components.Logic;
using Infrastructure.Services;
using Infrastructure.Services.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.States;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;
        private IExitableState CurrentState;

        public GameStateMachine(GameBootstrapper gameBootstrapper, ServiceLocator serviceLocator, CustomNetworkManager networkManager)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                { typeof(BootstrapState), 
                    new BootstrapState(this, serviceLocator, gameBootstrapper) },
                { typeof(LoadNetworkState), 
                    new LoadNetworkState(this, networkManager, gameBootstrapper, ServiceLocator.GetService<ISceneLoaderService>(), ServiceLocator.GetService<IGameFactory>()) },
                { typeof(GameLoadState), 
                    new GameLoadState(this, networkManager, ServiceLocator.GetService<ISceneLoaderService>(), ServiceLocator.GetService<IGameFactory>()) },
                { typeof(GameLoopState), 
                    new GameLoopState(this) },
                { typeof(EndGameState), 
                    new EndGameState(this, gameBootstrapper, ServiceLocator.GetService<IDataService>(), networkManager) },
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = _states[typeof(TState)] as TState;
            CurrentState?.Exit();
            CurrentState = state;
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = _states[typeof(TState)] as TState;
            CurrentState?.Exit();
            CurrentState = state;
            state.Enter(payload);
        }
    }
}