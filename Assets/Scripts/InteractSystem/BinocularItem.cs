using System.Collections.Generic;
using UnityEngine;

public class BinocularItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log(gameObject.name + " - Interact");
    }

    public string GetDescription()
    {
        return "Interact with Binocular";
    }

    public void Interact(InteractionType type)
    {
        throw new System.NotImplementedException();
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        throw new System.NotImplementedException();
    }
}
