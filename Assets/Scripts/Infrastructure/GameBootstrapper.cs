using Components.Logic;
using Infrastructure.Services;
using Infrastructure.States;
using Mirror;

namespace Infrastructure
{
    public class GameBootstrapper: NetworkBehaviour, ICoroutineRunner
    {
        public CustomNetworkManager NetworkManager;
        private GameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new GameStateMachine(this,new ServiceLocator(), NetworkManager);
            _stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}