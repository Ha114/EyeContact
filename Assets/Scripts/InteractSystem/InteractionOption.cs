using TMPro;
using UnityEngine;

public class InteractionOption : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactionDescription;

    public void SetInteractionDescriptionText(string newDescription)
    {
        interactionDescription.text = newDescription;
    }
}
