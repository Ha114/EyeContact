using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(ParticleSystem))]
public class StarManager : MonoBehaviour
{
    [SerializeField] bool m_usingWithSkybox = true;

    private Camera m_mainCamera;


    private void Start()
    {
        m_mainCamera = Camera.main;
        transform.position = m_mainCamera.transform.position;
        transform.parent = m_mainCamera.transform;

        if (m_usingWithSkybox)
        {
            var scale = transform.localScale * m_mainCamera.farClipPlane * 0.09f;
            transform.localScale = scale;
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;      
    }
}