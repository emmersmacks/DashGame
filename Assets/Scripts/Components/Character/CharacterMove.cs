using Components.CameraScripts;
using Data.Static;
using Infrastructure.Services.Input;
using Mirror;
using UnityEngine;

namespace Components.Character
{
    public class CharacterMove : NetworkBehaviour
    {
        public CharacterController CharacterController;
        public AnimationController AnimationController;
        public ThirdPersonCamera ThirdPersonCamera;
        public Transform CameraTarget;

        private MovementData _data;
        private IInputService _inputService;

        private void Awake()
        {
            ThirdPersonCamera = Camera.main.GetComponent<ThirdPersonCamera>();
        }

        public void Construct(IInputService inputService, MovementData data)
        {
            _data = data;
            _inputService = inputService;
        }
    
        [Client]
        private void Update()
        {
            transform.localRotation = Quaternion.Euler(0, ThirdPersonCamera.Horizontal, 0);
            if (isLocalPlayer)
            {
                if (_inputService != null)
                {
                    if (_inputService.MoveDirection == Vector3.zero)
                    {
                        AnimationController.PlayIdleAnimation();
                    }
                    else
                    {
                        Move(_inputService.MoveDirection);
                    }
                }
                
            }
        }

        private void Move(Vector3 direction)
        {
            AnimationController.PlayRunAnimation();
            var newDirection = CameraTarget.TransformDirection(direction);
            CharacterController.Move(newDirection * Time.deltaTime * _data.Speed);
            transform.LookAt( newDirection + transform.position);
            
        }
    }
}
