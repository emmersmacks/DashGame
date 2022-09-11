using System;
using System.Collections;
using Infrastructure;
using Mirror;
using UnityEngine;

namespace Components.Logic
{
    public class CustomNetworkManager : NetworkManager
    {
        private GameStateMachine _stateMachine;

        private GameStateMachine _gameStateMachine;

        public void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void OnClientConnect(NetworkConnection connection)
        {
            if (!clientLoadedScene)
            {
                if (!NetworkClient.ready)
                    NetworkClient.Ready();
            }
        }

        public void SpawnPlayers()
        {
            NetworkClient.AddPlayer();
        }

        public void GetAuthorityPlayer(Action<GameObject> onPlayerCreated)
        {
            StartCoroutine(WaitPlayerLoaded(onPlayerCreated));
        }

        private IEnumerator WaitPlayerLoaded(Action<GameObject> onPlayerCreated)
        {
            while (NetworkClient.localPlayer == null)
            {
                yield return null;
            }

            onPlayerCreated?.Invoke(NetworkClient.localPlayer.gameObject);
        }
    }
}
