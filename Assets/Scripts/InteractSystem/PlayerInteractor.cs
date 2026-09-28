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
    [SerializeField] public TextMeshProUGUI interactionText;

    private bool hitSomething;

    [SerializeField] private InputActionReference primaryInput;
    [SerializeField] private InputActionReference secondaryInput;

    public IInteractable CurrentTarget { get; private set; }

    private void OnEnable()
    {
        primaryInput.action.performed += OnPrimary;
        primaryInput.action.canceled += OnPrimary;
        secondaryInput.action.performed += OnSecondary;
        secondaryInput.action.canceled += OnSecondary;
    }

    private void OnDisable()
    {
        primaryInput.action.performed -= OnPrimary;
        primaryInput.action.canceled -= OnPrimary;
        secondaryInput.action.performed -= OnSecondary;
        secondaryInput.action.canceled -= OnSecondary;
    }

    private void OnPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("Interact Primary");
        CurrentTarget?.Interact(InteractionType.Primary);
    }

    private void OnSecondary(InputAction.CallbackContext context)
    {
        Debug.Log("Interact Secondary");
        CurrentTarget?.Interact(InteractionType.Secondary);
    }

    void Update()
    {
       HandleInteraction();
    }

    void HandleInteraction()
    {
        CurrentTarget = null;

        Ray ray = mainCamera.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;

        // hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            CurrentTarget = hit.collider.GetComponent<IInteractable>();
            if (CurrentTarget != null)
            {
                // hitSomething = true;
                
                IInteractable interactable = CurrentTarget;

                if (interactable != null)
                {
                    IReadOnlyList<Interaction> interactions =
                        interactable.GetInteractions();

                    foreach (Interaction interaction in interactions)
                    {
                        Debug.Log(interaction.Description);
                    }
                }
            }
        }
        interactionUI.SetActive(CurrentTarget != null);    
    }
}
