using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(fileName = "DashData")]
    public class DashData : ScriptableObject
    {
        public float DashSpeed;
        public float DashDistance;
        public float DashTimeInSeconds;
    }
}
