using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    [SerializeField] public float interactionDistance = 3f;
    [SerializeField] public GameObject interactionUI;
    [SerializeField] public InputActionReference primaryInput;
    [SerializeField] public InputActionReference secondaryInput;
    [SerializeField] public InputActionReference tertiaryInput;
    [SerializeField] private PlayerInput playerInput;
    
    public IInteractable CurrentTarget { get; private set; }

    public delegate void OnInteractionExecute(InteractionSlot value);
    public static event OnInteractionExecute onInteractionExecute;

    private void OnEnable()
    {
        primaryInput.action.performed += OnPrimary;
        primaryInput.action.canceled += OnPrimary;
        secondaryInput.action.performed += OnSecondary;
        secondaryInput.action.canceled += OnSecondary;
        tertiaryInput.action.performed += OnTertiary;
        tertiaryInput.action.canceled += OnTertiary;
    }

    private void OnDisable()
    {
        primaryInput.action.performed -= OnPrimary;
        primaryInput.action.canceled -= OnPrimary;
        secondaryInput.action.performed -= OnSecondary;
        secondaryInput.action.canceled -= OnSecondary;
        tertiaryInput.action.performed -= OnTertiary;
        tertiaryInput.action.canceled -= OnTertiary;
    }
    private void OnPrimary(InputAction.CallbackContext ctx)
    {
        ExecuteInteraction(InteractionSlot.Primary);
    }

    private void OnSecondary(InputAction.CallbackContext ctx)
    {
        ExecuteInteraction(InteractionSlot.Secondary);
    }

    private void OnTertiary(InputAction.CallbackContext ctx)
    {
        ExecuteInteraction(InteractionSlot.Tertiary);
    }

    private void ExecuteInteraction(InteractionSlot slot)
    {
        var target = CurrentTarget;

        if (target == null)
            return;

        var interactions = target.GetInteractions();

        foreach (var interaction in interactions)
        {
            if (interaction.Slot == slot)
            {
                interaction.Execute();
                onInteractionExecute?.Invoke(slot);
                return;
            }
        }
    }

    void Update()
    {
       HandleInteraction();
    }

    void HandleInteraction()
    {
        CurrentTarget = null;

        Ray ray = mainCamera.ViewportPointToRay(Vector3.one/2f);

        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, interactionDistance))
        {
            CurrentTarget = hit.collider.GetComponent<IInteractable>();
        }

        interactionUI.SetActive(CurrentTarget != null);    
    }

    public string GetInteractionKey(InteractionSlot slot)
    {
        InputAction action = slot switch
        {
            InteractionSlot.Primary => primaryInput.action,
            InteractionSlot.Secondary => secondaryInput.action,
            InteractionSlot.Tertiary => tertiaryInput.action,
            _ => null
        };

        if (action == null)
            return "";

        string controlScheme = playerInput.currentControlScheme;

        int bindingIndex = action.GetBindingIndex(
            InputBinding.MaskByGroup(controlScheme)
        );

        if (bindingIndex < 0)
            return "";

        return action.GetBindingDisplayString(
            bindingIndex,
            InputBinding.DisplayStringOptions.DontIncludeInteractions
        );
    }
}
