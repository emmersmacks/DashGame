using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject LoadGameObject(string path);
        ScriptableObject LoadScriptableObject(string path);
    }
}