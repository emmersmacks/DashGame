using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class DesktopInput : IInputService
    {
        public Vector3 MoveDirection
        {
            get => GetMoveDirection();
        }

        public Vector3 LookAxis
        {
            get => GetLookAxis();
        }

        private Vector3 GetLookAxis()
        {
            var xAxis = UnityEngine.Input.GetAxis("Mouse X");
            var yAxis = UnityEngine.Input.GetAxis("Mouse Y");
            return new Vector3(xAxis, yAxis);
        }

        public bool IsDash()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        private Vector3 GetMoveDirection()
        {
            var vertical = UnityEngine.Input.GetAxisRaw("Vertical");
            var horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
            return new Vector3(horizontal, 0, vertical);
        }
    }
}