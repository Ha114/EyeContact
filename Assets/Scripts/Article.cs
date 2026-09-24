using UnityEngine;

public class Article: MonoBehaviour
{
    [SerializeField] Archives archives;

    public bool isNew = true;
    public bool isOpen = false;

    public string title = "Title";
    public string body = "Body";


    public void OnOpen()
    {
        isOpen = true;
        isNew = false;

        archives.text_Title.text = title;
        archives.text_Body.text = body;

    }
}
