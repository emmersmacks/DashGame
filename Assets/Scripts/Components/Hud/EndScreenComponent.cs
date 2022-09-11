using UnityEngine;
using UnityEngine.UI;

namespace Components.Hud
{
    internal class EndScreenComponent : MonoBehaviour
    {
        public Text WinnerText;

        public void Construct(string winnerName)
        {
            WinnerText.text = winnerName;
        }
        
        public void ShowScreen()
        {
            gameObject.SetActive(true);
        }

        public void HideScreen()
        {
            gameObject.SetActive(false);
        }
    }
}