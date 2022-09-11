using Data.Static;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Components.CameraScripts
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        internal Transform Target;

        internal float Horizontal = 0.0F;
        internal float Vertical = 0.0F;

        private Quaternion mouseRotation;

        private IInputService _inputService;
        private ThirdPersonCameraData _cameraData;

        private const int FullAngle = 360;

        void Start()
        {
            Horizontal = transform.eulerAngles.x;
            Vertical = transform.eulerAngles.y;
        }

        public void Construct(IInputService inputService, ThirdPersonCameraData cameraData)
        {
            _cameraData = cameraData;
            _inputService = inputService;
        }

        void LateUpdate()
        {
            if (_inputService != null)
            {
                Horizontal += _inputService.LookAxis.x * _cameraData.SpeedX * 0.02F;
                Vertical -= _inputService.LookAxis.y * _cameraData.SpeedY * 0.02F;

                CameraRotate();
            }
        }

        private void CameraRotate()
        {
            Vertical = ClampAngle(Vertical, _cameraData.MinLimitY, _cameraData.MaxLimitY);

            mouseRotation = Quaternion.Euler(Vertical, Horizontal, 0);
            transform.rotation = mouseRotation;
        
            Vector3 mPosition = mouseRotation * new Vector3(0.0F, 0.0F, -6) + Target.position;
        
            transform.position = mPosition;
        }
    
        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -FullAngle) angle += FullAngle;
            if (angle > FullAngle) angle -= FullAngle;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
