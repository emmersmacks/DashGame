using Components.Character;
using Data.NetworkMessagesData;
using Infrastructure.Services;
using Infrastructure.Services.ScoreService;
using Mirror;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private IScoreService _scoreService;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _scoreService = ServiceLocator.GetService<IScoreService>();
        }

        public void Enter()
        {
            RegisterHandlers();
            SubscribeOnActions();
        }

        private void SubscribeOnActions()
        {
            NetworkClient.localPlayer.GetComponent<HitChecker>().Hit += _scoreService.AddScorePoint;
            _scoreService.IsWin += StartEndGame;
        }

        private void RegisterHandlers()
        {
            NetworkClient.RegisterHandler<WinnerMessage>(OnEndGame);
            if (NetworkServer.active)
            {
                NetworkServer.RegisterHandler<WinnerMessage>(OnServerEnterStateMessage);
            }
        }

        private void OnEndGame(WinnerMessage msg)
        {
            _gameStateMachine.Enter<EndGameState, string>(msg.WinnerName);
        }

        private void OnServerEnterStateMessage(NetworkConnectionToClient arg1, WinnerMessage message)
        {
            SendEndGameActionToClients(message);
        }

        private void StartEndGame()
        {
            SendEndGameActionToServer(NetworkClient.localPlayer.name);
        }

        private void SendEndGameActionToServer(string winnerName)
        {
            WinnerMessage message = new WinnerMessage()
            {
                WinnerName = winnerName
            };

            NetworkClient.Send(message);
        }

        private void SendEndGameActionToClients(WinnerMessage message)
        {
            NetworkServer.SendToAll(message);
        }

        public void Exit()
        {
            UnregisterHandlers();
            UnsubscribeActions();
        }

        private void UnregisterHandlers()
        {
            NetworkClient.UnregisterHandler<WinnerMessage>();
            if (NetworkServer.active)
            {
                NetworkServer.UnregisterHandler<WinnerMessage>();
            }
        }

        private void UnsubscribeActions()
        {
            NetworkClient.localPlayer.GetComponent<HitChecker>().Hit -= _scoreService.AddScorePoint;
            _scoreService.IsWin -= StartEndGame;
        }
    }
}