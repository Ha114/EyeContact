using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject InteractOptionPrefab;
    [SerializeField] private Transform InteractOptionsContextHolder;

    public IInteractable CurrentTarget { get; private set; }

    private PlayerInput playerInput;
    private PlayerInteractor playerInteractor;

    private BinocularItem currentBinocularItem;

    private void OnEnable()
    {
        playerInput = Player.GetComponent<PlayerInput>();
        playerInteractor = Player.GetComponent<PlayerInteractor>();

        if (playerInput != null)
            playerInput.onControlsChanged += OnControlsChanged;

        PlayerInteractor.onInteractionExecute += ChangeUI;

        BuildUI();
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.onControlsChanged -= OnControlsChanged;

        PlayerInteractor.onInteractionExecute -= ChangeUI;

        UnsubscribeFromTarget();

        ClearUI();
    }

    private void Update()
    {
        if (playerInteractor == null)
            return;

        if (CurrentTarget != playerInteractor.CurrentTarget)
        {
            BuildUI();
        }
    }

    private void OnControlsChanged(PlayerInput input)
    {
        BuildUI();
    }

    private void BuildUI()
    {
        UnsubscribeFromTarget();

        ClearUI();

        if (playerInteractor == null)
            return;

        CurrentTarget = playerInteractor.CurrentTarget;

        if (CurrentTarget == null)
            return;

        SubscribeToTarget();

        foreach (var interaction in CurrentTarget.GetInteractions())
        {
            GameObject option = Instantiate(
                InteractOptionPrefab,
                InteractOptionsContextHolder
            );

            InteractionOption interactionOption =
                option.GetComponent<InteractionOption>();

            if (interactionOption == null)
                continue;

            interactionOption.SetInteractionDescriptionText(
                interaction.Descriptiom
            );

            string key = playerInteractor.GetInteractionKey(
                interaction.Slot
            );

            interactionOption.SetInteractionKeyText(key);
        }
    }

    private void SubscribeToTarget()
    {
        currentBinocularItem = CurrentTarget as BinocularItem;

        if (currentBinocularItem != null)
        {
            currentBinocularItem.OnInteractionsChanged += BuildUI;
        }
    }

    private void UnsubscribeFromTarget()
    {
        if (currentBinocularItem != null)
        {
            currentBinocularItem.OnInteractionsChanged -= BuildUI;
            currentBinocularItem = null;
        }
    }

    private void ChangeUI(InteractionSlot value)
    {
        int index = (int)value;

        if (index < 0 || index >= InteractOptionsContextHolder.childCount)
            return;

        InteractionOption option =
            InteractOptionsContextHolder
                .GetChild(index)
                .GetComponent<InteractionOption>();

        if (option != null)
            option.Pressed();
    }

    private void ClearUI()
    {
        foreach (Transform child in InteractOptionsContextHolder)
        {
            Destroy(child.gameObject);
        }

        CurrentTarget = null;
    }
}