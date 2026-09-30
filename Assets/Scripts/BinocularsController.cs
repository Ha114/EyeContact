using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BinocularController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private GameObject binocularMenu;
    [SerializeField] private Camera mainCamera;

    [Header("FOV")]
    [SerializeField] private float zoomSpeed = 5f;

    [Tooltip("FOV for views 0 through 5.")]
    [SerializeField]
    private float[] viewFOVs = new float[6]
    {
        30f,
        20f,
        15f,
        10f,
        5f,
        1f
    };

    private bool binocularMode;

    public bool IsOpen => binocularMode;

    public event Action OnBinocularModeChanged;

    private float targetFOV;
    private float normalFOV;

    private int currentView = 0;

    public int CurrentView => currentView;

    public delegate void OnViewFieldChoosed(int value);
    public static event OnViewFieldChoosed onViewFieldChoosed;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
        {
            normalFOV = mainCamera.fieldOfView;
            targetFOV = normalFOV;
        }

        if (gameInput == null)
        {
            Debug.LogError(
                "BinocularController: GameInput is not assigned."
            );
        }

        if (binocularMenu == null)
        {
            Debug.LogError(
                "BinocularController: BinocularMenu is not assigned."
            );
        }
    }

    private void OnEnable()
    {
        SubscribeToInput();
    }

    private void OnDisable()
    {
        UnsubscribeFromInput();
    }

    private void SubscribeToInput()
    {
        if (gameInput == null)
            return;

        gameInput.NextView.action.performed += OnNextView;
        gameInput.PreviousView.action.performed += OnPreviousView;

        gameInput.View0.action.performed += OnView0;
        gameInput.View1.action.performed += OnView1;
        gameInput.View2.action.performed += OnView2;
        gameInput.View3.action.performed += OnView3;
        gameInput.View4.action.performed += OnView4;
        gameInput.View5.action.performed += OnView5;

        gameInput.Cancel.action.performed += OnCancel;
    }

    private void UnsubscribeFromInput()
    {
        if (gameInput == null)
            return;

        gameInput.NextView.action.performed -= OnNextView;
        gameInput.PreviousView.action.performed -= OnPreviousView;

        gameInput.View0.action.performed -= OnView0;
        gameInput.View1.action.performed -= OnView1;
        gameInput.View2.action.performed -= OnView2;
        gameInput.View3.action.performed -= OnView3;
        gameInput.View4.action.performed -= OnView4;
        gameInput.View5.action.performed -= OnView5;

        gameInput.Cancel.action.performed -= OnCancel;
    }

    public void Toggle()
    {
        if (binocularMode)
            Close();
        else
            Open();
    }

    public void Open()
    {
        if (binocularMode)
            return;

        if (viewFOVs == null || viewFOVs.Length < 6)
        {
            Debug.LogError(
                "BinocularController: viewFOVs must contain 6 values."
            );

            return;
        }

        binocularMode = true;

        targetFOV = viewFOVs[currentView];

        if (binocularMenu != null)
            binocularMenu.SetActive(true);

        if (gameInput != null)
            gameInput.SwitchToBinoculars();

        onViewFieldChoosed?.Invoke(currentView);

        OnBinocularModeChanged?.Invoke();
    }

    public void Close()
    {
        if (!binocularMode)
            return;

        binocularMode = false;

        targetFOV = normalFOV;

        if (binocularMenu != null)
            binocularMenu.SetActive(false);

        if (gameInput != null)
            gameInput.SwitchToGameplay();

        OnBinocularModeChanged?.Invoke();
    }

    private void OnNextView(InputAction.CallbackContext ctx)
    {
        if (!binocularMode)
            return;

        SelectView(currentView + 1);
    }

    private void OnPreviousView(InputAction.CallbackContext ctx)
    {
        if (!binocularMode)
            return;

        SelectView(currentView - 1);
    }

    private void OnView0(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(0);
    }

    private void OnView1(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(1);
    }

    private void OnView2(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(2);
    }

    private void OnView3(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(3);
    }

    private void OnView4(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(4);
    }

    private void OnView5(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            SelectView(5);
    }

    private void OnCancel(InputAction.CallbackContext ctx)
    {
        if (binocularMode)
            Close();
    }

    private void SelectView(int view)
    {
        if (viewFOVs == null || viewFOVs.Length < 6)
            return;

        int newView = Mathf.Clamp(view, 0, 5);

        if (newView == currentView)
            return;

        currentView = newView;

        targetFOV = viewFOVs[currentView];

        onViewFieldChoosed?.Invoke(currentView);
    }

    private void Update()
    {
        if (mainCamera == null)
            return;

        mainCamera.fieldOfView = Mathf.Lerp(
            mainCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );
    }
}