using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Gameplay Actions")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference look;
    [SerializeField] private InputActionReference interactPrimary;
    [SerializeField] private InputActionReference interactSecondary;
    [SerializeField] private InputActionReference interactTertiary;

    [Header("Binocular Actions")]
    [SerializeField] private InputActionReference nextView;
    [SerializeField] private InputActionReference previousView;
    [SerializeField] private InputActionReference view0;
    [SerializeField] private InputActionReference view1;
    [SerializeField] private InputActionReference view2;
    [SerializeField] private InputActionReference view3;
    [SerializeField] private InputActionReference view4;
    [SerializeField] private InputActionReference view5;
    [SerializeField] private InputActionReference cancel;
    [SerializeField] private InputActionReference lookBinocular;

    public InputActionReference Move => move;
    public InputActionReference Look => look;

    public InputActionReference InteractPrimary => interactPrimary;
    public InputActionReference InteractSecondary => interactSecondary;
    public InputActionReference InteractTertiary => interactTertiary;

    public InputActionReference NextView => nextView;
    public InputActionReference PreviousView => previousView;

    public InputActionReference View0 => view0;
    public InputActionReference View1 => view1;
    public InputActionReference View2 => view2;
    public InputActionReference View3 => view3;
    public InputActionReference View4 => view4;
    public InputActionReference View5 => view5;

    public InputActionReference Cancel => cancel;

    public InputActionReference LookBinocular => lookBinocular;

    public void SwitchToGameplay()
    {
        if (playerInput == null)
        {
            Debug.LogError(
                "GameInput: PlayerInput is not assigned."
            );

            return;
        }

        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void SwitchToBinoculars()
    {
        if (playerInput == null)
        {
            Debug.LogError(
                "GameInput: PlayerInput is not assigned."
            );

            return;
        }

        playerInput.SwitchCurrentActionMap("Binoculars");
    }
}