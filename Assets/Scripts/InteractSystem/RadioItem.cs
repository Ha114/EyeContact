using System.Collections.Generic;
using UnityEngine;

public class RadioItem : MonoBehaviour, IInteractable
{
    private readonly List<Interaction> interactions = new();

    [SerializeField] GameObject radio;

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
        radio.SetActive(!radio.activeInHierarchy);
        Debug.Log("<color=yellow>TEST Comunicate</color>");
    }
}