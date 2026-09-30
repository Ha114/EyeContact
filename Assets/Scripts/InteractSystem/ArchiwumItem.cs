using System.Collections.Generic;
using UnityEngine;

public class ArchiwumItem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Archive;
    private readonly List<Interaction> interactions = new();

    private void Awake()
    {
        interactions.Add(
            new Interaction(
                InteractionSlot.Primary,
                "Archives",
                "OpenArchives",
                OpenArchives
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

    private void OpenArchives()
    {
        Debug.Log(gameObject.name + " - Interact");
 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Archive.SetActive(!Archive.activeInHierarchy);
    }

    private void ShowManual()
    {
        Debug.Log("<color=yellow>TEST Comunicate</color>");
    }
}