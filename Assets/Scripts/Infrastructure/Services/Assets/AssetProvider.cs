using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadGameObject(string path) => 
            Resources.Load<GameObject>(path);

        public ScriptableObject LoadScriptableObject(string path) =>
            Resources.Load<ScriptableObject>(path);
    }
}