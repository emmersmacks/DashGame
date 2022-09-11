using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(fileName = "StaticDataContainer", menuName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        public int WinPointsNumber;
        public float EndScreenTime;
        
        public DashData DashData;
        public InvulnerabilityData InvulnerabilityData;
        public MovementData MovementData;
        public ThirdPersonCameraData ThirdPersonCameraData;
    }
}