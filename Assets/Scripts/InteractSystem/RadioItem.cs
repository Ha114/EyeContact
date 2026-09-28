using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private readonly List<Interaction> interactions = new();

    private void Awake()
    {
        interactions.Add(
            new Interaction("talk", "Talk", Talk)
        );

        interactions.Add(
            new Interaction("inspect", "Inspect", Inspect)
        );
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        return interactions;
    }

    private void Talk()
    {
        Debug.Log("Talk");
    }

    private void Inspect()
    {
        Debug.Log("Inspect");
    }

    public void Interact(InteractionType type)
    {
        throw new System.NotImplementedException();
    }
}