using UnityEngine;

namespace Data.Static
{
    [CreateAssetMenu(fileName = "AnimationData")]
    public class AnimationData : ScriptableObject
    {
        public string RunAnimationName;
        public string DashAnimationName;
    }
}
