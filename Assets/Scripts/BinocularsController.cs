using UnityEngine;

public class BinocularsController : MonoBehaviour
{
    [SerializeField]
    GameObject BinocularMenu;
    [SerializeField] 
    private float zoomSpeed = 5f;

    private Camera m_mainCamera;

    private bool _binoculatMode = false;

    private float targetFOV;
    private float normalFOV = 60f;


    public delegate void OnViewFieldChoosed(int value);
    public static event OnViewFieldChoosed onViewFieldChoosed;

    private void Start()
    {
        m_mainCamera = Camera.main;
        targetFOV = m_mainCamera.fieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _binoculatMode = !_binoculatMode;
            targetFOV = _binoculatMode ? 30f : normalFOV;
            BinocularMenu.SetActive(_binoculatMode);
        }

        if (_binoculatMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                onViewFieldChoosed?.Invoke(0);
                targetFOV = 1f; 
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                onViewFieldChoosed?.Invoke(1);
                targetFOV = 3f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                onViewFieldChoosed?.Invoke(2);
                targetFOV = 5f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                onViewFieldChoosed?.Invoke(3);
                targetFOV = 10f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                onViewFieldChoosed?.Invoke(4);
                targetFOV = 30f;
            }
        }

        m_mainCamera.fieldOfView = Mathf.Lerp(
            m_mainCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );
    }
}
