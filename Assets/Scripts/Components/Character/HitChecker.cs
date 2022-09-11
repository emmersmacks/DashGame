using System;
using Character;
using Mirror;
using UnityEngine;

namespace Components.Character
{
    public class HitChecker : NetworkBehaviour
    {
        public DashComponent DashComponent;
        public event Action Hit;

        public bool IsWaitServerResponce;

        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (isLocalPlayer)
            {
                if (CanSetInvulnerability(hit.gameObject))
                {
                    IsWaitServerResponce = true;
                    DashComponent.DashIsStart = false;
                    CmdSetInvulnerability(hit.gameObject);
                    Hit?.Invoke();
                }
            } 
        }

        private bool CanSetInvulnerability(GameObject hit)
        {
            if(hit.transform.TryGetComponent<InvulnerabilityComponent>(out var invulnerabilityComponent) && hit.transform.TryGetComponent<DashComponent>(out var dashComponent))
                return (DashStatusIsWin(dashComponent) && !invulnerabilityComponent.IsInvulnerable && !IsWaitServerResponce);
            return false;
        }

        private bool DashStatusIsWin(DashComponent dashComponent)
        {
            if (DashComponent.DashIsStart)
            {
                if (dashComponent.DashIsStart)
                {
                    return dashComponent.PassedDashTime < DashComponent.PassedDashTime;
                }
                return true;
            }
            return false;
        }

        [Command] void CmdSetInvulnerability (GameObject obj)
        {
            RpcSetInvulnerability(obj);
        }
    
        [ClientRpc] void RpcSetInvulnerability(GameObject obj)
        {
            obj.GetComponent<InvulnerabilityComponent>().StartInvulnerability();
            IsWaitServerResponce = false;
        }
    }
}
