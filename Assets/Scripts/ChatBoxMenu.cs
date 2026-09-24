using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxMenu : MonoBehaviour
{
    [SerializeField]
    public TMPro.TextMeshProUGUI text_Name;

    [SerializeField]
    public TMPro.TextMeshProUGUI text_Dialog;

    [SerializeField]
    public Image imageNameTitle;

    public float speedDelay = 0.3f;
    //public float fullText;
    private string fullTextPublic = "";
    private string currentText = "";

    public delegate void OnDialogRead(bool iAmRead);
    public static event OnDialogRead onDialogRead;

    private Coroutine _showTypeText;

    private void OnEnable()
    {
        GameProgress.onDialogSend += ApplyDialog;
    }


    private void ApplyDialog(string text)
    {
        string[] words = text.Split(':');
        text_Name.text = words[0];
        SetNamePanelColor(words[0]);
        _showTypeText = StartCoroutine(TypeText(words[1]));
    }

    private void SetNamePanelColor(string name)
    {
        switch (name)
        {
            case "Leo": imageNameTitle.GetComponent<Image>().color = new Color32(126, 196, 255, 200); break;
            case "Phoenix": imageNameTitle.GetComponent<Image>().color = new Color32(255, 245, 126, 200); break;
            case "You": imageNameTitle.GetComponent<Image>().color = new Color32(255, 126, 134, 200); break;
            default: imageNameTitle.GetComponent<Image>().color = new Color32(255, 255, 225, 200); break;

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_showTypeText != null)
            {
                text_Dialog.text = fullTextPublic;
                StopAllCoroutines();
            }
            onDialogRead?.Invoke(true);
        }
    }

    private IEnumerator TypeText(string fullTexr)
    {
        for(int i = 0; i <= fullTexr.Length; i++)
        {
            currentText = fullTexr.Substring(0, i);
            yield return new WaitForSeconds(speedDelay);
            text_Dialog.text = currentText;
        }
    }

    private void OnDisable()
    {
        if (_showTypeText != null)
        {
            StopAllCoroutines();
        }
        GameProgress.onDialogSend -= ApplyDialog;
    }
}
