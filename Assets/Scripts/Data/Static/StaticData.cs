using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(fileName = "StaticDataContainer", menuName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        public int WinPointsNumber;
        public float EndScreenDelayTime;
        
        public DashData DashData;
        public MovementData MovementData;
        public ThirdPersonCameraData ThirdPersonCameraData;
    }
}