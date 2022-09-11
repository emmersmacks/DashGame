using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector3 MoveDirection { get; }
        Vector3 LookAxis { get; }
        bool IsDash();
    }
}