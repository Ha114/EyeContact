using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionOption : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactionKey;
    [SerializeField] public TextMeshProUGUI interactionDescription;


    public void SetInteractionKeyText(string key)
    {
        Debug.Log("Key = " + key);
        interactionKey.text = key;
    }
    public void SetInteractionDescriptionText(string newDescription)
    {
        Debug.Log("newDescription = " + newDescription);
        interactionDescription.text = newDescription;
    }

    public void Pressed()
    {
        Debug.Log("I Option was pressed: " + interactionKey.text + ", " + interactionDescription.text);
        //this.GetComponent<Button>().IsPressed();
    }
}
