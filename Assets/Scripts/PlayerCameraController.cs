using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private BinocularController binocularController;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraPivot;

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 0.1f;

    [Header("Vertical Look")]
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.03f;

    private float yaw;
    private float pitch;

    private Vector2 currentLook;
    private Vector2 lookVelocity;

    private void Awake()
    {
        if (gameInput == null)
            Debug.LogError("PlayerLook: GameInput is not assigned.");

        if (playerBody == null)
            Debug.LogError("PlayerLook: Player Body is not assigned.");

        if (cameraPivot == null)
            Debug.LogError("PlayerLook: Camera Pivot is not assigned.");

        if (playerBody != null)
            yaw = playerBody.eulerAngles.y;

        if (cameraPivot != null)
        {
            pitch = cameraPivot.localEulerAngles.x;

            if (pitch > 180f)
                pitch -= 360f;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (gameInput == null)
            return;

        Vector2 targetLook = GetLookInput();

        currentLook = Vector2.SmoothDamp(
            currentLook,
            targetLook,
            ref lookVelocity,
            smoothTime
        );

        ApplyLook(currentLook);
    }

    private Vector2 GetLookInput()
    {
        if (binocularController != null &&
            binocularController.IsOpen)
        {
            return gameInput.LookBinocular.action.ReadValue<Vector2>()
                * mouseSensitivity;
        }

        return gameInput.Look.action.ReadValue<Vector2>()
            * mouseSensitivity;
    }

    private void ApplyLook(Vector2 lookInput)
    {
        yaw += lookInput.x;

        pitch -= lookInput.y;

        pitch = Mathf.Clamp(
            pitch,
            minPitch,
            maxPitch
        );

        if (playerBody != null)
        {
            playerBody.rotation = Quaternion.Euler(
                0f,
                yaw,
                0f
            );
        }

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(
                pitch,
                0f,
                0f
            );
        }
    }
}