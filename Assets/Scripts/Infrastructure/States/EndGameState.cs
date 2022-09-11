using System;
using System.Collections;
using Components.Hud;
using Components.Logic;
using Constants;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Data;
using Infrastructure.Services.ScoreService;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.States
{
    public class EndGameState : IPayloadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IDataService _dataService;
        private readonly CustomNetworkManager _networkManager;
        private string _winnerName;

        public EndGameState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner, IDataService dataService, CustomNetworkManager networkManager)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _dataService = dataService;
            _networkManager = networkManager;
        }

        public void Enter<TPayload>(TPayload payload)
        {
            _winnerName = payload as string;
            StartEndGame();
        }

        private void StartEndGame()
        {
            var endScreenComponent = InstantiateEndScreen();
            
            _coroutineRunner.StartCoroutine(ShowEndGameScreen(endScreenComponent, GoToNextState));
        }

        private EndScreenComponent InstantiateEndScreen()
        {
            var endGameScreenPrefab =
                ServiceLocator.GetService<IAssetProvider>().LoadGameObject(ConstantsResourcesPath.EndGameScreen);
            var endGameScreen = GameObject.Instantiate(endGameScreenPrefab);
            return endGameScreen.GetComponent<EndScreenComponent>();
        }

        private void ReloadLevel()
        {
            ServiceLocator.GetService<IScoreService>().ResetPoints();
            NetworkClient.localPlayer.transform.position = NetworkManager.startPositions[Random.Range(0, NetworkManager.startPositions.Count)].position;

        }

        private void GoToNextState()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        private IEnumerator ShowEndGameScreen(EndScreenComponent endScreen, Action showIsEnd)
        {
            endScreen.Construct(_winnerName);
            endScreen.ShowScreen();
            
            ReloadLevel();
            
            var _timePassed = 0f;
            while (_timePassed < _dataService.StaticData.EndScreenTime)
            {
                _timePassed += Time.deltaTime;
                yield return null;
            }
            endScreen.HideScreen();
            showIsEnd?.Invoke();
        }

        public void Exit()
        {
            
        }
    }
}