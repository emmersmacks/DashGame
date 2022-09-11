using System;

namespace Infrastructure.Services.SceneLoaderService
{
    public interface ISceneLoaderService : IService
    {
        void LoadScene(string sceneName, Action onLoaded);
    }
}