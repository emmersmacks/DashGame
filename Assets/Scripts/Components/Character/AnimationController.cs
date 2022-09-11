using Data.Static;
using Mirror;
using UnityEngine;

namespace Components.Character
{
    public class AnimationController : NetworkBehaviour
    {
        [SerializeField] public Animator _animator;
        
        private const string RunAnimationName = "Running";
        private const string DashAnimationName = "Dash";
        
    
        public void PlayRunAnimation()
        {
            _animator.SetBool(RunAnimationName, true);
            if(isLocalPlayer)
                CmdPlayRunAnimation();
            
        }

        public void PlayIdleAnimation()
        {
            _animator.SetBool(RunAnimationName, false);
            if(isLocalPlayer)
                CmdPlayIdleAnimation();
        }

        public void PlayDashAnimation()
        {
            _animator.SetBool(DashAnimationName, true);
            if(isLocalPlayer)
                CmdPlayDashAnimation();
            
        }
    
        public void StopDashAnimation()
        {
            _animator.SetBool(DashAnimationName, false);
            if(isLocalPlayer)
                CmdStopDashAnimation();
        }
    
        [Command]
        public void CmdPlayRunAnimation()
        {
            RpcPlayRunAnimation();
        }
    
        [Command]
        public void CmdPlayIdleAnimation()
        {
            RpcPlayIdleAnimation();
        }
    
        [Command]
        public void CmdPlayDashAnimation()
        {
            RpcPlayDashAnimation();
        }

        [Command]
        public void CmdStopDashAnimation()
        {
            RpcStopDashAnimation();
        }
    
        [ClientRpc]
        public void RpcPlayRunAnimation()
        {
            _animator.SetBool(RunAnimationName, true);
        }
    
        [ClientRpc]
        public void RpcPlayIdleAnimation()
        {
            _animator.SetBool(RunAnimationName, false);
        }
    
        [ClientRpc]
        public void RpcPlayDashAnimation()
        {
            _animator.SetBool(DashAnimationName, true);
        }

        [ClientRpc]
        public void RpcStopDashAnimation()
        {
            _animator.SetBool(DashAnimationName, false);

        }
    }
}
