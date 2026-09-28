using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject Archive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!Archive.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Archive.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Archive.SetActive(false);
            }
        }
    }
}
