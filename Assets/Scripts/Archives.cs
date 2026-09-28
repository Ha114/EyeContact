using UnityEngine;

public class Archives : MonoBehaviour
{
    [SerializeField] GameObject Archive;

    public TMPro.TextMeshProUGUI text_Title;
    public TMPro.TextMeshProUGUI text_Body;

    
    public void CloseArchive()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Archive.SetActive(false);
    }
}
