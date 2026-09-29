using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private readonly List<Interaction> interactions = new();

    private void Awake()
    {
        interactions.Add(
            new Interaction(
                InteractionSlot.Primary,
                "Music",
                "Music",
                Music
            )
        );

        interactions.Add(
            new Interaction(
                InteractionSlot.Secondary,
                "Comunicate",
                "Comunicate",
                Comunicate
            )
        );
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        return interactions;
    }

    private void Music()
    {
        Debug.Log("<color=red>TEST Music</color>");
    }

    private void Comunicate()
    {
        Debug.Log("<color=yellow>TEST Comunicate</color>");
    }
}