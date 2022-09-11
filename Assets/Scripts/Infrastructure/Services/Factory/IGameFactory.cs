using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject NetworkHud { get; set; }
        GameObject GameHud { get; set; }
        void InstantiateNetworkHud();
        void InitialPlayer();
        void InitialHud();
        void InitialCamera();
    }
}