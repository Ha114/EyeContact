using System;
using System.Collections.Generic;
using UnityEngine;

public class BinocularItem : MonoBehaviour, IInteractable
{
    [SerializeField] private BinocularController binocularController;
    private readonly List<Interaction> interactions = new();

    public event Action OnInteractionsChanged;

    private void Awake()
    {
        if (binocularController == null)
        {
            binocularController = GetComponent<BinocularController>();
        }

        UpdateInteraction();
    }

    private void OnEnable()
    {
        if (binocularController != null)
        {
            binocularController.OnBinocularModeChanged += UpdateInteraction;
        }
    }

    private void OnDisable()
    {
        if (binocularController != null)
        {
            binocularController.OnBinocularModeChanged -= UpdateInteraction;
        }
    }

    public IReadOnlyList<Interaction> GetInteractions()
    {
        return interactions;
    }

    private void UpdateInteraction()
    {
        interactions.Clear();

        string description =
            binocularController != null && binocularController.IsOpen
                ? "Close Binoculars"
                : "Use Binoculars";

        interactions.Add(
            new Interaction(
                InteractionSlot.Primary,
                "Binocular",
                description,
                BinocularInteract
            )
        );

        // interactions.Add(
        //     new Interaction(
        //         InteractionSlot.Secondary,
        //         "Manual",
        //         "Manual",
        //         ShowManual
        //     )
        // );

        OnInteractionsChanged?.Invoke();
    }

    private void BinocularInteract()
    {
        if (binocularController == null)
        {
            Debug.LogError(
                "BinocularItem: BinocularController is not assigned."
            );
            return;
        }

        binocularController.Toggle();
    }

    private void ShowManual()
    {
        Debug.Log("BinocularItem: ShowManual");
        Debug.Log("<color=yellow>TEST Manual</color>");
    }
}