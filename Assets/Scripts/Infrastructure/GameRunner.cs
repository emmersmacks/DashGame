﻿using UnityEngine;

namespace Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameObject BootstrapperPrefab;
        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();
            
            if(bootstrapper == null)
                Instantiate(BootstrapperPrefab);
        }
    }
}