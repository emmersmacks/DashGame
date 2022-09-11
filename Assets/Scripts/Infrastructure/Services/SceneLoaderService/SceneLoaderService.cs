using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoaderService
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoaderService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void LoadScene(string sceneName, Action onLoaded)
        {
            _coroutineRunner.StartCoroutine(Load(sceneName, onLoaded));
        }

        private IEnumerator Load(string sceneName, Action onLoaded)
        {
            var waiting = SceneManager.LoadSceneAsync(sceneName);

            while (!waiting.isDone)
            {
                yield return null;
            }
            onLoaded?.Invoke();
        }
    }
}