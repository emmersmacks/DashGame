using UnityEngine;
using UnityEngine.UI;

namespace Components.Hud
{
    public class GameHudView : MonoBehaviour
    {
        public Text ScoreField;

        public void SetScore(int score)
        {
            ScoreField.text = score.ToString();
        }
    }
}