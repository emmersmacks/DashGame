using System.Collections;
using Data.Static;
using Infrastructure.Services.Input;
using Mirror;
using UnityEngine;

namespace Components.Character
{
    public class DashComponent : NetworkBehaviour
    {
        public CharacterMove MoveComponent;
        public CharacterController CharacterController;
        public AnimationController AnimationController;

        public ParticleSystem DashEffect;

        [SyncVar] internal bool DashIsStart;
        [SyncVar] internal float PassedDashTime = 0f;
        
        private IInputService _inputService;
        private DashData _data;
        
        public void Construct(IInputService inputService, DashData dashData)
        {
            _data = dashData;
            _inputService = inputService;
        }
        
        private void Update()
        {
            if (isLocalPlayer)
            {
                if (DashIsStart)
                {
                    PassedDashTime += Time.deltaTime;
                }
                else
                {
                    PassedDashTime = 0f;
                }

                if (_inputService != null)
                {
                    if (_inputService.IsDash() && !DashIsStart)
                        StartDash();
                }
                    
            }
        }
        
        private void StartDash()
        {
            StartCoroutine(DashCoroutine());
        }
        
        private IEnumerator DashCoroutine()
        {
            DashActionSwich(true);
            StartDashEffect();
            var oldPosition = transform.position;
            var currentDashTime = 0f;
            
            while (DistanceNotAchieved(oldPosition) && currentDashTime <= _data.DashTimeInSeconds && DashIsStart)
            {
                Dash();
                currentDashTime += Time.deltaTime;
                yield return null;
            }

            StopDashEffect();
            DashActionSwich(false);
        }

        private void StartDashEffect()
        {
            AnimationController.PlayDashAnimation();
            DashEffect.Play();
            CmdStartDashEffect();
        }

        private void StopDashEffect()
        {
            AnimationController.StopDashAnimation();
            DashEffect.Stop();
            DashEffect.Clear();
            CmdStopDashEffect();
        }
        
        [Command]
        private void CmdStartDashEffect()
        {
            RpcStartDashEffect();
        }

        [Command]
        private void CmdStopDashEffect()
        {
            RpcStopDashEffect();
        }
        
        [ClientRpc]
        private void RpcStartDashEffect()
        {
            AnimationController.PlayDashAnimation();
            DashEffect.Play();
        }

        [ClientRpc]
        private void RpcStopDashEffect()
        {
            AnimationController.StopDashAnimation();
            DashEffect.Stop();
            DashEffect.Clear();
        }

        private void DashActionSwich(bool isActive)
        {
            DashIsStart = isActive;
            MoveComponent.enabled = !isActive;
        }

        private bool DistanceNotAchieved(Vector3 oldPosition)
        {
            return Vector3.Distance(transform.position, oldPosition) < _data.DashDistance;
        }

        private void Dash()
        {
            var dashDirection = transform.TransformDirection(Vector3.forward);
            CharacterController.Move(dashDirection * Time.deltaTime * _data.DashSpeed);
        }
    }
}