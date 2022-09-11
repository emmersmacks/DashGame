using UnityEngine;

namespace Components.Logic
{
    public class CursorComponent : MonoBehaviour
    {
        void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }
        }

        private void OnDestroy()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
