using System;
using System.Collections;
using Components.Logic;
using Constants;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.SceneLoaderService;
using Infrastructure.Services.ScoreService;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class GameLoadState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly CustomNetworkManager _networkManager;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameFactory _gameFactory;

        private Action<GameObject> PlayerCreated;

        public GameLoadState(GameStateMachine stateMachine, CustomNetworkManager networkManager,
            ISceneLoaderService sceneLoaderService, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _networkManager = networkManager;
            _sceneLoaderService = sceneLoaderService;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            if(SceneManager.GetActiveScene().name != SceneNamesConstants.GameScene)
                _sceneLoaderService.LoadScene(SceneNamesConstants.GameScene, InitialGameWorld);
            else
                InitialGameWorld();
        }

        private void InitialGameWorld()
        {
            _networkManager.SpawnPlayers();
            InitialPlayer();
        }

        private void InitialPlayer()
        {
            _networkManager.GetAuthorityPlayer(OnPlayerCreated);
        }

        private void OnPlayerCreated(GameObject player)
        {
            InitialGameObjects();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitialGameObjects()
        {
            _gameFactory.InitialCamera();
            _gameFactory.InitialHud();
            _gameFactory.InitialPlayer();
        }

        public void Exit()
        {
            
        }
    }
}