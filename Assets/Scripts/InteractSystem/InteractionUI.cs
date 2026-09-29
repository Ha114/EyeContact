using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject InteractOptionPrefab;
    [SerializeField] Transform InteractOptionsContextHolder;

    public IInteractable CurrentTarget { get; private set; }
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = Player.GetComponent<PlayerInput>();

        playerInput.onControlsChanged += OnControlsChanged;
        PlayerInteractor.onInteractionExecute += ChangeUI;

        BuildUI();
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.onControlsChanged -= OnControlsChanged;
        
        PlayerInteractor.onInteractionExecute -= ChangeUI;

        ClearUI();
    
    }
    private void OnControlsChanged(PlayerInput input)
    {
        BuildUI();
    }
    private void BuildUI()
    {
        ClearUI();

        var playerInteractor = Player.GetComponent<PlayerInteractor>();
        CurrentTarget = playerInteractor.CurrentTarget;

        if (CurrentTarget == null)
            return;

        foreach (var interaction in CurrentTarget.GetInteractions())
        {
            GameObject option = Instantiate(
                InteractOptionPrefab,
                InteractOptionsContextHolder
            );

            var interactionOption = option.GetComponent<InteractionOption>();

            interactionOption.SetInteractionDescriptionText(
                interaction.Descriptiom
            );

            string key = playerInteractor.GetInteractionKey(
                interaction.Slot
            );

            interactionOption.SetInteractionKeyText(key);
        }
    }

    private void ChangeUI(InteractionSlot value)
    {
        // Debug.Log("Ui Interact Button pressed, value = " + value + ", string = " + value.ToString() + ", int = " + (int)value);
        var x = InteractOptionsContextHolder.GetChild((int)value).gameObject.GetComponent<InteractionOption>();

        if (x != null)
            x.Pressed();
    }

    
    private void ClearUI()
    {
        foreach (Transform child in InteractOptionsContextHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
