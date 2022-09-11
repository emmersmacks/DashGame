using Components.CameraScripts;
using Components.Character;
using Components.Hud;
using Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Data;
using Infrastructure.Services.Input;
using Infrastructure.Services.ScoreService;
using Mirror;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;
        private readonly IScoreService _scoreService;

        public GameObject GameHud { get; set; }
        public GameObject NetworkHud { get; set; }

        public GameFactory(IAssetProvider assetProvider, IInputService inputService, IDataService dataService, IScoreService scoreService)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _dataService = dataService;
            _scoreService = scoreService;
        }

        public void InitialPlayer()
        {
            var player = NetworkClient.localPlayer;
            player.name = _dataService.PlayerName;
            
            player.GetComponent<CharacterMove>().
                Construct(_inputService, _dataService.StaticData.MovementData);
            player.GetComponent<DashComponent>().
                Construct(_inputService, _dataService.StaticData.DashData);
        }

        public void InitialHud()
        {
            if(GameHud != null) return;
            
            var prefab = _assetProvider.LoadGameObject(ConstantsResourcesPath.GameHud);
            GameHud = GameObject.Instantiate(prefab);
            var hudView = GameHud.GetComponent<GameHudView>();
            _scoreService.ScoreIsUpdate += hudView.SetScore;
        }

        public void InitialCamera()
        {
            var camera = Camera.main;
            var thirdPersonCamera = camera.GetComponent<ThirdPersonCamera>();
            thirdPersonCamera.Target = NetworkClient.localPlayer.GetComponent<CharacterMove>().CameraTarget;
            thirdPersonCamera.Construct(_inputService, _dataService.StaticData.ThirdPersonCameraData);
        }

        public void InstantiateNetworkHud()
        {
            var networkHudPref = _assetProvider.LoadGameObject(ConstantsResourcesPath.NetworkHud);
            NetworkHud = GameObject.Instantiate(networkHudPref);
        }
    }
}