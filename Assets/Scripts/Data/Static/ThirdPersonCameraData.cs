using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(fileName = "ThirdPersonCameraData")]
    public class ThirdPersonCameraData : ScriptableObject
    {
        public float SpeedX = 240;
        public float SpeedY = 120;

        public float MinLimitY = 5;
        public float MaxLimitY = 180;  
    }
}