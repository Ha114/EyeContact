using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log(gameObject.name + " - Interact");
    }

    public string GetDescription()
    {
        return "Interact with map";
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(InteractionType primary)
    {
        throw new System.NotImplementedException();
    }
}
