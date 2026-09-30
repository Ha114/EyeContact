using System.Collections.Generic;
using UnityEngine;

public class BinocularItem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject BinocularMenu;
    private readonly List<Interaction> interactions = new();

    private void Awake()
    {
        interactions.Add(
            new Interaction(
                InteractionSlot.Primary,
                "Binocular",
                "Show/Close Binocular",
                BinocularInteract
            )
        );

        interactions.Add(
            new Interaction(
                InteractionSlot.Secondary,
                "Manual",
                "Manual",
                ShowManual
            )
        );
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        return interactions;
    }

    private void BinocularInteract()
    {
        BinocularMenu.SetActive(!BinocularMenu.activeInHierarchy);
        Debug.Log("<color=red>TEST BinocularInteract</color>");
    }

    private void ShowManual()
    {
        Debug.Log("<color=yellow>TEST Comunicate</color>");
    }
}