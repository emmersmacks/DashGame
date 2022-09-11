using System.Collections;
using Components.Hud;
using Components.Logic;
using Constants;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.SceneLoaderService;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadNetworkState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly CustomNetworkManager _networkManager;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameFactory _gameFactory;

        public LoadNetworkState(GameStateMachine stateMachine, CustomNetworkManager networkManager,
            ICoroutineRunner coroutineRunner, ISceneLoaderService sceneLoaderService, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _networkManager = networkManager;
            _coroutineRunner = coroutineRunner;
            _sceneLoaderService = sceneLoaderService;
            _gameFactory = gameFactory;
        }
        
        public void Enter()
        {
            _networkManager.Construct(_stateMachine);
            
            if(SceneManager.GetActiveScene().name != SceneNamesConstants.NetworkScene)
                _sceneLoaderService.LoadScene(SceneNamesConstants.NetworkScene, StartNetworkLoading);
            else
                StartNetworkLoading();
        }

        private void StartNetworkLoading()
        {
            _gameFactory.InstantiateNetworkHud();
            RegisterButtons();
        }

        private void RegisterButtons()
        {
            var view = _gameFactory.NetworkHud.GetComponent<NetworkHudView>();
            view.CreateRoom.onClick.AddListener(CreateRoom);
            view.JoinToRoom.onClick.AddListener(JoinToRoom);
        }

        private void CreateRoom()
        {
            _networkManager.StartHost();
            _coroutineRunner.StartCoroutine(WaitConnection());
        }

        private void JoinToRoom()
        {
            _networkManager.StartClient();
            _coroutineRunner.StartCoroutine(WaitConnectionClient());
        }

        private IEnumerator WaitConnection()
        {
            while (!NetworkServer.active)
            {
                yield return null;
            }
            _coroutineRunner.StartCoroutine(WaitConnectionClient());
        }
        
        private IEnumerator WaitConnectionClient()
        {
            NetworkClient.Ready();
            while (!NetworkClient.ready)
            {
                yield return null;
            }
            StartGame();
        }

        private void StartGame()
        {
            GameObject.Destroy(_gameFactory.NetworkHud);
            _stateMachine.Enter<GameLoadState>();
        }

        public void Exit()
        {
            var name = _gameFactory.NetworkHud.GetComponent<NetworkHudView>().NameField.text;
            var data = ServiceLocator.GetService<IDataService>();
            
            if (name == "")
                data.PlayerName = "Player" + _networkManager.GetInstanceID();
            else
                data.PlayerName = name;
        }
    }
}